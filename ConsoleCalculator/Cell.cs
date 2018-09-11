namespace ConsoleCalculator
{
    public class Cell
    {
        public Cell(string variable, double number)
        {
            Id = variable;
            Val = number;
        }

        public string Id { get; set; }
        public double Val { get; set; }
    }
}
