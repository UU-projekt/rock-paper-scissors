using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rockpaperscissors
{

    internal interface IDiskWritable
    {
        public string[] ReadFile(string path);
        public void WriteFile(string path);
    }

    // 1: här använder vi multibla intefaces
    // 2: vi använder detta genom att ange de intefaces vi vill att vår klass använder separerat med kommatäcken
    // 3:
    // -    Vi vill att klassen implementerar IDiskWritable vilket är ett interface vi har skrivit men även
    // -    att det inbyggda interfaces IDisposable implementeras.
    internal class ScoreBoard : IDiskWritable, IDisposable
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

        public string[] ReadFile(string filename)
        {
            using (var sr = new StreamReader(Path.Join(this.ioPath, filename)))
            {
                string str = sr.ReadToEnd();
                return str.Split('\n');
            }
        }

        public void WriteFile(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
