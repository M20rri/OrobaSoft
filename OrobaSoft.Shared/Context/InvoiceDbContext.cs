using Microsoft.EntityFrameworkCore;
using OrobaSoft.Shared.Models;

namespace OrobaSoft.Shared.Context
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options) { }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Invoice>().HasMany(i => i.Items).WithOne(ii => ii.Invoice).HasForeignKey(ii => ii.InvoiceId);
        }
    }
}
