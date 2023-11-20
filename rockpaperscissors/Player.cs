using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rockpaperscissors
{



    interface IPlayer
    {
        public abstract string GetName();
        public abstract Game.Move GetMove();
    }

    public class PersonPlayer : IPlayer
    {
        public string GetName()
        {
            Console.Write("Spelarens namn: ");
            string? name = Console.ReadLine();
            if(name == null) return GetName();
            return name;
        }

        Game.Move IPlayer.GetMove()
        {
            Console.Write("val: ");
            string? move = Console.ReadLine();
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
            Game.Move m;

            switch(r.Next(0, 3))
            {
                case 0:
                    m = Game.Move.Sten;
                    break;
                case 1:
                    m = Game.Move.Sax;
                    break;
                case 2:
                    m = Game.Move.Påse;
                    break;
                default:
                    m = Game.Move.Sten;
                    break;
            }

            return m;
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

        public Player()
        {
            this.move = new PersonPlayer();
            this.Name = move.GetName();
        }
    }
}
