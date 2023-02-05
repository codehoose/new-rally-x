namespace NewRallyX.Models
{
    internal class GameState
    {
        public const int INITIAL_FUEL = 64;
        public const int INITIAL_LIVES = 3;

        public int[] Scores { get; set; } = new int[] { 0, 0 };

        public int HiScore { get; set; }

        public int[] Fuel { get; set; } = new int[] { INITIAL_FUEL, INITIAL_FUEL };

        public int[] Lives { get; set; } = new int[] { INITIAL_LIVES, INITIAL_LIVES };

        public int Round { get; set; } = 1;

        public int Player { get; set; } = 1;
    }
}
