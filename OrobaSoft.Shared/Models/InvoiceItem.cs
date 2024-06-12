namespace OrobaSoft.Shared.Models;
public class InvoiceItem
{
    public int Id { get; set; }
    public string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
}
