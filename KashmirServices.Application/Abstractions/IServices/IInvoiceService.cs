using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Abstractions.IServices;

public interface IInvoiceService
{
    Task<APIResponse<string>> AddInvoice(Guid callBookingId);


    Task<APIResponse<string>> AddItem(InvoiceItems model);

    Task<APIResponse<InvoiceBasicDetails>> GenerateInvoice(Guid invoiceId);
}
