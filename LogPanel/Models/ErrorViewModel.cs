using LogPanelEntities.Entities;

namespace LogPanel.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}

public class CustomClient : BaseClient
{
    public string CustomDescription { get; set; }
}