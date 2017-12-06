using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AppIFES.Models;

namespace AppIFES.Controllers
{
    public class DisciplinasApiController : ApiController
    {
        private DadosBanco db = new DadosBanco();

        // GET: api/DisciplinasApi/5
        [ResponseType(typeof(Disciplina))]
        public IHttpActionResult GetDisciplina(int idusuario)
        {
            List<Disciplina> disciplina = db.Disciplinas.Where( a => a.idusuario == idusuario).ToList();
            if (disciplina == null)
            {
                return NotFound();
            }

            return Ok(disciplina);
        }      

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DisciplinaExists(int id)
        {
            return db.Disciplinas.Count(e => e.iddisciplina == id) > 0;
        }
    }
}