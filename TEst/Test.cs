using NGS_TG.Commands;
using TEst;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;
using NGS_TG;

public class TestModule : Module
{
    public override string Name { get; set; } = "Test module";

    public override void OnLoad()
    {
        Console.WriteLine("Module Loaded");
    }

    [Command("help", "help command")]
    public async Task TestCommand(CommandContext ctx)
    {
        Message msg = await ctx.RespondAsync("huy",
            parseMode: ParseMode.MarkdownV2,
            disableNotification: true,
            replyToMessageId: ctx.Message.MessageId,
            replyMarkup: new InlineKeyboardMarkup(
                InlineKeyboardButton.WithCallbackData(
                    text: "Check sendMessage method", "huy")));

        await Task.Delay(5000);

        await msg.EditAsync("huy228");
    }
}