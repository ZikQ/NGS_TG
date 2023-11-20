using NGS_TG.Events;
using Telegram.Bot;

namespace NGS_TG.EventArgs
{
    public class ClientReadyEventArgs : AsyncEventArgs
    {
        public ITelegramBotClient Client { get; internal set; }
        public Bot Bot { get; internal set; }
    }
}
