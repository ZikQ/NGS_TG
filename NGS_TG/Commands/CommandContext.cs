using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace NGS_TG.Commands
{
    public class CommandContext
    {
        public Message Message { get; internal set; }
        public Chat Chat => Message.Chat;
        public User Author => Message.From;
        public Command Command { get; internal set; }
        public ITelegramBotClient Bot { get; internal set; }
        public Task<Message> RespondAsync(string content)
        {
            return Bot.SendTextMessageAsync(Chat, content);
        }
    }
}
