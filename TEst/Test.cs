using NGS_TG.Commands;
using TEst;

public class TestModule : Module
{
    public override string Name { get; set; } = "����";

    public override void OnLoad()
    {
        Console.WriteLine("Module Loaded");
    }

    [Command("test", "тестовая команда"), Checker]
    public async Task TestCommand(CommandContext ctx)
    {
        await ctx.RespondAsync($"Хуй");
    }
}