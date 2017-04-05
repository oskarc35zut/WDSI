//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Laboratory2
{
    static class PrintOn
    {
        static int[,] buffor;



        public static void Print(int[,] tab)
        {
            buffor = new int[Connect4State.Size, Connect4State.Size];
            Array.Copy(tab, buffor, buffor.Length);

            #region BackgroundColor
            Console.BackgroundColor = ConsoleColor.DarkRed;
            for (int i = 0; i < Connect4State.Size; i++)
            {
                for (int j = 0; j < Console.BufferWidth; j++)
                {
                    Console.Write(" ");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(0, 0);
            #endregion //BackgroundColor

            for (int i = 0; i < Connect4State.Size; i++)
            {
                for (int j = 0; j < Connect4State.Size; j++)
                {

                }
            }


            Console.SetCursorPosition(0, Connect4State.Size);
        }
    }
}