using WorldRank.Domain.Enums;

namespace WorldRank.Api.Requests;
public class AddWalletRequest
{
	public Guid PlayerId { get; set; }
	public Currency  currency{ get; set; }
}