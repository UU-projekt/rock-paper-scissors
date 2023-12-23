using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CA1416

namespace rockpaperscissors.Sounds
{

    // 1: här använder vi arv av klasser
    // 2: vi använder arv av klasser för att skapa en klass som utökar en redan existerande klass med flera funktioner
    // 3:
    // -    Vi ärver Sound då denna superklass innehåller funktioner som varje ljudsekvent behöver använda sig av.
    // -    Detta låter oss också hantera olika ljud direkt som en instans av "Sound" 
    internal class MenuJingle : Sound
    {
        // Denna jingel kommer inte att vinna nån grammy men den gör användarupplevelsen mer interesant
        protected override void play()
        {
            for(int i = 0; i < 3; i++)
            {
                PlayNote("D#", 50 + (i * 25));
                PlayNote("C", 100 + (i * 25));
            }
            PlayNote("C", 350);
            PlayNote("D#");
        }
    }
}
