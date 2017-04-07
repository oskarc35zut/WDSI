using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Laboratory2 {
    class Program {
        static void Main(string[] args) {
            Console.SetWindowPosition(0, 0);
            Console.CursorVisible = false;
            Console.Write(" Connect4");

            int width = 10;
            int heigth = 5;
            int deep = 5;
            Connect4State.Init(width, heigth, deep);

            //Connect4State parrent = new Connect4State();
            //Connect4State.GetChoise(parrent.Table);
            //Console.WriteLine("\n\n\nWybrano" + Connect4State.GetChoise(parrent.Table));
            
            //Console.Read();


            int[,] table = new int[Connect4State.Heigth, Connect4State.Width];

            //table 0 builder
            for (int i = 0; i < Connect4State.Heigth; i++)
            {
                for (int j = 0; j < Connect4State.Width; j++)
                {
                    table[i, j] = 0;
                }
            }

            int who = 2;
            while (true)
            {
                table = Connect4State.Move(table, Connect4State.GetChoise(table, who % 2 == 0 ? 1 : 2), who % 2 == 0 ? 1 : 2);
                if (Connect4State.isWin(who % 2 == 0 ? 1 : 2, table)) break;
                who++;

                //table = Connect4State.Move(table, Connect4State.GetChoise(table, who % 2 == 0 ? 1 : 2), who % 2 == 0 ? 1 : 2);

                table = Connect4State.ComputerChoiceTable(table, who % 2 == 0 ? 1 : 2);
                if (Connect4State.isWin(who % 2 == 0 ? 1 : 2, table)) break;
                who++;
            }

            Connect4State.Print(table);
            if (who%2 == 0)
            {
                Console.WriteLine("\n\n\n\n\n\nGratulacje, wygrales!!!");
                
            }
            else
            {
                Console.WriteLine("\n\n\n\n\n\nPrzegrana...");
            }

            Console.Read();
        }
    }
}
