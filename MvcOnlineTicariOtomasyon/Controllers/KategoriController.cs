using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index(int sayfa=1)
        {
            var values= c.Kategoris.ToList().ToPagedList(sayfa,6);
            return View(values);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();  
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori kategori)
        {
            c.Kategoris.Add(kategori);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id)
        {
            var value = c.Kategoris.Find(id);
            c.Kategoris.Remove(value);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult KategoriGuncelle(int id)
        {
            var value = c.Kategoris.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult KategoriGuncelle(Kategori kategori)
        {
            var value = c.Kategoris.Find(kategori.KategoriID);
            value.KategoriAd = kategori.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CascadeDropdown()
        {
            Cascade cascade = new Cascade();
            cascade.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cascade.Urunler = new SelectList(c.Uruns, "Urunid", "UrunAd");
            return View(cascade);
        }
        [HttpPost]
        public JsonResult UrunGetir(int p)
        {
            var urunlistesi = (from x in c.Uruns
                               join y in c.Kategoris
                               on x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID == p
                               select new
                               {
                                   Text=x.UrunAd,
                                   Value= x.Urunid.ToString()
                               }).ToList();
            return Json(urunlistesi, JsonRequestBehavior.AllowGet);
        }
    }
}