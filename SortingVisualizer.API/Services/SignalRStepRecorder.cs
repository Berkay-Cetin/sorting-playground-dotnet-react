using Microsoft.AspNetCore.SignalR;
using SortingVisualizer.API.Hubs;

namespace SortingVisualizer.API.Services;

public class SignalRStepRecorder
{
    private readonly IHubContext<SortingHub> _hubContext;
    private readonly string _connectionId;

    public SignalRStepRecorder(IHubContext<SortingHub> hubContext, string connectionId)
    {
        _hubContext = hubContext;
        _connectionId = connectionId;
    }

    public ObservableList CreateObservableList(List<int> initial)
    {
        return new ObservableList(initial, async (state, highlighted) =>
        {
            await _hubContext.Clients.Client(_connectionId).SendAsync("SortStep", new
            {
                values = state,
                highlighted = highlighted
            });
            await Task.Delay(120);
        });
    }
}