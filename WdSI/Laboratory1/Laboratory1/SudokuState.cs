using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory1
{
    public class SudokuState : State

    {
        public const int SMALL_GRID_SIZE = 3;

        public const int GRID_SIZE = SMALL_GRID_SIZE * SMALL_GRID_SIZE;

        private string id;
        
        private int[,] table;

        public int[,] Table{
            get{ return this.table; }
            set {this.table = value; }
        }
        public override string ID
        {
            get { return this.id; }
        }

        public override double ComputeHeuristicGrade()
        {
            throw new NotImplementedException();
        }
        public void Print()
        {
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine("");
                for (int j = 0; j < 9; j++)
                {
                    if(table[i,j] == 0) 
                    {
                        Console.Write(" ");
                    }
                    else{
                    Console.Write(table[i,j]);
                    if (j == 2 || j == 5 || j == 8)
                    {
                        Console.Write(" || ");
                    }
                    }
                }
                if (i == 2 || i == 5 || i == 8)
                {
                    Console.WriteLine("----------------------------");
                }
            }

        }

        public SudokuState(string sudokuPattern) : base() {
            if (sudokuPattern.Length != GRID_SIZE * GRID_SIZE)
            {
                throw new ArgumentException("SudokuString posiada niewlasciwa dlugosc.");
            }
            this.id = sudokuPattern;
            this.table = new int[GRID_SIZE, GRID_SIZE];

            for (int i = 0; i < GRID_SIZE; ++i)
            {
                for (int j = 0; j < GRID_SIZE; ++j)
                {
                    this.table[i, j] = sudokuPattern[i * GRID_SIZE + j] - 48;
                }
            }
            this.h = ComputeHeuristicGrade();
        }
        public SudokuState(SudokuState parent, int newValue, int x , int y) : base(parent) {
            this.table = new int[GRID_SIZE, GRID_SIZE];
            Array.Copy(parent.table, this.table, this.table.Length);
            this.table[x, y] = newValue;
            StringBuilder builder = new StringBuilder(parent.id);
            builder[x * GRID_SIZE + y] = (char)(newValue + 48);
            this.id = builder.ToString();
            this.h = ComputeHeuristicGrade();
         }
    }
}
