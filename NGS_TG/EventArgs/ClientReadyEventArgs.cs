using NGS_TG.Events;
using Telegram.Bot;

namespace NGS_TG.EventArgs
{
    public class ClientReadyEventArgs : AsyncEventArgs
    {
        public ITelegramBotClient Bot { get; internal set; }
    }
}
