using MediatR;
using WorldRank.Domain.Entities;

namespace WorldRank.Application.Commands;

public interface ICreateWlletPersistence
{
    Task Persist(Wallet wallet , CancellationToken ct);
}

public class CreateWalletCmdHandler : IRequestHandler<CreateWalletCommand, Guid>
{
    
    private readonly ICreateWlletPersistence _persistence;

    public CreateWalletCmdHandler (ICreateWlletPersistence persistence)
    {
        _persistence = persistence;
    }

    public async Task<Guid> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
    {
        var wallet = Wallet.CreateNew(request.playerId , request.currency);
        await _persistence.Persist(wallet , cancellationToken);
        return wallet.Id;
    }
}