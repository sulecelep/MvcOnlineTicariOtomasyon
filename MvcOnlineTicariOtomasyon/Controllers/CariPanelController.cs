using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        Context context=new Context();
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {

            var mail = (string)Session["CariMail"];
            var degerler = context.Mesajlars.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail;
            var cariid = context.Carilers.Where(x => x.CarilerMail == mail).Select(y => y.Carilerid).FirstOrDefault();
            var toplamSatis = context.SatisHarekets.Where(x => x.Carilerid == cariid).Count();
            ViewBag.toplamSatis = toplamSatis;
            var toplamTutar = context.SatisHarekets.Where(x => x.Carilerid == cariid).Sum(y => y.ToplamTutar);
            ViewBag.toplamTutar = toplamTutar;
            var toplamUrun = context.SatisHarekets.Where(x => x.Carilerid == cariid).Sum(y => y.Adet);
            ViewBag.toplamUrun = toplamUrun;
            var adsoyad = context.Carilers.Where(x => x.CarilerMail == mail).Select(y => y.CarilerAd + " " + y.CarilerSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var email = context.Carilers.Where(x => x.CarilerMail == mail).Select(y => y.CarilerMail).FirstOrDefault();
            ViewBag.email = email;

            return View(degerler);
        }
        [HttpGet]
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id=context.Carilers.Where(x=>x.CarilerMail==mail).Select(y=>y.Carilerid).FirstOrDefault();
            var degerler = context.SatisHarekets.Where(x=>x.Carilerid==id).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar= context.Mesajlars.Where(x => x.Alici == mail).OrderByDescending(x=>x.MesajlarID).ToList();
            var gelensayisi= context.Mesajlars.Count(x => x.Alici == mail).ToString();
            var gidensayisi = context.Mesajlars.Count(x => x.Gonderici == mail).ToString();

            ViewBag.d1=gelensayisi;
            ViewBag.d2= gidensayisi;
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = context.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x => x.MesajlarID).ToList();
            var gelensayisi = context.Mesajlars.Count(x => x.Alici == mail).ToString();
            var gidensayisi = context.Mesajlars.Count(x => x.Gonderici == mail).ToString();

            ViewBag.d1 = gelensayisi;
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);
        }
        [HttpGet]
        public ActionResult MesajDetay(int id)
        {
            var mail = (string)Session["CariMail"];
            var degerler = context.Mesajlars.Where(x => x.MesajlarID == id).ToList();
            var gelensayisi = context.Mesajlars.Count(x => x.Alici == mail).ToString();
            var gidensayisi = context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            ViewBag.d2 = gidensayisi;
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gelensayisi = context.Mesajlars.Count(x => x.Alici == mail).ToString();
            var gidensayisi = context.Mesajlars.Count(x => x.Gonderici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            ViewBag.d2 = gidensayisi;
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar m)
        {
            var mail = (string)Session["CariMail"];
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gonderici = mail; 
            context.Mesajlars.Add(m);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult KargoTakip(string p)
        {
            var kargo = from x in context.KargoDetays select x;
          
            kargo = kargo.Where(y => y.TakipKodu.Contains(p));
           
            return View(kargo.ToList());
        }
        [HttpGet]
        public ActionResult CariKargoTakip(string id)
        {
            var values = context.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            ViewBag.takipkodu = id;
            return View(values);
        }
        public PartialViewResult Partial1()
        {
            var mail = (string)Session["CariMail"];
            var id = context.Carilers.Where(x => x.CarilerMail == mail).Select(y => y.Carilerid).FirstOrDefault();

            var cari = context.Carilers.Find(id);
            return PartialView("Partial1", cari);
        }
        public PartialViewResult Duyuru()
        {
            var mail = (string)Session["CariMail"];
            var veriler = context.Mesajlars.Where(x => x.Gonderici == mail).OrderByDescending(x=>x.Tarih).ToList();
            return PartialView(veriler);
        }
        [HttpPost]
        public ActionResult CariBilgiGuncelle(Cariler cariler)
        {
            var cari = context.Carilers.Find(cariler.Carilerid);
            cari.CarilerAd=cariler.CarilerAd;
            cari.CarilerSoyad=cariler.CarilerSoyad;
            cari.CarilerMail=cariler.CarilerMail;
            cari.CarilerSehir=cariler.CarilerSehir;
            cari.Sifre=cariler.Sifre;

            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}