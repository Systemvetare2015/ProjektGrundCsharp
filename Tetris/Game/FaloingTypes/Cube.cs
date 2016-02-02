using System;
using System.Collections.Generic;

namespace Tetris.Game.FaloingTypes
{
    class Cube:FallingType
    {
        public Cube()
        {
            RefPoses = new List<RefPos> { new RefPos(0, 0), new RefPos(1, 0), new RefPos(0, 1), new RefPos(1, 1), };
        }
        public override ConsoleColor Color =>ConsoleColor.DarkMagenta;
    }
}
