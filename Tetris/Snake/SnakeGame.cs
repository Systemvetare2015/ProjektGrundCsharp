using System;
using System.Linq;
using System.Threading;
using Tetris.Game;

namespace Tetris.Snake
{
    class SnakeGame
    {
        private int gameSize
        {
            get { return 40; }
        }
        private int GameTopPos { get; set; }
        public int Score { get; set; }
        
        private RefPos Meat { get; set; }
        private Snake Snake { get; set; }
        private Random random = new Random();

        public SnakeGame()
        {
            GameTopPos = 2;
            Score = 0;
            Console.CursorVisible = false;
            Snake = new Snake(gameSize);
            GetRandomFood();
            Render();
        }
        
        public void Run()
        {
            while (!TouchWall())
            {
                Snake.Move();
                if (Snake.TouchSelf())
                    break;
                TouchFood();
                Render();
                Thread.Sleep(200);
            }
            Console.Clear();
            Console.Out.WriteLine("GameOver");
        }

        private void GetRandomFood()
        {
            Meat = new RefPos(random.Next(1, gameSize - 2), random.Next(1, gameSize / 2));
        }
        private void Render()
        {
            Console.SetCursorPosition(0, 0);
            Console.Out.WriteLine("Snake");
            Console.Out.WriteLine("Score: {0}",Score);
            Console.SetCursorPosition(0, GameTopPos);
            Console.Out.WriteLine(new string('x', gameSize));
            for (var i = 0; i < gameSize/2; i++)
            {
                Console.Out.WriteLine("X{0}X", new string(' ', gameSize - 2));
            }
           
            Console.Out.WriteLine(new string('x', gameSize));
            Console.BackgroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(Meat.X,Meat.Y+GameTopPos);
            Console.Out.WriteLine("O");
            Console.ResetColor();
            foreach (var refPose in Snake.RefPoses)
            {
                Console.SetCursorPosition(refPose.X, refPose.Y+GameTopPos);
                Console.Out.Write("O");
            }
        }

        private void TouchFood()
        {
            if (!Snake.RefPoses.Any((pos => (pos.X == Meat.X && pos.Y == Meat.Y)))) return;
            Snake.Add();
            GetRandomFood();
            Score = Score + 10;
        }
        private bool TouchWall()
        {
            if (Snake.RefPoses.Any((pos => pos.X == 0 || pos.X > (gameSize-2))))
                return true;
            if (Snake.RefPoses.Any((pos => pos.Y == 0 || pos.Y > gameSize/2)))
                return true;
            return false;
        }
        public void Right()
        {
            Snake.Right();
        }
        public void Left()
        {
            Snake.Left();
        }
        public static int Play()
        {
            var snakeGame = new SnakeGame();
            Thread childThread = new Thread(snakeGame.Run);
            childThread.Start();
            ConsoleKeyInfo keyInfo;
             while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Escape)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.RightArrow:
                        snakeGame.Right();
                        break;
                    case ConsoleKey.LeftArrow:
                        snakeGame.Left();
                        break;
                }

            }
            return snakeGame.Score;
        }
    }
}
