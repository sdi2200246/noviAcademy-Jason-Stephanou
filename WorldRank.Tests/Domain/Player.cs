using Xunit;
using WorldRank.Domain.Entities;

namespace WorldRank.Tests.Domain;

public class PlayerTests
{
    [Fact]
    public void CreateNew_WithValidName_CreatesPlayer()
    {
        var expectedName = "Jason";

        var player = Player.CreateNew(expectedName);

        Assert.NotEqual(Guid.Empty, player.Id);
        Assert.Equal(expectedName, player.Name);
        Assert.Equal(0, player.Score);
    }

    [Fact]
    public void CreateNew_WithEmptyName_ThrowsArgumentException()
    {
        string emptyName = "";
        
        void CreateNewPLayer() => Player.CreateNew(emptyName);

        Assert.Throws<ArgumentException>(CreateNewPLayer);
    }

    [Fact]
    public void AddScore_Negative_ThrowsOutOfRgne()
    {
        var player = Player.CreateNew("kostas");

        int score = -100;
        void UpdateScore() => player.AddScore(score);

        Assert.Throws<ArgumentOutOfRangeException>(UpdateScore);
    }


}