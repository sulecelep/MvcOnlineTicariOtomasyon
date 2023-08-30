using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MvcOnlineTicariOtomasyon.Models.Siniflar
{
    public class Cariler
    {
        [Key]
        public int Carilerid { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string CarilerAd { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string CarilerSoyad { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(15)]
        public string CarilerSehir { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string CarilerMail { get; set; }
        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}