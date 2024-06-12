using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrobaSoft.API.Interfaces;
using OrobaSoft.Shared.DTOs;
using OrobaSoft.Shared.Models;
using OrobaSoft.UI.Services;

namespace OrobaSoft.UI.Controllers
{
    public class InvoicesController : Controller
    {
        private readonly InvoiceService _invoiceService;

        public InvoicesController(InvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public async Task<IActionResult> Index()
        {
            var invoices = await _invoiceService.GetInvoices();
            return View(invoices);
        }

        public async Task<IActionResult> Details(int id)
        {
            var invoice = await _invoiceService.GetInvoiceById(id);
            if (invoice == null) return NotFound();
            return View(invoice);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] InvoiceDto invoiceDto)
        {
            if (!ModelState.IsValid) return View(invoiceDto);
             await _invoiceService.CreateInvoice(invoiceDto);
            //return RedirectToAction(nameof(Index));
            return Json(invoiceDto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await _invoiceService.GetInvoiceById(id);
            if (invoice == null) return NotFound();
            return View(invoice);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _invoiceService.DeleteInvoice(id);
            return Json(id);
        }
    }
}
