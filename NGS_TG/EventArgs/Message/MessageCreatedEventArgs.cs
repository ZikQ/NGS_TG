
using NGS_TG.Events;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NGS_TG.EventArgs
{
    public class MessageCreatedEventArgs : AsyncEventArgs
    {
        public Message Message { get; internal set; }
        public Chat MessageChat => Message.Chat;
        public User Author => Message.From;
        public ITelegramBotClient Client { get; internal set; }
        public Bot Bot { get; internal set; }
    }
}
