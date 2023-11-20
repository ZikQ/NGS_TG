using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NGS_TG.Commands.Attributes;

namespace NGS_TG.Commands
{
    public class Command
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public List<MethodInfo> Checkers { get; set; }
        public static Command ToCommand(CommandAttribute attr, MethodInfo mtd)
        {
            return new()
            {
                Name = attr.Name,
                Description = attr.Description,
                MethodInfo = mtd,
            };
        }
    }
}
