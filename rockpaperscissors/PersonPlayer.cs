using rockpaperscissors.Sounds;

namespace rockpaperscissors
{
    public class PersonPlayer : IMoveBehaviour, INameSelector
    {
        private string? Name;
        public string GetName()
        {
            //Menu.ColourWrite("Spelarens namn: ", ConsoleColor.Magenta);
            string? name = Menu.Ask("Spelarens namn: ", ConsoleColor.White, ConsoleColor.Magenta);
            if (string.IsNullOrEmpty(name)) 
            {
                new Error().Play(true);
                return GetName();
             }
            Name = name;
            return name;
        }

        Game.Move IMoveBehaviour.GetMove()
        {
            string prompt = $"{Name}s drag: ";
            string? move = Menu.Ask(prompt, ConsoleColor.White, ConsoleColor.Blue);

            //Om inget riktigt drag skrivs in så har vi en defualt på sten
            if (move == null || string.IsNullOrWhiteSpace(move)) move = "sten";

            // denna bit av kod väljet drag baserat på användarens input
            Game.Move m = Game.GetMoveFromString(move);

            Console.SetCursorPosition(prompt.Length, Console.GetCursorPosition().Top - 1);
            Console.Write("**********\n");

            return m;
        }
    }
}
