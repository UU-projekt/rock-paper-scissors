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
    interface IPlayer
    {
        public abstract string GetName();
        public abstract Game.Move GetMove();
    }

    public class PersonPlayer : IPlayer
    {
        public string GetName()
        {
            //Menu.ColourWrite("Spelarens namn: ", ConsoleColor.Magenta);
            string? name = Menu.Ask("Spelarens namn: ", ConsoleColor.White, ConsoleColor.Magenta);
            if(name == null) return GetName();
            return name;
        }

        Game.Move IPlayer.GetMove()
        {
            string? move = Menu.Ask("drag: ", ConsoleColor.White, ConsoleColor.Blue);
            if (move == null || string.IsNullOrWhiteSpace(move)) move = "sten";

            Game.Move m = Game.Move.Sten;

            switch(move)
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

    public class AiPlayer : IPlayer
    {
        public string GetName()
        {
            return "Botten Anna";
        }

        Game.Move IPlayer.GetMove()
        {
            var r = new Random();

            return (Game.Move) r.Next(0,3);
        }
    }

    internal class Player
    {
        // 1: Här använder vi Beroendeinjektion
        // 2: Klassen har en implm av IPlayer som har en funktion där spelarens drag väljs
        // 3:
        // - Vi använder detta för att enkelt kunna implementera olika typer av inputs för val av drag.
        // - Detta låter oss enkelt implementera en AI-spelare, en riktig spelare, osv utan att behöva göra stora ändringar i koden   
        public IPlayer move;
        public string Name { get; private set; }

        public Player(IPlayer move)
        {
            this.move = move;
            this.Name = move.GetName();
        }

        // 1: här använder vi constructor chaining
        // 2: vi använder detta genom att 
        // 3: vi vill kunna instansiera en Player utan några parametrar 
        public Player() : this(new PersonPlayer())
        {

        }
    }
}
