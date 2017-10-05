using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Please enter the name of the file and press <return>");
            string fileName = System.Console.ReadLine();
            System.Console.WriteLine(new Rover(fileName).Run());
            System.Console.WriteLine("Press any key to exit");
            System.Console.ReadKey();
        }
    }
}
