namespace WorldRank.Api.Requests;
public class AddPlayerRequest
{
	public string Name { get; set; } = string.Empty;
	public int Score { get; set; }
}