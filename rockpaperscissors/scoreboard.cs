using System.Text;
using System.Text.RegularExpressions;

namespace rockpaperscissors
{
    // 1: här använder vi multibla intefaces
    // 2: vi använder detta genom att ange de intefaces vi vill att vår klass använder separerat med kommatäcken
    // 3:
    // -    Vi vill att klassen implementerar IDiskWritable vilket är ett interface vi har skrivit men även
    // -    att det inbyggda interfaces IDisposable implementeras.
    internal class ScoreBoard : IDiskWritable<Match[]>, IDisposable
    {
        private string? ioPath;
        private List<string[]> rounds = new List<string[]>();
        private string[] names = new string[] { "John_Doe", "Jane_Doe", "Vinnare" };

        public ScoreBoard() : this(Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ssp_proj")) {

        }

        public ScoreBoard(string IoPath)
        {
            if(!Directory.Exists(IoPath))
            {
                Directory.CreateDirectory(IoPath);

                // Vi skapar en readme fil i mappen som projektet skapade så att det är klart och tydligt varför
                // en mapp som heter "ssp_proj" plötsligt dykt upp under %appdata%. Man kan annars lätt tro att ett elakt virus har lekt rövare vilket vi inte vill
                var stream = File.Create(Path.Join(IoPath, "README.txt"));

                string txt = "Tjenare!\nDenna directory skapades automatiskt av vårat sten sax påse projekt för att spara spelets scoreboard.\n\n// Jonathan & Arvin";
                byte[] info = new UTF8Encoding(true).GetBytes(txt);
                stream.Write(info, 0, info.Length);
                stream.Close();

                // Vi kan också passa på att skapa filen för scoreboardet
                File.Create(Path.Join(IoPath, "scoreboard.csv")).Close();
            }

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

            if (!File.Exists(Path.Join(this.ioPath, filename))) return new Match[0];

            using (var sr = new StreamReader(Path.Join(this.ioPath, filename)))
            {
                string str = sr.ReadToEnd();
                var matchesRaw = str.Split('!');

                foreach(string _match in matchesRaw)
                {
                    if(String.IsNullOrEmpty(_match)) continue;
                    string match = _match.Trim('\r', '\n');
                    var lines = match.Split('\n');
                    int roundsPlayed = lines.Length - 3;
                    string p1name = lines[0], p2name = lines[1], winner = lines[2];

                    Round[] rounds = new Round[roundsPlayed];

                    for(int i = 3; i < lines.Length; i++)
                    {
                        string[] data = lines[i].Split(',');
                        if (String.IsNullOrEmpty(lines[i])) continue;

                        rounds[i - 3] = new Round
                        {
                            p1Move = Game.GetMoveFromString(data[0]),
                            p2Move = Game.GetMoveFromString(data[1]),
                            Result = Game.GetOutcomeFromString(data[2])
                        };
                    }

                    // Vi stötte på problem där namn med konstiga karaktärer ställde till det för oss
                    // så vi väljer att rengöra input
                    // src: https://stackoverflow.com/questions/3210393/how-do-i-remove-all-non-alphanumeric-characters-from-a-string-except-dash
                    Regex rgx = new Regex("[^a-zA-Z0-9 -]");

                    matches.Add(new Match { rounds = rounds, P1Name = rgx.Replace(p1name, ""), P2Name = rgx.Replace(p2name, ""), Winner = rgx.Replace(winner, "") });
                }

                return matches.ToArray();
            }
        }



        public void AddRound(Game.Move p1Move, Game.Move p2Move)
        {
            rounds.Add(new string[] { p1Move.ToString(), p2Move.ToString(), Game.Logic.Wins(p1Move, p2Move).ToString() });
        }

        public void SetNames(string p1, string p2)
        {
            names[0] = p1;
            names[1] = p2;
        }

        public void WriteFile(string filename)
        {
            string data = $"!\n{names[0]}\n{names[1]}\n{names[2]}\n";

            foreach (string[] rnd in rounds)
                data += $"{rnd[0]},{rnd[1]},{rnd[2]}\n";

            File.AppendAllText(Path.Join(this.ioPath, filename), data);
        }

        internal void SetWinner(string name)
        {
            names[2] = name;
        }
    }
}
