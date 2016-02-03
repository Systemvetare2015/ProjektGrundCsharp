using System;
using System.Collections.Generic;
using System.Threading;
using Tetris.Game;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            var stats = new List<UserStat>();
            while (true)
            {
                
                var alternatives = new List<string> {"Spela" ,"Kolla statistik","ska[a anvan"};
                var val = MenuHelper.AskFromAlternative("hej! vad vill du göra?", alternatives);
                switch (val)
                {
                    case 0:
                        stats.Add(new UserStat
                        {
                            Namn = MenuHelper.Ask("hej vad heter du?"),
                            Score = PlayGame()
                        });
                     break;
                    case 1:
                        PrintStat(stats);
                        break;
                    default:
                        Console.Out.WriteLine("Finns inte än");
                        break;

                }

            }

        }

        private static void PrintStat(List<UserStat> userStats)
        {
            userStats.ForEach((stat =>
            {
                Console.Out.WriteLine("{0} : {1}",stat.Namn,stat.Score);
            }));
            Console.ReadKey();
        }

        private static int PlayGame()
        {
            var tetrisGame = new TetrisGame();
            var childThread = new Thread(tetrisGame.Run);
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
           return tetrisGame.score;
        }

    }
}
