using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace SignalRServer.Hubs
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            Console.WriteLine("--> connection Established :" + Context.ConnectionId);
            Clients.Client(Context.ConnectionId).SendAsync("RecievedConnID", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public async Task SendMessageAsync(string message)
        {
            var routeObj = JsonConvert.DeserializeObject<dynamic>(message);
            string toClient = routeObj.To;
            Console.WriteLine("Message Recieved on :" + Context.ConnectionId);
            if (toClient == string.Empty)
            {
                await Clients.All.SendAsync("ReceiveMessage", message);
            }
            else
            {
                await Clients.Client(toClient).SendAsync("ReceiveMessage", message);
            }
        }
    }
}