//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Laboratory2
{
    class Connect4Search : AlphaBetaSearcher
    {
        public Connect4Search(IState startState, bool isMaximizingPlayerFirst, int
        maximumDepth) : base(startState, isMaximizingPlayerFirst, maximumDepth){}

        protected override void buildChildren(IState parent)
        {
            Connect4State state = (Connect4State)parent;

            //int[,] tab = state.Table;
            int Heigth = Connect4State.Heigth;
            int Width = Connect4State.Width;

            //kogo dzieci szukamy
            int who = 2;
            int[,] Move = new int[Heigth,Width];
            int[,] array_tmp = new int[Heigth, Width];
            Array.Copy(state.Table, array_tmp, array_tmp.Length);


            for (int i = 0; i < Width; i++)
            {
                Array.Copy(state.Table, array_tmp, array_tmp.Length);
                for (int j = Heigth-1; j >= 0; j--)
                {
                    if (array_tmp[j,i] == 0)
                    {
                        
                        Move = Connect4State.Move(array_tmp, i, who);
                        Connect4State child = new Connect4State(state, Move);
                        parent.Children.Add(child);
                        break;
                    }
                }
            }


            Console.Read();


            //for (int i = 0; i < Heigth; i++)
            //{
            //    for (int j = 0; j < Width; j++)
            //    {
            //        for (int choise = -1; choise <= 1; choise++)
            //        {
            //            Move = Connect4State.Move(tab, j + choise, who);
            //            if (tab[i, j] == who && Move != tab)
            //            {
            //                Connect4State child = new Connect4State(state, Move);
            //                parent.Children.Add(child);
            //            }
            //        }
            //    }
            //}



        }
        
    }
}