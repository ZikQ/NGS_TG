using NGS_TG.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace NGS_TG.Interactivity.Requests
{
    internal class MessageMatchRequest
    {
        public Message Message { get; private set; }
        public TaskCompletionSource<MessageCreatedEventArgs> Tcs { get; private set; } = new();

        protected readonly CancellationToken _cancellation;
        protected readonly Func<MessageCreatedEventArgs, bool> _predicate;

        public MessageMatchRequest(Message message, TaskCompletionSource<MessageCreatedEventArgs> tcs, CancellationToken cancellation, Func<MessageCreatedEventArgs, bool> predicate)
        {
            Message = message;
            Tcs = tcs;
            _cancellation = cancellation;
            _cancellation.Register(() => this.Tcs.TrySetResult(null));
        }

        public bool IsMatch(MessageCreatedEventArgs args) => _predicate(args);
    }
}
