using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CA1416

namespace rockpaperscissors
{
    // 1: här använder vi en abstrakt klass
    // 2: vi använder abstrakta klasser för att skapa ett kontrakt som klasser som ärver behöver uppfylla
    // 3:
    // -    Vi använder detta då vi vill ha flera olika "ljud" som använder sig av de redan etablerade funktionerna
    // -    som finns i bas-klassen "Sounds". Vi markerar "Sound" som abstrakt då den inte är fullständig
    internal abstract class Sound
    {

        // Denna funktion vart till stor del skriven av chatGPT. Detta för att snabba upp den meniala proccessen av att
        // skapa ett lookup-table med noter och deras frekvenser.
        // prompten som användes var:
        // "Hey there! I would like you to construct a C# dict where the key is a musical note and the value is an int containing that notes frequency in hz."
        // samma information skulle kunna matas in manuellt med hjälp av https://pages.mtu.edu/~suits/notefreqs.html

        static Dictionary<string, int> getScale()
        {
            Dictionary<string, int> noteFrequencies = new Dictionary<string, int>();

            // Populate the dictionary with note-frequency pairs
            noteFrequencies.Add("C", 261);    // C4
            noteFrequencies.Add("C#", 277);   // C#4/Db4
            noteFrequencies.Add("D", 293);    // D4
            noteFrequencies.Add("D#", 311);   // D#4/Eb4
            noteFrequencies.Add("E", 329);    // E4
            noteFrequencies.Add("F", 349);    // F4
            noteFrequencies.Add("F#", 369);   // F#4/Gb4
            noteFrequencies.Add("G", 391);    // G4
            noteFrequencies.Add("G#", 415);   // G#4/Ab4
            noteFrequencies.Add("A", 440);    // A4 (standard tuning reference)
            noteFrequencies.Add("A#", 466);   // A#4/Bb4
            noteFrequencies.Add("B", 493);    // B4

            return noteFrequencies;
        }

        protected static Dictionary<string, int> notes = getScale();

        private static bool hasWarned = false;
        // Detta är funktionen som faktiskt spelar ljuden. 
        // Denna är protected då vi vill se till att den bara kallas
        // när användaren använder windows vilket vi gör genom att skapa en "middleware" funktion
        // som i sin tur kallar på funktionen
        protected abstract void play();

        protected void PlaySequence(int[] notes) {
            foreach (int note in notes)
                Console.Beep(note, 500);
        }

        protected void PlayNote(string note, int time = 500, float modifier = 5 ) {
            notes.TryGetValue(note, out var noteFreq);
            Console.Beep((int)(noteFreq * modifier), time);
        }

        // Detta är "middleware" funktionen som nämns åvan
        public void Play(bool async = false) {

            // Kollar om operativsystemet är windows.
            // Windows = true
            // src: https://stackoverflow.com/questions/38790802/determine-operating-system-in-net-core
            bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                                               .IsOSPlatform(OSPlatform.Windows);

            if(!isWindows)
            {
                if(!hasWarned) Menu.ColourLog("Du kör inte windows\nVi använder 'Console.Beep()' funktionen för att spela ljud. Denna funktion använder en API som bara stöds av windows.\nInget kommer krascha men ljud kommer inte att spelas", ConsoleColor.DarkYellow);
                
                // Vi vill bara att varningen visas 1 gång per session så den inte förstör upplevelsen
                // vi har därför en statisk variabel som håller koll på om medelandet har visats tidigare
                hasWarned = true;
                return;
            }

            // Vi vill inte alltid att ljud som spelar sätter stopp för spelet tills ljudet är klart.
            // Vi ger därför utvecklaren valet att köra ljudsekvensen i en ny tråd vilket gör att ljudet
            // kan spelas samtidigt som själva spelet körs
            if (async)
            {
                new Thread(() =>
                    this.play()
                ).Start();
            } else
            {
                this.play();
            }
        }

        public void Multiple(int times) {
            for(int i = 0; i < times; i++)
                this.Play();
        }
    }
}
