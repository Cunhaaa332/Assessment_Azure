using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using WebMVC.Models;
using WebMVC.Models.HomeModel;

namespace WebMVC.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            var client = new RestClient();

            ListarTudoViewModel listarTudoViewModel = new ListarTudoViewModel();

            var requestAmigo = new RestRequest("https://localhost:5001/api/amigos", DataFormat.Json);
            var responseAmigo = client.Get<List<Amigo>>(requestAmigo);
            listarTudoViewModel.Amigos = responseAmigo.Data.Count;
            var requestPais = new RestRequest("https://localhost:5001/api/paises", DataFormat.Json);
            var responsePais = client.Get<List<Pais>>(requestPais);
            listarTudoViewModel.Paises = responsePais.Data.Count;
            var requestEstado = new RestRequest("https://localhost:5001/api/estados", DataFormat.Json);
            var responseEstado = client.Get<List<Estado>>(requestEstado);
            listarTudoViewModel.Estados = responseEstado.Data.Count;


            return View(listarTudoViewModel);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
