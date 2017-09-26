using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppIFES.Models
{
    [Table("disciplina")]
    public class Disciplina
    {
        [Key]
        [Display(Name = "Disciplina")]
        [Column(Order = 0)]
        public Int32 iddisciplina { get; set; }

        [Display(Name = "Disciplina")]
        [StringLength(80, ErrorMessage = "No máximo são 80 caracteres.")]
        [Required(ErrorMessage = "Digite o nome da Disciplina."), Column(Order = 1)]
        public string descricao { get; set; }

        [Display(Name = "Usuário")]
        [Column(Order = 2)]
        public Int32 idusuario { get; set; }

        [ForeignKey("idusuario")]
        public Usuario usuario { get; set; }

    }
}