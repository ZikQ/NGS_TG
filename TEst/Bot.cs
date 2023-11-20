using NGS_TG;
using NGS_TG.Commands;
using NGS_TG.EventArgs;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TEst
{
    internal class NGSBot
    {
        public string Token { get; set; }

        public async Task Start()
        {
            Bot bot = new(Token);

            Module.Register<TestModule>();
            
            bot.MessageCreated += OnMessageCreated;
            bot.ClientReady += OnReady;
            bot.CommandError += async (e) =>
            {
                Console.WriteLine(e.Message);
            };

            await bot.ConnectAsync();
        }

        public async Task OnReady(ClientReadyEventArgs e)
        {
            Console.WriteLine($"{e.Bot.User.Username} ready to use!");
        }
        public async Task OnMessageCreated(MessageCreatedEventArgs e)
        {
            Console.WriteLine($"{e.Message.Text} created!");
        }
    }
}
