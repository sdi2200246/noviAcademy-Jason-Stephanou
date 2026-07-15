using WorldRank.Domain.Enums;
using WorldRank.Domain.Exceptions;

namespace WorldRank.Domain.Entities;

public class Wallet : IWallet
{
	public Currency Currency { get; }
	public Guid PlayerId { get; private set;}
	public Guid Id {get; }
	public decimal Balance { get; private set; }
	public bool IsBlocked { get; private set; }

	public  Player? Player { get; set; }

	private Wallet(Guid playerId , Guid id , Currency currency,    decimal balance, bool isBlocked = false)
	{
		if (balance < 0)
			throw new InsufficientFundsException(balance);

		PlayerId = playerId;	
		Id = id;
		Balance = balance;
		Currency = currency;
		IsBlocked = isBlocked;
	}
	static public Wallet CreateNew(Guid playerId , Currency currency)
	{
		return new Wallet(playerId , Guid.NewGuid(),currency , 0 , false);
	}

	public void Block() => IsBlocked = true;

	public void Unblock() => IsBlocked = false;

	public void SetBalance(decimal balance)
	{
		if (balance < 0)
			throw new InsufficientFundsException(balance);

		Balance = balance;
	}

	public void Deposit(decimal amount)
	{
		if (amount <= 0)
			throw new InvalidAmountException(amount);

		if (IsBlocked)
			throw new WalletBlockedException(Currency);

		Balance += amount;
	}

	public void Withdraw(decimal amount)
	{
		if (amount <= 0)
			throw new InvalidAmountException(amount);

		if (IsBlocked)
			throw new WalletBlockedException(Currency);

		var newBalance = Balance - amount;
		if (newBalance < 0)
			throw new InsufficientFundsException(newBalance);

		Balance = newBalance;
	}

	public void ForceSubtractFunds(decimal amount)
	{
		if (amount <= 0)
			throw new InvalidAmountException(amount);

		Balance -= amount;
	}

	public override string ToString() => $"Balance -> {Balance} Currency -> {Currency} IsBlocked -> {IsBlocked}";
}
