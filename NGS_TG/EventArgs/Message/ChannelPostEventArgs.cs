using NGS_TG.Events;
using Telegram.Bot.Types;

namespace NGS_TG.EventArgs
{
    public class ChannelPostEventArgs : AsyncEventArgs
    {
        public Message Message { get; internal set; }
        public Chat Chat => Message.Chat;
        public DateTime DateTim => Message.Date;
    }
}
