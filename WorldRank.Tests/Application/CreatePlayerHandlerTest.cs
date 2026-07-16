using Xunit;
using WorldRank.Application.Commands;
using Moq;
using WorldRank.Domain.Entities;
namespace WorldRank.Tests.Domain;

public class CreatePlayerCommandHandlerTests
{
    private readonly Mock<ICreatePlayerPersistence> _createPlayerPersistMock = new();
    private readonly CreatePlayerCmdHandler _sut;

    public CreatePlayerCommandHandlerTests()
    {
        _sut = new CreatePlayerCmdHandler(_createPlayerPersistMock.Object);
    }

    [Fact]
    public async Task Handle_NotEmptyNameRequest_ReturnsNewPlayerId()
    {
        var name = "Iasonas";
        var cmd = new CreatePlayerCommand(name);

        Guid result = await _sut.Handle(cmd, CancellationToken.None);

        Assert.NotEqual(Guid.Empty, result);

        _createPlayerPersistMock.Verify(p => p.Persist(
            It.Is<Player>(pl => pl.Name == name),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_EmptyNameRequest_ThrowsArgumentException()
    {
        var name = "";
        var cmd = new CreatePlayerCommand(name);

        async Task Handle() => await _sut.Handle(cmd, CancellationToken.None);

        await Assert.ThrowsAsync<ArgumentException>(Handle);

        _createPlayerPersistMock.Verify(p => p.Persist(
        It.IsAny<Player>(),
        It.IsAny<CancellationToken>()),
        Times.Never);
    }
}