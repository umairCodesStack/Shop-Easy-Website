using Microsoft.AspNetCore.SignalR;

namespace ShopEasy.Hubs
{
    public class MessageHub : Hub
    {
        public async Task sendNotification(string data)
        {
            await Clients.Others.SendAsync("receiveNotification",
                  data
                );
        }
    }
}
