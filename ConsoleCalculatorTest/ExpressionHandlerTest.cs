using ConsoleCalculator;
using Xunit;

namespace ConsoleCalculatorTest
{
    public class ExpressionHandlerTest
    {
        [Theory]
        [InlineData(5.0, 5.0, "add")]
        [InlineData(5.0, 5.0, "sub")]
        [InlineData(5.0, 5.0, "mul")]
        [InlineData(5.0, 5.0, "sub")]
        public void Test1(double var, double num, string op)
        {
            
        }
    }
}