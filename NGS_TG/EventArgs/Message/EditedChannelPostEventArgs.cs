using NGS_TG.Events;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NGS_TG.EventArgs
{
    public class EditedChannelPostEventArgs : AsyncEventArgs
    {
        public Message Message { get; internal set; }
        public ITelegramBotClient Client { get; internal set; }
        public Bot Bot { get; internal set; }
        public Chat Chat => Message.Chat;
        public DateTime DateTim => Message.Date;
        
    }
}
