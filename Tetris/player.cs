using System;

namespace Tetris
{
    public class Player
    {
        public Player(string playername)
        {
            PlayerName = playername;
                        TimePlayed =DateTime.Now;
        }

        public Player()
        {
            
        }
        public string PlayerName { get; set; }
        public int Score { get; set; }
        public DateTime TimePlayed { get; set; }
        public GameType Game { get; set; }

        public enum GameType
        {
            Tetris,
            Snake
        }
    }
}