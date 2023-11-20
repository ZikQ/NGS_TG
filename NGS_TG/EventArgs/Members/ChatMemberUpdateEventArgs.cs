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
    public class ChatMemberUpdateEventArgs : AsyncEventArgs
    {
        public ITelegramBotClient Bot { get; internal set; }
        internal ChatMemberUpdated Updated { get; set; }
        public User User => Updated.From;
        public Chat Chat => Updated.Chat;
        public DateTime Date => Updated.Date;
        public ChatMember OldChatMember => Updated.OldChatMember;
        public ChatMember NewChatMember => Updated.NewChatMember;
    }
}
