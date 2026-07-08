using System;
using System.Collections.Generic;
using System.Linq;
using WorldRank;

public class Menu
{
    private readonly Dictionary<Guid, IPlayer> _store = new();
    private readonly IPlayerRepo _players;
    private readonly IWalletRepo _wallets;

    public Menu()
    {
        _players = new PlayerRepo(_store);
        _wallets = new InMemWalletRepo(_store);
    }

    static void Main()
    {
        var menu = new Menu();
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\n--- Player Management System ---");
            Console.WriteLine("1. Add player");
            Console.WriteLine("2. List all players");
            Console.WriteLine("3. Find player by name");
            Console.WriteLine("4. Update a player's score");
            Console.WriteLine("5. Delete a player");
            Console.WriteLine("6. Group players by score (high -> low)");
            Console.WriteLine("7. Add a wallet to a player");
            Console.WriteLine("8. Show a player's wallets");
            Console.WriteLine("9. Deposit / withdraw");
            Console.WriteLine("0. Exit");

            Console.Write("Choose an option: ");
            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1": menu.AddPlayer(); break;
                    case "2": menu.ListPlayers(); break;
                    case "3": menu.FindByName(); break;
                    case "4": menu.UpdateScore(); break;
                    case "5": menu.DeletePlayer(); break;
                    case "6": menu.GroupByScore(); break;
                    case "7": menu.AddWallet(); break;
                    case "8": menu.ShowWallets(); break;
                    case "9": menu.Transact(); break;
                    case "0":
                        Console.WriteLine("\nExiting the program. Goodbye!");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("\nInvalid input! Please choose a listed option.");
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }

    // ---------- helpers ----------

    private string? GetNameFromUser()
    {
        Console.Write("Give Name: ");
        return Console.ReadLine();
    }
    private Guid? PickPlayerId()
    {
        var name = GetNameFromUser();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty.");
            return null;
        }

        var matches = _players.GetAllPlayers()
                              .Where(p => p.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                              .ToList();

        if (matches.Count == 0)
        {
            Console.WriteLine($"No player found by the name \"{name}\".");
            return null;
        }
        if (matches.Count == 1)
            return matches[0].Id;

        Console.WriteLine("Multiple players share that name:");
        for (int i = 0; i < matches.Count; i++)
            Console.WriteLine($"  {i + 1}. {matches[i]}");
        Console.Write("Pick a number: ");
        if (int.TryParse(Console.ReadLine(), out int idx) && idx >= 1 && idx <= matches.Count)
            return matches[idx - 1].Id;

        Console.WriteLine("Invalid selection.");
        return null;
    }

    private Currency? PickCurrency()
    {
        Console.WriteLine($"Currencies: {string.Join(", ", Enum.GetNames<Currency>())}");
        Console.Write("Currency: ");
        if (Enum.TryParse<Currency>(Console.ReadLine(), ignoreCase: true, out var currency))
            return currency;

        Console.WriteLine("Unknown currency.");
        return null;
    }

    private void AddPlayer()
    {
        Console.WriteLine("\n[ Adding a new player... ]");
        var player = new Player(GetNameFromUser());
        _players.AddPlayer(player);
        Console.WriteLine($"Player added successfully. Id: {player.Id}");
    }

    private void ListPlayers()
    {
        Console.WriteLine("\n[ Listing all players... ]");
        _players.ListPlayers();
    }

    private void FindByName()
    {
        Console.WriteLine("\n[ Finding player... ]");
        var id = PickPlayerId();
        if (id is null) return;

        Console.WriteLine(_players.FindPlayer(id.Value));
    }

    private void UpdateScore()
    {
        Console.WriteLine("\n[ Updating score... ]");
        var id = PickPlayerId();
        if (id is null) return;

        Console.Write("New score: ");
        if (!int.TryParse(Console.ReadLine(), out int score))
        {
            Console.WriteLine("That's not a number.");
            return;
        }

        var player = _players.FindPlayer(id.Value);
        player.UpdateScore(score);
        Console.WriteLine($"Updated: {player}");
    }

    private void DeletePlayer()
    {
        Console.WriteLine("\n[ Deleting player... ]");
        var id = PickPlayerId();
        if (id is null) return;

        _players.DeletePlayer(id.Value);
        Console.WriteLine("Player deleted.");
    }

    private void GroupByScore()
    {
        Console.WriteLine("\n[ Players by score (high -> low) ]");
        var dictionary = _players.GroupPlayerNamesByScore();
        if (dictionary.Count == 0)
        {
            Console.WriteLine("No players registered.");
            return;
        }
        foreach (var (score, names) in dictionary)
            Console.WriteLine($"Score {score}: {string.Join(", ", names)}");
    }

    private void AddWallet()
    {
        Console.WriteLine("\n[ Adding a wallet... ]");
        var id = PickPlayerId();
        if (id is null) return;

        var currency = PickCurrency();
        if (currency is null) return;

        _wallets.AddWallet(new Wallet(currency.Value), id.Value);
        Console.WriteLine($"{currency} wallet added.");
    }

    private void ShowWallets()
    {
        Console.WriteLine("\n[ Player's wallets... ]");
        var id = PickPlayerId();
        if (id is null) return;

        var wallets = _wallets.GetByPlayer(id.Value);
        if (wallets.Count == 0)
        {
            Console.WriteLine("This player has no wallets.");
            return;
        }

        foreach (var w in wallets)
            Console.WriteLine($"{w.Currency}: {w.Balance}{(w.IsBlocked ? " [BLOCKED]" : "")}");
    }

    private void Transact()
    {
        Console.WriteLine("\n[ Deposit / withdraw... ]");
        var id = PickPlayerId();
        if (id is null) return;

        var currency = PickCurrency();
        if (currency is null) return;

        var wallet = _wallets.GetByPlayer(id.Value)
                             .FirstOrDefault(w => w.Currency == currency.Value);
        if (wallet is null)
        {
            Console.WriteLine("That player has no wallet in that currency.");
            return;
        }

        Console.Write("d = deposit, w = withdraw: ");
        var op = Console.ReadLine()?.Trim().ToLower();

        Console.Write("Amount: ");
        if (!decimal.TryParse(Console.ReadLine(), out var amount))
        {
            Console.WriteLine("That's not a valid amount.");
            return;
        }

        if (op == "d") wallet.Deposit(amount);
        else if (op == "w") wallet.Withdraw(amount);
        else { Console.WriteLine("Unknown operation."); return; }

        Console.WriteLine($"OK. {wallet.Currency}: {wallet.Balance}");
    }
}