using Microsoft.AspNetCore.Mvc;
using WorldRank.Application.Interfaces;
using WorldRank.Api.Requests;
namespace WorldRank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private IPlayerService _service;
    public PlayersController(IPlayerService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> AddPlayer(AddPlayerRequest request , CancellationToken ct)
    {
        await _service.AddPlayer(request.Name, request.Score , ct);
        return StatusCode(201);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await _service.ListPlayers(ct));

    [HttpGet("playerId:int")]
    public async Task<IActionResult> GetById(int playerId , CancellationToken ct)
    {
        var player = await _service.FindPlayerById(playerId , ct);
        if (player is null)
            return NotFound();

        return Ok(player);
    }

    [HttpGet("playerName:string")]
    public async Task<IActionResult> GetByName(string playerName , CancellationToken ct)
    {
        var players = await _service.FindPlayerByName(playerName , ct);
        if (players.Count == 0)
            return NotFound();

        return Ok(players);
    }


    [HttpGet("rankings")] 
    public async Task<IActionResult> GetRankings(CancellationToken ct)
    {
        var players = await _service.ListPlayersByScore(ct);
        if (players.Count == 0)
            return NotFound();

        return Ok(players);
    }
}