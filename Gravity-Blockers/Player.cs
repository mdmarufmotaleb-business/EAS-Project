namespace EAS_Project
{
    public class Player
    {
        public string name { get; private set; }
        public int movesRemaining { get; private set; }
        public bool isBot { get; private set; }

        public Player(string playerName, bool isBot = false)
        {
            name = playerName;
            isBot = isBot;
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

        public void SetBot(bool isBot)
        {
            this.isBot = isBot;
        }
    }
}
