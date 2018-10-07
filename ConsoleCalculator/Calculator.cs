using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using static System.Double;

namespace ConsoleCalculator
{
    public class Add : ICalculate
    {
        public decimal Calculate(decimal x, decimal y)
        {
            return x + y;
        }
    }
    
    public class Sub : ICalculate
    {
        public decimal Calculate(decimal x, decimal y)
        {
            return x - y;
        }
    }
    
    public class Mul : ICalculate
    {
        public decimal Calculate(decimal x, decimal y)
        {
            return x * y;
        }
    }
    
    public class Calculator
    {
        private IPrinter _printer;
        
        public Calculator(IPrinter printer)
        {
            _printer = printer;
        }
        
        private readonly ICalculate _calculateAdd = new Add();
        
        private readonly ICalculate _calculateSub = new Sub();
        
        private readonly ICalculate _calculateMul = new Mul();
        
        private void AddValueToCell(decimal val)
        {
            if (val == 0m) return;
            foreach (var cell in Register.Instance.MyRegister)
            {
                if (cell.Id == Register.Instance.CurrentId)
                {
                    cell.Val = val;
                }
            }
        }
        
        private decimal AddNewCellOrCalculateNewValue(ExpressionHandler.ExpressionType type, 
            decimal leftNum, decimal rightNum, string leftOperand)
        {
            var leftNumIsInList = leftNum != 0m;
            switch (type)
            {
                case ExpressionHandler.ExpressionType.AddExpression when leftNumIsInList:
                    return _calculateAdd.Calculate(leftNum, rightNum);
                case ExpressionHandler.ExpressionType.AddExpression:
                    Register.Instance.MyRegister.Add(new Cell(leftOperand, rightNum));
                    break;
                case ExpressionHandler.ExpressionType.SubExpression when leftNumIsInList:
                    return _calculateSub.Calculate(leftNum, rightNum);
                case ExpressionHandler.ExpressionType.SubExpression:
                    Register.Instance.MyRegister.Add(new Cell(leftOperand, -rightNum));
                    break;
                case ExpressionHandler.ExpressionType.MulExpression when leftNumIsInList:
                    return _calculateMul.Calculate(leftNum, rightNum);
                case ExpressionHandler.ExpressionType.MulExpression:
                    Register.Instance.MyRegister.Add(new Cell(leftOperand, 0));
                    break;
            }

            return 0m;
        }
        
        private decimal GetNumber(string operand)
        {
            foreach (var cell in Register.Instance.MyRegister)
            {
                if (cell.Id == operand)
                {
                    Register.Instance.CurrentId = cell.Id;
                    return cell.Val;
                }
            }

            return 0m;
        }
        
        private decimal GetCellVal(string leftOperand, decimal rightNum, string expression)
        {
            var type = ExpressionHandler.SetExpressionType(expression);
            var leftNum = GetNumber(leftOperand);
            return AddNewCellOrCalculateNewValue(type, leftNum, rightNum, leftOperand);
        }
        
        private void ParseStringAndHandleCellVal(string expression)
        {
            try
            {
                var leftOperand = expression.Split(" ")[0];
                var rightOperand = expression.Split(" ")[2];
                var isNumber = decimal.TryParse(rightOperand, out var rightNum);
                if (!isNumber)
                {
                    rightNum = GetNumber(rightOperand);
                }
                var val = GetCellVal(leftOperand, rightNum, expression);
                AddValueToCell(val);
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void PrintOrCalc(string expression)
        {
            var type = ExpressionHandler.SetExpressionType(expression);

            switch (type)
            {
                case ExpressionHandler.ExpressionType.PrintCell:
                    _printer.PrintCell(expression);
                    break;
                case ExpressionHandler.ExpressionType.PrintRegister:
                    _printer.PrintRegister();
                    break;
                default:
                    ParseStringAndHandleCellVal(expression);
                    break;
            }
        }

        public void ReadFile(string args)
        {
            var lines = File.ReadAllLines(args);
            foreach (var line in lines)
            {
                PrintOrCalc(line);
            }
        }

        public void ReadUserInput()
        {
            string expression;
            Console.WriteLine("Waiting for user input...");
            while ((expression = Console.ReadLine()?.ToLower()) != "quit")
            {
                PrintOrCalc(expression);
            }
        }
    }
}
