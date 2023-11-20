using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rockpaperscissors
{
    internal class Game
    {
        public enum Move { Sten = 1, Påse = 2, Sax = 3 };
        public enum Outcome { Win, Loss, Tie }

        public class Logic
        {
            public static Outcome Wins(Move move1, Move move2)
            {
                int m1 = (int) move1;
                int m2 = (int) move2;

                // you == (foe + 1) % 3

                if (move1 == move2) return Outcome.Tie;
                else if (m1 == (m2 + 1) % 3) return Outcome.Win;
                else return Outcome.Loss;
            }
        }

    }
}
