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
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync();

        Task<IEnumerable<CategoryResponse>> GetManagerCategoriesAsync(Guid id);

       Task<int> AddCategory(Category model);


        Task<int> AddAppFile(AppFile model);

        Task<CategoryResponse?> GetCategoriesWithAppFileByEntityId(Guid id);


        Task<string?> GetAppFileByEntityId(Guid id);

        Task<CategoryResponse?> GetCategoryById(Guid id);


        Task<int> DeleteCategory(Guid id);

        Task<int> DeleteCategoryWithAppFile(Guid id);

        Task<int> UpdateAppFile(Guid id,string FilePath);
    }

}
