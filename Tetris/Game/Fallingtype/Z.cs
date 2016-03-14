using System;
using System.Collections.Generic;

namespace Tetris.Game.Fallingtype
{
    /// <summary>
    /// Type of falingblock
    /// </summary>
    class Z:FallingType
    {
        public Z()
        {
            RefPoses = new List<RefPos>() { new RefPos(0, 0), new RefPos(-1, 0), new RefPos(0, -1), new RefPos(1, -1) };
        }
        public override ConsoleColor Color
        {
            get { return ConsoleColor.Green;}
        }
    }
}
