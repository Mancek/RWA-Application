using AdventureWorks.DAL;
using AdventureWorks.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorks.Models
{

    public class ControllerRepository
    {
        private static UnitOfWork unitOfWork = new UnitOfWork();

        public static void EditProizvod(ProizvodVM p)
        {
            Proizvod proizvod = unitOfWork.Proizvodi.GetByID(p.IDProizvod);
            proizvod.Naziv = p.Naziv;
            proizvod.BrojProizvoda = p.BrojProizvoda;
            proizvod.Boja = p.Boja;
            proizvod.MinimalnaKolicinaNaSkladistu = p.MinimalnaKolicinaNaSkladistu;
            proizvod.CijenaBezPDV = p.CijenaBezPDV;
            proizvod.PotkategorijaID = p.PotkategorijaID;
            unitOfWork.Proizvodi.Update(proizvod);
            unitOfWork.Commit();
        }

        public static void DeleteProizvod(int id)
        {
            Proizvod proizvod = unitOfWork.Proizvodi.GetByID(id);
            IEnumerable<Stavka> stavke = unitOfWork.Stavke.Get(s => s.ProizvodID == proizvod.IDProizvod);
            stavke.ToList().ForEach(s => unitOfWork.Stavke.Delete(s));
            unitOfWork.Proizvodi.Delete(proizvod);
            unitOfWork.Commit();
        }

        public static void EditKategorija(Kategorija k)
        {
            Kategorija kategorija = unitOfWork.Kategorije.GetByID(k.IDKategorija);
            kategorija.Naziv = k.Naziv;
            unitOfWork.Kategorije.Update(kategorija);
            unitOfWork.Commit();
        }
        public static void DeleteKategorija(int id)
        {
            Kategorija kategorija = unitOfWork.Kategorije.GetByID(id);
            IEnumerable<Potkategorija> potkategorije = unitOfWork.Potkategorije.Get(p => p.KategorijaID == kategorija.IDKategorija);
            potkategorije.ToList().ForEach(p =>
            {
                IEnumerable<Proizvod> proizvodi = unitOfWork.Proizvodi.Get(pr => pr.PotkategorijaID == p.IDPotkategorija);
                proizvodi.ToList().ForEach(pr =>
                {
                    IEnumerable<Stavka> stavke = unitOfWork.Stavke.Get(s => s.ProizvodID == pr.IDProizvod);
                    stavke.ToList().ForEach(s => unitOfWork.Stavke.Delete(s));
                });
                proizvodi.ToList().ForEach(pr => unitOfWork.Proizvodi.Delete(pr));
            });
            potkategorije.ToList().ForEach(p => unitOfWork.Proizvodi.Delete(p));
            unitOfWork.Kategorije.Delete(kategorija);
            unitOfWork.Commit();
        }

        public static void EditPotkategorija(PotkategorijaVM p)
        {
            Potkategorija potkategorija = unitOfWork.Potkategorije.GetByID(p.IDPotkategorija);
            potkategorija.Naziv = p.Naziv;
            potkategorija.KategorijaID = p.KategorijaID;
            unitOfWork.Potkategorije.Update(potkategorija);
            unitOfWork.Commit();
        }
        public static void DeletePotkategorija(int id)
        {
            Potkategorija potkategorija = unitOfWork.Potkategorije.GetByID(id);
            IEnumerable<Proizvod> proizvodi = unitOfWork.Proizvodi.Get(p => p.PotkategorijaID == potkategorija.IDPotkategorija);
            proizvodi.ToList().ForEach(p =>
            {
                IEnumerable<Stavka> stavke = unitOfWork.Stavke.Get(s => s.ProizvodID == p.IDProizvod);
                stavke.ToList().ForEach(s => unitOfWork.Stavke.Delete(s));
            });
            proizvodi.ToList().ForEach(p => unitOfWork.Proizvodi.Delete(p));
            unitOfWork.Potkategorije.Delete(potkategorija);
            unitOfWork.Commit();
        }
    }
}