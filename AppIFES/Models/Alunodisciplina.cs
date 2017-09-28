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
        [Required(ErrorMessage = "Informe um Aluno", AllowEmptyStrings = false)]
        public Int32 iddisciplina { get; set; }

        [Required(ErrorMessage = "Informe um Aluno", AllowEmptyStrings = false)]
        [ForeignKey("iddisciplina")]
        public Disciplina disciplina { get; set; }

        [Key]
        [Display(Name = "Aluno")]
        [Column(Order = 1)]
        [Required(ErrorMessage = "Informe um Aluno", AllowEmptyStrings = false)]
        public Int32 idaluno { get; set; }

        [Required(ErrorMessage = "Informe um Aluno", AllowEmptyStrings = false)]
        [ForeignKey("idaluno")]
        public Aluno aluno { get; set; }        
    }
}