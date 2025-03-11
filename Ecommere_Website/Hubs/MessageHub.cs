using Microsoft.AspNetCore.SignalR;

namespace Ecommere_Website.Hubs
{
    public class MessageHub:Hub
    {
        public async Task sendNotification(string data)
        {
            await Clients.Others.SendAsync("receiveNotification",
                  data
                );
        }


    }
}
