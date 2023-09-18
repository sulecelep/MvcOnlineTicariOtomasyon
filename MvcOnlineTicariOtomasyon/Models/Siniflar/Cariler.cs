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

        [Column(TypeName = "Nvarchar")]
        [StringLength(30,ErrorMessage ="En fazla 30 karakter girişi yapabilirsiniz")]
        public string CarilerAd { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(30)]
        [Required(ErrorMessage ="Bu alanı boş geçemezsiniz.")]
        public string CarilerSoyad { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(15)]
        public string CarilerSehir { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(50)]
        public string CarilerMail { get; set; }

        [Column(TypeName = "Nvarchar")]
        [StringLength(50)]
        public string Sifre { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }
        public bool Durum { get; set; }
    }
}