using NGS_TG.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace NGS_TG.EventArgs
{
    internal class ExceptionEventArgs : AsyncEventArgs
    {
        public string Name { get; internal set; }
        public Exception Exception { get; internal set; }
        public ITelegramBotClient Bot { get; internal set; }
    }
}
