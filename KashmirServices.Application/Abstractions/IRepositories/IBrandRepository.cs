using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;
using KashmirServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Application.Abstractions.IRepositories
{
    public interface IBrandRepository : IBaseRepository<Brand>
    {
        Task<IEnumerable<BrandResponse>> GetAllBrands();
        Task<BrandResponse> GetBrandsById(Guid id);

        Task<IEnumerable<BrandResponse>> GetByCategoryId(Guid id);
    }
}
