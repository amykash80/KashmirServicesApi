using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.RRModels;
using KashmirServices.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KashmirServices.Persistence.Repositories
{
    public class BrandRepository : BaseRepository<Brand>,IBrandRepository
    {
        public BrandRepository(KashmirServicesDbContext context) : base(context){ }


        private readonly string baseQuery = $@"SELECT  B.Id, B.[Name], B.[Description],B.IsDeleted, 
		                                        B.CreatedOn, C.Id AS CategoryId,  C.[Name] AS CategoryName 
		                                        FROM Brands B
	                                            INNER JOIN Categories C
	                                            ON B.CategoryId =C.Id 
                                                WHERE B.IsDeleted =0 ";
        public async Task<IEnumerable<BrandResponse>> GetAllBrands()
        {
            return await QueryAsync<BrandResponse>(baseQuery);
        }

        public async Task<BrandResponse> GetBrandsById(Guid id)
        {
            return await FirstOrDefaultAsync<BrandResponse>(baseQuery + " AND B.Id =@id", new { id });
        }


        public async Task<IEnumerable<BrandResponse>> GetByCategoryId(Guid id)
        {
            return await QueryAsync<BrandResponse>(baseQuery + " AND B.CategoryId =@id", new { id });
        }
    }
}
