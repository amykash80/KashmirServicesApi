using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.RRModels;
using KashmirServices.Domain.Entities;

namespace KashmirServices.Persistence.Repositories;

internal sealed class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
{
    private readonly KashmirServicesDbContext context;

    public InvoiceRepository(KashmirServicesDbContext context) : base(context)
    {
        this.context = context;
    }

    public async Task<InvoiceBasicDetails> GenerateInvoice(Guid callBookingId)
    {
        string query = $@"SELECT C.Id AS CallBookingId, C.JobNo, Cust.FullName, S.[Name] AS ServiceType, 
	                    S.Charges, S.FreeServiceDistance, S.PerKilometerCharge
	
	                    FROM CallBookings C
	                    INNER JOIN ServiceTypes S
	                    ON C.ServiceTypeId =S.Id
	                    INNER JOIN Users Cust
	                    ON C.CustomerId = Cust.Id
	                    WHERE C.Id=@callBookingId ";
        return await FirstOrDefaultAsync<InvoiceBasicDetails>(query, new { callBookingId });
    }
}
