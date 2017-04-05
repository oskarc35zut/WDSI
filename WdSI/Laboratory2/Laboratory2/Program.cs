using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laboratory2 {
    class Program {
        static void Main(string[] args) {

            int size = 10;
            int deep = 5;

            Connect4State parrent = new Connect4State(size, deep);

            PrintOn.Print(parrent.Table);

            Console.WriteLine("Hello World");
            Console.Read();
        }
    }
}
