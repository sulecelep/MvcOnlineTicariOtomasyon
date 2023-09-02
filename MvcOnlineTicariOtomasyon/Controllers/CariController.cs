using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        Context context = new Context();    
        // GET: Cari
        public ActionResult Index()
        {
            var values = context.Carilers.Where(x=>x.Durum==true).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CariEkle(Cariler cari)
        {
            cari.Durum = true;
            context.Carilers.Add(cari);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariSil(int id)
        {
            var value = context.Carilers.Find(id);
            value.Durum = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CariGuncelle(int id)
        {
            var value = context.Carilers.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult CariGuncelle(Cariler cari)
        {
            if(!ModelState.IsValid)
            {
                return View("CariGuncelle");
            }
            var value = context.Carilers.Find(cari.Carilerid);
            value.CarilerAd = cari.CarilerAd;
            value.CarilerSoyad = cari.CarilerSoyad;
            value.CarilerSehir = cari.CarilerSehir;
            value.CarilerMail = cari.CarilerMail;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CariSatis(int id)
        {
            var values = context.SatisHarekets.Where(x => x.Carilerid == id).ToList();
            var cari = context.Carilers.Where(x => x.Carilerid == id).Select(y => y.CarilerAd + " " + y.CarilerSoyad).FirstOrDefault();
            ViewBag.cari = cari;
            return View(values);
        }
    }
}