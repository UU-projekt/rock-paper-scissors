// See https://aka.ms/new-console-template for more information
using rockpaperscissors;

/**
 *   Inkapsling / Informationsgömning    CHECK
 *   Overloading av instansmetoder       CHECK
 *   Overloading av konstruktorer        CHECK
 *   Computed properties                 CHECK
 *   Objektkomposition                   CHECK
 *   Interfaces                          CHECK
 *   Abstrakta klasser                   CHECK   
 *   Subtypspolymorfism
 *   Multipla gränssnitt                 CHECK
 *   Arv av klasser                      CHECK
 *   Default interface methods
 *   Konstruktor-kedjning                CHECK
 *   Åtkomstmodifieraren `protected`     
 *   Beroendeinjektion                   CHECK  
 */

/*
 * Vänligen lägg varje egen klass / eget gränssnitt i en egen fil. 
 * GODKÄNT: - Computed properties - Overloading av instansmetoder - Interfaces - Beroendeinjektion - Objektkomposition - Constructor-chaining - Constructor overloading 
 * KOMPLETTERING: - Multiple interfaces: Ej meningsfullt. Gränssnittet "IMoveBehavior" ärver redan av "INameSelector". Detta innebär att ni inte behöver explicit deklarera att "PersonPlayer" implementerar två gränssnitt. - 
 * Abstrakta klasser: Ej meningsfullt
 * Jag förstår inte vilket värde denna abstraktion tillför. - Arv av klasser: Se ovan kommentar.
 * 
 * fix:
 * - arv av klasser
 * - abstrakta klasser
 * - multiple interfaces
 */

//Visar introt 
Menu.Intro();

//Skapar objekt som ska representera spelare
Player p1 = new Player();
Player p2;

//Kollar om spelaren vill köra mot en bot eller riktig människa
if (Menu.PlayAgainstBot())
{
    p2 = new Player(new AiPlayer());
} else p2 = new Player();

Game game = new Game(3) { p1 = p1, p2 = p2 };

int roundCounter = 0;

// här körs själva game-loopen. Kommer att köras tills en spelare har vunnit 5 ggr
while (game.Round(out Round result))
{
    // updatera variabeln som kollar hur många rundor som har körts
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

// Visa meddelandet som säger vem som vann
winnerMessage.Show();
Console.WriteLine($"{p1.Name}: {endOfGameStanding.player1RoundsWon}\n{p2.Name}: {endOfGameStanding.player2RoundsWon}");
