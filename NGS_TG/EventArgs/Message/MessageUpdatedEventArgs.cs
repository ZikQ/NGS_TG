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
    public class MessageUpdatedEventArgs : AsyncEventArgs
    {
        public Message OldMessage { get; internal set; }
        public Message NewMessage { get; internal set; }
        public Chat Chat => OldMessage.Chat;
        public User Author => OldMessage.From;
        public ITelegramBotClient Bot { get; internal set; }
    }
}
