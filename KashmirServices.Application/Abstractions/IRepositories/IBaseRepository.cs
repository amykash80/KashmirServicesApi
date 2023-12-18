using KashmirServices.Domain.Shared;
using System.Data;
using System.Linq.Expressions;

namespace KashmirServices.Application.Abstractions.IRepositories;

public interface IBaseRepository<T> where T : BaseEntity
{

    #region sync
        int Insert(T model);

        int Update(T model);

        int Delete(T model);

        IEnumerable<T?> GetAll();

        IEnumerable<T?> FindBy(Expression<Func<T, bool>> expression);

        T? GetById(Guid id);
    #endregion
   
    
    #region async methods
    Task<int> InsertAsync(T model);

    Task<int> UpdateAsync(T model);

    Task<int> DeleteAsync(T model);

    Task<IEnumerable<T?>> GetAllAsync();

    Task<IEnumerable<T?>> FetchAllAsync(int pageNo, int pageSize);

    Task<IEnumerable<T?>> FindByAsync(Expression<Func<T, bool>> expression);

    Task <bool> IsExist(Expression<Func<T, bool>> expression);

    Task<T?> GetByIdAsync(Guid id);

    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

    #endregion


    #region dapper methods

    Task<int> ExecuteAsync<T>(string sql, object? param, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null);


    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null);


    Task<T?> FirstOrDefaultAsync<T>(string sql, object? param, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null);


    Task<T?> SingleOrDefaultAsync<T>(string sql, object? param, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null);


    #endregion



}
