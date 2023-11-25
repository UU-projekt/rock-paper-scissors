using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rockpaperscissors
{
    internal class Round
    {
        public Game.Move p1Move { get; init; }
        public Game.Move p2Move { get; init; }
        public Game.Outcome Result { get; init; }
    }



    internal class Game
    {
        public enum Move { Sten = 0, Påse = 1, Sax = 2 };
        public enum Outcome { Win, Loss, Tie }

        public List<Round> Rounds;
        private int RoundsLeft;
        public Player p1 { get; init; }
        public Player p2 { get; init; }

        public struct Standing
        {
            public int player1RoundsWon { get; set; }
            public int player2RoundsWon { get; set; }
        }

        public Player Winner
        {
            get
            {
                if (this.RoundStanding.player1RoundsWon > this.RoundStanding.player2RoundsWon) return p1;
                else return p2;
            }
        }

        public Standing RoundStanding
        {
            get
            {
                int p1Wins = 0, p2Wins = 0;

                foreach(var e in Rounds)
                {
                    if (e.Result == Outcome.Win) p1Wins += 1;
                    else if (e.Result == Outcome.Loss) p2Wins += 1;
                }

                    return new Standing { player1RoundsWon = p1Wins, player2RoundsWon = p2Wins };

            }
        }

        public Game(int roundsLeft)
        {
            Rounds = new List<Round>();
            RoundsLeft = roundsLeft;
        }

        public bool Round(out Round roundResult)
        {
            var m1 = p1.move.GetMove();
            var m2 = p2.move.GetMove();

            roundResult = new rockpaperscissors.Round() { Result = Logic.Wins(m1, m2), p1Move = m1, p2Move = m2 };

            Rounds.Add(roundResult);

            var standing = this.RoundStanding;

            return standing.player1RoundsWon < RoundsLeft && standing.player2RoundsWon < RoundsLeft;
        }


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
