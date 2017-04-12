using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace Laboratory2 {
    class Program {
        static void Main(string[] args) {

            #region init

            

            Console.SetWindowPosition(0, 0);
            Console.Write(" Connect4");

            int who = 2;
            int width = 7;
            int heigth = 4;
            int deep = 3;

            DialogResult dialogResult_sudoku = MessageBox.Show("Komputer zaczyna? ( ͡° ͜ʖ ͡°)", "", MessageBoxButtons.YesNo);
            if (dialogResult_sudoku == DialogResult.Yes)
            {
                who++;
            }

            Console.Write("\nJak gleboko szukac: ");
            deep = Console.Read()-48;
            Connect4State.Init(width, heigth, deep);

            Console.Clear();
            Console.SetWindowPosition(0, 0);
            Console.Write(" Connect4");
            Console.CursorVisible = false;

            int[,] table = new int[Connect4State.Heigth, Connect4State.Width];


            

            //table 0 builder
            for (int i = 0; i < Connect4State.Heigth; i++)
            {
                for (int j = 0; j < Connect4State.Width; j++)
                {
                    table[i, j] = 0;
                }
            }
            #endregion //Init

            #region Gra

            
            while (true)
            {
                switch (who%2)
                {
                    case 0:
                        table = Connect4State.Move(table, Connect4State.GetChoise(table, who % 2 == 0 ? 1 : 2),
                                                    who % 2 == 0 ? 1 : 2);

                        break;

                    case 1:
                        Thread.Sleep(200);
                        table = Connect4State.ComputerChoiceTable(table, who % 2 == 0 ? 1 : 2);
                        //Thread.Sleep(300);
                        break;


                    default:
                        break;
                }

                if (Connect4State.isWin(who % 2 == 0 ? 1 : 2, table) || Connect4State.isFull(table)) break;

                who++;
            }

            #endregion //Gra

            #region Wynik rozgrywki
            
            Connect4State.Print(table);
            if (!Connect4State.isFull(table))
            { 
                if (who%2 == 0)
                {
                    Console.WriteLine("\n\n\n\n\n\nGratulacje, wygrales!!!");
                
                }
                else
                {
                    Console.WriteLine("\n\n\n\n\n\nPrzegrana...");
                }
            }
            else
            {
                Console.WriteLine("\n\n\n\n\n\nRemis!");
            }
            Thread.Sleep(5000);
            #endregion //Wynik rozgrywki
        }
    }
}
