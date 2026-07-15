using MediatR;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Commands;

public interface ICreatePlayerPersistence
{
    Task Persist(Player player , CancellationToken ct);
}

public class CreatePlayerCmdHandler : IRequestHandler<CreatePlayerCommand, Guid>
{
    
    private readonly ICreatePlayerPersistence _persistence;

    public CreatePlayerCmdHandler(ICreatePlayerPersistence persistence)
    {
        _persistence = persistence;
    }

    public async Task<Guid> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
    {
        var player = Player.CreateNew(request.Name);
        await _persistence.Persist(player , cancellationToken);
        return player.Id;
    }
}