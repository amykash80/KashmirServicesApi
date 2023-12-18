namespace KashmirServices.Application.RRModels.Service;

public record ServiceTypeResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Charges,
    bool IsAvailable,
    int FreeServiceDistance, 
    int PerKilometerCharge ,
    Guid BrandId 
    );