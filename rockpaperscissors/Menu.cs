using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rockpaperscissors
{
    internal class Menu
    {
        public static string logo = 
@"
 ________   ________  ________   
|\   ____\ |\   ____\|\   __  \  
\ \  \___|_\ \  \___|\ \  \|\  \ 
 \ \_____  \\ \_____  \ \   ____\
  \|____|\  \\|____|\  \ \  \___|
    ____\_\  \ ____\_\  \ \__\   
   |\_________\\_________\|__|   
   \|_________\|_________|       
     Sten       Sax        Påse
";

        // En metod som används för att enkelt skriva ut text i färg. Återställer automatiskt konsolens färg till standarden efter funktionen har printat
        public static void ColourWrite(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }

        // En helper-metod för att skriva ut en ny rad med ColourWrite
        public static void ColourLog(string message, ConsoleColor colour)
            => ColourWrite($"{message}\n", colour);

        // Denna metod skriver ut en prompt i konsolen i valfri färg och tar input från användaren. Använderns svar går också att välja färg på
        public static string? Ask(string question, ConsoleColor promptColour, ConsoleColor answerColour)
        {
            ColourWrite(question, promptColour);
            Console.ForegroundColor = answerColour;
            string? rv = Console.ReadLine();
            Console.ResetColor();
            return rv;
        }
        // 1: här använder vi Overloading av instansmetoder
        // 2: vi gör detta genom att definera en funktion med samma namn som en annan funktion fast med annorlunda signatur
        // 3: Vi gör detta så man kan strunta i att ange vissa parametrar som kan anses onödiga om man vill anropa funktionen
        public static string? Ask(string question, ConsoleColor promptColour)
        {
            return Ask(question, promptColour, ConsoleColor.White);
        }

        // En boolisk metod som tar in information från anvädnaren angående val av spelare. Värdet av boolen används sedan för att sätta igång en AI eller
        // Spela mot en annan spelare.
        public static Boolean PlayAgainstBot()
        {
            Console.Write("Vill du spela mot AI (");
            ColourWrite("JA", ConsoleColor.Green);
            Console.Write("/");
            ColourWrite("NEJ", ConsoleColor.Red);
            Console.Write(")? ");

            string? res = Console.ReadLine();

            if (res == null || string.IsNullOrEmpty(res)) return PlayAgainstBot();

            return res.ToLower()[0] == 'j';
        }

        // Metoden som presneterar våra namn och resterande intro, logan etfc
        public static void Intro()
        {
            ColourLog("Arvin & Jonathan presenterar...", ConsoleColor.DarkGray);
            ColourLog(logo, ConsoleColor.DarkRed);
            Console.WriteLine("Ni spelar försten till 3. Du anger vad du vill spela genom att skriva sten, sax, eller påse.");
        }
    }
}
