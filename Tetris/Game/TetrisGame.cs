﻿using System;
﻿using System.Collections.Generic;
﻿using System.Linq;
﻿using System.Media;
﻿using System.Threading;
﻿using Tetris.Game.Fallingtype;

namespace Tetris.Game
{
    class TetrisGame
    {
        //local varibles
        private int GametopPosistion { get; set; }
        private bool First { get; set; }
        public int Score { get; set; }
        private Queue<FallingType> NextMovingBlock { get; set; }
        private FallingType MovingBlock { get; set; }
        List<Block> StillBlocks { get; set; }
        private bool _canMove = true;
        private bool _gameOver = false;
        private SoundPlayer music = new SoundPlayer("tetris.wav");
        /// <summary>
        /// init game
        /// </summary>
        public TetrisGame()
        {
            music.Play();
            NextMovingBlock = new Queue<FallingType>();
            NextMovingBlock.Enqueue(GetRandomBlock());
            NextMovingBlock.Enqueue(GetRandomBlock());
            NextMovingBlock.Enqueue(GetRandomBlock());
            Console.Clear();
            Score = 0;
            StillBlocks = new List<Block>();
            Console.Out.WriteLine("Tetris");
            Console.Out.WriteLine("Score : {0}", Score);

            Console.Out.WriteLine("X X X X X X X X X X X X X");
            GametopPosistion = Console.CursorTop - 1;
            for (var i = 0; i < 20; i++)
            {
                Console.Out.WriteLine("X                       X");
            }
            Console.Out.WriteLine("X X X X X X X X X X X X X");

            First = true;
        }

        /// <summary>
        /// Generate Random block
        /// </summary>
        /// <returns>Randon fallingtype</returns>
        private FallingType GetRandomBlock()
        {
            FallingType newBlock = new L();
            var listOfBs = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                            from assemblyType in domainAssembly.GetTypes()
                            where typeof(FallingType).IsAssignableFrom(assemblyType)
                            select assemblyType).ToArray();
            var random = new Random().Next(listOfBs.Length - 1);
            switch (random)
            {
                case 0:
                    newBlock = new Long(); break;
                case 1:
                    newBlock = new L(); break;
                case 2:
                    newBlock = new Triangle(); break;
                case 3:
                    newBlock = new ReverseL(); break;
                case 4:
                    newBlock = new Cube(); break;
                case 5:
                    newBlock = new Z(); break;
                case 6:
                    newBlock = new ReverseZ(); break;


            }
            return newBlock;
        }
        /// <summary>
        /// adds new current moving block
        /// </summary>
        private void addTemps()
        {
            MovingBlock = NextMovingBlock.Dequeue();
            MovingBlock.X = 6;
            MovingBlock.Y = -1;
            NextMovingBlock.Enqueue(GetRandomBlock());
            RenderNext();

            First = true;


        }
        /// <summary>
        /// Render block
        /// </summary>
        /// <param name="output">What type of char that will outputted for each block</param>
        /// <param name="blocks">List of bloxks that will be outputted</param>
        private void Render(char output, List<Block> blocks)
        {
            foreach (var block in blocks.Where(block => block.Y > 0))
            {
                Console.SetCursorPosition((block.X * 2), block.Y + GametopPosistion);
                Console.ForegroundColor = block.Color;
                Console.Out.WriteLine(output);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        /// <summary>
        /// Moves fallingblock to right
        /// </summary>
        private void Right()
        {
            Move(1, 0, true);
        }
        /// <summary>
        /// Moves fallingblock to left
        /// </summary>
        private void Left()
        {


            Move(-1, 0, true);
        }

        /// <summary>
        /// Moves block to position
        /// </summary>
        /// <param name="x">move x from current x position</param>
        /// <param name="y">move y from current y position</param>
        /// <param name="direction">parameter if its turning right or left</param>
        private void Move(int x, int y, bool direction = false)
        {
            if (!_canMove) return;
            _canMove = false;
            var curretPos = MovingBlock.ToPostitions();
            var tempx = MovingBlock.X;
            var tempY = MovingBlock.Y;
            var bottom = MovingBlock.ToucingOnNextMove(x, y, StillBlocks);
            if (First && bottom)
            {
                if (direction)
                {
                    GameOver();
                    return;

                }
            }
            First = false;
            if (bottom)
            {
                if (direction)
                {
                    MovingBlock.X = tempx;
                    MovingBlock.Y = tempY;
                }
                else
                {
                    StillBlocks.AddRange(MovingBlock.ToPostitions());
                    addTemps();
                    ClearAndRender(new List<Block>(), StillBlocks, false);
                    CheckIfRowIsFull();
                    return;

                }

            }

            ClearAndRender(curretPos, MovingBlock.ToPostitions());
        }
        /// <summary>
        /// ends the game and outputs score
        /// </summary>
        private void GameOver()
        {
            _gameOver = true;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Out.WriteLine("GameOver, Tryck ESC för att återgå till meny");
            Console.ResetColor();
            UpdateScore();
        }
        /// <summary>
        /// Checks if some rows is full
        /// </summary>
        private void CheckIfRowIsFull()
        {

            var toRemove = new List<Block>();
            var moved = false;
            var rowsRemoved = 0;
            do
            {
                moved = false;

                for (int i = 20; i >= 0; i--)
                {
                    var sameRow = StillBlocks.Where(block => block.Y == i).ToList();
                    if (sameRow.Count() > 10)
                    {
                        rowsRemoved++;
                        UpdateScore();
                        sameRow.ForEach((block => StillBlocks.Remove(block)));
                        toRemove.AddRange(sameRow);

                        for (int j = i; j >= 0; j--)
                        {
                            var willMove = StillBlocks.Where(c => c.Y == j).ToList();
                            willMove.ForEach((block => StillBlocks.Remove(block)));

                            toRemove.AddRange(willMove);
                            StillBlocks.AddRange(willMove.Select(p => new Block(p.Color, p.X, p.Y + 1)));

                        }
                        moved = true;
                    }

                }
            } while (moved);
            Score = Score + (10 * (rowsRemoved * rowsRemoved));

            ClearAndRender(toRemove, StillBlocks);
        }
        /// <summary>
        /// updates current score on screen
        /// </summary>
        private void UpdateScore()
        {
            Console.SetCursorPosition(0, 1);
            Console.Out.WriteLine("Score : {0}", Score);

        }

        /// <summary>
        /// Rotates block
        /// </summary>
        private void Rotate()
        {
            var oldPos = MovingBlock.ToPostitions();
            MovingBlock.Rotate();

            if (MovingBlock.ToPostitions().Any((block => block.X == 1)))
            {
                MovingBlock.X++;
            }
            if (MovingBlock.ToPostitions().Any((block => block.X > 10)))
            {
                MovingBlock.X--;
            }
            ClearAndRender(oldPos, MovingBlock.ToPostitions());
        }
        /// <summary>
        /// Moves moving block to bottom
        /// </summary>
        private void MoveDown()
        {
            if (!_canMove) return;
            if (_gameOver) return;
            _canMove = false;
            var curentPos = MovingBlock.ToPostitions();
            var touch = false;
            while (!touch)
            {
                touch = MovingBlock.ToucingOnNextMove(0, 1, StillBlocks);
            }

            ClearAndRender(curentPos, StillBlocks);
        }
        /// <summary>
        /// Render the uppcomming movingblock on side
        /// </summary>
        private void RenderNext()
        {
            var left = 27;
            var top = GametopPosistion;
            Console.SetCursorPosition(left, top);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    Console.SetCursorPosition(left + i, top + j);

                    Console.Out.WriteLine(" ");
                }
            }
            var pos = 0;
            foreach (var fallingType in NextMovingBlock)
            {
                var temp = fallingType;
                temp.X = 15;
                temp.Y = top + (pos * 5);
                Console.SetCursorPosition(left, top + (pos * 5) + 1);
                Console.Out.WriteLine(pos + 1);
                Render('O', temp.ToPostitions());
                pos++;

            }
        }
        /// <summary>
        /// render new blocks and removes others
        /// </summary>
        /// <param name="blocksToRemove">list of blocks to remove</param>
        /// <param name="blocksToRender">list of block to render</param>
        /// <param name="canMoveAfter">can block move after operation</param>
        private void ClearAndRender(List<Block> blocksToRemove, List<Block> blocksToRender, bool canMoveAfter = true)
        {
            _canMove = false;
            Render(' ', blocksToRemove);
            Render('O', blocksToRender);
            _canMove = canMoveAfter;

        }
        /// <summary>
        /// stops game
        /// </summary>
        private void Stop()
        {
            _gameOver = true;
            music.Stop();
            Console.Clear();
        }
        /// <summary>
        /// runs game
        /// moves block and updates score
        /// </summary>
        private void Run()
        {
            var def = 500;
            addTemps();
            Thread.Sleep(1000);
            var speed = 500;
            while (!_gameOver)
            {
                Move(0, 1);
                if (Score < 300)
                {
                    speed = def - Score;
                }
                Thread.Sleep(speed);
            }
            music.Stop();

        }

        /// <summary>
        /// start a session of the game
        /// </summary>
        /// <returns>Score of the game</returns>
        public static int Play()
        {
            var tetrisGame = new TetrisGame();

            Thread childThread = new Thread(tetrisGame.Run);
            childThread.Start();
            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        tetrisGame.Rotate();

                        break;

                    case ConsoleKey.RightArrow:
                        tetrisGame.Right();
                        break;

                    case ConsoleKey.DownArrow:
                        tetrisGame.MoveDown();

                        break;

                    case ConsoleKey.LeftArrow:
                        tetrisGame.Left();
                        break;
                }

            }
            tetrisGame.Stop();
            var score = tetrisGame.Score;
            return score;
        }
    }


}
