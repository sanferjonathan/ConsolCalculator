using System;

namespace ConsoleCalculator
{
    public class Printer : IPrinter
    {
        public void PrintCell(string expression)
        {
            var id = expression.Split(" ")[1];
            foreach (var cell in Register.Instance.MyRegister)
            {
                if (id != cell.Id) continue;
                Console.WriteLine("{0} has value: {1}", cell.Id, cell.Val);
                break;
            }
        }

        public void PrintRegister()
        {
            foreach (var cell in Register.Instance.MyRegister)
            {
                Console.WriteLine("{0} has value: {1}", cell.Id, cell.Val);
            }
        }
    }
}