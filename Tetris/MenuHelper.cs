﻿using System;
using System.Collections.Generic;

namespace Tetris
{
    public static class MenuHelper
    {
        public static string Ask(string question)
        {
            Console.Out.WriteLine(question);
            return Console.ReadLine();
        }

        public static int AskFromAlternative(string message, List<string> alternatives)
        {
            Console.CursorVisible = false;
            var returnIndex = 0;

            if (alternatives == null)
                throw new ArgumentNullException();
            if (alternatives.Count == 0)
                throw new ArgumentNullException();
            Console.Clear();
            var longestQuestion = message.Length;
            Console.Out.WriteLine();
            Console.Out.WriteLine("X   {0}", message);
            Console.Out.WriteLine();

            var menuPos = Console.CursorTop;
            for (int j = 0; j < alternatives.Count; j++)
            {
                var alternative = alternatives[j].Trim();
                longestQuestion = alternative.Length > longestQuestion ? alternative.Length : longestQuestion;
                Console.Out.WriteLine("X  {0} {1}", j + 1, alternative);
            }


            Console.Out.WriteLine(new string('X', longestQuestion + 7));
            WriteToPos(0, 0, new string('X', longestQuestion + 7));
            WriteToPos(0, 2, new string('X', longestQuestion + 7));

            for (int i = 0; i < alternatives.Count + menuPos; i++)
            {
                WriteToPos(longestQuestion + 6, i, "X");


            }
            WriteToPos(2, menuPos, ">", ConsoleColor.Green);
            ConsoleKeyInfo keyInfo;
            while ((keyInfo = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (returnIndex > 0)
                        {
                            WriteToPos(2, Console.CursorTop, " ");
                            returnIndex--;

                            WriteToPos(2, menuPos + returnIndex, ">", ConsoleColor.Green);
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (returnIndex < alternatives.Count - 1)
                        {
                            WriteToPos(2, Console.CursorTop, " ");
                            returnIndex++;
                            WriteToPos(2, menuPos + returnIndex, ">", ConsoleColor.Green);

                        }
                        break;
                }
            }
            Console.ResetColor();
            Console.Clear();
            Console.CursorVisible = true;

            return returnIndex;
        }


        private static void WriteToPos(int x, int y, string ouput, ConsoleColor color = ConsoleColor.White)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Out.Write(ouput);
        }
    }
}
