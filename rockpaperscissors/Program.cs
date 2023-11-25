// See https://aka.ms/new-console-template for more information
using rockpaperscissors;

/**
 * 
    Inkapsling / Informationsgömning    CHECK
    Overloading av instansmetoder       CHECK
    Overloading av konstruktorer        CHECK
    Computed properties                 CHECK
    Objektkomposition                   CHECK
    Interfaces                          CHECK
    Abstrakta klasser                   CHECK   
    Subtypspolymorfism
    Multipla gränssnitt                 CHECK
    Arv av klasser                      CHECK
    Default interface methods
    Konstruktor-kedjning                CHECK
    Åtkomstmodifieraren `protected`     
    Beroendeinjektion                   CHECK


 * 
 * 
 * 
 */
Menu.Intro();

Player p1 = new Player();
Player p2;

if (Menu.PlayAgainstBot())
{
    p2 = new Player(new AiPlayer());
} else p2 = new Player();

Game game = new Game(5) { p1 = p1, p2 = p2 };

int roundCounter = 0;
while(game.Round(out Round result))
{
    roundCounter++;
    Console.WriteLine("Runda " + roundCounter + ": ");



    string winner = p1.Name;

    Menu.ColourWrite(p1.Name + ": ", ConsoleColor.Blue);
    Console.WriteLine(result.p1Move);

    Menu.ColourWrite(p2.Name + ": ", ConsoleColor.Red);
    Console.WriteLine(result.p2Move);

    if (result.Result == Game.Outcome.Loss) winner = p2.Name;
    else if (result.Result == Game.Outcome.Tie)
    {
        Console.WriteLine("Lika! Ingen vann\n", ConsoleColor.Blue);
        continue;
    }

    Console.WriteLine($"{winner} vann!\n");
}

var endOfGameStanding = game.RoundStanding;
var winnerMessage = new Win(game.Winner);

winnerMessage.Show();
Console.WriteLine($"{p1.Name}: {endOfGameStanding.player1RoundsWon}\n{p2.Name}: {endOfGameStanding.player2RoundsWon}");
