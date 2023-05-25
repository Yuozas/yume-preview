using SQLite4Unity3d;

public abstract class Table
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
}