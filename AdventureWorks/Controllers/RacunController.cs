using AdventureWorks.DAL;
using AdventureWorks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdventureWorks.Controllers
{
    public class RacunController : Controller
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        // GET: Racun
        public ActionResult Get(int id)
        {
            IEnumerable<Racun> model = unitOfWork.Racuni.Get(r => r.KupacID == id);
            return View(model);
        }
    }
}