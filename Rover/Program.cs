using System;

namespace Rover
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter the name of the file and press <return>");
            string fileName = Console.ReadLine();
            Console.WriteLine(new Model.Rover(fileName).Run());
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
