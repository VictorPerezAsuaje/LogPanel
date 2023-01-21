using System.ComponentModel.DataAnnotations;

namespace LogPanelEntities.Entities;

public abstract class BaseLog
{
    public int Id { get; set; }
    public LogType LogType { get; set; } = LogType.Info;
    public DateTime Time { get; set; }
    public string Message { get; set; }
    public string StackTrace { get; set; }

    [DataType(DataType.Html)]
    public string LogTypeIcon => LogType switch
    {
        LogType.Success => "<i class='bi bi-check-circle text-success'></i>",
        LogType.Info => "<i class='bi bi-info-circle-fill text-primary'></i>",
        LogType.Debug => "<i class='bi bi-bug-fill text-secondary'></i>",
        LogType.Warning => "<i class='bi bi-exclamation-triangle text-warning'></i>",
        LogType.Error => "<i class='bi bi-exclamation-circle-fill text-danger'></i>",
        LogType.Exception => "<i class='bi bi-exclamation-diamond text-danger'></i>",
        _ => "<i class='bi bi-info-circle-fill'></i>"
    };
}

public class Log : BaseLog
{    
}
