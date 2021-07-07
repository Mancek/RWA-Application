using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdventureWorks.Models;

namespace AdventureWorks.DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {

        private AdventureWorksContext _dbContext = new AdventureWorksContext();
        SqlRepository<Drzava> _drzave;
        SqlRepository<Grad> _gradovi;
        SqlRepository<Kategorija> _kategorije;
        SqlRepository<Komercijalist> _komercijalisti;
        SqlRepository<KreditnaKartica> _kreditneKartice;
        SqlRepository<Kupac> _kupci;
        SqlRepository<Potkategorija> _potkategorije;
        SqlRepository<Proizvod> _proizvodi;
        SqlRepository<Racun> _racuni;
        SqlRepository<Stavka> _stavke;

        public IRepository<Drzava> Drzave
        {
            get => _drzave ?? (_drzave = new SqlRepository<Drzava>(_dbContext));
        }

        public IRepository<Grad> Gradovi
        {
            get => _gradovi ?? (_gradovi = new SqlRepository<Grad>(_dbContext));
        }

        public IRepository<Kategorija> Kategorije
        {
            get => _kategorije ?? (_kategorije = new SqlRepository<Kategorija>(_dbContext));
        }

        public IRepository<Komercijalist> Komercijalisti
        {
            get => _komercijalisti ?? (_komercijalisti = new SqlRepository<Komercijalist>(_dbContext));
        }
        public IRepository<KreditnaKartica> KreditneKartice
        {
            get => _kreditneKartice ?? (_kreditneKartice = new SqlRepository<KreditnaKartica>(_dbContext));
        }

        public IRepository<Kupac> Kupci
        {
            get => _kupci ?? (_kupci = new SqlRepository<Kupac>(_dbContext));
        }
        public IRepository<Potkategorija> Potkategorije
        {
            get => _potkategorije ?? (_potkategorije = new SqlRepository<Potkategorija>(_dbContext));
        }
        public IRepository<Proizvod> Proizvodi
        {
            get => _proizvodi ?? (_proizvodi = new SqlRepository<Proizvod>(_dbContext));
        }
        public IRepository<Racun> Racuni
        {
            get => _racuni ?? (_racuni = new SqlRepository<Racun>(_dbContext));
        }
        public IRepository<Stavka> Stavke
        {
            get => _stavke ?? (_stavke = new SqlRepository<Stavka>(_dbContext));
        }

        public void Commit() => _dbContext.SaveChanges();

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}