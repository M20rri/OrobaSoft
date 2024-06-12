﻿using AutoMapper;
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
    private readonly IMapper _mapper;

    public InvoicesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    //[HttpPost]
    //public async Task<IActionResult> CreateInvoice([FromBody] InvoiceDto invoiceDto)
    //{
    //    if (invoiceDto == null) return BadRequest("Invoice data is null.");

    //    var invoice = _mapper.Map<Invoice>(invoiceDto);
    //    await _unitOfWork.Invoices.Add(invoice);
    //    await _unitOfWork.Complete();

    //    var createdInvoice = _mapper.Map<InvoiceDto>(invoice);
    //    return CreatedAtAction(nameof(GetInvoiceById), new { id = createdInvoice.Id }, createdInvoice);
    //}

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetInvoiceById(int id)
    //{
    //    var invoice = await _unitOfWork.Invoices.GetById(id);
    //    if (invoice == null) return NotFound();

    //    var invoiceDto = _mapper.Map<InvoiceDto>(invoice);
    //    return Ok(invoiceDto);
    //}

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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] InvoiceDto invoiceDto)
    {
        if (id != invoiceDto.Id || !ModelState.IsValid) return BadRequest(ModelState);

        var invoice = await _unitOfWork.Invoices.GetById(id);
        if (invoice == null) return NotFound();

        invoice.CustomerName = invoiceDto.CustomerName;
        invoice.InvoiceDate = invoiceDto.InvoiceDate;
        invoice.Items = invoiceDto.Items.Select(item => new InvoiceItem
        {
            Id = item.Id,
            Description = item.Description,
            UnitPrice = item.UnitPrice,
            Quantity = item.Quantity,
            InvoiceId = id
        }).ToList();

        _unitOfWork.Invoices.Update(invoice);
        await _unitOfWork.Complete();

        return NoContent();
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
