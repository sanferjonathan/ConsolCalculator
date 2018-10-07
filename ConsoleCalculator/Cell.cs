namespace ConsoleCalculator
{
    public class Cell
    {
        public Cell(string variable, decimal number)
        {
            Id = variable;
            Val = number;
        }

        public string Id { get; }
        public decimal Val { get; set; }
    }
}
