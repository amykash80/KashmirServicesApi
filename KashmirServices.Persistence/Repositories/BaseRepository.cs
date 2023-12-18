using Dapper;
using KashmirServices.Application.Abstractions.IRepositories;
using KashmirServices.Domain.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace KashmirServices.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly KashmirServicesDbContext context;
    public BaseRepository(KashmirServicesDbContext context)
    {
        this.context = context;
    }
    #region sync methods

    public int Delete(T model)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> FindBy(Expression<Func<T, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public T GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public int Insert(T model)
    {
        throw new NotImplementedException();
    }

    public int Update(T model)
    {
        throw new NotImplementedException();
    }

    #endregion


    #region async methods

    public async Task<int> DeleteAsync(T model)
    {
        await Task.Run(() => context.Set<T>().Remove(model));
        return await context.SaveChangesAsync();
    }


    public async Task<IEnumerable<T?>> FindByAsync(Expression<Func<T, bool>> expression)
    {
        return await Task.Run(() => context.Set<T>().Where(expression));
    }


    public async Task<IEnumerable<T?>> GetAllAsync()
    {
        return await Task.Run(() => context.Set<T>());
    }

    public async Task<IEnumerable<T?>> FetchAllAsync(int pageNo, int pageSize)
    {
        return await context.Set<T>()
            .Skip((pageNo - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }


    public async Task<int> InsertAsync(T model)
    {
        await context.AddAsync(model);
        return await context.SaveChangesAsync();
    }


    public async Task<int> UpdateAsync(T model)
    {
        await Task.Run(() => context.Update(model));
        return await context.SaveChangesAsync();
    }


    public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression)
    {
        return await context.Set<T>().FirstOrDefaultAsync(expression);
    }
    public async Task<bool> IsExist(Expression<Func<T, bool>> expression)
    {
        return await Task.Run(() => context.Set<T>().Any(expression));
    }
    #endregion





    #region dapper methods

    public async Task<int> ExecuteAsync<T>(string sql, object? param = default, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null)
    {
        return await context.ExecuteAsyncExtentionMethod<T>(sql, param, commandType, transaction);
    }


    public async Task<T> FirstOrDefaultAsync<T>(string sql, object? param = default, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null)
    {
        return await context.FirstOrDefaultAsyncDapperExtentionMethod<T>(sql, param, commandType, transaction);
    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = default, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null)
    {
        return await context.QueryAsyncDapperExtentionMethod<T>(sql, param, commandType, transaction);
    }


    public Task<T?> SingleOrDefaultAsync<T>(string sql, object? param, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null)
    {
        return context.SingleOrDefaultAsyncDapperExtentionMethod<T>(sql, param, commandType, transaction);
    }
    #endregion
}

#region Dapper Methods As Extension On Ef Core
public static class DapperAsExtensionOnEfCore
{
    public static async Task<IEnumerable<T>> QueryAsyncDapperExtentionMethod<T>(this DbContext context, string sql, object? param = default, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null)
    {
        try
        {
            SqlConnection connection = new(context.Database.GetConnectionString());
            return await connection.QueryAsync<T>(sql, param, transaction, null, commandType);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public static async Task<int> ExecuteAsyncExtentionMethod<T>(this DbContext context, string sql, object? param = default, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null)
    {
        try
        {
            SqlConnection connection = new(context.Database.GetConnectionString());
            return await connection.ExecuteAsync(sql, param, transaction, null, commandType);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public static async Task<T?> SingleOrDefaultAsyncDapperExtentionMethod<T>(this DbContext context, string sql, object? param = default, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null)
    {
        try
        {
            SqlConnection connection = new(context.Database.GetConnectionString());
            return await connection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, null, commandType);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }


    public static async Task<T?> FirstOrDefaultAsyncDapperExtentionMethod<T>(this DbContext context, string sql, object? param = default, CommandType commandType = CommandType.Text, IDbTransaction? transaction = null)
    {
        try
        {
            SqlConnection connection = new(context.Database.GetConnectionString());
            return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, null, commandType);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

#endregion