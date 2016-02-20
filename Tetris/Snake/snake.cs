using System;
using System.Collections.Generic;
using System.Linq;
using Tetris.Game;

namespace Tetris.Snake
{
    class Snake
    {
        public int X { get; set; }
        public int Y { get; set; }
        private int _xDirection = 1;
        private int _yDirection = 0;
        public Queue<RefPos> RefPoses { get; set; }

        public Snake(int gameSize)
        {
            RefPoses = new Queue<RefPos>();
             X = gameSize/2;
            Y = gameSize/4;
            RefPoses.Enqueue(new RefPos(X, Y));
            RefPoses.Enqueue(new RefPos(X+1, Y));
            RefPoses.Enqueue(new RefPos(X+2, Y));

        }

        public bool TouchSelf()
        {
            var last = RefPoses.Peek();
            return RefPoses.Where((pos => pos != last)).Any((pos => pos.X == last.X && pos.Y == last.Y));
        }

        public void Move()
        {
            RefPoses.Dequeue();
            var next = RefPoses.Last();
            RefPoses.Enqueue(new RefPos(next.X+_xDirection,next.Y+_yDirection));
        }

        public void Add()
        {
            var last = RefPoses.Last();
            RefPoses.Enqueue(last);
        }
        public void Right()
        {
            if (_xDirection != 0)
            {
                _yDirection = _xDirection;
                _xDirection = 0;

            }
            else if (_yDirection != 0)
            {
                _xDirection = -_yDirection;
                _yDirection = 0;
            }
        }
        public void Left()
        {
            if (_xDirection !=0)
            {
                _yDirection = -_xDirection;
                _xDirection = 0;

            }else if (_yDirection != 0)
            {
                _xDirection = _yDirection;
                _yDirection = 0;
            }
        }
    }
}
