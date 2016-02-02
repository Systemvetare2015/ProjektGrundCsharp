using System;
using System.Collections.Generic;

namespace Tetris
{
    class Long:FallingType
    {
        public Long()
        {
            RefPoses = new List<RefPos>() {new RefPos(0,0), new RefPos(0, -1), new RefPos(0, 1), new RefPos(0, 2), };
        }
        public override ConsoleColor Color =>ConsoleColor.DarkRed;
    }
}
