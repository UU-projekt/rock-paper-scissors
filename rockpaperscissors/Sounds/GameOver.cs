using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CA1416

namespace rockpaperscissors.Sounds
{
    internal class GameOver : Sound
    {
        protected override void play()
        {
            foreach(string note in notes.Keys)
            {
                PlayNote(note, 125);
            }
        }
    }
}
