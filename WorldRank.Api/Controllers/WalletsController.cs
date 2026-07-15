using Microsoft.AspNetCore.Mvc;
using WorldRank.Api.Requests;
using MediatR;
using WorldRank.Application.Commands;

namespace WorldRank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletsController : ControllerBase
{
    private IMediator _mediator;
    public WalletsController(IMediator mediator)
    {   
        _mediator = mediator;
    }


    [HttpPost]
    public async Task<IActionResult> AddWallet(AddWalletRequest request , CancellationToken ct)
    {
        var id = await _mediator.Send(new CreateWalletCommand(request.PlayerId , request.currency) , ct);
        return Ok(id);
    }
}