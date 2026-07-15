using MediatR;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Querys;

public interface IReadPlayerPersistence
{
    Task <List<Player>> GetPlayersAsync(CancellationToken ct);
}

public class ListPlayersQueryHandler : IRequestHandler<ListPlayersQuery, List<Player>>
{
    private readonly IReadPlayerPersistence _persistence;

    public  ListPlayersQueryHandler(IReadPlayerPersistence persistence)
    {
        _persistence = persistence;
    }
    public async Task<List<Player>> Handle(ListPlayersQuery request, CancellationToken cancellationToken)
    {
        return await _persistence.GetPlayersAsync(cancellationToken);
    }
}