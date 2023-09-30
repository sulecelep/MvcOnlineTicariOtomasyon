﻿using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            var grafikciz = new Chart(600, 600);
            grafikciz.AddTitle("Kategori - Ürün Stok Sayısı").AddLegend("Stok")
                .AddSeries("Değerler", xValue: new[] { "Mobilya","Ofis Eşyaları","Bilgisayar" }, yValues: new[] {85,66,98}).Write();
            return File(grafikciz.ToWebImage().GetBytes(),"image/jpeg");
        }
        public ActionResult Index3()
        {
            ArrayList xvalue= new ArrayList();
            ArrayList yvalue= new ArrayList();
            var sonuclar = c.Uruns.ToList();
            sonuclar.ToList().ForEach(x=>xvalue.Add(x.UrunAd));
            sonuclar.ToList().ForEach(y=>yvalue.Add(y.Stok));
            var grafik= new Chart(600, 600).AddTitle("Stoklar")
                .AddSeries(chartType:"Pie", name:"Stok", xValue:xvalue,yValues:yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");

        }

        public ActionResult PieChart() 
        {
            return View();
        }
        public ActionResult ColumnChart() 
        {
            return View();
        }
        public ActionResult LineChart() 
        {
            return View();
        }

        public ActionResult VisualizeUrunResult()
        {
            return Json(Urunlistesi(), JsonRequestBehavior.AllowGet);
        }
        public List<Sinif1> Urunlistesi()
        {
            List<Sinif1> snf = new List<Sinif1>();
            snf.Add(new Sinif1()
            {
                urunad="Bilgisayar",
                stok=120
            });
            snf.Add(new Sinif1()
            {
                urunad = "Beyaz Eşya",
                stok = 150
            });
            snf.Add(new Sinif1()
            {
                urunad = "Mobilya",
                stok = 100
            });
            snf.Add(new Sinif1()
            {
                urunad = "Küçük Ev Aletleri",
                stok = 180
            });
            snf.Add(new Sinif1()
            {
                urunad = "Mobil Cihazlar",
                stok = 90
            });
            return snf;
        }
        public ActionResult Index5()
        {
            return View();
        }
        public ActionResult Index6()
        {
            return View();
        }
        public ActionResult Index7()
        {
            return View();
        }
        public ActionResult VisualizeUrunResult2()
        {
            return Json(Urunlistesi2(), JsonRequestBehavior.AllowGet);
        }
        public List<Sinif2> Urunlistesi2()
        {
            List<Sinif2> snf = new List<Sinif2>();
            using (var c = new Context())
            {
                snf = c.Uruns.Select(x => new Sinif2
                {
                    urn = x.UrunAd,
                    stk = x.Stok,
                }).ToList();
            };
            return snf;
        }
    }
}