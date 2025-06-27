namespace BackendApi.Models
{
    public class Quote
    {
        public int Id { get; set; }

        public required string Text { get; set; }
         public string? Author { get; set; }
    }
}
