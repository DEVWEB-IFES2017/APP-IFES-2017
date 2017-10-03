using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;


namespace AppIFES.Models
{
    [Table("agenda")]
    public class Agenda
    {
        [Key]
        [Display(Name = "Código da Agenda")]
        [Column(Order = 0)]
        public Int32 idagenda { get; set; }

        [Display(Name = "Código da Disciplina")]
        [Column(Order = 1)]
        public Int32 iddisciplina { get; set; }

        [ForeignKey("iddisciplina")]
        public Disciplina Disciplina { get; set; }

        [Display(Name = "Data do Evento")]
        [Column(Order = 2)]
        public DateTime dataevento { get; set; }

        [Display(Name = "Título")]
        [Column(Order = 3)]
        [StringLength(30, ErrorMessage = "No máximo são 30 caracteres.")]
        [Required(ErrorMessage = "Digite o título do evento.")]
        public string titulo { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(300, ErrorMessage = "No máximo são 300 caracteres.")]
        [Column(Order = 4)]
        [Required(ErrorMessage = "Informe a descrição do evento", AllowEmptyStrings = false)]
        public string descricao { get; set; }

        [Display(Name = "Local")]
        [StringLength(150, ErrorMessage = "No máximo são 150 caracteres.")]
        [Column(Order = 5)]
        [Required(ErrorMessage = "Informe o local do evento")]
        public string local { get; set; }

    

    }
}