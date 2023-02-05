using MarketPlace_Orders.Models;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Orders.Hubs
{
    [EnableCors("AllowAll")]
    public class SignalServer : Hub, ISignalServer
    {
        private readonly static ConcurrentDictionary<string,Guid> _connections = new ConcurrentDictionary<string,Guid>();
        //public async Task Send(OrderApi order,Guid restaurantId)
        //{
        //    //var list = _connections.Where(a => a.Value == restaurantId).ToList();

        //    //foreach (var item in list)
        //    //{
        //    //    await Clients.Client(item.Key).SendAsync("ReceiveOrder", order);
            
        //    await Ihubcon.All.SendAsync("ReceiveOrder", "asd");
        //}

        public async Task Get(string id)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, id);
        }
        
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }


    }

}
