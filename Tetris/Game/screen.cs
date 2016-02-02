using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Tetris.Game.FaloingTypes;

namespace Tetris.Game
{
    class TetrisGame
    {
        private int gametopPosistion { get; set; }
        private bool first { get; set; }
        public int score { get; set; }
        private FallingType  MovingBlock{ get; set; }
        List<Block>  stillBlocks{ get; set; }
        private bool canMove = true;
        public TetrisGame()
        {
            Console.Clear();
            score = 0;
            stillBlocks = new List<Block>();
            Console.Out.WriteLine("Tetris");
            Console.Out.WriteLine($"Score : {score}");
            
            Console.Out.WriteLine("X X X X X X X X X X X X X");
            gametopPosistion = Console.CursorTop-1;
            for (var i = 0; i < 20; i++)
            {
                Console.Out.WriteLine("X                       X");  
            }
            Console.Out.WriteLine("X X X X X X X X X X X X X");
            first = true;
        }

        private void addTemps()
        {
            var listOfBs = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                            from assemblyType in domainAssembly.GetTypes()
                            where typeof(FallingType).IsAssignableFrom(assemblyType)
                            select assemblyType).ToArray();
            var random = new Random().Next(listOfBs.Length - 1);
            switch (random)
            {
                case 0:
                    MovingBlock = new Long();
                    break;
                case 1:
                    MovingBlock = new L();
                    break;
                case 2:
                    MovingBlock = new Triangle();
                    break;
                case 3:
                    MovingBlock = new ReverseL();
                    break;
            }
            first = true;


        }
        private void Render(char output)
        {
            foreach (var block in MovingBlock.ToPostitions())
            {
                if (block.Y>0)
                {
                    Console.SetCursorPosition((block.X * 2), block.Y + gametopPosistion);
                    Console.ForegroundColor = block.Color;
                    Console.Out.WriteLine(output);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            
        }

        public void Right()
        {
            Move(1,0);
        }
        public void Left()
        {
            Move(-1,0);
        }
        private void RenderStillBlocks(char output = 'O')
        {
            foreach (var block in stillBlocks)
            {
                Console.SetCursorPosition((block.X * 2), block.Y + gametopPosistion);
                Console.ForegroundColor = block.Color;
                Console.Out.WriteLine(output);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private void Move(int x, int y)
        {
            if (!canMove) return;
            canMove = false;
            Render(' ');
            var bottom = MovingBlock.ToucingOnNextMove(x, y, stillBlocks);
            if (first && bottom)
            {
                GameOver();
                return;
            }
            first = false;
            if (bottom)
            {
                stillBlocks.AddRange(MovingBlock.ToPostitions());
                ClearAndRender(false, addTemps);
                RenderStillBlocks();
                ClearAndRender(true, CheckIfRowIsFull);
                RenderStillBlocks();
                return;
            }

            Render('O');
            canMove = true;
        }

        private void GameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Out.WriteLine("GameOver");
            Console.ResetColor();
            UpdateScore();
        }

        public void CheckIfRowIsFull()
        {
            var Moved = false;
            do
            {
                Moved = false;

                for (int i = 20; i >= 0; i--)
                {
                    var sameRow = stillBlocks.Where(block => block.Y == i).ToList();
                    if (sameRow.Count() > 10)
                    {
                       score = score + 10;
                        UpdateScore();
                        RenderStillBlocks(' ');
                        sameRow.ForEach(r => stillBlocks.Remove(r));
                        RenderStillBlocks();

                        for (int j = i ; j >= 0; j--)
                        {
                            var willMove = stillBlocks.Where(c => c.Y == j).ToList();
                            RenderStillBlocks(' ');

                            willMove.ForEach(r => stillBlocks.Remove(r));
                            RenderStillBlocks();

                            stillBlocks.AddRange(willMove.Select(p =>new Block(p.Color,p.X,p.Y+1)));



                        }
                        Moved = true;
                    }
                    
                }
            } while (Moved);
        }

        private void UpdateScore()
        {
            Console.SetCursorPosition(0,1);
            Console.Out.WriteLine($"Score : {score}");

        }

        public void Rotate()
        {
            ClearAndRender(true,MovingBlock.Rotate);
        }

        public void MoveDown()
        {
            return;
            canMove = false;
            while (!MovingBlock.ToucingOnNextMove(MovingBlock.X,MovingBlock.Y,stillBlocks))
            {

            }
            stillBlocks.AddRange(MovingBlock.ToPostitions());
            ClearAndRender(true, addTemps);
            RenderStillBlocks();
            canMove = true;
        }

       

        private void ClearAndRender(bool CanMove = true,RunBetweenRender runBetweenRender = null,RunBetweenRender runBetweenRender2 = null)
        {
            canMove = false;
            Render(' ');
            runBetweenRender?.Invoke();
            runBetweenRender2?.Invoke();
            Render('O');
            canMove = CanMove;

        }
        public void Run()
        {
            addTemps();

            Render('O');
            Thread.Sleep(1000);

            while (true)
            {
                
                Move(0,1);


                Thread.Sleep(200);
            }
                
            
        }
        public delegate void RunBetweenRender();

    }

    
}
