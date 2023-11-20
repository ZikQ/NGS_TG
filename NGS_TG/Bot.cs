using Newtonsoft.Json.Linq;
using NGS_TG.Commands;
using NGS_TG.EventArgs;
using NGS_TG.Events;
using System.Reflection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace NGS_TG
{
    public class Bot
    {
        private AsyncEvent<ClientReadyEventArgs> _clientReady;
        private AsyncEvent<MessageCreatedEventArgs> _messageCreated;
        private AsyncEvent<MessageUpdatedEventArgs> _messageUpdated;
        private AsyncEvent<ExceptionEventArgs> _clientError;
        private AsyncEvent<ChannelPostEventArgs> _channelPosted;
        private AsyncEvent<EditedChannelPostEventArgs> _channelPostEdited;
        private AsyncEvent<CommandErrorEventArgs> _commandError;
        private AsyncEvent<PollEventArgs> _poll;
        private AsyncEvent<PollAnswerEventArgs> _pollAnswer;
        private AsyncEvent<ChatMemberUpdateEventArgs> _chatMemberUpdated;
        private AsyncEvent<ChatJoinRequestEventArgs> _chatJoinRequest;

        private ITelegramBotClient _client;
        
        public ReceiverOptions ReceiverOptions { get; set; } = new ReceiverOptions
        {
            AllowedUpdates = { }
        };
        public event Events.AsyncEventHandler<ClientReadyEventArgs> ClientReady
        {
            add => _clientReady.Register(value);
            remove => _clientReady.Unregister(value);
        }
        public event Events.AsyncEventHandler<MessageCreatedEventArgs> MessageCreated
        {
            add => _messageCreated.Register(value);
            remove => _messageCreated.Unregister(value);
        }
        public event Events.AsyncEventHandler<MessageUpdatedEventArgs> MessageUpdated
        {
            add => _messageUpdated.Register(value);
            remove => _messageUpdated.Unregister(value);
        }
        public event Events.AsyncEventHandler<ChannelPostEventArgs> ChannelPosted
        {
            add => _channelPosted.Register(value);
            remove => _channelPosted.Unregister(value);
        }
        public event Events.AsyncEventHandler<EditedChannelPostEventArgs> ChannelPostEdited
        {
            add => _channelPostEdited.Register(value);
            remove => _channelPostEdited.Unregister(value);
        }
        public event Events.AsyncEventHandler<CommandErrorEventArgs> CommandError
        {
            add => _commandError.Register(value);
            remove => _commandError.Unregister(value);
        }
        public event Events.AsyncEventHandler<PollEventArgs> Poll
        {
            add => _poll.Register(value);
            remove => _poll.Unregister(value);
        }
        public event Events.AsyncEventHandler<PollAnswerEventArgs> PollAnswer
        {
            add => _pollAnswer.Register(value);
            remove => _pollAnswer.Unregister(value);
        }
        public event Events.AsyncEventHandler<ChatMemberUpdateEventArgs> ChatMemberUpdate
        {
            add => _chatMemberUpdated.Register(value);
            remove => _chatMemberUpdated.Unregister(value);
        }
        public event Events.AsyncEventHandler<ChatJoinRequestEventArgs> ChatJoinRequest
        {
            add => _chatJoinRequest.Register(value);
            remove => _chatJoinRequest.Unregister(value);
        }

        public Bot(TelegramBotClientOptions options, HttpClient? httpClient = null)
        {
            _client = new TelegramBotClient(options, httpClient);
            InternalInit();
        }
        public User User { get; private set; }
        public Bot(string token, HttpClient? httpClient = null)
        : this(new TelegramBotClientOptions(token), httpClient)
        {
            _client = new TelegramBotClient(token, httpClient);
            InternalInit();
        }
        private void InternalInit()
        {
            _clientReady = new("CLIENT_READY", ErrorHandler);
            _clientError = new("CLIENT_ERROR", ErrorHandler);
            _messageCreated = new("MESSAGE_CREATED", ErrorHandler);
            _messageUpdated = new("MESSAGE_UPDATED", ErrorHandler);
            _channelPosted = new("CHANNEL_POSTED", ErrorHandler);
            _channelPostEdited = new("CHANNEL_POST_EDITED", ErrorHandler);
            _commandError = new("COMMAND_ERROR", ErrorHandler);
            _poll = new("POLL", ErrorHandler);
            _pollAnswer = new("POLL_ANSWER", ErrorHandler);
            _chatMemberUpdated = new("CHAT_MEMBER_UPDATED", ErrorHandler);

            User = _client.GetMeAsync().GetAwaiter().GetResult();
        }
        private async void ErrorHandler(string name, Exception ex)
        {
            ExceptionEventArgs e = new()
            {
                Name = name,
                Exception = ex,
                Bot = _client
            };

            await _clientError.InvokeAsync(e);
        }
        private static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);

        }
        private async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.ChatJoinRequest)
            {
                ChatJoinRequestEventArgs ev = new()
                {
                    Bot = _client,
                    JoinRequest = update.ChatJoinRequest
                };

                await _chatJoinRequest.InvokeAsync(ev);
            }
            if (update.Type == UpdateType.ChatMember)
            {
                ChatMemberUpdateEventArgs ev = new()
                {
                    Updated = update.ChatMember,
                    Bot = _client
                };

                await _chatMemberUpdated.InvokeAsync(ev);
            }
            if (update.Type == UpdateType.MyChatMember)
            {
                ChatMemberUpdateEventArgs ev = new()
                {
                    Updated = update.MyChatMember,
                    Bot = _client
                };

                await _chatMemberUpdated.InvokeAsync(ev);
            }
            if (update.Type == UpdateType.Poll)
            {
                PollEventArgs ev = new()
                {
                    Poll = update.Poll,
                    Bot = _client
                };

                await _poll.InvokeAsync(ev);
            }
            if (update.Type == UpdateType.ChannelPost)
            {
                ChannelPostEventArgs ev = new()
                {
                    Message = update.Message
                };

                await _channelPosted.InvokeAsync(ev);
            }
            if (update.Type == UpdateType.Message)
            {
                bool flag = await CheckAndExecuteCommand(update.Message);

                if (!flag)
                {
                    return;
                }

                MessageCreatedEventArgs ev = new()
                {
                    Message = update.Message,
                    Bot = _client
                };

                await _messageCreated.InvokeAsync(ev);
            }

            if (update.Type == UpdateType.EditedMessage)
            {
                MessageUpdatedEventArgs ev = new()
                {
                    OldMessage = update.Message,
                    NewMessage = update.EditedMessage,
                    Bot = _client
                };

                await _messageUpdated.InvokeAsync(ev);
            }
        }
        public async Task ConnectAsync()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;

            await SendCommandsAsync();

            await Ready();

            _client.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                ReceiverOptions,
                cancellationToken
            );

            Console.ReadKey();
        }
        private async Task Ready()
        {
            ClientReadyEventArgs e = new()
            {
                Bot = _client
            };

            await _clientReady.InvokeAsync(e);
        }
        private CommandContext GenerateContext(Message message, Command command)
        {
            return new()
            {
                Message = message,
                Bot = _client,
                Command = command
            };
        }
        private ParsedCommand ParseCommandName(string name)
        {
            return ParsedCommand.ParseCommand(name);
        }
        private async Task<bool> CheckAndExecuteCommand(Message message)
        {
            if (message.Text == null) return true;
            if (!message.Text.StartsWith("/")) return true;
            ParsedCommand parse = ParseCommandName(message.Text);

            if (parse.CommandName.Contains("@"))
            {
                string[] vpadlu = parse.CommandName.Split("@");

                parse.CommandName = vpadlu[0];
                string name = vpadlu[1];

                if (User.Username != name) return true;
            }

            Command command = Commands.Module.FindCommandByName(parse.CommandName);

            if (command == null)
            {
                CommandErrorEventArgs ev = new()
                {
                    Message = "CommandNotFound",
                    Bot = _client
                };
                await _commandError.InvokeAsync(ev);
                return false;
            }
            var obj = Activator.CreateInstance(command.MethodInfo.DeclaringType);

            CommandContext ctx = GenerateContext(message, command);

            var arguments = new List<object> { ctx };
            foreach (var checkerMethod in command.Checkers)
            {
                var checkerInstance = Activator.CreateInstance(checkerMethod.DeclaringType);
                bool checkerResult = await (Task<bool>)checkerMethod.Invoke(checkerInstance, arguments.ToArray());

                if (!checkerResult)
                {
                    CommandErrorEventArgs ev = new()
                    {
                        Context = ctx,
                        Message = "CheckFailed",
                        Bot = _client
                    };
                    await _commandError.InvokeAsync(ev);
                    return false;
                }
            }
            await (Task)command.MethodInfo.Invoke(obj, arguments.ToArray());
            return false;
           
        }
        private async Task SendCommandsAsync()
        {
            List<BotCommand> commands = new List<BotCommand>();

            foreach (var i in Commands.Module.Modules)
            {
                foreach (var a in i.Commands)
                {
                    commands.Add(new()
                    {
                        Command = a.Name,
                        Description = a.Description,
                    });
                }
            }

            await _client.SetMyCommandsAsync(commands);
        }
    }
}