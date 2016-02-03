using System;
using System.Collections.Generic;
using System.Linq;

namespace Tetris
{
    public abstract class FallingType
    {
        protected FallingType()
        {
            Y = -1;
            X = 6;
        }
        public  int Y { get; set; }
        public  int X { get; set; }
        public abstract ConsoleColor Color { get; }
        public  List<RefPos> RefPoses { get;set;}
        public List<Block> ToPostitions()
        {
            return RefPoses.Select((block => new Block(Color, X + block.X,block.Y + Y))).ToList();
        }

        public bool ToucingOnNextMove(int x, int y, List<Block> stillBlocks)
        {
            if (!(ToPostitions().Any(pos => (pos.X + x) < 1 || (pos.X + x) > 11)))
            {
                X=X + x;
            }
            var touching = ToPostitions().Any(p => p.Y + y > 20 || stillBlocks.Any(v =>v.X== p.X && (v.Y -y)== p.Y));
            if (!touching)
            {
                Y = Y + y;
            }
            return touching;
        }

        public void Rotate()
        {
            RefPoses = RefPoses.ConvertAll((input => new RefPos(-input.Y, input.X)));
        }
        
    }

    
}
