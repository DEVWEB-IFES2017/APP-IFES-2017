using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppIFES.Models
{
    [Table("simnao")]
    public class Simnao
    {
        [Key]
        [Column(Order = 0)]
        public Int32 ativo { get; set; }
        [Display(Name = "Descrição")]
        [Column(Order = 1)]
        public string descricao { get; set; }
    }

}