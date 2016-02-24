using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Tetris.Game;
using Tetris.Snake;

namespace Tetris
{
    class Program
    {


        static void Main(string[] args)
        {
            //var tetrisScore = TetrisGame.Play();
            //var snakeScore = SnakeGame.Play();

            MainMeny();

        }

        public static void MenyVal1()
        {
            Console.WriteLine(" =================================");
            Console.WriteLine(" Välj ett av följande alternativ: ");
            Console.WriteLine(" =================================" + Environment.NewLine);
            Console.WriteLine(" 1. Spela ");
            Console.WriteLine(" 2. Se end credit ");




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
                    Console.WriteLine(" Hej " + playername + ", vad vill du spela? (Svara 1 eller 2)");
                    Console.WriteLine("1. Tetris" + Environment.NewLine + "2. Snake ");
                    string gamechoice = Console.ReadLine();

                    if (gamechoice == "1")
                    {
                        newPlayer.Score = TetrisGame.Play();
                        newPlayer.Game = Player.GameType.Tetris;
                    }
                    else if (gamechoice == "2")
                    {
                        var snakeScore = SnakeGame.Play();
                    }
                    else
                    {
                        Console.WriteLine(" Du matade in fel, försök igen!");
                        Console.ReadKey();

                    }

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
            Console.Clear();
            Console.WriteLine(" =======================================================");
            Console.WriteLine(" =========           Welcome Player!           =========");
            Console.WriteLine(" =======================================================");

            Console.WriteLine(" =========     Välj ett alternativen nedan:    =========");
            Console.WriteLine(" =======================================================");
            Console.WriteLine(" ");
            Console.WriteLine(" 1. Logga in och spela   2. Visa Highscore    3. Avsluta ");
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
                default: 
                    Console.Clear();
                    Console.WriteLine(" ===================================");
                    Console.WriteLine(" Du angav fel menyval, försök igen!");
                    Console.WriteLine(" ===================================");
                    Console.ReadKey();

                    MainMeny();
                    break;

            }
        }

        private static void HighScore()
        {
            Console.Clear();
            Console.WriteLine("===================== Top 5 Highscores ==================");
            // StreamReader infil = new StreamReader("scores.csv");
            // while (true)
            // {
            //      string line = infil.ReadLine();
            //      if (line == null) break;
            //      Console.WriteLine(line);
            //  }
            //  infil.Close();
            //  Console.ReadLine();


        }

        private static void EndProgram()
        {
            Console.Clear();
            Console.WriteLine(" Är du säker att du vill avsluta? \n 1. Ja\n 2. Nej ");
            var programEnd = Console.ReadLine();
            while (programEnd != "1")
            {
                MainMeny();
                Console.ReadKey();
            }
            Environment.Exit(1);

        }

    }
}


