using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rockpaperscissors
{
    internal class Game
    {
        public enum Move { Sten = 0, Påse = 1, Sax = 2 };
        public enum Outcome { Win, Loss, Tie }

        public class Logic
        {
            public static Outcome Wins(Move move1, Move move2)
            {
                // Gör om enum till int
                int you = (int)move1, foe = (int)move2;

                // Allt jag ber om är guds förlåtelse efter detta underverk till kodrad
                if (you == ((foe + 1) % 3))
                {
                    return Outcome.Win;
                }
                else if (you == foe)
                {
                    return Outcome.Tie;
                }

                return Outcome.Loss;
            }
        }

    }
}
