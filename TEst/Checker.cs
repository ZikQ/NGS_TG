using NGS_TG.Commands;
using NGS_TG.Commands.Attributes;

namespace TEst
{
    internal class Checker : CheckBaseAttribute
    {
        public override async Task<bool> ExecuteCheckAsync(CommandContext ctx)
        {
            return ctx.Author.Id != 13123214;
        }
    }
}
