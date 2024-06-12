namespace OrobaSoft.Shared.Models;
public class Invoice
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public DateTime InvoiceDate { get; set; }
    public ICollection<InvoiceItem> Items { get; set; }
}
