using System;
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

            var answ = MenuHelper.Ask("hej");
            var result = playerDB.SearchPlayers(answ);
            
            MainMeny();

        }

        public static void MenyVal1(PlayerScores playerDB)
        {
            Console.Clear();
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
                        playerDB.AddPlayer(newPlayer);
                    }
                    else if (gamechoice == "2")
                    {
                        newPlayer.Score = SnakeGame.Play();
                        newPlayer.Game = Player.GameType.Snake;
                        playerDB.AddPlayer(newPlayer);


                    }
                    else
                    {
                        Console.WriteLine(" Du matade in fel, försök igen!");

                        Console.ReadKey();
                        MenyVal1(playerDB);
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
                default:
                    Console.Clear();
                    Console.WriteLine(" ===================================");
                    Console.WriteLine(" Du angav fel menyval, försök igen!");
                    Console.WriteLine(" ===================================");
                    Console.ReadKey();

                    MenyVal1(playerDB);
                    break;





            }
        }

        private static void MainMeny()
        {
            var playerDB = new PlayerScores();
           
            var running = true;
            while (running)
            {

                Console.Clear();
                Console.WriteLine(" =======================================================");
                Console.WriteLine(" =========           Welcome Player!           =========");
                Console.WriteLine(" =======================================================");

                Console.WriteLine(" =========     Välj ett alternativen nedan:    =========");
                Console.WriteLine(" =======================================================");
                Console.WriteLine(" ");
                Console.WriteLine(" 1. Spela   2. Visa Highscore    3. Avsluta ");
                var menyVal = int.Parse(Console.ReadLine());

                switch (menyVal)
                {
                    case 1:
                        Console.Clear();
                        MenyVal1(playerDB);
                        break;

                    case 2:
                        HighScore(playerDB.GetAllPlayerScores());
                        Console.ReadKey();

                        break;

                    case 3:
                        running = false;
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

        }

        private static void HighScore(Player[] PlayerScores)
        {
            Console.Clear();
            Console.WriteLine("===================== Top 5 Highscores ==================");
            for (int i = 0; i < PlayerScores.Length; i++)
            {
                Console.WriteLine("{0}. {1} : {2} : {3}", i + 1, PlayerScores[i].PlayerName, PlayerScores[i].Score, PlayerScores[i].Game);
            }

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


