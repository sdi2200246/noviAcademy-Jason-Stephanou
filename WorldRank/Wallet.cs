namespace WorldRank;

public enum Currency
{
    USD,
    EUR,
    GBP,
    JPY,
    AUD
}    


public interface Iwallet
{
     public Currency Currency { get;}
     public decimal Balance { get; }
     public bool IsBlocked { get;}
}

public class Wallet:Iwallet
{
    public Currency Currency { get; }
    public decimal Balance { get;}
    public bool IsBlocked { get;}
}