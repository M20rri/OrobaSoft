using OrobaSoft.API.Interfaces;
using OrobaSoft.Shared.Context;
using OrobaSoft.Shared.Interfaces;
using OrobaSoft.Shared.Models;

namespace OrobaSoft.Shared.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly InvoiceDbContext _context;

    public UnitOfWork(InvoiceDbContext context)
    {
        _context = context;
        Invoices = new Repository<Invoice>(_context);
        InvoiceItems = new Repository<InvoiceItem>(_context);
    }

    public IRepository<Invoice> Invoices { get; private set; }
    public IRepository<InvoiceItem> InvoiceItems { get; private set; }

    public async Task<int> Complete()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
