using System;
using System.Threading;
using Tetris.Game;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
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
            var score = tetrisGame.score;

            Console.ReadKey();
        }

    }
}
