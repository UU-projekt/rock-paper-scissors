// See https://aka.ms/new-console-template for more information
using rockpaperscissors;

/**
 * 
    Inkapsling / Informationsgömning
    Overloading av instansmetoder
    Overloading av konstruktorer
    Computed properties                 
    Objektkomposition                   CHECK(?)
    Interfaces                          CHECK
    Abstrakta klasser
    Subtypspolymorfism
    Multipla gränssnitt
    Arv av klasser
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


while(true)
{
    var m1 = p1.move.GetMove();
    var m2 = p2.move.GetMove();

    Console.WriteLine($"{p1.Name}: {m1} | {p2.Name}: {m2}");
    Console.WriteLine($"{p1.Name} {Game.Logic.Wins(m1, m2)}");
}

