using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OrobaSoft.API.Interfaces;
using OrobaSoft.Shared.DTOs;
using OrobaSoft.Shared.Models;

namespace OrobaSoft.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public InvoicesController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var invoices = await _unitOfWork.Invoices.GetAll(i => i.Items);
        var invoiceDtos = invoices.Select(invoice => new InvoiceDto
        {
            Id = invoice.Id,
            CustomerName = invoice.CustomerName,
            InvoiceDate = invoice.InvoiceDate,
            Items = invoice.Items.Select(item => new InvoiceItemDto
            {
                Id = item.Id,
                Description = item.Description,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            }).ToList()
        });
        return Ok(invoiceDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var invoice = await _unitOfWork.Invoices.GetById(id, i => i.Items);
        if (invoice == null) return NotFound();

        var invoiceDto = new InvoiceDto
        {
            Id = invoice.Id,
            CustomerName = invoice.CustomerName,
            InvoiceDate = invoice.InvoiceDate,
            Items = invoice.Items.Select(item => new InvoiceItemDto
            {
                Id = item.Id,
                Description = item.Description,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            }).ToList()
        };

        return Ok(invoiceDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InvoiceDto invoiceDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var invoice = new Invoice
        {
            CustomerName = invoiceDto.CustomerName,
            InvoiceDate = invoiceDto.InvoiceDate,
            Items = invoiceDto.Items.Select(item => new InvoiceItem
            {
                Description = item.Description,
                UnitPrice = item.UnitPrice,
                Quantity = item.Quantity
            }).ToList()
        };

        await _unitOfWork.Invoices.Add(invoice);
        await _unitOfWork.Complete();

        return CreatedAtAction(nameof(GetById), new { id = invoice.Id }, invoiceDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var invoice = await _unitOfWork.Invoices.GetById(id);
        if (invoice == null) return NotFound();

        _unitOfWork.Invoices.Delete(invoice);
        await _unitOfWork.Complete();

        return NoContent();
    }
}
