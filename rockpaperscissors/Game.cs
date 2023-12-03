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
        // Här skapas en enum med olika värden. Dessa värder representerar de drag du kan göra i sten sax påse
        public enum Move { Sten = 0, Påse = 1, Sax = 2 };
        public enum Outcome { Win, Loss, Tie }

        protected List<Round> Rounds;
        private int RoundsLeft;
        public Player p1 { get; init; }
        public Player p2 { get; init; }

        public struct Standing
        {
            public int player1RoundsWon { get; set; }
            public int player2RoundsWon { get; set; }
        }

        // Denna getter gör att man enkelt kan få fram vinnaren av ett spel genom att använda .Winner variabeln på en instans av Game
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
            // Denna getter returnerar en struct som innehåller antalet rundor varje spelare har vunnit

            // 1: här använder vi computed properties
            // 2: vi använder detta genom att istället för att ge "RoundStanding" ett diskret värde så definierar vi en sk getter
            // 3: vi använder detta eftersom det variabeln syftar till (hur många rundor en spelare har vunnit) är dynamiskt och är något vi vill räkna ut när variabeln anropas 
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

        /// <summary>
        ///     Denna fuktion tar spelarnas inputs och skriver dom till variabeln ut-variabeln roundReslut. Funktionen kommer returnera true så länge ingen spelare
        ///     överstiger den valda mängden rundor som krävs för att vinna
        /// </summary>
        /// <param name="roundResult"></param>
        /// <returns></returns>
        public bool Round(out Round roundResult)
        {
            // Ta inputs från båda spelare
            var m1 = p1.Move();
            var m2 = p2.Move();

            // Skapa en instans av Round som skickas tillbaka i out variabeln för vidare användning
            roundResult = new rockpaperscissors.Round() { Result = Logic.Wins(m1, m2), p1Move = m1, p2Move = m2 };

            // Lägg till rundan i klassens lista över rundor
            Rounds.Add(roundResult);

            // Hämta spelets nuvarande status för att ta reda på om en till runda ska köras
            var standing = this.RoundStanding;

            // om en till runda ska köras så returneras true
            return standing.player1RoundsWon < RoundsLeft && standing.player2RoundsWon < RoundsLeft;
        }


        public class Logic
        {
            /// <summary>
            ///     Denna funktion tar emot två enum-värden och listar ut om det första draget vinner, förlorar, eller är lika med den andra spelarens drag
            /// </summary>
            /// <param name="move1">Första spelarens drag. Return-värdet är relativt till detta värde. Dvs om detta drag vinner mot moståndarens ("move2") så kommer Outcome.Win returneras</param>
            /// <param name="move2"></param>
            /// <returns>rundans resultat. Win, Loss, eller Tie</returns>
            public static Outcome Wins(Move move1, Move move2)
            {
                // Gör om enum till int för att logiken ska fungera
                int you = (int)move1, foe = (int)move2;

                // Denna matematiska funktion granskar första spalrens (you) drag och jämför med motståndaren (foe)
                // Modulus används för att värdet ska "slå över" om värdet är Move.Sax
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
