using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ApiController
{
    private readonly IInvoiceService service;

    public InvoiceController(
        IInvoiceService service)
    {
        this.service = service;
    }


    [HttpGet("add-invoice/{callBookingId:guid}")]
    public async Task<IResult> Post(Guid callBookingId)
    {
        var result = await service.AddInvoice(callBookingId);

        return this.APIResult(result);
    }


    [HttpPost("invoice-item")]
    public async Task<IResult> AddInvoiceItem(InvoiceItems model)
    {
        var result = await service.AddItem(model);

        return this.APIResult(result);
    }


    [HttpGet("generate/{invoiceId:guid}")]
    public async Task<IResult> GenerateInvoice(Guid invoiceId)
    {
  
        var result = await service.GenerateInvoice(invoiceId);

        return this.APIResult(result);
    }
}
