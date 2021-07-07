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
    public class ProizvodController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Proizvod
        public ActionResult Index()
        {
            IEnumerable<Proizvod> model = unitOfWork.Proizvodi.Get();
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Proizvod proizvod = unitOfWork.Proizvodi.GetByID(id);
            ProizvodVM proizvodVM = AutoMapperConfig.Mapper.Map<ProizvodVM>(proizvod);
            proizvodVM.SvePotkategorije = unitOfWork.Potkategorije.Get();
            return View(proizvodVM);
        }
        
        [HttpPost]
        public ActionResult Edit(ProizvodVM p)
        {
            ControllerRepository.EditProizvod(p);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            try
            {
                ControllerRepository.DeleteProizvod(id);
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
            ViewBag.SvePotkategorije = unitOfWork.Potkategorije.Get();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Proizvod p)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Proizvodi.Insert(p);
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