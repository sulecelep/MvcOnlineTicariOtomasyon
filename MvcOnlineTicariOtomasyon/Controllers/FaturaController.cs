﻿using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        Context _context = new Context();
        // GET: Fatura
        public ActionResult Index()
        {
            var liste = _context.Faturalars.ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar fatura)
        {
            _context.Faturalars.Add(fatura);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult FaturaGuncelle(int id)
        {
            var value = _context.Faturalars.Find(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult FaturaGuncelle(Faturalar fatura)
        {
            var value = _context.Faturalars.Find(fatura.Faturaid);
            value.FaturaSeriNo = fatura.FaturaSeriNo;
            value.FaturaSıraNo = fatura.FaturaSıraNo;
            value.VergiDairesi = fatura.VergiDairesi;
            value.Tarih = fatura.Tarih;
            value.Saat = fatura.Saat;
            value.TeslimEden = fatura.TeslimEden;
            value.TeslimAlan = fatura.TeslimAlan;
            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult FaturaDetay(int id)
        {
            var values = _context.FaturaKalems.Where(x => x.Faturaid == id).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult FaturaKalemEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult FaturaKalemEkle(FaturaKalem faturaKalem)
        {
            _context.FaturaKalems.Add(faturaKalem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}