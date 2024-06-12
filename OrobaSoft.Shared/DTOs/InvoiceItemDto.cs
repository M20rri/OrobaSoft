namespace OrobaSoft.Shared.DTOs;
public class InvoiceItemDto
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}
