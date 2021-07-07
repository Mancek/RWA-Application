using AdventureWorks.App_Start;
using AdventureWorks.DAL;
using AdventureWorks.Models;
using AdventureWorks.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdventureWorks.Controllers
{
    public class PotkategorijaController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Potkategorija
        public ActionResult Index()
        {
            IEnumerable<Potkategorija> model = unitOfWork.Potkategorije.Get();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Potkategorija potkategorija = unitOfWork.Potkategorije.GetByID(id);
            PotkategorijaVM potkategorijaVM = AutoMapperConfig.Mapper.Map<PotkategorijaVM>(potkategorija);
            potkategorijaVM.SveKategorije = unitOfWork.Kategorije.Get();
            return View(potkategorijaVM);
        }

        [HttpPost]
        public ActionResult Edit(PotkategorijaVM p)
        {
            ControllerRepository.EditPotkategorija(p);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                ControllerRepository.DeletePotkategorija(id);
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
            ViewBag.SveKategorije = unitOfWork.Kategorije.Get();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Potkategorija p)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Potkategorije.Insert(p);
                unitOfWork.Commit();
                return RedirectToAction("Index");
            }
            else
            {
                return View(p);
            }
        }
    }
}