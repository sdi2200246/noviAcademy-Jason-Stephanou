using MediatR;
using WorldRank.Domain.Enums;

namespace WorldRank.Application.Commands;
public record CreateWalletCommand(Guid playerId  , Currency currency) : IRequest<Guid>;