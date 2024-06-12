using OrobaSoft.Shared.DTOs;

namespace OrobaSoft.UI.Services;
public class InvoiceService
{
    private readonly HttpClient _httpClient;

    public InvoiceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<InvoiceDto>> GetInvoices()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<InvoiceDto>>("api/invoices");
    }

    public async Task<InvoiceDto> GetInvoiceById(int id)
    {
        return await _httpClient.GetFromJsonAsync<InvoiceDto>($"api/invoices/{id}");
    }

    public async Task CreateInvoice(InvoiceDto invoiceDto)
    {
        await _httpClient.PostAsJsonAsync("api/invoices", invoiceDto);
    }

    public async Task DeleteInvoice(int id)
    {
        await _httpClient.DeleteAsync($"api/invoices/{id}");
    }
}