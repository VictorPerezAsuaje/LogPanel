using LogPanelEntities.Entities;
using LogPanelEntities.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LogPanelViews.Controllers;

[Area("Log")]
public class ExceptionsController : Controller
{
    ILogRepository _logRepo;
    IClientLogDbConfig dbConfig;
    public ExceptionsController(ILogRepository logRepository, IClientLogDbConfig databaseConfig)
    {
        _logRepo = logRepository;
        dbConfig = databaseConfig;
    }

    public IActionResult Index(string? clientSelected = null)
    {
        if(clientSelected == null)
            clientSelected = dbConfig.GetAllClients().FirstOrDefault().Name;

        return View(_logRepo.GetLogsByClientName(clientSelected, dbConfig));
    }

    [HttpGet]
    [Route("[area]/[controller]/[action]")]
    public IActionResult PollClientLogs(string clientSelected)
    {
        if (clientSelected == null)
            clientSelected = dbConfig.GetAllClients().FirstOrDefault().Name;

        List<Log> logs = _logRepo.GetLogsByClientName(clientSelected, dbConfig);
        return PartialView("_ListadoLogs", logs);
    }
}