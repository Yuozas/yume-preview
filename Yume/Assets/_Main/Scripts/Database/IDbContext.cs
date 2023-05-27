using SQLite4Unity3d;
using System.Collections.Generic;

public interface IDbContext
{
    TableQuery<TTable> GetTableQuery<TTable>() where TTable : Table, new();
    int Insert<TTable>(TTable row) where TTable : Table, new();
    int InsertOrReplace<TTable>(TTable row) where TTable : Table, new();
    int InsertAll<TTable>(IEnumerable<TTable> rows) where TTable : Table, new();
    int Update<TTable>(TTable rows) where TTable : Table, new();
    int UpdateAll<TTable>(IEnumerable<TTable> rows) where TTable : Table, new();
    int GetNextId<TTable>() where TTable : Table, new();
}