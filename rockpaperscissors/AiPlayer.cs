namespace rockpaperscissors
{
    //Klassen för AI spelaren, den använder interface från IMoveBehaviour och INameSelector
    public class AiPlayer : IMoveBehaviour, INameSelector
    {
        // En funktion som returnerar string som vi anvädner för att ge ett namn till botten
        // Denna funktion krävs för att uppfylla kraven från INameSelector
        public string GetName()
        {
            return "Botten Anna";
        }

        // Generar ett slumpmäsigt drag
        Game.Move IMoveBehaviour.GetMove()
        {
            var r = new Random();

            return (Game.Move) r.Next(0,3);
        }
    }
}
