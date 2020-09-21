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
using WebMVC.Models.AmigoModel;

namespace WebMVC.Controllers
{
    public class AmigoController : Controller
    {
        // GET: Amigo
        public ActionResult Index()
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/amigos", DataFormat.Json);
            var response = client.Get<List<Amigo>>(request);

            return View(response.Data);
        }

        // GET: Amigo/Details/5
        public ActionResult Details(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/amigos/" + id, DataFormat.Json);
            var response = client.Get<Amigo>(request);

            return View(response.Data);
        }

        // GET: Amigo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Amigo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CriarAmigoViewModel criarAmigoViewModel)
        {
            if (ModelState.IsValid) {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:5001/api/amigos", DataFormat.Json);
                var urlFoto = UploadFotoAmigo(criarAmigoViewModel.Fotoarq);
                criarAmigoViewModel.Foto = urlFoto;
                request.AddJsonBody(criarAmigoViewModel);
                var response = client.Post<CriarAmigoViewModel>(request);
                return Redirect("/amigo/index");
            }
            return BadRequest();
        }

        // GET: Amigo/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/amigos/" + id, DataFormat.Json);
            var response = client.Get<Amigo>(request);

            return View(response.Data);
        }

        // POST: Amigo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Amigo amigoN)
        {
            try
            {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:5001/api/amigos/" + id, DataFormat.Json);
                request.AddJsonBody(amigoN);
                var response = client.Put<Amigo>(request);

                return Redirect("/amigo/index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Amigo/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/amigos/" + id, DataFormat.Json);
            var response = client.Get<Amigo>(request);

            return View(response.Data);
        }

        // POST: Amigo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:5001/api/amigos/" + id, DataFormat.Json);
                var response = client.Delete<Amigo>(request);

                return Redirect("/amigo");
            }
            catch
            {
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
            var destinoDaImagemNaNuvem =  blob.Uri.ToString();
            return destinoDaImagemNaNuvem;
        }
    }
}