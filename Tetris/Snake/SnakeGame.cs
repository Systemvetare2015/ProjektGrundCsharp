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

        private int GameTopPos = 2;
        public int Score = 0;
        
        private RefPos Meat { get; set; }
        private Snake Snake { get; set; }
        private Random random = new Random();
        private bool GameOver = false;
        /// <summary>
        /// Init a new game
        /// </summary>
        public SnakeGame()
        {
            Console.CursorVisible = false;
            Snake = new Snake(gameSize);
            GetRandomFood();
            Render();
        }
        /// <summary>
        /// start game
        /// </summary>
        public void Run()
        {
            while (!TouchWall() && !GameOver)
            {
                Snake.Move();
                if (Snake.TouchSelf())
                    break;
                TouchFood();
                Render();
                Thread.Sleep(200);
            }
            Console.Clear();
            Console.Out.WriteLine("GameOver \nPress Esc to leave");
        }
        /// <summary>
        /// stop game
        /// </summary>
        public void Stop()
        {
            GameOver = true;
        }
        /// <summary>
        /// outputs food on game plan
        /// </summary>
        private void GetRandomFood()
        {
            Meat = new RefPos(random.Next(1, gameSize - 2), random.Next(1, gameSize / 2));
        }
        /// <summary>
        /// render out all
        /// </summary>
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
        /// <summary>
        /// Check if touches food
        /// </summary>
        private void TouchFood()
        {
            if (!Snake.RefPoses.Any((pos => (pos.X == Meat.X && pos.Y == Meat.Y)))) return;
            Snake.Add();
            GetRandomFood();
            Score = Score + 10;
        }
        /// <summary>
        /// checks if touch wall
        /// </summary>
        /// <returns>If touch wall</returns>
        private bool TouchWall()
        {
            if (Snake.RefPoses.Any((pos => pos.X == 0 || pos.X > (gameSize-2))))
                return true;
            if (Snake.RefPoses.Any((pos => pos.Y == 0 || pos.Y > gameSize/2)))
                return true;
            return false;
        }
        /// <summary>
        /// trun right
        /// </summary>
        public void Right()
        {
            Snake.Right();
        }
        /// <summary>
        /// turn left
        /// </summary>
        public void Left()
        {
            Snake.Left();
        }
        /// <summary>
        /// Play game
        /// </summary>
        /// <returns>returns score</returns>
        public static int Play()
        {
            Console.Clear();
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
            snakeGame.Stop();
            Console.Clear();
            return snakeGame.Score;
        }
    }
}
