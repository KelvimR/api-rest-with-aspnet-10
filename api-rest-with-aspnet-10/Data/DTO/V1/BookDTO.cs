namespace api_rest_with_aspnet_10.Data.DTO.V1;

public class BookDTO
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public DateTime Launch_Date { get; set; }
}
