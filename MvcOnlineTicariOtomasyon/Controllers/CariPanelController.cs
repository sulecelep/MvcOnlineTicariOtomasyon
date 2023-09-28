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
            var degerler= context.Carilers.FirstOrDefault(x=>x.CarilerMail == mail);
            ViewBag.m = mail;
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
    }
}