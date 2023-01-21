namespace LogPanelEntities.Entities;

public abstract class BaseClient
{
    public string Name { get; set; }
    public Database Database { get; set; }
    public string ConnectionString => Database.ConnectionString();
    public string Description { get; set; }
}
