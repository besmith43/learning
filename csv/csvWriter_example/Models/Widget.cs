namespace csvWriter_example.Models;

public class Widget
{
	public string Name { get; set; }
	public DateOnly CreatedDate { get; set; }
	public string Description { get; set; }
	public string SerialNumber { get; set; }
	public DateOnly? ExpirationDate { get; set; }
	public decimal Price { get; set; }
}