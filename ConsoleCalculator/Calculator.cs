using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleCalculator
{
    public class Calculator
    {
        private List<Cell> register = new List<Cell>();

        private ExpressionHandler myHandler = new ExpressionHandler();

        private bool isInList = false;
        private string leftVar, rightVar, op, expression = "";
        private double number;

        private double Calculate(double var, double num, string op)
        {
            if (op.ToLower() == "add")
            {
                return var + num;
            }
            if (op.ToLower() == "subtract")
            {
                return var - num;
            }
            if (op.ToLower() == "multiply")
            {
                return var * num;
            }
            return 0;
        }

        private void AddNewCell(string leftVar, string op, double number)
        {
            if (op.ToLower() == "add")
            {
                register.Add(new Cell(leftVar, number));
            }
            if (op.ToLower() == "subtract")
            {
                var num = 0;
                register.Add(new Cell(leftVar, num - number));
            }
            if (op.ToLower() == "multiply")
            {
                register.Add(new Cell(leftVar, 0));
            }
        }

        private void ComputeVarAndNum(string expression)
        {
            leftVar = expression.Split(" ")[0];
            op = expression.Split(" ")[1];
            number = Double.Parse(expression.Split(" ")[2]);

            foreach (Cell cell in register)
            {
                if (cell.Id == leftVar)
                {
                    cell.Val = Calculate(cell.Val, number, op);
                    isInList = true;
                }
            }
            if (!isInList)
            {
                AddNewCell(leftVar, op, number);
            }
            isInList = false;
        }

        private void ComputeVarAndVar(string expression)
        {
            leftVar = expression.Split(" ")[0];
            op = expression.Split(" ")[1];
            rightVar = expression.Split(" ")[2];
            number = 0;

            foreach (Cell cell in register)
            {
                if (cell.Id == rightVar)
                {
                    number = cell.Val;
                }
            }

            foreach (Cell cell in register)
            {
                if (cell.Id == leftVar)
                {
                    cell.Val = Calculate(cell.Val, number, op);
                    isInList = true;
                }
            }

            if (!isInList)
            {
                AddNewCell(leftVar, op, number);
            }
            isInList = false;
        }

        private void PrintCell(string expression)
        {
            var id = expression.Split(" ")[1];
            foreach (Cell cell in register)
            {
                if (id == cell.Id)
                {
                    Console.WriteLine("{0} has value: {1}", cell.Id, cell.Val);
                }
            }
        }

        private void PrintRegister()
        {
            foreach (Cell cell in register)
            {
                Console.WriteLine("{0} has value: {1}", cell.Id, cell.Val);
            }
        }

        private void Run(string exp)
        {
            ExpressionHandler.ExpressionType type = myHandler.SetExpressionType(exp);

            switch (type)
            {
                case ExpressionHandler.ExpressionType.varAndNum:
                    ComputeVarAndNum(exp);
                    break;
                case ExpressionHandler.ExpressionType.varAndVar:
                    ComputeVarAndVar(exp);
                    break;
                case ExpressionHandler.ExpressionType.printCell:
                    PrintCell(exp);
                    break;
                case ExpressionHandler.ExpressionType.printRegister:
                    PrintRegister();
                    break;
                default:
                    Console.WriteLine("---Invalid expression---");
                    break;
            }
        }

        public void ReadFile(string args)
        {
            string[] lines = File.ReadAllLines(args);
            foreach (string line in lines)
            {
                Run(line);
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        public void ReadUserInput()
        {
            Console.WriteLine("Waiting for user input...");
            while (expression.ToLower() != "quit")
            {
                expression = Console.ReadLine();
                Run(expression);
            }
        }
    }
}
