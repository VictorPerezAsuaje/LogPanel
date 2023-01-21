using LogPanelEntities.Entities;
using LogPanelEntities.Extensions;
using System.Data.SqlClient;
using System.Diagnostics;

namespace LogPanelEntities.Repositories;

public interface ILogRepository
{
    List<Log> GetLogsByClientName(string name, IClientLogDbConfig config);
}

internal class LogRepository : ILogRepository
{
    public List<Log> GetLogsByClientName(string name, IClientLogDbConfig config)
    {
        Database _clientDb = config.GetClientByName(name).Database;
        SqlConnection con = new SqlConnection(_clientDb.ConnectionString());

        List<Log> logs = new List<Log>();

        try
        {
            using (con)
            {
                con.Open();

                string query = $"SELECT {_clientDb.ColNameForId}";

                if (!string.IsNullOrEmpty(_clientDb.ColNameForLogType))
                    query += $", {_clientDb.ColNameForLogType}";

                if (!string.IsNullOrEmpty(_clientDb.ColNameForTime))
                    query += $", {_clientDb.ColNameForTime}";

                if (!string.IsNullOrEmpty(_clientDb.ColNameForMessage))
                    query += $", {_clientDb.ColNameForMessage}";

                if (!string.IsNullOrEmpty(_clientDb.ColNameForStacktrace))
                    query += $", {_clientDb.ColNameForStacktrace}";

                query += $" FROM {_clientDb.LogTable} ORDER BY {_clientDb.ColNameForId} DESC";

                SqlCommand cmd = new SqlCommand(query, con);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Log log = new Log()
                    {
                        Id = rdr.TypeOrNull<int>(_clientDb.ColNameForId)
                    };

                    if (!string.IsNullOrEmpty(_clientDb.ColNameForTime))
                        log.Time = rdr.TypeOrNull<DateTime>(_clientDb.ColNameForTime);

                    if (!string.IsNullOrEmpty(_clientDb.ColNameForMessage))
                        log.Message = rdr.TypeOrNull<string>(_clientDb.ColNameForMessage);

                    if (!string.IsNullOrEmpty(_clientDb.ColNameForStacktrace))
                        log.StackTrace = rdr.TypeOrNull<string>(_clientDb.ColNameForStacktrace);

                    if (!string.IsNullOrEmpty(_clientDb.ColNameForLogType))
                        log.LogType = rdr.TypeOrNull<LogType>(_clientDb.ColNameForLogType);

                    logs.Add(log);
                }
            }
        }
        catch (Exception ex)
        {
            con.Close();
        }

        return logs;
    }
}
