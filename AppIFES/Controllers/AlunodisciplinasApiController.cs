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
    public class AlunodisciplinasApiController : ApiController
    {
        private DadosBanco db = new DadosBanco();

        // GET: api/AlunodisciplinasApi/5
        [ResponseType(typeof(Disciplina))]
        public IHttpActionResult GetAlunodisciplina(int id)
        {
            List<Disciplina> disciplina = (from disc in db.Disciplinas
                                           join aldi in db.Alunodisciplinas on disc.iddisciplina equals aldi.iddisciplina
                                           where disc.iddisciplina == aldi.iddisciplina && aldi.idaluno == id
                                          select disc).ToList();

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

        private bool AlunodisciplinaExists(int id)
        {
            return db.Alunodisciplinas.Count(e => e.iddisciplina == id) > 0;
        }
    }
}