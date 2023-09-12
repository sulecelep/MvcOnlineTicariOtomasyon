using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {

        Context context = new Context();
        // GET: Satis
        public ActionResult Index()
        {
            var values= context.SatisHarekets.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> urunler = (from x in context.Uruns.ToList()
                                         select new SelectListItem
                                         {
                                             Text= x.UrunAd,
                                             Value= x.Urunid.ToString()
                                         }).ToList();
            

            List<SelectListItem> cariler = (from x in context.Carilers.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.CarilerAd+" "+x.CarilerSoyad,
                                                Value = x.Carilerid.ToString()
                                            }).ToList();

            List<SelectListItem> personeller = (from x in context.Personels.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                Value = x.Personelid.ToString()
                                            }).ToList();


            ViewBag.Urunler = urunler;
            ViewBag.Cariler = cariler;
            ViewBag.Personeller = personeller;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket satisHareket)
        {
            satisHareket.Tarih=DateTime.Parse(DateTime.Now.ToShortDateString());
            context.SatisHarekets.Add(satisHareket);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult SatisGuncelle(int id)
        {
            List<SelectListItem> urunler = (from x in context.Uruns.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.UrunAd,
                                                Value = x.Urunid.ToString()
                                            }).ToList();


            List<SelectListItem> cariler = (from x in context.Carilers.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.CarilerAd + " " + x.CarilerSoyad,
                                                Value = x.Carilerid.ToString()
                                            }).ToList();

            List<SelectListItem> personeller = (from x in context.Personels.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                    Value = x.Personelid.ToString()
                                                }).ToList();


            ViewBag.Urunler = urunler;
            ViewBag.Cariler = cariler;
            ViewBag.Personeller = personeller;
            var value = context.SatisHarekets.Find(id);
            
            return View(value);
        }
        [HttpPost]
        public ActionResult SatisGuncelle(SatisHareket p)
        {
            var value= context.SatisHarekets.Find(p.SatisHareketid);
            value.Carilerid = p.Carilerid;
            value.Adet = p.Adet;
            value.Fiyat = p.Fiyat;
            value.Personelid = p.Personelid;
            value.Tarih = p.Tarih;
            value.ToplamTutar = p.ToplamTutar;
            value.Urunid = p.Urunid;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var values= context.SatisHarekets.Where(x=>x.SatisHareketid==id).ToList(); 
            return View(values);
        }
    }
}