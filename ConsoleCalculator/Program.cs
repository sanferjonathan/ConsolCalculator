using System;

namespace ConsoleCalculator
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("This is a simple calculator mad by Jonathan Sanfer. \n");
            var printer = new Printer();
            var calculator = new Calculator(printer);
            if (args.Length > 0)
            {
                calculator.ReadFile(args[0]);
            }
            else
            {
                calculator.ReadUserInput();
            }
        }
    }
}