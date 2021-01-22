using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace webapi_signalr_test
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ChatController : ControllerBase
    {
        private IHubContext<ChatHub> _hub;
        public ChatController(IHubContext<ChatHub> hub)
        {
            _hub = hub;
        }

        /// <summary>
        /// Send message to all
        /// </summary>
        /// <param name="message"></param>
        [HttpPost("{message}")]
        public void Post(string message)
        {
            _hub.Clients.All.SendAsync("publicMessageMethodName", message);
            Debug.WriteLine("MENSAGEM: " + message);
            Console.WriteLine("MENSAGEM: " + message);
        }

        /// <summary>
        /// Send message to specific client
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="message"></param>
        [HttpPost("{connectionId}/{message}")]
        public void Post(string connectionId, string message)
        {
            _hub.Clients.Client(connectionId).SendAsync("privateMessageMethodName", message);
        }
    }
}
