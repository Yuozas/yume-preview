using System;
using System.Linq.Expressions;
using System.Linq;

public interface IDbContext
{
    IQueryable<T> All<T>() where T : class;
    void Add<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void Delete<T>(T entity) where T : class;
    IQueryable<T> Where<T>(Expression<Func<T, bool>> predicate) where T : class;
    void BeginTransaction();
    void CommitTransaction();
    void CancelTransaction();
}