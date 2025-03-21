using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs
{
    public class SalaEsperaHub : Hub
    {
        public async Task SendUpdate(string message)
        {
            await Clients.All.SendAsync("ReceiveUpdate", message);
        }

        public async Task NotifyNewTurno(Guid idSucursal)
        {
            await Clients.Group(idSucursal.ToString()).SendAsync("ReceiveNewTurno");
        }

        public async Task JoinGroup(string idSucursal)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, idSucursal);
        }

        public async Task LeaveGroup(string idSucursal)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, idSucursal);
        }
    }
}
