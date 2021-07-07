using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdventureWorks.Models;

namespace AdventureWorks.DAL
{
    public interface IUnitOfWork
    {
        IRepository<Drzava> Drzave { get; }
        IRepository<Grad> Gradovi { get; }
        IRepository<Kategorija> Kategorije { get; }
        IRepository<Komercijalist> Komercijalisti { get; }
        IRepository<KreditnaKartica> KreditneKartice { get; }
        IRepository<Kupac> Kupci { get; }
        IRepository<Potkategorija> Potkategorije { get; }
        IRepository<Proizvod> Proizvodi { get; }
        IRepository<Racun> Racuni { get; }
        IRepository<Stavka> Stavke { get; }

        void Commit();
    }
}