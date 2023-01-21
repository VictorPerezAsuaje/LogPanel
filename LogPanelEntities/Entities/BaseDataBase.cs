using System.Data.SqlClient;

namespace LogPanelEntities.Entities;

public abstract class BaseDataBase
{
    public string Name { get; set; }
    public string Server { get; set; }
    public string? User { get; set; }
    public string? Password { get; set; }
    public string LogTable { get; set; }
    public List<Log> Logs { get; set; } = new List<Log>();

    public string ColNameForId { get; set; }
    public string? ColNameForLogType { get; set; }
    public string? ColNameForTime { get; set; }
    public string? ColNameForMessage { get; set; }
    public string? ColNameForStacktrace { get; set; }

    public class DatabaseColumnBuilder
    {
        public string ColNameForId { get; set; }
        public string? ColNameForLogType { get; set; }
        public string? ColNameForTime { get; set; }
        public string? ColNameForMessage { get; set; }
        public string? ColNameForStacktrace { get; set; }
    }

    public BaseDataBase(string name, string server, string logTable,
        Action<DatabaseColumnBuilder> columnConfig, string? user = null, string? password = null)
    {
        Name = name;
        Server = server;
        User = user;
        Password = password;
        LogTable = logTable;

        DatabaseColumnBuilder columns = new DatabaseColumnBuilder();
        columnConfig(columns);

        if(columns.ColNameForId == null) 
            throw new ArgumentNullException(nameof(columns.ColNameForId), $"You have to specify the ColNameForId in the configuration for table {logTable} in the database named {name}");

        ColNameForId = columns.ColNameForId;
        ColNameForLogType = columns.ColNameForLogType;
        ColNameForTime = columns.ColNameForTime;
        ColNameForMessage = columns.ColNameForMessage;
        ColNameForStacktrace = columns.ColNameForStacktrace;
    }

    public string ConnectionString()
    {
        SqlConnectionStringBuilder conBuilder = new SqlConnectionStringBuilder();

        conBuilder.DataSource = Server;
        conBuilder.InitialCatalog = Name;

        if (!string.IsNullOrEmpty(User) && !string.IsNullOrEmpty(Password))
        {
            conBuilder.PersistSecurityInfo = true;
            conBuilder.UserID = User;
            conBuilder.Password = Password;
        }
        else
        {
            conBuilder.IntegratedSecurity = true;
            conBuilder.MultipleActiveResultSets = true;
        }

        return conBuilder.ConnectionString;
    }
}
