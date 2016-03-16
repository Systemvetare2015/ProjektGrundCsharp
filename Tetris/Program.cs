using System;
using System.Collections.Generic;
using Tetris.Game;
using Tetris.Snake;

namespace Tetris
{
    class Program
    {



        static void Main(string[] args)
        {
            

            MainMeny();
        }
        /// <summary>
        /// Metoden tar hand om alternativen inom menyval 1 (spela)
        /// kontrolleras med en switch case
        /// </summary>
        
        public static void MenyVal1(PlayerScores playerDB)
        {
            Console.Clear();
            Console.WriteLine(" =================================");
            Console.WriteLine(" Välj ett av följande alternativ: ");
            Console.WriteLine(" =================================" + Environment.NewLine);
            Console.WriteLine(" 1. Spela ");
            Console.WriteLine(" 2. Se end credit ");


            int menyVal1 = 0;


            while (menyVal1 < 1 || menyVal1 > 2)
            {
                if (!int.TryParse(Console.ReadLine(), out menyVal1))
                {
                    Console.Out.WriteLine("Du skrev fel, välj mellan siffrorna på menyvalet! ");
                    
                  
                }
            }


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
                    Console.Clear();
                    EndCredit.Start();
                    //Console.WriteLine(" ============================");
                    //Console.WriteLine("         END CREDIT          ");
                    //Console.WriteLine(" ============================" + Environment.NewLine);

                    //Console.WriteLine(" Felix Svensson - Gamedesigner" + Environment.NewLine);
                    //Console.WriteLine(" Martin Olsson - Menymaker" + Environment.NewLine);
                    //Console.WriteLine(" Adam Strömberg - Textdesigner");
                    //Console.ReadLine();
                    

                    


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
        /// <summary>
        /// Metoden tar hand om Menyvalen som programmet erbjuder
        /// Menyvalen hanteras med en switch case 
        /// </summary>
        private static void MainMeny()
        {
            //Instaniserar databasen för att ha samma conext genom hela applicationen.
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
                var menyVal = 0;
                while (menyVal < 1 || menyVal > 3)
                {
                    if (!int.TryParse(Console.ReadLine(), out menyVal))
                    {
                        Console.Out.WriteLine("Det är inget menyval, skriv med menysiffra");
                        
                    }
                }

                
                switch (menyVal)
                {
                    case 1:
                        Console.Clear();
                        MenyVal1(playerDB);
                        break;

                    case 2:
                        HighScore(playerDB);
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
        /// <summary>
        /// Metoden tar hand om valen inom menyval 2 (Highscores)
        /// </summary>
        /// <param name="playerDB"></param>
        private static void HighScore(PlayerScores playerDB)
        {

            var svar = MenuHelper.AskFromAlternative("Väj vad du vill göra",
                new List<string>() { "sök på namn", "Visa topplista" });

            switch (svar)
            {
                case 0:
                    Console.Clear();
                    var answ = MenuHelper.Ask("Skriv namn som du vill söka på");
                    var result = playerDB.SearchPlayers(answ);
                    for (int i = 0; i < result.Length; i++)
                    {
                        Console.WriteLine("{0}. {1} : {2} : {3}", i + 1, result[i].PlayerName, result[i].Score, result[i].Game);
                    }
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine("===================== Top 5 Highscores ==================");
                    var all = playerDB.GetAllPlayerScores();
                    for (int i = 0; i < all.Length; i++)
                    {
                        Console.WriteLine("{0}. {1} : {2} : {3}", i + 1, all[i].PlayerName, all[i].Score, all[i].Game);
                    }
                    break;

            }


        }
        /// <summary>
        /// Metoden tar hand om avslutning av programmet.
        /// </summary>
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


///PLayerdb gör att spara påäng hämta
/// Vi behöver inte bry oss om hur den gör det men bara det gör det. 
/// dvs från vår player db container kan man bara se vilka operationer som finns (Vilka funktioner som finns) Du behöver inte se vad som ligger under i comtainern
/// All vår "Affräslogik" ligger i conatainern dvs att om man lägger till en 6 spelare som kommer den jämnföras med de nuvarande och kolla om den kan ha en utmanade poäng