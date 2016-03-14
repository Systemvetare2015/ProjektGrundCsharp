using System;
using System.Collections.Generic;

namespace Tetris.Game.Fallingtype
{
    /// <summary>
    /// Type of falingblock
    /// </summary>
    class Triangle:FallingType
    {
        public Triangle()
        {
            RefPoses = new List<RefPos>(){ new RefPos(-1, 0), new RefPos(0, 0), new RefPos(+1, 0), new RefPos(0, -1) };
        }
        public override ConsoleColor Color { get { return ConsoleColor.Cyan; }}

    }
}
