namespace WorldRank;

public enum Currency
{
    USD,
    EUR,
    GBP,
    JPY,
    AUD
}

public interface IWallet
{
    Currency Currency { get; }
    decimal Balance { get; }
    bool IsBlocked { get; }

    void Deposit(decimal amount);
    void Withdraw(decimal amount);
    void Block();
    void Unblock();
}

public class Wallet : IWallet
{
    public Currency Currency { get; }
    public decimal Balance { get; private set; }
    public bool IsBlocked { get; private set; }

    public Wallet(Currency currency, decimal startingBalance = 0m)
    {
        if (startingBalance < 0)
            throw new ArgumentOutOfRangeException(
                nameof(startingBalance), "Starting balance cannot be negative.");

        Currency = currency;
        Balance = startingBalance;
        IsBlocked = false;
    }

    public void Deposit(decimal amount)
    {
        if (IsBlocked)
            throw new InvalidOperationException("Wallet is blocked.");
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Deposit must be positive.");

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (IsBlocked)
            throw new InvalidOperationException("Wallet is blocked.");
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Withdrawal must be positive.");
        if (amount > Balance)
            throw new InvalidOperationException("Balance cannot go negative.");

        Balance -= amount;
    }

    public void Block() => IsBlocked = true;
    public void Unblock() => IsBlocked = false;

    public override string ToString() =>
        $"{Currency}: {Balance}{(IsBlocked ? " [BLOCKED]" : "")}";
}
