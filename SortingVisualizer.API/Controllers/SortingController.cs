using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SortingVisualizer.API.Hubs;
using SortingVisualizer.API.Services;

namespace SortingVisualizer.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SortingController : ControllerBase
{
    private readonly AlgorithmDiscoveryService _discovery;
    private readonly IHubContext<SortingHub> _hubContext;

    public SortingController(AlgorithmDiscoveryService discovery, IHubContext<SortingHub> hubContext)
    {
        _discovery = discovery;
        _hubContext = hubContext;
    }

    [HttpGet("algorithms")]
    public IActionResult GetAlgorithms() => Ok(_discovery.GetAvailableAlgorithms());

    [HttpPost("sort")]
    public async Task<IActionResult> Sort([FromBody] SortRequest request)
    {
        var recorder = new SignalRStepRecorder(_hubContext, request.ConnectionId);
        var list = recorder.CreateObservableList(request.List ?? GenerateRandomList());

        try
        {
            await _discovery.RunAlgorithm(request.AlgorithmName, list);
            return Ok(new { message = "Sort completed" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("generate")]
    public IActionResult GenerateList() => Ok(GenerateRandomList());

    private List<int> GenerateRandomList()
    {
        var rnd = new Random();
        return Enumerable.Range(0, 12).Select(_ => rnd.Next(1, 100)).ToList();
    }
}

public record SortRequest(string AlgorithmName, string ConnectionId, List<int>? List);