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
using WebMVC.Models.EstadoModel;

namespace WebMVC.Controllers
{
    public class EstadoController : Controller
    {
        // GET: Estado
        public ActionResult Index()
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/estados", DataFormat.Json);
            var response = client.Get<List<Estado>>(request);

            return View(response.Data);
        }

        // GET: Estado/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/estados/" + id, DataFormat.Json);
            var response = client.Get<Estado>(request);

            return View(response.Data);
        }

        // GET: Estado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CriarEstadoViewModel criarEstadoViewModel)
        {
            if (ModelState.IsValid) {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:5001/api/estados", DataFormat.Json);
                var urlFoto = UploadFotoAmigo(criarEstadoViewModel.Fotoarq);
                criarEstadoViewModel.Bandeira = urlFoto;
                request.AddJsonBody(criarEstadoViewModel);
                var response = client.Post<CriarEstadoViewModel>(request);
                return Redirect("/estado/index");
            }
            return BadRequest();
        }

        // GET: Estado/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/estados/" + id, DataFormat.Json);
            var response = client.Get<Estado>(request);

            return View(response.Data);
        }

        // POST: Estado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Estado estadoN)
        {
            try {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:5001/api/estados/" + id, DataFormat.Json);
                request.AddJsonBody(estadoN);
                var response = client.Put<Estado>(request);

                return Redirect("/estado/index");
            } catch {
                return View();
            }
        }

        // GET: Estado/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/estados/" + id, DataFormat.Json);
            var response = client.Get<Estado>(request);

            return View(response.Data);
        }

        // POST: Estado/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:5001/api/estados/" + id, DataFormat.Json);
                var response = client.Delete<Estado>(request);

                return Redirect("/estado");
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