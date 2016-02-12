using System;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" test");
            var exit = false;
            while (!exit)
            {
                
            var Menyval = MainMeny();

            switch (Menyval)
            {
                case 1:

                    Console.WriteLine(" För att spela krävs en inloggning");
                    Console.WriteLine(" Välj ett av följande alternativ: ");
                    Console.WriteLine(" 1. Logga in ");
                    Console.WriteLine(" 2. Skapa ny användare "+ Environment.NewLine);
                    

                    var i = Convert.ToInt32(Console.ReadLine());

                    if (i == 1)
                    {
                        Console.WriteLine(" Ange dina användaruppgifter! ");
                        Console.Write(" Ange användarnamn:      ");
                        string användarnamn = Console.ReadLine();
                        Console.Write(" Ange lösenord:          ");
                        string lösenord = Console.ReadLine();
                        var loggedInUsernam = "";
                        do
                        {
                            loggedInUsernam = LoggaIn(användarnamn, lösenord);
                        } while (!string.IsNullOrEmpty(loggedInUsernam));
                    }
                    else
                    {
                        Console.WriteLine(" Ange ett nytt användarnamn och lösenord " + Environment.NewLine);

                        Console.Write(" Ange nytt användarnamn:       ");
                        string nyttanvändarnamn = Console.ReadLine();
                        Console.Write(" Ange nytt lösenord            ");
                        string nyttlösenord = Console.ReadLine();
                        


                        Console.WriteLine(" ===================================");
                        Console.WriteLine(" ==========    Grattis!   ==========");
                        Console.WriteLine(" =====  Ditt konto har skapats =====");
                        Console.WriteLine(" ===================================");
                        Console.ReadKey();
                        Console.Clear();
                     
                    }

                    break;

                case 2:

                    break;
            }
            }

        }

        public static int MainMeny()
        {
            Console.WriteLine(" =======================================================");
            Console.WriteLine(" =========           Welcome Player!           =========");
            Console.WriteLine(" =======================================================");

            Console.WriteLine(" =========     Välj ett alternativen nedan:    =========");
            Console.WriteLine(" =======================================================");
            Console.WriteLine(" ");
            Console.WriteLine(" 1. Logga in och spela   2. Visa Highscore    3. Avsluta ");
            var returValue = int.Parse(Console.ReadLine());
            Console.Clear();
            return returValue;
        }

        public static string LoggaIn(string Username, string Password)
        {
            var username = "martin";
            var password = " olsson";

            return null;
        }
    }
}
