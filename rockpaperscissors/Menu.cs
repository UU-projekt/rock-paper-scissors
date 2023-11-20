using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rockpaperscissors
{
    internal class Menu
    {
        public static void Intro()
        {
            Console.WriteLine("Vill du spela mot en annan spelare?");
            string? alternativ = Console.ReadLine();

            if (alternativ == "ja" || alternativ == "Ja" || alternativ == "JA")
            {
            }
        }
    }
}
