using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        Context c =new Context();
        // GET: Urun
        public ActionResult Index(string p)
        {
            var urunler = from x in c.Uruns select x;
            if(!string.IsNullOrEmpty(p))
            {
                urunler=urunler.Where(y=>y.UrunAd.Contains(p));
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();

            List<SelectListItem> deger2 = new List<SelectListItem>()
            {
                new SelectListItem { Text="Aktif", Value= "True" },
                new SelectListItem { Text="Pasif", Value= "False" }

            };

            ViewBag.kategoriList = deger1;
            ViewBag.durumList = deger2;

            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun urun)
        {
            urun.Durum = true;
            c.Uruns.Add(urun);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var value=c.Uruns.Find(id);
            value.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UrunGuncelle(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();

            List<SelectListItem> deger2 = new List<SelectListItem>()
            {
                new SelectListItem { Text="Aktif", Value= "True" },
                new SelectListItem { Text="Pasif", Value= "False" }

            };

            ViewBag.kategoriList = deger1;
            ViewBag.durumList = deger2;
            var value = c.Uruns.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UrunGuncelle(Urun urun)
        {
            var value = c.Uruns.Find(urun.Urunid);
            value.AlisFiyat = urun.AlisFiyat;
            value.SatisFiyat =urun.SatisFiyat;
            value.Durum = urun.Durum;
            value.Stok = urun.Stok;
            value.Kategoriid = urun.Kategoriid;
            value.Marka = urun.Marka;
            value.UrunAd = urun.UrunAd;
            value.UrunGorsel = urun.UrunGorsel;
            
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
        {
            var values = c.Uruns.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {

            List<SelectListItem> personeller = (from x in c.Personels.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                    Value = x.Personelid.ToString()
                                                }).ToList();



            ViewBag.Personeller = personeller;
            var urun = c.Uruns.Find(id);
            ViewBag.urunAd = urun.UrunAd;
            ViewBag.urunId = urun.Urunid;
            ViewBag.urunSatisFiyat = urun.SatisFiyat;
            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {
            p.Tarih= DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index","Satis");
        }
    }
}