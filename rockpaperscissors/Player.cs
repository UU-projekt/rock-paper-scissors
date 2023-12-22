using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rockpaperscissors
{


    // 1: här använder vi en abstrakt klass
    // 2: vi använder abstrakta klasser för att skapa ett kontrakt som klasser som ärver behöver uppfylla
    // 3: Vi använder detta då vi vill ha flera olika GameMessages men med olika implementationer
    public abstract class GameMessage
    {
        public abstract void Show();
    }

    // 1: här använder vi arv av klasser
    // 2: vi använder arv av klasser för att skapa en klass som utökar en redan existerande klass med flera funktioner
    // 3: vi använder detta för att utöka klassen "GameMessage"
    internal class Win : GameMessage
    {
        string Winner;
        public Win(Player Winner)
        {
            this.Winner = Winner.Name;
        }
        public override void Show()
        {
            Menu.ColourLog($"{Winner} vann!", ConsoleColor.Yellow);
        }
    }

    internal class Player
    {
        // 1: Här använder vi Beroendeinjektion
        // 2: Klassen har en impl av IPlayer som har en funktion där spelarens drag väljs
        // 3:
        // - Vi använder detta för att enkelt kunna implementera olika typer av inputs för val av drag.
        // - Detta låter oss enkelt implementera en AI-spelare, en riktig spelare, osv utan att behöva göra stora ändringar i koden   

        // 1: här använder vi också objektkomposition
        // 2: Vi använder detta genom att klassen player innehåller en instans av en klass som implementerar IMoveHehaviour eller INameSelector
        // 3: I detta fall kommer båda från en instans av PersonPlayer eller AiPlayer
        public IMoveBehaviour move;
        public INameSelector name;
        public string Name { get; private set; }

        public Player(IMoveBehaviour move)
        {
            this.move = move;
            this.name = move;

            string n = move.GetName();

            if (n == null) n = "Spelare";

            Name = n;
        }

        // 1: här använder vi constructor chaining
        // 2: vi använder detta genom att köra bas-konstruktorn med this()
        // 3: vi vill kunna instansiera en Player utan några parametrar 

        // 1: här använder vi också constructor overloading
        // 2: vi använder detta genom att definera en till konstruktor med en annan signatur
        // 3: vi vill låta programeraren initiera en Player utan att ange vilken IMoveBehaviour som används
        public Player() : this(new PersonPlayer())
        {

        }

        public Game.Move Move()
            => this.move.GetMove();
    }
}
