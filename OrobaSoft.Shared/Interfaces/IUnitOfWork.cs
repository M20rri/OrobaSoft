using OrobaSoft.Shared.Interfaces;
using OrobaSoft.Shared.Models;

namespace OrobaSoft.API.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IRepository<Invoice> Invoices { get; }
    IRepository<InvoiceItem> InvoiceItems { get; }
    Task<int> Complete();
}
