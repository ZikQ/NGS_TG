using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using NGS_TG.EventArgs;
using NGS_TG.Interactivity.Requests;

namespace NGS_TG.Interactivity.Waiters
{
    internal class MessageEventWaiter
    {
        private readonly Bot _bot;
        private readonly ConcurrentHashSet<MessageMatchRequest> _matchRequests = new();
        private readonly ConcurrentHashSet<MessageMatchRequest> _collectRequests = new();

        public MessageEventWaiter(Bot bot)
        {
            _bot = bot;
        }

        public async Task<MessageCreatedEventArgs> WaitForMatchAsync(MessageMatchRequest request)
        {
            _matchRequests.Add(request);

            try
            {
                return await request.Tcs.Task;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                _matchRequests.Remove(request);
            }
        }

        private async Task Handle(Bot _, MessageCreatedEventArgs args)
        {
           
        }
    }
}
