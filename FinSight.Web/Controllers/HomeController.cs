using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinSight.Web.Models;
using FinSight.Web.Interfaces;
using System.Threading.Tasks;
using FinSight.Web.Models.Calculos.Request;

namespace FinSight.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IDataService _data;
    private readonly ICalculoService _calculo;

    public HomeController(ILogger<HomeController> logger, IDataService data, ICalculoService calculo)
    {
        _logger = logger;
        _data = data;
        _calculo = calculo;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        double USDBRL = await _data.GetUSDBRL();
        double EURBRL = await _data.GetEURBRL();
        double BTCBRL = await _data.GetBTCBRL();
        double SELIC = await _data.GetSelicData();
        double CDI = await _data.GetCDIData();
        double IPCA = await _data.GetIPCAData();

        ViewBag.USDBRL = USDBRL;
        ViewBag.EURBRL = EURBRL;
        ViewBag.BTCBRL = Convert.ToDecimal(BTCBRL).ToString("N2", new System.Globalization.CultureInfo("pt-BR"));
        ViewBag.SELIC = SELIC;
        ViewBag.CDI = CDI;
        ViewBag.IPCA = IPCA;

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CalcularSimulacao(SimulacaoForm request)
    {
        double USDBRL = await _data.GetUSDBRL();
        double EURBRL = await _data.GetEURBRL();
        double BTCBRL = await _data.GetBTCBRL();
        double SELIC = await _data.GetSelicData();
        double CDI = await _data.GetCDIData();
        double IPCA = await _data.GetIPCAData();

        ViewBag.USDBRL = USDBRL;
        ViewBag.EURBRL = EURBRL;
        ViewBag.BTCBRL = Convert.ToDecimal(BTCBRL).ToString("N2", new System.Globalization.CultureInfo("pt-BR"));
        ViewBag.SELIC = SELIC;
        ViewBag.CDI = CDI;
        ViewBag.IPCA = IPCA;
        var resultado = await _calculo.CalcularSimulacao(request);
        request.Resultado = resultado;
        return View("Index", request);
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
