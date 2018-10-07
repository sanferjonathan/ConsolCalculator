using System;
using Xunit;
using ConsoleCalculator;

namespace ConsoleCalculatorTest
{
    public class CalculatorTest
    {
        private readonly Calculator _calculator;
        private readonly Printer _printer;
        
        private CalculatorTest()
        {
            _printer = new Printer();
            _calculator = new Calculator(_printer);
        }
        
        [Fact]
        public void Test1()
        {
            
        }
    }
}