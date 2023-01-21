using LogPanel.Models;
using LogPanelEntities.Entities;
using LogPanelEntities.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LogPanel.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    ILogRepository _logRepo;
    IClientLogDbConfig dbConfig;

    public HomeController(ILogger<HomeController> logger, ILogRepository logRepository, IClientLogDbConfig databaseConfig)
    {
        _logger = logger;
        _logRepo = logRepository;
        dbConfig = databaseConfig;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult CustomLogger(string? clientSelected = null)
    {
        if (clientSelected == null)
            clientSelected = dbConfig.GetAllClients().FirstOrDefault().Name;

        return View(_logRepo.GetLogsByClientName(clientSelected, dbConfig));
    }

    [HttpGet]
    [Route("Logs/PollClientLogs")]
    public List<Log> PollClientLogs(string clientSelected)
    {
        if (clientSelected == null)
            clientSelected = dbConfig.GetAllClients().FirstOrDefault().Name;

        List<Log> logs = _logRepo.GetLogsByClientName(clientSelected, dbConfig);

        return logs;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}