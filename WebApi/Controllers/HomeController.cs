using Microsoft.AspNetCore.Mvc;
using Rotativa.AspNetCore;
using System.Diagnostics;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicesAPI servicesAPI;

        public HomeController(IServicesAPI _servicesAPI)
        {
          servicesAPI = _servicesAPI;
        }

        public async Task<IActionResult> Index()
        {
            List<Aspirante> lista = await servicesAPI.Listar();
            return View(lista);
        }
        public async Task<IActionResult> ImprimirReporte()
        {
            List<Aspirante> lista = await servicesAPI.Listar();
            return new ViewAsPdf("ImprimirReporte", lista)
            {
                FileName = $"Reporte Aspirantes.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4

            };
     
        }


        public async Task<IActionResult> Aspirante(int IdAspirante)
        {
            Aspirante model = new Aspirante();
            ViewBag.Accion = "New Aspirante";

            if(IdAspirante != 0)
            {
                model = await servicesAPI.Buscar(IdAspirante);
                ViewBag.Accion = "Editar Aspirante";
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Guardar(Aspirante aspirante)
        {
            bool response;

            if(aspirante.IdAspirante == 0)
            {
                response = await servicesAPI.Guardar(aspirante);
            }
            else
            {
                response = await servicesAPI.Editar(aspirante);
            }

            if (response)
                return RedirectToAction("Index");
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int idAspirante)
        {
            var response = await servicesAPI.Eliminar(idAspirante);

            if (response)
                return RedirectToAction("Index");
            else
                return NoContent();
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
}