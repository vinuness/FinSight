using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FinSight.Web.Interfaces;
using FinSight.Web.Models.Metas;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FinSight.Web.Controllers
{
    public class MetaController : Controller
    {
        private readonly ILogger<MetaController> _logger;
        private readonly IMetaService _service;

        public MetaController(ILogger<MetaController> logger, IMetaService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet]   
        public async Task<IActionResult> Index()
        {
            var UsuarioId =  User.FindFirstValue(ClaimTypes.NameIdentifier);
            List<Meta> metas = await _service.FindAllGoals(Guid.Parse(UsuarioId)) ?? new List<Meta>();
            return View(metas);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            Meta meta = await _service.FindGoalById(id);
            MetaUpdate update = new MetaUpdate
            {   
                Nome = meta.Nome,
                Descricao = meta.Descricao,
                ValorAtual = meta.ValorAtual,
                ValorDesejado = meta.ValorDesejado
            };
            return View(update);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, MetaUpdate update)
        {
            await _service.UpdateGoal(id, update);
            return RedirectToAction("Index", "Meta");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MetaDTO meta)
        {
            meta.UsuarioId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            await _service.SaveGoal(meta);
            return RedirectToAction("Index", "Meta");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteGoal(id);
            return RedirectToAction("Index", "Meta");
        }
    }
}