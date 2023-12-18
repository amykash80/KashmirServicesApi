using KashmirServices.Application.RRModels;
using KashmirServices.Application.RRModels.User;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Application.Abstractions.IRepositories;

public interface IInvoiceRepository : IBaseRepository<Invoice>
{
    Task<InvoiceBasicDetails> GenerateInvoice(Guid callBookingId);
}
