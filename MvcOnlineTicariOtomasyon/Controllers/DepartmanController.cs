using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        Context context = new Context();
        // GET: Departman

        public ActionResult Index()
        {
            var values = context.Departmans.Where(x=>x.Durum==true).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DepartmanEkle(Departman departman)
        {
            departman.Durum = true; 
            context.Departmans.Add(departman);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanSil(int id)
        {
            var value = context.Departmans.Find(id);
            value.Durum = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DepartmanGuncelle(int id)
        {
            var value = context.Departmans.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult DepartmanGuncelle(Departman departman)
        {
            var value = context.Departmans.Find(departman.Departmanid);
            value.DepartmanAd = departman.DepartmanAd;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DepartmanDetay(int id)
        {
            var values = context.Personels.Where(x => x.Personelid == id).ToList();
            var departman=context.Departmans.Where(x=>x.Departmanid == id).Select(y=>y.DepartmanAd).FirstOrDefault();
            ViewBag.departman = departman;
            return View(values);
        }
        [HttpGet]
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var values = context.SatisHarekets.Where(x => x.Personelid == id).ToList();
            var personel = context.Personels.Where(x => x.Personelid == id).Select(y=>y.PersonelAd+" "+y.PersonelSoyad).FirstOrDefault();
            ViewBag.personel = personel;
            return View(values);
        }
    }
}