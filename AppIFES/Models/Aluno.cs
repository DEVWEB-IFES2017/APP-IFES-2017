using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using GridMvc.DataAnnotations;

namespace AppIFES.Models
{
    [Table("aluno")]
    [GridTable(PagingEnabled = true, PageSize = 2)]
    public class Aluno
    {
        [Key]
        [Display(Name = "Código")]
        [Column(Order = 0)]
        public Int32 idaluno { get; set; }

        [Display(Name = "Nome")]
        [StringLength(120, ErrorMessage = "No máximo são 120 caracteres.")]
        [Required(ErrorMessage = "Digite o nome do aluno."), Column(Order = 1)]
        public string nome { get; set; }

        [Display(Name = "Email")]
        [StringLength(120, ErrorMessage = "No máximo são 120 caracteres.")]
        [Column(Order = 2)]
        [Required(ErrorMessage = "Informe um Email válido!", AllowEmptyStrings = false)]
        public string email { get; set; }

        [ForeignKey("idaluno")]
        public ICollection<Alunodisciplina> alunodiciplinas { get; set; }

    }
}