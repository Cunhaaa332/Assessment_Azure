using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using RestSharp;
using WebMVC.Models.PaisModel;

namespace WebMVC.Controllers
{
    public class PaisController : Controller
    {
        // GET: Pais
        public ActionResult Index()
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/paises", DataFormat.Json);
            var response = client.Get<List<Pais>>(request);

            return View(response.Data);
        }

        // GET: Pais/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/paises/" + id, DataFormat.Json);
            var response = client.Get<Pais>(request);

            return View(response.Data);
        }

        // GET: Pais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CriarPaisViewModel criarPaisViewModel)
        {
            if (ModelState.IsValid) {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:5001/api/paises", DataFormat.Json);
                var urlFoto = UploadFotoAmigo(criarPaisViewModel.Fotoarq);
                criarPaisViewModel.Bandeira = urlFoto;
                request.AddJsonBody(criarPaisViewModel);
                var response = client.Post<CriarPaisViewModel>(request);
                return Redirect("/pais/index");
            }
            return BadRequest();
        }

        // GET: Pais/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/paises/" + id, DataFormat.Json);
            var response = client.Get<Pais>(request);

            return View(response.Data);
        }

        // POST: Pais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Pais paisN)
        {
            try {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:5001/api/paises/" + id, DataFormat.Json);
                request.AddJsonBody(paisN);
                var response = client.Put<Pais>(request);

                return Redirect("/pais/index");
            } catch {
                return View();
            }
        }

        // GET: Pais/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/paises/" + id, DataFormat.Json);
            var response = client.Get<Pais>(request);

            return View(response.Data);
        }

        // POST: Pais/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:5001/api/paises/" + id, DataFormat.Json);
                var response = client.Delete<Pais>(request);

                return Redirect("/pais");
            } catch {
                return View();
            }
        }

        private string UploadFotoAmigo(IFormFile foto) {
            var reader = foto.OpenReadStream();
            var cloudStorageAccount = CloudStorageAccount.Parse(@"DefaultEndpointsProtocol=https;AccountName=fotosassessmentazure;AccountKey=6RmpIbZzWx26AzY/DD88+cKg3fZUaoALQBMJzQCkYPpOJ6WtfW3JGZZoobz9aChxIQo+z5nVfVVAgb00lqRoGg==;EndpointSuffix=core.windows.net");
            var blobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("fotos-amigos");
            container.CreateIfNotExists();
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            blob.UploadFromStream(reader);
            var destinoDaImagemNaNuvem = blob.Uri.ToString();
            return destinoDaImagemNaNuvem;
        }
    }
}