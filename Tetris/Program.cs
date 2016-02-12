using System;
using System.Diagnostics;
using Tetris.Game;

namespace Tetris
{
    class Program
    {
        private static string _newLine;

        static void Main(string[] args)
        {
            MainMeny();


            var MenyVal = int.Parse(Console.ReadLine());

            switch (MenyVal)
            {
                case 1:
                    Console.Clear();
                    MenyVal1();
                    break;

                case 2:
                    HighScore();
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
                    // starta spel
                    break;

                case 2:
                    Console.WriteLine(" ============================");
                    Console.WriteLine("         END CREDIT          ");
                    Console.WriteLine(" ============================" + Environment.NewLine);
                    _newLine = Environment.NewLine;
                    Console.WriteLine(" Felix Svensson - Gamedesigner" + Environment.NewLine);
                    Console.WriteLine(" Martin Olsson - Menymaker");
                    Console.ReadLine();
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
            // anropa highscores
            Console.ReadKey();
            MainMeny();

        }

    }

    }


