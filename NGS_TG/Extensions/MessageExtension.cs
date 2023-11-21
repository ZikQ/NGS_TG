using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace NGS_TG
{
    public static class MessageExtension
    {
        public static ITelegramBotClient _client { get; internal set; }
        public static async Task RespondAsync(this Message message, 
            string text,
            int? messageThreadId = default,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? entities = default,
            bool? disableWebPagePreview = default,
            bool? disableNotification = default,
            bool? protectContent = default,
            int? replyToMessageId = default,
            bool? allowSendingWithoutReply = default,
            IReplyMarkup? replyMarkup = default,
            CancellationToken cancellationToken = default)
        {
            await _client.SendTextMessageAsync(message.Chat, text, messageThreadId, parseMode, entities, disableWebPagePreview, disableNotification, protectContent, replyToMessageId, allowSendingWithoutReply, replyMarkup, cancellationToken);
        }

        public static async Task EditAsync(this Message message, 
            string text,
            ParseMode? parseMode = default,
            IEnumerable<MessageEntity>? entities = default,
            bool? disableWebPagePreview = default,
            InlineKeyboardMarkup? keyboardMarkup = default)
        {
            await _client.EditMessageTextAsync(message.Chat, message.MessageId, text, parseMode, entities, disableWebPagePreview, keyboardMarkup);
        }

        public static async Task EditAsync(this Message message,
            InlineKeyboardMarkup? keyboardMarkup = default)
        {
            await _client.EditMessageReplyMarkupAsync(message.Chat, message.MessageId, keyboardMarkup);
        }
        public static async Task DeleteAsync(this Message message)
        {
            await _client.DeleteMessageAsync(message.Chat, message.MessageId);
        }

        public static async Task PinAsync(this Message message, bool isNotify = false)
        {
            await _client.PinChatMessageAsync(message.Chat, message.MessageId, isNotify);
        }

        public static async Task UnPinAsync(this Message message)
        {
            await _client.UnpinChatMessageAsync(message.Chat, message.MessageId);
        }

        public static async Task ForwardAsync(this Message message,
            Chat chat,
            int? messageThreadId = default,
            bool? disableNotification = default,
            bool? protectContent = default)
        {
            await _client.ForwardMessageAsync(chat, message.Chat, message.MessageId, messageThreadId, disableNotification, protectContent);
        }
    }
}
