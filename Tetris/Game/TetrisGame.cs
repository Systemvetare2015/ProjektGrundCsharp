﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
﻿using Tetris.Game.Fallingtype;

namespace Tetris.Game
{
    class TetrisGame
    {
        private int gametopPosistion { get; set; }
        private bool first { get; set; }
        public int score { get; set; }
        private FallingType MovingBlock { get; set; }
        List<Block> stillBlocks { get; set; }
        private bool canMove = true;
        private bool gameOver = false;
        public TetrisGame()
        {
            Console.Clear();
            score = 0;
            stillBlocks = new List<Block>();
            Console.Out.WriteLine("Tetris");
            Console.Out.WriteLine("Score : {0}", score);

            Console.Out.WriteLine("X X X X X X X X X X X X X");
            gametopPosistion = Console.CursorTop - 1;
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
                case 4:
                    MovingBlock = new Cube();
                    break;
                case 5:
                    MovingBlock = new Z();
                    break;
                case 6:
                    MovingBlock = new ReverseZ();
                    break;

            }
            first = true;


        }
        private void Render(char output, List<Block> blocks)
        {
            foreach (var block in blocks.Where(block => block.Y > 0))
            {
                Console.SetCursorPosition((block.X * 2), block.Y + gametopPosistion);
                Console.ForegroundColor = block.Color;
                Console.Out.WriteLine(output);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void Right()
        {
            Move(1, 0);
        }
        public void Left()
        {
            Move(-1, 0);
        }


        private void Move(int x, int y)
        {
            if (!canMove) return;
            canMove = false;
            var curretPos = MovingBlock.ToPostitions();
            var bottom = MovingBlock.ToucingOnNextMove(x, y, stillBlocks);
            if (first && bottom)
            {
                GameOver();
                return;y
            }
            first = false;
            if (bottom)
            {
                stillBlocks.AddRange(MovingBlock.ToPostitions());
                addTemps();
                ClearAndRender(new List<Block>(), stillBlocks, false);
                CheckIfRowIsFull();
                return;
            }
            ClearAndRender(curretPos, MovingBlock.ToPostitions());
        }

        private void GameOver()
        {
            gameOver = true;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Out.WriteLine("GameOver");
            Console.ResetColor();
            UpdateScore();
        }

        public void CheckIfRowIsFull()
        {
            
            var toRemove = new List<Block>();
            var moved = false;
            var rowsRemoved = 0;
            do
            {
                moved = false;

                for (int i = 20; i >= 0; i--)
                {
                    var sameRow = stillBlocks.Where(block => block.Y == i).ToList();
                    if (sameRow.Count() > 10)
                    {
                        rowsRemoved++;
                        UpdateScore();
                        sameRow.ForEach((block => stillBlocks.Remove(block)));
                        toRemove.AddRange(sameRow);

                        for (int j = i; j >= 0; j--)
                        {
                            var willMove = stillBlocks.Where(c => c.Y == j).ToList();
                            willMove.ForEach((block => stillBlocks.Remove(block)));

                            toRemove.AddRange(willMove);
                            stillBlocks.AddRange(willMove.Select(p => new Block(p.Color, p.X, p.Y + 1)));

                        }
                        moved = true;
                    }

                }
            } while (moved);
            score = score + (10 * (rowsRemoved * rowsRemoved));

            ClearAndRender(toRemove, stillBlocks);
        }

        private void UpdateScore()
        {
            Console.SetCursorPosition(0, 1);
            Console.Out.WriteLine("Score : {0}", score);

        }

        public void Rotate()
        {
            var oldPos = MovingBlock.ToPostitions();
            MovingBlock.Rotate();
            ClearAndRender(oldPos, MovingBlock.ToPostitions());
        }

        public void MoveDown()
        {
            if (!canMove) return;
            if (gameOver) return;
            canMove = false;
            var curentPos = MovingBlock.ToPostitions();
            var touch = false;
            while (!touch)
            {
                touch = MovingBlock.ToucingOnNextMove(0, 1, stillBlocks);
            }

            ClearAndRender(curentPos, stillBlocks);
        }



        private void ClearAndRender(List<Block> blocksToRemove, List<Block> blocksToRender, bool canMoveAfter = true)
        {
            canMove = false;
            Render(' ', blocksToRemove);
            Render('O', blocksToRender);
            canMove = canMoveAfter;

        }

        public void Stop()
        {
            gameOver = true;
            Console.Clear();
        }
        public void Run()
        {
            var def = 500;
            addTemps();
            Thread.Sleep(1000);
            var speed = 500;

            while (!gameOver)
            {
                Move(0, 1);
                if (score < 300)
                {
                    speed = def - score;
                }
                Thread.Sleep(speed);
            }
        }
        public delegate void RunBetweenRender();

    }


}
