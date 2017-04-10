using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Windows.Forms.Keys;
using System.Threading;

namespace Laboratory2
{
    class Connect4State : State
    {
        #region Values

        static readonly double Infinity = double.PositiveInfinity;
        static readonly double NInfinity = double.NegativeInfinity;

        private static int width;

        static int startmiddle;

        public static int Width
        {
            get { return Connect4State.width; }
        }

        private static int heigth;

        public static int Heigth
        {
            get { return Connect4State.heigth; }
        }

        private static int howdeep;

        public static int Howdeep
        {
            get { return Connect4State.howdeep; }
        }

        int[,] table;

        public int[,] Table
        {
            get { return this.table;  }
            set { this.table = value; }
        }

        private string id;

        public override string ID
        {
            get { return this.id; }
        }
        #endregion //Values

        public override double ComputeHeuristicGrade()
        {
            double H(int who, int[,]tabH)
            {
                bool isOnce(int h, int w)
                {
                    int[,] tmp_tab = new int[Heigth, Width];
                    Array.Copy(tabH, tmp_tab, tmp_tab.Length);

                    bool?[] hV = new bool?[] { false, false, false, null, null, true, true, true };
                    bool?[] wV = new bool?[] { false, null, true, false, true, false, null, true };

                    int n = 0, m = 0;
                    
                    for (int i = 0; i < hV.Length; i++)
                    {
                        if (hV[i] ==  true)  n =  1;
                        if (hV[i] ==  null)  n =  0;
                        if (hV[i] == false)  n = -1;

                        if (wV[i] ==  true)  m =  1;
                        if (wV[i] ==  null)  m =  0;
                        if (wV[i] == false)  m = -1;

                        if ((h+n >= 0 && h+n < Heigth && w+m >= 0 && w+m <Width) && tmp_tab[h+n,w+m] == who)
                        {
                            return false;
                        }
                    }
                    return true;
                }

                //licznik calkowity
                double counter = 0;

                int[,] tmp_tabH = new int[Heigth, Width];
                Array.Copy(tabH, tmp_tabH, tmp_tabH.Length);

                for (int i = 0; i < Heigth; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (tmp_tabH[i, j] == who)
                        {
                            if (isOnce(i, j))
                            {
                                counter++;
                                tmp_tabH[i, j] = 0;
                            }
                        }
                    }
                }


                int[,] tmp2_tabH = new int[Heigth, Width];
                Array.Copy(tmp_tabH, tmp2_tabH, tmp2_tabH.Length);

                bool?[] HV = new bool?[] { null, true, true, true  };
                bool?[] WV = new bool?[] { true, null, true, false };

                int k = 0, l = 0;
                int licz;

                for (int v = 0; v < HV.Length; v++)
                {
                    Array.Copy(tmp_tabH, tmp2_tabH, tmp2_tabH.Length);

                    for (int i = 0; i < Heigth; i++)
                    {
                        for (int j = 0; j < Width; j++)
                        {
                            if (tmp2_tabH[i,j] == who)
                            {
                                licz = 0;
                                for (int p = 1; p < 4; p++)
                                {
                                    if (HV[v] == true) k = p;
                                    if (HV[v] == null) k = 0;
                                    if (HV[v] == false) k = p * (-1);

                                    if (WV[v] == true) l = p;
                                    if (WV[v] == null) l = 0;
                                    if (WV[v] == false) l = p * (-1);

                                    if ((i + k >= 0 && i + k < Heigth && j + l >= 0 && j + l < Width) && tmp2_tabH[i + k, j + l] == who)
                                    {
                                        licz++;
                                        tmp2_tabH[i + k, j + l] = 3;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }

                                if (licz == 3)
                                {
                                    return Infinity;
                                }
                                counter += Math.Pow(licz * 2, 2);

                            }
                        }
                    }

                    
                }


                return counter;
            }

            //nie mam pojecia co tu robie
            if (H(2, Table) == Infinity)
            {
                return Infinity;
            }

            if (H(1, Table) == Infinity)
            {
                return NInfinity;
            }

            return H(2, Table) - H(1, Table);
        }
        
        static public void Init(int width, int heigth, int deep)
        {
            Connect4State.width = width;
            Connect4State.heigth = heigth;
            Connect4State.howdeep = deep;
            startmiddle = (Console.BufferWidth / 2) - (Width / 2);
        }

        //konstruktor inicjujący
        public Connect4State(int[,] tab) : base() 
        {
            table = new int[Heigth, Width];
            Array.Copy(tab, this.table, tab.Length);

            // ustawienie stringa identyfikujacego stan.
            //id builder
            
            for (int i = 0; i < Heigth; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    id += "" + Table[i, j];
                }
            }
        }

        public Connect4State(Connect4State parent, int[,] tab) : base(parent)
        {
            table = new int[Heigth, Width];
            Array.Copy(tab, this.table, tab.Length);
            
            // ustawienie stringa identyfikujacego stan.
            //id builder
            for (int i = 0; i < Heigth; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    id += "" + Table[i, j];
                }
            }

            // ustawienie na ktorym poziomie w drzwie znajduje sie stan .
            this.depth = parent.depth + 0.5;

            // Bardzo wazne nie ustawiany na czubek drzewa z ktorego budujemy stany.Tylko na pierwsze pokolenie stanow potomnych
            if (parent.rootMove == null)
            {
                this.rootMove = this.id;
            }
            else {
                this.rootMove = parent.rootMove;
            }
        }

        public static void Print(int[,]tab)
        {
            


            for (int i = 0; i < Heigth+2; i++)
            {
                Console.SetCursorPosition(startmiddle-1, i);
                for (int j = 0; j < Width+2; j++)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    //Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.Write("\n");
            }


            int isDark = 0;
            for (int i = 0; i < Heigth; i++)
            {
                Console.SetCursorPosition(startmiddle, i+1);



                for (int j = 0; j < Width; j++)
                {
                    if (j == 0 && Width % 2 == 0) isDark++;
                    switch (tab[i,j])
                    {
                        case 0:
                            if(isDark%2 == 0)
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkGray;
                            }
                            
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 1:
                            Console.BackgroundColor = ConsoleColor.DarkBlue;
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 2:
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        case 3:
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;

                        case 4:
                            Console.BackgroundColor = ConsoleColor.Yellow;
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            break;
                        default:
                            break;
                    }
                isDark++;
                }
                Console.Write("\n");
            }
        }

        public static int GetChoise(int[,]tab, int who)
        {
            Print(tab);

            PlayerColorBar(who);

            string info1 = "<- -> - use left and right arrows to move";
            Console.SetCursorPosition(Console.BufferWidth/2-info1.Length/2, Heigth + 3);
            Console.WriteLine(info1);
            string info2 = "[ENTER] to confirm";
            Console.SetCursorPosition(Console.BufferWidth / 2 - info2.Length / 2, Heigth + 4);
            Console.WriteLine(info2);
            //Console.SetCursorPosition(startmiddle, Heigth + 3);


            int choice = 0;
            ConsoleKey choice_key;
            bool isChosen = true;
            while (isChosen)
            {
                for (int i = 0; i < Heigth; i++)
                {
                    Console.SetCursorPosition(startmiddle + choice, 1 + i);
                    if (tab[i, choice] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }

                choice_key = Console.ReadKey().Key;
                Print(tab);
                switch (choice_key)
                {
                    case ConsoleKey.RightArrow:
                        if (choice < Width-1) choice++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (choice > 0) choice--;
                        break;
                    case ConsoleKey.Enter:
                        if(tab[0,choice] ==0)
                        {
                            isChosen = false;
                        }
                        else
                        {
                            Console.SetCursorPosition(Console.BufferWidth / 2 - Width/2 + choice, 0);
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.Write("X");
                            Console.BackgroundColor = ConsoleColor.Black;

                            Thread.Sleep(150);
                            Console.SetCursorPosition(Console.BufferWidth / 2 - Width / 2 + choice, 0);
                            Console.Write(" ");
                        }
                        break;
                    default:
                        break;
                }
                

                Console.SetCursorPosition(0, 0);
            }



            

            
            Console.SetCursorPosition(startmiddle, Heigth + 5);
            return choice;
        }

        public static void PlayerColorBar(int who)
        {
            //kolor antywnego gracza
            Console.SetCursorPosition(startmiddle, Heigth + 1);
            for (int i = 0; i < Width; i++)
            {
                if (who % 2 != 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                }

                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public static int[,] ComputerChoiceTable(int[,] tab, int who)
        {
            Print(tab);
            PlayerColorBar(who);

            bool isMaximizingPlayerFirst = true;

            

            Connect4State startState = new Connect4State(tab);
            Connect4Search searcher = new Connect4Search(startState, isMaximizingPlayerFirst, Howdeep);

            searcher.DoSearch();

            

             return tab;
        }

        public static bool isWin(int who, int[,]tab)
        {
            int counterX, counterY, counterB, counterF;
            for (int i = 0; i < Heigth; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (tab[i, j] == who)
                    {
                        counterX = 0; counterY = 0; counterB = 0; counterF = 0;

                        for (int k = 1; k <= 3; k++){
                            
                            if (j+k >= 0 && j+k < Width && tab[i, j + k] == who)
                                 counterX++;

                            if (i + k >= 0 && i + k < Heigth && tab[i + k, j] == who)
                                counterY++;

                            if (i + k >= 0 && i + k < Heigth && j + k >= 0 && j + k < Width && tab[i + k, j + k] == who)
                                counterB++;

                            if (i + k >= 0 && i + k < Heigth && j - k >= 0 && j - k < Width && tab[i + k, j - k] == who)
                                counterF++;
                        }

                        if (counterX > 2 || counterY > 2 || counterB > 2 || counterF > 2){
                            //return true;
                        }



                    }
                }
            }
            


            return false;
        }

        public static int[,] Move(int[,] tab, int chose, int who)
        {
            for (int i = Heigth-1; i >= 0; i--)
            {
                if (chose >= 0 && chose < Width)
                {
                    if (tab[i, chose] == 0)
                    {
                        tab[i, chose] = who;
                        break;
                    }
                }
            }

            return tab;
        }
    }
}
