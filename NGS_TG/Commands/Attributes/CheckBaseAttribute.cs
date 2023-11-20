using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGS_TG.Commands.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class CheckBaseAttribute : Attribute
    {
        public abstract Task<bool> ExecuteCheckAsync(CommandContext ctx);
    }
}
