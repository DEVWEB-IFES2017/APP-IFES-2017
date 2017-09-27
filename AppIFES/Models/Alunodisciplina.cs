using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppIFES.Models
{
    [Table("alunodisciplina")]
    public class Alunodisciplina
    {
        [Key]
        [Display(Name = "Grupo")]
        [Column(Order = 0)]
        public Int32 iddisciplina { get; set; }

        [ForeignKey("iddisciplina")]
        public Disciplina disciplina { get; set; }

        [Key]
        [Display(Name = "Aluno")]
        [Column(Order = 1)]
        public Int32 idaluno { get; set; }

        [ForeignKey("idaluno")]
        public Aluno aluno { get; set; }        
    }
}