using System.Text.RegularExpressions;

namespace ConsoleCalculator
{
    public class ExpressionHandler
    {
        private readonly string addPattern1 = @"(?i)^[a-zåäö]+[a-zåäö0-9]* add \d+$";
        private readonly string subtractPattern1 = @"(?i)^[a-zåäö]+[a-zåäö0-9]* subtract \d+$";
        private readonly string multiplyPattern1 = @"(?i)^[a-zåäö]+[a-zåäö0-9]* multiply \d+$";

        private readonly string addPattern2 = @"(?i)^[a-zåäö]+[a-zåäö0-9]* add [a-zåäö]+[a-zåäö0-9]$";
        private readonly string subtractPattern2 = @"(?i)^[a-zåäö]+[a-zåäö0-9]* subtract [a-zåäö]+[a-zåäö0-9]*$";
        private readonly string multiplyPattern2 = @"(?i)^[a-zåäö]+[a-zåäö0-9]* multiply [a-zåäö]+[a-zåäö0-9]*$";

        private readonly string printPattern = @"(?i)^print [a-zåäö]+[a-zåäö0-9]*$";

        public enum ExpressionType
        {
            varAndNum,
            varAndVar,
            printCell,
            printRegister,
            none
        };

        public ExpressionType SetExpressionType(string exp)
        {
            Regex rgxAdd1 = new Regex(addPattern1);
            Regex rgxSubtract1 = new Regex(subtractPattern1);
            Regex rgxMultiply1 = new Regex(multiplyPattern1);

            Regex rgxAdd2 = new Regex(addPattern2);
            Regex rgxSubtract2 = new Regex(subtractPattern2);
            Regex rgxMultiply2 = new Regex(multiplyPattern2);

            Regex rgxPrint1 = new Regex(printPattern);
            Regex rgxPrint2 = new Regex(@"(?i)^print$");

            if (rgxAdd1.IsMatch(exp) || rgxSubtract1.IsMatch(exp) || rgxMultiply1.IsMatch(exp))
            {
                return ExpressionType.varAndNum;
            }
            if (rgxAdd2.IsMatch(exp) || rgxSubtract2.IsMatch(exp) || rgxMultiply2.IsMatch(exp))
            {
                return ExpressionType.varAndVar;
            }
            if (rgxPrint1.IsMatch(exp))
            {
                return ExpressionType.printCell;
            }
            if (rgxPrint2.IsMatch(exp))
            {
                return ExpressionType.printRegister;
            }
            return ExpressionType.none;
        }
    }
}
