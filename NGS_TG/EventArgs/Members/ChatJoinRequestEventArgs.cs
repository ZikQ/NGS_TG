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
    public class ChatJoinRequestEventArgs : AsyncEventArgs
    {
        public ITelegramBotClient Client { get; internal set; }
        public Bot Bot { get; internal set; }
        public ChatJoinRequest JoinRequest { get; internal set; }
        public Chat Chat => JoinRequest.Chat;
        public User From => JoinRequest.From;
        public DateTime Date => JoinRequest.Date;
    }
}
