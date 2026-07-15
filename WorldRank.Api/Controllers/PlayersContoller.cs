using Microsoft.AspNetCore.Mvc;
using WorldRank.Api.Requests;
using MediatR;
using WorldRank.Application.Commands;
using WorldRank.Application.Querys;

namespace WorldRank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private IMediator _mediator;
    public PlayersController(IMediator mediator)
    {   
        _mediator = mediator;
     }


    [HttpPost]
    public async Task<IActionResult> AddPlayer(AddPlayerRequest request , CancellationToken ct)
    {
        await _mediator.Send(new CreatePlayerCommand(request.Name) , ct);
        return StatusCode(201);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var players = await _mediator.Send(new ListPlayersQuery() , ct);
        return Ok(players);
    }
}