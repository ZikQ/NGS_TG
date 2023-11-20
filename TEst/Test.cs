using NGS_TG.Commands;
using TEst;

public class TestModule : Module
{
    public override string Name { get; set; } = "����";

    public override void OnLoad()
    {
        Console.WriteLine("Module Loaded");
    }

    [Command("test", "тестовая команда")]
    public async Task TestCommand(CommandContext ctx)
    {
        await ctx.RespondAsync($"Хуй");
    }
    [Command("test1", "тестовая2 команда")]
    public async Task Test1Command(CommandContext ctx)
    {
        await ctx.RespondAsync($"Хуй");
    }
    [Command("test2", "тестовая1 команда")]
    public async Task Test2Command(CommandContext ctx)
    {
        await ctx.RespondAsync($"Хуй");
    }
    [Command("test3", "тестовая3 команда")]
    public async Task Test3Command(CommandContext ctx)
    {
        await ctx.RespondAsync($"Хуй");
    }
}