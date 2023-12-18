using KashmirServices.Application.RRModels;
using KashmirServices.Application.Shared;

namespace KashmirServices.Application.Abstractions.IServices;

public  interface ICategoryService
{
    Task<APIResponse<CategoryResponse>> Add(CategoryRequest model);


    Task<APIResponse<CategoryResponse>> Update(UpdateCategoryRequest model);


    Task<APIResponse<IEnumerable<CategoryResponse>>> GetAll();

    Task<APIResponse<IEnumerable<CategoryResponse>>> GetManagerCategoriesAsync(Guid id);

    Task<APIResponse<CategoryResponse>> GetById(Guid id);


    Task<APIResponse<int>> Delete(Guid id);


    Task<bool> IsCategoryUnique(string categoryName);
}
