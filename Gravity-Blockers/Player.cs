namespace EAS_Project
{
    public class Player
    {
        public string name { get; private set; }
        public int movesRemaining { get; private set; }

        public Player(string playerName)
        {
            name = playerName;
            movesRemaining = 3;
        }

        public void DecrementMoves()
        {
            movesRemaining--;
        }

        public void ResetMoves()
        {
            movesRemaining = 3;
        }

        public bool HasMovesRemaining()
        {
            return movesRemaining > 0;
        }
    }
}
