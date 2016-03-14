using System;

namespace Tetris
{
    public class EndCredit
    {
        public static void Start()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WindowLeft = Console.WindowTop = 0;
            //Console.WindowHeight = Console.BufferHeight = Console.LargestWindowHeight;
            //Console.WindowWidth = Console.BufferWidth = Console.LargestWindowWidth;

            Console.CursorVisible = false;
            int width, height;
            int[] y;
            int[] l;
            Initialize(out width, out height, out y, out l);
            int ms;
            ConsoleKeyInfo keyInfo;
            do
            {
            while (!Console.KeyAvailable)
            {
                DateTime t1 = DateTime.Now;
                MatrixStep(width, height, y, l);
                ms = 10 - (int)((TimeSpan)(DateTime.Now - t1)).TotalMilliseconds;
                if (ms > 0)
                    System.Threading.Thread.Sleep(ms);
                if (Console.KeyAvailable)
                    if (Console.ReadKey().Key == ConsoleKey.F5)
                        Initialize(out width, out height, out y, out l);
            }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        static bool thistime = false;

        private static void MatrixStep(int width, int height, int[] y, int[] l)
        {
            int x;
            thistime = !thistime;
            for (x = 0; x < width; ++x)
            {
               
                if (x % 11 == 10)
                {
                    if (!thistime)
                        continue;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.SetCursorPosition(x, inBoxY(y[x] - 2 - (l[x] / 40 * 2), height));
                        Console.Write(R);
                        Console.ForegroundColor = ConsoleColor.Green;
                    
                    
                }
               
                    Console.SetCursorPosition(x, y[x]);
                    Console.Write(R);
                    y[x] = inBoxY(y[x] + 1, height);

                    Console.SetCursorPosition(x, inBoxY(y[x] - l[x], height));
                    Console.Write(' ');
                    Console.ForegroundColor = ConsoleColor.White;

                Console.SetCursorPosition(width/2 - 10,height/2-2);
                Console.WriteLine(" Felix Svensson - Gamedesigner");
                Console.SetCursorPosition(width / 2 - 10, height / 2 - 1);
                Console.WriteLine(" Martin Olsson - Menymaker");
                Console.SetCursorPosition(width / 2 - 10, height / 2 );
                Console.WriteLine(" Adam Strömberg - Textdesigner");
                
            }
        }

        private static void Initialize(out int width, out int height, out int[] y, out int[] l)
        {
            int h1;
            int h2 = (h1 = (height = Console.WindowHeight) / 2) / 2;
            width = Console.WindowWidth - 1;
            y = new int[width];
            l = new int[width];
            int x;
            Console.Clear();
            for (x = 0; x < width; ++x)
            {
                y[x] = r.Next(height);
                l[x] = r.Next(h2 * ((x % 11 != 10) ? 2 : 1), h1 * ((x % 11 != 10) ? 2 : 1));
            }
        }

        static Random r = new Random();
        static char R
        {
            get
            {
                int t = r.Next(10);
                if (t <= 2)
                    return (char)('0' + r.Next(10));
                else if (t <= 4)
                    return (char)('a' + r.Next(27));
                else if (t <= 6)
                    return (char)('A' + r.Next(27));
                else
                    return (char)(r.Next(32, 255));
            }
        }

        public static int inBoxY(int n, int height)
        {
            n = n % height;
            if (n < 0)
                return n + height;
            else
                return n;
        }
    }
}
