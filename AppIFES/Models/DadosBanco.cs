using System.Data.Entity;

namespace AppIFES.Models
{
    public class DadosBanco : DbContext
    {
        public DadosBanco(): base("name=ConfBanco")
        {
        }
        public System.Data.Entity.DbSet<AppIFES.Models.Usuario> Usuarios { get; set; }
        public System.Data.Entity.DbSet<AppIFES.Models.Simnao> Simnaos { get; set; }
        public System.Data.Entity.DbSet<AppIFES.Models.Aluno> Alunoes { get; set; }

        public System.Data.Entity.DbSet<AppIFES.Models.Disciplina> Disciplinas { get; set; }
    }
}