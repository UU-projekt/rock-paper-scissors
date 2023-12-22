namespace rockpaperscissors
{
    internal class Round
    {
        public Game.Move p1Move { get; init; }
        public Game.Move p2Move { get; init; }
        public Game.Outcome Result { get; init; }

        public static implicit operator Round(string v)
        {
            throw new NotImplementedException();
        }
    }
}
