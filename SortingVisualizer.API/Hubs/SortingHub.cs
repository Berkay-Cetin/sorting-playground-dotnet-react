using Microsoft.AspNetCore.SignalR;

namespace SortingVisualizer.API.Hubs;

public class SortingHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }
}