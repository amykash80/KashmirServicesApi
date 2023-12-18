using AutoMapper;
using KashmirServices.Application.Abstractions.Identity;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Application.Services;

public class InvoiceService : IInvoiceService
{
    private readonly IInvoiceRepository repository;
    private readonly ICallBookingRepository callBookingRepository;
    private readonly IVisitRepository visitRepository;
    private readonly IBaseRepository<AssignedEngineer> baseRepository;
    private readonly IBaseRepository<InvoiceDetails> invoiceDetailRepository;
    private readonly IMapper mapper;

    public InvoiceService(IInvoiceRepository repository, ICallBookingRepository callBookingRepository,
        IVisitRepository visitRepository, IBaseRepository<AssignedEngineer> baseRepository,
        IBaseRepository<InvoiceDetails> invoiceDetailRepository,
        IMapper mapper)
    {
        this.repository = repository;
        this.callBookingRepository = callBookingRepository;
        this.visitRepository = visitRepository;
        this.baseRepository = baseRepository;
        this.invoiceDetailRepository = invoiceDetailRepository;
        this.mapper = mapper;
    }


    public async Task<APIResponse<string>> AddInvoice(Guid callBookingId)
    {
        var isExist = await repository.IsExist(x => x.CallBookingId == callBookingId);
        if (isExist) return APIResponse<string>.ErrorResponse("Invoice already generated", APIStatusCodes.Conflict);


        Invoice invoice = new()
        {
            InvoiceNo = new Random().Next(1000, 10000).ToString(),
            InvoiceDate = DateTimeOffset.Now,
            CallBookingId = callBookingId,
        };
        var assignEngineer = await baseRepository.FirstOrDefaultAsync(x => x.CallBookingId == callBookingId);
        var visits = await visitRepository.FindByAsync(x => x.AssingedEngineerId == assignEngineer.Id);
        double totalDis = 0;
        foreach (var item in visits)
        {
            totalDis += item.TotalDistance;
        }
        invoice.TotalDistance = totalDis;

        int returnValue = await repository.InsertAsync(invoice);
        if (returnValue > 0)
            return APIResponse<string>.SuccessResponse("Invoice generated successfully");

        return APIResponse<string>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
    }



    public async Task<APIResponse<string>> AddItem(InvoiceItems model)
    {
        var isExist = await invoiceDetailRepository.IsExist(x => x.PartId == model.PartId);
        if (isExist) return APIResponse<string>.ErrorResponse("Parts already added", APIStatusCodes.Conflict);

        InvoiceDetails invoiceDetails = new()
        {
            PartId = model.PartId,
            InvoiceId = model.InvoiceId,
            Discount = model.Discount,
        };
        int returnValue = await invoiceDetailRepository.InsertAsync(invoiceDetails);
        if (returnValue > 0)
            return APIResponse<string>.SuccessResponse("Item Added successfully");

        return APIResponse<string>.ErrorResponse(APIMessages.TechnicalError, APIStatusCodes.InternalServerError);
    }

    public async Task<APIResponse<InvoiceBasicDetails>> GenerateInvoice(Guid invoiceId)
    {
        var invoice=await repository.GetByIdAsync(invoiceId);
        if(invoice is null )  return APIResponse<InvoiceBasicDetails>.ErrorResponse("No invoice found", APIStatusCodes.NotFound);


        var invoiceBasicDetails = await repository.GenerateInvoice(invoice.CallBookingId);
        double totalDistance = invoice.TotalDistance;
        double calcDist = totalDistance - invoiceBasicDetails.FreeServiceDistance;
        var totalDistanceCharages= invoiceBasicDetails.PerKilometerCharge  * calcDist;
        invoiceBasicDetails.GrandTotal = invoiceBasicDetails.Charges + totalDistanceCharages;
        invoiceBasicDetails.InvoiceNo = invoice.InvoiceNo;

        return APIResponse<InvoiceBasicDetails>.SuccessResponse(invoiceBasicDetails);
    }
}
