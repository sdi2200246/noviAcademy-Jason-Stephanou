using System.Linq;
using WorldRank;
public class Menue
{
    static void Main()
    {
        var menue = new Menue();
        List<Player> players = new List<Player>(); 
        bool isRunning = true; 

        while (isRunning)
        {
            Console.WriteLine("\n--- Player Management System ---");
            Console.WriteLine("1. Add player");
            Console.WriteLine("2. List all players");
            Console.WriteLine("3. Find by name");
            Console.WriteLine("4. Exit");
            

            Console.Write("Choose an option (1-4): "); 
            
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n[ Adding a new player... ]");
                    menue.AddPlayer(players , menue.GetNameFromUser());
                    break;
                    
                case "2":
                    Console.WriteLine("\n[ Listing all players... ]");
                    menue.ListPlayers(players);
                    break;
                    
                case "3":
                    Console.WriteLine("\n[ Finding player... ]");
                    menue.FindAllByName(players , menue.GetNameFromUser());
                    break;
                    
                case "4":
                    Console.WriteLine("\nExiting the program. Goodbye!");
                    isRunning = false; 
                    break;
                    
                default:
                    Console.WriteLine("\nInvalid input! Please type a number between 1 and 4 and try again.");
                    break;
            }
        }
    }

    string? GetNameFromUser()
    {
        Console.Write("Give Name: ");
        return Console.ReadLine();
    }

    void AddPlayer(List<Player> players , string? name)
    {
        try
        {
            players.Add(new Player(name));
            Console.WriteLine("Player added succesfully");
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
    }
    void ListPlayers(List<Player> players)
    {

        if (players.Count() == 0)
        {
            Console.WriteLine("No players registered as of this time");
            return;
        }

        players.ForEach(p => Console.WriteLine(p.ToString()));

    }
    void FindAllByName(List<Player> players , string? query)
    {
        var found  = players.Where(p => p.Name.Equals(query)).ToList();
        ListPlayers(found);
        if (found.Count() == 0)
        {
            Console.WriteLine($"No players found by the given name({query})");
        }
    }
}
