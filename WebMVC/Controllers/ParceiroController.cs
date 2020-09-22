using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace WebMVC.Controllers
{
    public class ParceiroController : Controller
    {
        // GET: Parceiro
        public ActionResult Index()
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/parceiros", DataFormat.Json);
            var response = client.Get<List<Parceiro>>(request);

            return View(response.Data);        
        }

        // GET: Parceiro/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Parceiro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Parceiro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ParceiroResponse parceiro)
        {
            if (ModelState.IsValid) {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:5001/api/parceiros", DataFormat.Json);
                request.AddJsonBody(parceiro);
                var response = client.Post<Parceiro>(request);


                return Redirect("/amigo/index");
            }
            return BadRequest();
        }

        // GET: Parceiro/Edit/5
        public ActionResult Edit(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/parceiros/" + id, DataFormat.Json);
            var response = client.Get<Parceiro>(request);

            return View(response.Data);
        }

        // POST: Parceiro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Parceiro parceiroN)
        {
            try {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:5001/api/parceiros/" + id, DataFormat.Json);
                request.AddJsonBody(parceiroN);
                var response = client.Put<Parceiro>(request);

                return Redirect("/amigo/index");
            } catch {
                return View();
            }
        }

        // GET: Parceiro/Delete/5
        public ActionResult Delete(int id)
        {
            var client = new RestClient();

            var request = new RestRequest("https://localhost:5001/api/parceiros/" + id, DataFormat.Json);
            var response = client.Get<Parceiro>(request);

            return View(response.Data);
        }

        // POST: Parceiro/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try {
                var client = new RestClient();

                var request = new RestRequest("https://localhost:5001/api/parceiros/" + id, DataFormat.Json);
                var response = client.Delete<Parceiro>(request);

                return Redirect("/amigo");
            } catch {
                return View();
            }
        }
    }
}