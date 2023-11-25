using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rockpaperscissors
{


    // 1: här använder vi interface
    // 2: vi skapar ett interface med dom funktioner vi vill ha                                 
    // 3: vi använder detta för att kunna använda oss av dependency injection i Player klassen  
    interface IMoveBehaviour : INameSelector
    {
        public abstract Game.Move GetMove();
    }

    interface INameSelector
    {
        public abstract string GetName();
    }

    // 1: här använder vi multibla intefaces
    // 2: vi använder detta genom att ange de intefaces vi vill att vår klass använder separerat med kommatäcken
    // 3: vi har flera olika interfaces som vi vill att klassen implementerar
    public class PersonPlayer : IMoveBehaviour, INameSelector
    {
        private string? Name;
        public string GetName()
        {
            //Menu.ColourWrite("Spelarens namn: ", ConsoleColor.Magenta);
            string? name = Menu.Ask("Spelarens namn: ", ConsoleColor.White, ConsoleColor.Magenta);
            if(name == null) return GetName();
            Name = name;
            return name;
        }

        Game.Move IMoveBehaviour.GetMove()
        {
            string? move = Menu.Ask($"{Name}s drag: ", ConsoleColor.White, ConsoleColor.Blue);
            if (move == null || string.IsNullOrWhiteSpace(move)) move = "sten";

            Game.Move m = Game.Move.Sten;

            switch(move.ToLower().Trim())
            {
                case "sten":
                    m = Game.Move.Sten;
                    break;
                case "sax":
                    m = Game.Move.Sax;
                    break;
                case "påse":
                    m = Game.Move.Påse;
                    break;
            }

            return m;
        }
    }

    public class AiPlayer : IMoveBehaviour, INameSelector
    {
        public string GetName()
        {
            return "Botten Anna";
        }

        Game.Move IMoveBehaviour.GetMove()
        {
            var r = new Random();

            return (Game.Move) r.Next(0,3);
        }
    }

    public abstract class GameMessage
    {
        public abstract void Show();
    }

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
        // 2: Klassen har en implm av IPlayer som har en funktion där spelarens drag väljs
        // 3:
        // - Vi använder detta för att enkelt kunna implementera olika typer av inputs för val av drag.
        // - Detta låter oss enkelt implementera en AI-spelare, en riktig spelare, osv utan att behöva göra stora ändringar i koden   
        public IMoveBehaviour move;
        public INameSelector name;
        public string Name { get; private set; }

        public Player(IMoveBehaviour move)
        {
            this.move = move;
            this.Name = move.GetName();
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
    }
}
