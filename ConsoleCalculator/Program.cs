using System;

namespace ConsoleCalculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a simple calculator mad by Jonathan Sanfer. \n");
            Calculator calculator = new Calculator();
            if (args.Length == 1)
            {
                calculator.ReadFile(args[0]);
            }
            if (args.Length == 0)
            {
                calculator.ReadUserInput();
            }
        }
    }
}