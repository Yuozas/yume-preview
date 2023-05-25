using SQLite4Unity3d;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;

public class DbContext : SQLiteConnection, IDbContext
{
    private readonly IDataStoreHelper _dataStoreHelper;

    public DbContext(DbConfigurations dbConfigurations, IDataStoreHelper dataStoreHelper) 
        : base(dbConfigurations.DatabasePath, true)
    {
        _dataStoreHelper = dataStoreHelper;
    }

    public TableQuery<TTable> GetTableQuery<TTable>() where TTable : Table, new()
    {
        return Table<TTable>();
    }

    public int Insert<TTable>(TTable row) where TTable : Table, new()
    {
        return base.Insert(row);
    }

    public int InsertOrReplace<TTable>(TTable row) where TTable : Table, new()
    {
        return base.InsertOrReplace(row);
    }

    public int InsertAll<TTable>(IEnumerable<TTable> rows) where TTable : Table, new()
    {
        return base.InsertAll(rows);
    }

    public int Update<TTable>(TTable rows) where TTable : Table, new()
    {
        return base.Update(rows);
    }

    public int UpdateAll<TTable>(IEnumerable<TTable> rows) where TTable : Table, new()
    {
        return base.UpdateAll(rows);
    }

    public int GetNextId<TTable>() where TTable : Table, new()
    {
        return GetTableQuery<TTable>().Max(t => t.Id) + 1;
    }

    public void Build()
    {
        CreateModelTables();
        DropUnknownTables();
    }

    private void CreateModelTables()
    {
        var tables = _dataStoreHelper.GetAllTypes();
        foreach (var type in tables)
            CreateTable(type, CreateFlags.AllImplicit);
    }

    private void DropUnknownTables()
    {
        var modelTableNames = _dataStoreHelper.GetAllTypeNames().ToArray();
        var allTableNames = GetTableNamesInDb().ToArray();
        var unknownTableNames = allTableNames.Except(modelTableNames).ToArray();

        foreach (string tableName in unknownTableNames)
            Execute($"DROP TABLE IF EXISTS {tableName}");
    }

    private IEnumerable<string> GetTableNamesInDb()
    {
        const string sql = "SELECT `name` as `Name` FROM `sqlite_master` WHERE `type` = 'table' AND `name` != 'sqlite_sequence';";
        var allTableNames = Query<SqliteMaster>(sql);
        return allTableNames.Where(t => t is not null).Select(s => s.Name);
    }

    private class SqliteMaster
    {
        public string Name { get; set; }
    }
}