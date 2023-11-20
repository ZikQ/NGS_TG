﻿using NGS_TG.Commands;
using NGS_TG.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace NGS_TG.EventArgs
{
    public class CommandErrorEventArgs : AsyncEventArgs
    {
        public CommandContext Context { get; internal set; }
        public string Message { get; internal set; }
        public ITelegramBotClient Client { get; internal set; }
        public Bot Bot { get; internal set; }
    }
}
