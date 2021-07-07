using AdventureWorks.App_Start;
using AdventureWorks.DAL;
using AdventureWorks.Models;
using AdventureWorks.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace AdventureWorks.Controllers
{
    public class StavkaController : ApiController
    {
        UnitOfWork unitOfWork = new UnitOfWork();
        [HttpGet]
        public IHttpActionResult GetStavke(int id)
        {
            Racun racun = unitOfWork.Racuni.GetByID(id);
            IEnumerable<Stavka> stavke = unitOfWork.Stavke.Get(s => s.RacunID == id).ToList();
            List<StavkaRacun> stavkeRacuni = new List<StavkaRacun>();
            stavke.ToList().ForEach(s => stavkeRacuni.Add(new StavkaRacun
            {
                IDStavka = s.IDStavka,
                Proizvod = s.Proizvod,
                Kategorija = s.Proizvod.Potkategorija.Kategorija,
                Potkategorija = s.Proizvod.Potkategorija,
                Komercijalist = racun.Komercijalist,
                KreditnaKartica = racun.KreditnaKartica
            }));
            var stavkeDTO = AutoMapperConfig.Mapper.Map<IEnumerable<StavkaDTO>>(stavkeRacuni);
            if (stavkeDTO == null) return NotFound();
            return Ok(stavkeDTO);
        }
    }
}
