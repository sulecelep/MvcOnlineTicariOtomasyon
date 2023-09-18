﻿using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        Context context = new Context();    
        // GET: Personel
        public ActionResult Index()
        {
            var values= context.Personels.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in context.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.departmanList= deger1;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel personel)
        {
            context.Personels.Add(personel);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult PersonelGuncelle(int id)
        {
            List<SelectListItem> deger1 = (from x in context.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.Departmanid.ToString()
                                           }).ToList();
            ViewBag.departmanList = deger1;
            var value = context.Personels.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult PersonelGuncelle(Personel personel)
        {
            var value = context.Personels.Find(personel.Personelid);
            value.PersonelAd = personel.PersonelAd;
            value.PersonelSoyad = personel.PersonelSoyad;
            value.PersonelGorsel = personel.PersonelGorsel;
            value.Departmanid= personel.Departmanid;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelListe()
        {
            var sorgu = context.Personels.ToList();
            return View(sorgu);  
        }
    }
}