namespace TestWebApiApplication.Models
{
    public class Expense
    {
        public long Id { get; set; }
        public string? Title { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

    }
}
