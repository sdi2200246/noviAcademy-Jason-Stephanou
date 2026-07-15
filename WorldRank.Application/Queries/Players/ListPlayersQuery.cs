using MediatR;
using WorldRank.Domain.Entities;
namespace WorldRank.Application.Querys;
public record ListPlayersQuery() : IRequest<List<Player>>;