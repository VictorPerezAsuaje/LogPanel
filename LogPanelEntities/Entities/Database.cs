namespace LogPanelEntities.Entities;

public class Database : BaseDataBase
{
    public Database(string name, string server, string logTable, Action<DatabaseColumnBuilder> columnConfig, string? user = null, string? password = null) : base(name, server, logTable, columnConfig, user, password)
    {
    }
}
