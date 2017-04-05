using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static System.Windows.Forms.Keys;

namespace Laboratory2
{
    class Connect4State : State
    {
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

        public override double ComputeHeuristicGrade()
        {
            throw new NotImplementedException();
        }

        public Connect4State(int width, int heigth, int deep) : base() //konstruktor inicjujący
        {
            Connect4State.width = width;
            Connect4State.heigth = heigth;
            Connect4State.howdeep = deep;

            table = new int[Heigth, Width];

            //table 0 builder
            for (int i = 0; i < Heigth; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Table[i,j] = 0;
                }
            }

            //id builder
            id = "";
            for (int i = 0; i < Width * Heigth; i++)
            {
                this.id += 0;
            }

            startmiddle = (Console.BufferWidth / 2) - (Width / 2);
        }

        public Connect4State(Connect4State parent, int[,] tab) : base(parent)
        {
            // reszta implementacji

            // ustawienie stringa identyfikujacego stan.
            //id builder
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < heigth; j++)
                {
                    id += tab[i, j];
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

        public static int GetChoise(int[,]tab)
        {
            //tab[2, 2] = 1;

            //tab[2, 6] = 2;
            //tab[2, 7] = 2;
            Print(tab);

            //kolor antywnego gracza
            Console.SetCursorPosition(startmiddle, Heigth+1);
            for (int i = 0; i < Width; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkBlue;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
            }

            string info1 = "<- -> - use left and right arrows to move";
            Console.SetCursorPosition(Console.BufferWidth/2-info1.Length/2, Heigth + 3);
            Console.WriteLine(info1);
            string info2 = "[ENTER] to confirm";
            Console.SetCursorPosition(Console.BufferWidth / 2 - info2.Length / 2, Heigth + 4);
            Console.WriteLine(info2);
            //Console.SetCursorPosition(startmiddle, Heigth + 3);


            int chose = 0;
            ConsoleKey choise_key;
            bool isChosen = true;
            while (isChosen)
            {
                for (int i = 0; i < Heigth; i++)
                {
                    Console.SetCursorPosition(startmiddle + chose, 1 + i);
                    if (tab[i, chose] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.WriteLine(" ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                }

                choise_key = Console.ReadKey().Key;
                Print(tab);
                switch (choise_key)
                {
                    case ConsoleKey.RightArrow:
                        if (chose < Width-1) chose++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (chose > 0) chose--;
                        break;
                    case ConsoleKey.Enter:
                        isChosen = false;
                        break;
                    default:
                        break;
                }
                

                Console.SetCursorPosition(0, 0);
            }



            

            //kolor antywnego gracza
            Console.SetCursorPosition(startmiddle, Heigth + 1);
            for (int i = 0; i < Width; i++)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write("O");
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Console.SetCursorPosition(startmiddle, Heigth + 5);
            return chose;
        }


    }
}
