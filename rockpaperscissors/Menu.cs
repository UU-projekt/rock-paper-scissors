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
        public static void ColourWrite(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(message);
            Console.ResetColor();
        }
        public static void ColourLog(string message, ConsoleColor colour)
            => ColourWrite($"{message}\n", colour);

        public static string? Ask(string question, ConsoleColor promptColour, ConsoleColor answerColour)
        {
            ColourWrite(question, promptColour);
            Console.ForegroundColor = answerColour;
            string? rv = Console.ReadLine();
            Console.ResetColor();
            return rv;
        }

        public static string? Ask(string question, ConsoleColor promptColour)
        {
            return Ask(question, promptColour, ConsoleColor.White);
        }
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

        public static void Intro()
        {
            ColourLog("Arvin & Jonathan presenterar...", ConsoleColor.DarkGray);
            ColourLog(logo, ConsoleColor.DarkRed);
        }
    }
}
