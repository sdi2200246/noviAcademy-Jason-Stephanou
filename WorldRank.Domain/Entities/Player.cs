namespace WorldRank.Domain.Entities;

public class Player : IPlayer
{
	public Guid Id { get;}
	public string Name { get; private set; }
	public int Score { get; private set; }

	public List<Wallet>? Wallets {get; private set;}
	 
	private Player(Guid id, string name , int score)
	{
		Id = id;
		Name = name;
		Score = score;
	}

	public static Player CreateNew(string name)
	{	
		if (string.IsNullOrWhiteSpace(name))
			throw new ArgumentException("Name cannot be null or empty.", nameof(name));

		return new Player(Guid.NewGuid() , name , 0);
	}


	public void AddScore(int points)
	{
		if (points < 0)
			throw new ArgumentOutOfRangeException(nameof(points), "Points cannot be negative.");

		Score += points;
	}

	public override string ToString() => $"[{Id}] {Name} - Score: {Score}";
}
