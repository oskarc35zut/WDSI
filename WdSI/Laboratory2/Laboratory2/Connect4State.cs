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
            int H(int who)
            {
                int [,]tab = new int[Heigth, Width];
                Array.Copy(tab, this.Table, tab.Length);
            
                bool?[] i_v = new bool?[] { true, false, null, null, true, true, false, false };
                bool?[] j_v = new bool?[] { null, null, true, false, true, false, true, false };

                int counter = 0;

                int n = 0;
                int m = 0;

                for (int i = 0; i < Heigth; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (tab[i,j] == who)
                        {
                            for (int k = 1; k < 3; k++)
                            {
                                for (int v = 0; v < i_v.Length; v++)
                                {
                                    if (v%2 == 0) Array.Copy(tab, this.Table, tab.Length);

                                    if (i_v[v] == true) n = k;
                                    if (i_v[v] == false) n = -k;
                                    if (i_v[v] == null) n = 0;

                                    if (j_v[v] == true) m = k;
                                    if (j_v[v] == false) m = -k;
                                    if (j_v[v] == null) m = 0;

                                    if ((i+n) > 0 && (i + n) < Heigth && (j + n) > 0 && (j + n) < Width && tab[i+n,j+m] == who)
                                    {
                                        counter++;
                                        tab[i + n, j + m] = 0;
                                    }
                                }
                            }
                        }
                    }
                }

                return counter;
            }





            return (double)(H(2));
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
            
            for (int i = 0; i < heigth; i++)
            {
                for (int j = 0; j < width; j++)
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
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
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

            bool isMaximizingPlayerFirst = who%2 != 0 ? true : false;//chuj wi czy to dobra kolejnosc


            Connect4State startState = new Connect4State(tab);
            Connect4Search searcher = new Connect4Search(startState, isMaximizingPlayerFirst, Howdeep);

            searcher.DoSearch();

            

             return tab;
        }

        public static bool isWin(int who, int[,]tab)
        {
            int counter = 0;
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
                            return true;
                        }

                        counter += counterX > 1 ? counterX : 0;
                        counter += counterY > 1 ? counterY : 0;
                        counter += counterF > 1 ? counterF : 0;
                        counter += counterB > 1 ? counterB : 0;

                        //counterX = 0; counterY = 0; counterB = 0; counterF = 0;
                        //int k = 1;

                        //if (j + k >= 0 && j + k < Width && tab[i, j + k] == who)
                        //    counterX++;

                        //if (i + k >= 0 && i + k < Heigth && tab[i + k, j] == who)
                        //    counterY++;

                        //if (i + k >= 0 && i + k < Heigth && j + k >= 0 && j + k < Width && tab[i + k, j + k] == who)
                        //    counterB++;

                        //if (i + k >= 0 && i + k < Heigth && j - k >= 0 && j - k < Width && tab[i + k, j - k] == who)
                        //    counterF++;

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
