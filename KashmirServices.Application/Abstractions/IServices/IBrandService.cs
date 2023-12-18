using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Application.Abstractions.IServices;

public interface IBrandService
{
    Task<APIResponse<BrandResponse>> Add(BrandRequest model);


    Task<APIResponse<BrandResponse>> Update(UpdateBrandRequest model);


    Task<APIResponse<IEnumerable<BrandResponse>>> GetAll();
    

    Task<APIResponse<IEnumerable<BrandNames>>> GetBrandNames();


    Task<APIResponse<BrandResponse>> GetById(Guid id);

    Task<APIResponse<IEnumerable<BrandResponse>>> GetByCategoryId(Guid id);

    Task<APIResponse<BrandResponse>> Delete(Guid id);


    Task<bool> IsBrandNameUnique(string brandName);
}
