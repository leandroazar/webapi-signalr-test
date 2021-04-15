using System;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace webapi_signalr_test
{
    public class ChatHub : Hub
    {
        /*public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }*/

        public void GetDataFromClient(string userId, string connectionId)
        {
            Clients.Client(connectionId).SendAsync("clientMethodName", $"Updated userid {userId}");
        }

        public override Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            ClientRepsository.clients.Add(connectionId, connectionId);
            Clients.Client(connectionId).SendAsync("WelcomeMethodName", connectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            ClientRepsository.clients.Remove(connectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}