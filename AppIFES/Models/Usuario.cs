using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppIFES.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Key]
        [Display(Name = "Código")]
        [Column(Order = 0)]
        public Int32 idusuario { get; set; }

        [Display(Name = "Nome")]
        [StringLength(120, ErrorMessage = "No máximo são 120 caracteres.")]
        [Required(ErrorMessage = "Digite o nome do Usuário."), Column(Order = 1)]
        public string nome { get; set; }

        [Display(Name = "Email")]
        [StringLength(120, ErrorMessage = "No máximo são 120 caracteres.")]
        [Column(Order = 2)]
        [Required(ErrorMessage = "Informe um Email válido!", AllowEmptyStrings = false)]
        public string email { get; set; }

        [Display(Name = "Senha")]
        [StringLength(20, ErrorMessage = "No máximo são 20 caracteres.")]
        [Column(Order = 3)]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Informe uma senha válida!", AllowEmptyStrings = false)]
        public string senha { get; set; }

        [Column(Order = 4)]
        [Display(Name = "Supervisor")]
        public Int32 supervisor { get; set; }

        [ForeignKey("supervisor")]
        public Simnao simnao { get; set; }
        
        [ForeignKey("idusuario")]
        public ICollection<Disciplina> disciplinas { get; set; }
    }
}