using AdventureWorks.DAL;
using AdventureWorks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdventureWorks.Controllers
{
    public class KategorijaController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Kategorija
        public ActionResult Index()
        {
            IEnumerable<Kategorija> model = unitOfWork.Kategorije.Get();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Kategorija kategorija = unitOfWork.Kategorije.GetByID(id);
            return View(kategorija);
        }

        [HttpPost]
        public ActionResult Edit(Kategorija k)
        {
            ControllerRepository.EditKategorija(k);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                ControllerRepository.DeleteKategorija(id);
                return new HttpStatusCodeResult(HttpStatusCode.OK, "Uspjesno brisanje.");
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Neuspjesno brisanje.");
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Kategorija k)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Kategorije.Insert(k);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View(k);
            }
        }
    }
}