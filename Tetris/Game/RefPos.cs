﻿namespace Tetris.Game
{
    /// <summary>
    /// Refrenc posisions to a certain position
    /// </summary>
    public class RefPos
    {
        public RefPos(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
