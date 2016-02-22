using System;
using System.Diagnostics;
using Tetris.Game;
using Tetris.Snake;

namespace Tetris
{
    class Program
    {


        static void Main(string[] args) 
        {
            //var tetrisScore = TetrisGame.Play();
            var snakeScore = SnakeGame.Play();

            MainMeny();


            var menyVal = int.Parse(Console.ReadLine());

            switch (menyVal)
            {
                case 1:
                    Console.Clear();
                    MenyVal1();
                    break;

                case 2:
                    HighScore();
                    break;

                case 3:
                    EndProgram();
                    break;

            }


        }

        public static void MenyVal1()
        {
            Console.WriteLine(" =================================");
            Console.WriteLine(" Välj ett av följande alternativ: ");
            Console.WriteLine(" =================================" + Environment.NewLine);
            Console.WriteLine(" 1. Spela ");
            Console.WriteLine(" 2. Se end credit ");
            Console.ReadKey();


            
            int menyVal1 = int.Parse(Console.ReadLine());

            switch (menyVal1)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine(" ===========================");
                    Console.WriteLine("    Ange ett spelarnamn ");
                    Console.WriteLine(" ===========================" + Environment.NewLine);
                    Console.Write(" Namn:       ");
                    string playername = Console.ReadLine();
                    var newPlayer = new Player(playername);
                    newPlayer.Score = TetrisGame.Play();
                    newPlayer.Game = Player.GameType.Tetris;
                    // starta spel
                    break;

                case 2:
                    Console.WriteLine(" ============================");
                    Console.WriteLine("         END CREDIT          ");
                    Console.WriteLine(" ============================" + Environment.NewLine);

                    Console.WriteLine(" Felix Svensson - Gamedesigner" + Environment.NewLine);
                    Console.WriteLine(" Martin Olsson - Menymaker");

                    Console.Clear();

                    MainMeny();


                    break;




            }
        }

        private static void MainMeny()
        {
            Console.WriteLine(" =======================================================");
            Console.WriteLine(" =========           Welcome Player!           =========");
            Console.WriteLine(" =======================================================");

            Console.WriteLine(" =========     Välj ett alternativen nedan:    =========");
            Console.WriteLine(" =======================================================");
            Console.WriteLine(" ");
            Console.WriteLine(" 1. Logga in och spela   2. Visa Highscore    3. Avsluta ");
            

        }

        private static void HighScore()
        {
            Console.Clear();
            Console.WriteLine("===================== Top 5 Highscores ==================");
            // anropa lista highscores
            Console.ReadLine();
            MainMeny();

        }

        private static void EndProgram()
        {
            Console.WriteLine("Är du säker att du vill avsluta? (Ja/Nej");
            string endanswer = Console.ReadLine();
            Console.ReadKey();

            if (endanswer == "1")
            {

                Environment.Exit(0);
            }
            else
            {
                MainMeny();
            }
        }

    }

    }


