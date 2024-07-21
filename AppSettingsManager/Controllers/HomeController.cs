using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppSettingsManager.Models;

namespace AppSettingsManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;
    private TwilioSettings _twilioSettings;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        // bind settings to a class, instead of using settings directly
        _twilioSettings = new TwilioSettings();
        _configuration.GetSection("Twilio").Bind(_twilioSettings);
    }

    public IActionResult Index()
    {
        ViewBag.SendGridKeys = _configuration.GetValue<string>("SendGridKeys") ?? string.Empty;
        ViewBag.TwilioKey = _configuration.GetValue<string>("Twilio:Key") ?? string.Empty;
        ViewBag.TwillioToken = _configuration.GetValue<string>("Twilio:Token") ?? string.Empty;
        ViewBag.PhoneNumber = _twilioSettings.PhoneNumber; // get the value from app settings using class binding
        ViewBag.TwilioKey = _configuration.GetSection("Twilio").GetValue<string>("Key") ?? string.Empty;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}