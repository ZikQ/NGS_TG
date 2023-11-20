using NGS_TG.Commands;
using TEst;

public class TestModule : Module
{
    public override string Name { get; set; } = "����";

    public override void OnLoad()
    {
        Console.WriteLine("Module Loaded");
    }

    [Command("help", "help command")]
    public async Task TestCommand(CommandContext ctx)
    {
        await ctx.RespondAsync($"Hello, i am {ctx.Bot.User} and i have commands handler and events handler");
    }
}