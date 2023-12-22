namespace rockpaperscissors
{
    // 1: här använder vi interface
    // 2: vi skapar ett interface med dom funktioner vi vill ha                                 
    // 3: vi använder detta för att kunna använda oss av dependency injection i Player klassen  
    internal interface IMoveBehaviour : INameSelector
    {
        public abstract Game.Move GetMove();
    }
}
