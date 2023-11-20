using NGS_TG.Events;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NGS_TG.EventArgs
{
    public class PollEventArgs : AsyncEventArgs
    {
        public Telegram.Bot.Types.Poll Poll { get; internal set; }
        public ITelegramBotClient Bot { get; internal set; }
        public string Questions => Poll.Question;
        public IReadOnlyList<PollOption> Options => Poll.Options;
        public bool IsClosed => Poll.IsClosed;
    }
}
