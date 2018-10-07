using System.Text.RegularExpressions;

namespace ConsoleCalculator
{
    public static class ExpressionHandler
    {
        private const string AddPattern = @"(?i)^[a-zåäö]+[a-zåäö0-9]* add [a-zåäö0-9]+$";
        private const string SubPattern = @"(?i)^[a-zåäö]+[a-zåäö0-9]* sub [a-zåäö0-9]+$";
        private const string MulPattern = @"(?i)^[a-zåäö]+[a-zåäö0-9]* mul [a-zåäö0-9]+$";

        private const string PrintPattern = @"(?i)^print [a-zåäö]+[a-zåäö0-9]*$";

        public enum ExpressionType
        {
            AddExpression,
            SubExpression,
            MulExpression,
            PrintCell,
            PrintRegister,
            None
        };

        public static ExpressionType SetExpressionType(string exp)
        {
            var add = new Regex(AddPattern);
            var sub = new Regex(SubPattern);
            var mul = new Regex(MulPattern);

            var printCell = new Regex(PrintPattern);
            var printAll = new Regex(@"(?i)^print$");
            
            if (add.IsMatch(exp))
            {
                return ExpressionType.AddExpression;
            }
            if (sub.IsMatch(exp))
            {
                return ExpressionType.SubExpression;
            }
            if (mul.IsMatch(exp))
            {
                return ExpressionType.MulExpression;
            }
            if (printCell.IsMatch(exp))
            {
                return ExpressionType.PrintCell;
            }
            return printAll.IsMatch(exp) ? ExpressionType.PrintRegister : ExpressionType.None;
        }
    }
}
