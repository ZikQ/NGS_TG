using NGS_TG.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NGS_TG.EventArgs
{
    public class PollAnswerEventArgs : AsyncEventArgs
    {
        public PollAnswer PollAnswer { get; internal set; }
        public ITelegramBotClient Client { get; internal set; }
        public Bot Bot { get; internal set; }
        public string PollId => PollAnswer.PollId;
        public User? Chat => PollAnswer.User;
    }
}
