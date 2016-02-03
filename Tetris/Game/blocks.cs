using System;
using System.Collections.Generic;

namespace Tetris
{
    public  class Block
    {
        public Block(ConsoleColor color, int x, int y)
        {
            Color = color;
            X = x;
            Y = y;
        }
        public ConsoleColor Color { get; set; }
        public  int X { get; set; }
        public  int Y { get; set; }
    }
}
