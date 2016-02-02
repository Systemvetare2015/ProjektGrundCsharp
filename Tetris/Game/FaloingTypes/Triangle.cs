using System;
using System.Collections.Generic;
using System.Linq;
using Tetris.Game;
using Tetris.Game.FaloingTypes;

namespace Tetris
{
    class Triangle:FallingType
    {
        public Triangle()
        {
            RefPoses = new List<RefPos> {new RefPos(-1, 0), new RefPos(0, 0), new RefPos(+1, 0), new RefPos(0, -1)};
        }
        public override ConsoleColor Color => ConsoleColor.Cyan;
    }
}
