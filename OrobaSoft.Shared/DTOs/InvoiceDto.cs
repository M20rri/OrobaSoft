namespace OrobaSoft.Shared.DTOs;
public class InvoiceDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; }
    public DateTime InvoiceDate { get; set; }
    public List<InvoiceItemDto> Items { get; set; }
}
