﻿using System;
using System.Collections.Generic;

namespace Tetris.Game.Fallingtype
{
    class L:FallingType
    {
        public L()
        {
            RefPoses =new List<RefPos> { new RefPos(0, 0), new RefPos(0, 1), new RefPos(0, -1), new RefPos(1, -1) }; 
        }
        public override ConsoleColor Color { get { return ConsoleColor.DarkGreen; } }

    }
}