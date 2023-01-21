namespace LogPanelEntities.Entities;

public interface IClientLogDbConfig
{
    void AddClientDb(BaseClient client);
    List<BaseClient> GetAllClients();
    BaseClient? GetClientByName(string name);
}

internal class ClientLogDbConfig : IClientLogDbConfig
{
    Dictionary<string, BaseClient> _Clientes { get; set; } = new Dictionary<string, BaseClient>();

    public void AddClientDb(BaseClient client)
    {
        _Clientes.Add(client.Name, client);
    }

    public List<BaseClient> GetAllClients() => _Clientes.Values.ToList();

    public BaseClient? GetClientByName(string name)
    {
        try
        {
            return _Clientes[name];
        }
        catch(Exception ex)
        {
            return null;
        }
    }
}
