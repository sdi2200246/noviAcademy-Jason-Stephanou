using MediatR;

namespace WorldRank.Application.Commands;
public record CreatePlayerCommand(string Name) : IRequest<Guid>;