using AdventureWorks.Models;
using AdventureWorks.Models.DTO;
using AdventureWorks.Models.VM;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdventureWorks.App_Start
{
    public static class AutoMapperConfig
    {
        public static IMapper Mapper { get; set; }

        public static void Init()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<StavkaRacun, StavkaDTO>()
                .ForMember(d => d.Stavka, a => a.MapFrom(s => s.IDStavka))
                .ForMember(d => d.Proizvod, a => a.MapFrom(s => s.Proizvod.Naziv))
                .ForMember(d => d.Potkategorija, a => a.MapFrom(s => s.Proizvod.Potkategorija.Naziv))
                .ForMember(d => d.Kategorija, a => a.MapFrom(s => s.Proizvod.Potkategorija.Kategorija.Naziv))
                .ForMember(d => d.Komercijalist, a => a.MapFrom(s => s.Komercijalist.ToString()))
                .ForMember(d => d.KreditnaKartica, a => a.MapFrom(s => s.KreditnaKartica.Tip));

                cfg.CreateMap<Proizvod, ProizvodVM>();
                cfg.CreateMap<ProizvodVM, Proizvod>();
                cfg.CreateMap<Potkategorija, PotkategorijaVM>();
                cfg.CreateMap<PotkategorijaVM, Potkategorija>();
            });

            Mapper = config.CreateMapper();
        }
    }
}