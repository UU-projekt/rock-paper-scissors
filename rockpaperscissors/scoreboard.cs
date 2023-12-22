using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rockpaperscissors
{

    public struct Match
    {
        internal Round[] rounds;
        internal string P1Name;
        internal string P2Name;
    }

    internal interface IDiskWritable<T>
    {
        public T ReadFile(string path);
        public void WriteFile(string path);
    }

    // 1: här använder vi multibla intefaces
    // 2: vi använder detta genom att ange de intefaces vi vill att vår klass använder separerat med kommatäcken
    // 3:
    // -    Vi vill att klassen implementerar IDiskWritable vilket är ett interface vi har skrivit men även
    // -    att det inbyggda interfaces IDisposable implementeras.
    internal class ScoreBoard : IDiskWritable<Match[]>, IDisposable
    {
        private string? ioPath;


        public ScoreBoard() : this(System.IO.Path.GetTempPath()) {

        }

        public ScoreBoard(string IoPath)
        {
            this.ioPath = IoPath;
        }

        public void Dispose()
        {
            // När en instans av denna klass är på väg att bli borttagen så ser vi till att spara all data först.
            // Detta går såklart att göra direkt med WriteFile som måste implementeras enligt IDiskWritable
            // men detta låter oss "glömma" detta då det sker automatiskt
            WriteFile("scoreboard.csv");
        }

        public Match[] ReadFile(string filename)
        {
            List<Match> matches = new List<Match>();
            using (var sr = new StreamReader(Path.Join(this.ioPath, filename)))
            {
                string str = sr.ReadToEnd();
                var matchesRaw = str.Split('!');

                foreach(string match in matchesRaw)
                {
                    var lines = match.Split('\n');
                    int roundsPlayed = lines.Length - 3;
                    string p1name = lines[0], p2name = lines[1], winner = lines[3];

                    Round[] rounds = new Round[roundsPlayed];

                    for(int i = 3; i < lines.Length; i++)
                    {
                        string[] data = lines[i].Split(',');

                        // TODO: add type conv str -> Game.Outcome & str -> Game.Move
                        rounds[i - 3] = new Round
                        {
                            p1Move = data[0],
                            p2Move = data[1],
                            Result = data[2] as Game.Outcome
                        };
                    }
                    matches.Add(new Match { rounds = rounds, P1Name = p1name, P2Name = p2name });
                }

                return matches.ToArray();
            }
        }

        public void WriteFile(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
