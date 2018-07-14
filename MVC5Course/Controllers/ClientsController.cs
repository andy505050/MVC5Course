using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    [RoutePrefix("xxx")]
    public class ClientsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();
        ClientRepository repo;
        OccupationRepository occup;

        public ClientsController()
        {
            repo = RepositoryHelper.GetClientRepository();
            occup = RepositoryHelper.GetOccupationRepository(repo.UnitOfWork);
        }
        // GET: Clients
        [Route("Index")]
        public ActionResult Index()
        {
            var client = repo.All();
            return View(client.Take(10).OrderByDescending(x => x.ClientId).ToList());
        }
        [Route("S")]
        public ActionResult Search(string Keyword)
        {
            var data = repo.Search(Keyword);

            return View("Index", data.Take(10));
        }
        [Route("D/{id}")]
        // GET: Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }
        [Route("C")]
        // GET: Clients/Create
        public ActionResult Create()
        {

            ViewBag.OccupationId = new SelectList(occup.All(), "OccupationId", "OccupationName");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("C")]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes,IDnumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                repo.Add(client);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.OccupationId = new SelectList(occup.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Edit/5
        [Route("E/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.OccupationId = new SelectList(occup.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("E/{id}")]
        public ActionResult Edit(int id, FormCollection form)
        {

            var client = repo.Find(id);
            if(TryUpdateModel(client,"",null,new string[] { "FirstName" }))
            {
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
  
            ViewBag.OccupationId = new SelectList(occup.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Delete/5
        [Route("DD/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = repo.Find(id.Value);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("DD")]
        [ValidateAntiForgeryToken]
        [Route("DD/{id}")]
        public ActionResult DeleteConfirmed(int id)
        {
            Client client = repo.Find(id);
            repo.Delete(client);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("BacthUpdate")]
        public ActionResult BatchUpdate(List<ClientsBatchUpdateVM> data)
        {

            if (ModelState.IsValid)
            {

                foreach (var item in data)
                {
                    var client = repo.Find(item.ClientId);
                    client.FirstName = item.FirstName;
                    client.MiddleName = item.MiddleName;
                    client.LastName = item.LastName;
                }
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            ViewData.Model = repo.All().Take(10);
            return View("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
