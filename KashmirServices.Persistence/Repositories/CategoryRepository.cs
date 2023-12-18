using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Application.RRModels;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;

namespace KashmirServices.Persistence.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(KashmirServicesDbContext context) : base(context)
    {

    }

    private readonly string baseQuery = $@"SELECT C.Id ,C.[Name],[Description], C.ManagerId, U.FullName AS ManagerName,C.CreatedOn,F.FilePath 
                                                        FROM Categories C
							                            INNER JOIN Users U
							                            ON C.ManagerId = U.Id
                                                        LEFT JOIN AppFiles F
                                                        ON C.ID = F.EntityId 
                                                        WHERE C.IsDeleted = 0 ";



    string query = $@"SELECT C.Id AS CategoryId, U.FullName, U.Gender, U.PhoneNumber, U.Email, C.[Name], C.[Description] , C.ManagerId , M.FullName AS ManagerName, M.PhoneNumber AS ManagerPhoneNumber
							FROM Users U
							INNER JOIN EngineerJobRoles E
							ON U.Id = E.EngineerId
							INNER JOIN  Categories C 
							ON C.Id = E.CategoryId
							INNER JOIN Users M
							ON M.Id = C.ManagerId
							WHERE C.Id ='F6230D5E-1CA6-4945-988C-0AFBE84F76B0'";
    public async Task<int> AddAppFile(AppFile model)
    {
        string query = $@" INSERT INTO AppFiles (Id,Module,FilePath,EntityId,CreatedOn) VALUES(@id,@module,@filePath,@entityId,@createdOn) ";
        return await ExecuteAsync<int>(query, model);
    }


    public async Task<int> AddCategory(Category model)
    {
        string query = $@"INSERT INTO Categories (Id,[Name],[Description],CreatedOn,IsDeleted,ManagerId) VALUES(@id,@name,@description,@createdOn,0,@managerId);";
        return await ExecuteAsync<int>(query, model);
    }


    public async Task<int> DeleteCategory(Guid id)
    {
        return await ExecuteAsync<int>($@"UPDATE Categories SET IsDeleted=1 WHERE Id='{id}' ", new { Id = id });
    }

    public async Task<int> DeleteCategoryWithAppFile(Guid id)
    {
        return await ExecuteAsync<int>($@" Delete from AppFiles WHERE EntityId=@id;
                                           UPDATE Categories SET IsDeleted=1 WHERE Id=@id ",new { Id=id});
    }

    public async Task<IEnumerable<CategoryResponse>> GetAllCategoriesAsync()
    {
        return await QueryAsync<CategoryResponse>(baseQuery);
    }

    public async Task<CategoryResponse?> GetCategoryById(Guid id)
    {
        return await FirstOrDefaultAsync<CategoryResponse>(baseQuery + $" AND C.Id='{id}' AND C.IsDeleted = 0 ");
    }


    public async Task<CategoryResponse?> GetCategoriesWithAppFileByEntityId(Guid id)
    {
        return await FirstOrDefaultAsync<CategoryResponse>(baseQuery + $" AND C.Id='{id}' AND C.IsDeleted = 0 ");
    }


    public async Task<string?> GetAppFileByEntityId(Guid id)
    {
        return await FirstOrDefaultAsync<string>(@$" SELECT Id from AppFiles where EntityId=@id",new { id=id});
    }


    public async Task<int> UpdateAppFile(Guid id, string filePath)
    {
        return await ExecuteAsync<int>($@"UPDATE AppFiles SET FilePath=@filePath	
                                            WHERE EntityId=@id ", new { filePath=filePath, id = id });

    }

    public async Task<IEnumerable<CategoryResponse>> GetManagerCategoriesAsync(Guid id)
    {
        return await QueryAsync<CategoryResponse>(baseQuery + " AND  C.ManagerId=@id", new { id});
    }
}
