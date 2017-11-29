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
    public class AgendaApiController : ApiController
    {
        private DadosBanco db = new DadosBanco();

        // GET: api/AgendaApi
        public IQueryable<Agenda> GetAgenda()
        {
            return db.Agenda;
        }

        // GET: api/AgendaApi/5
        [ResponseType(typeof(Agenda))]
        public IHttpActionResult GetAgenda(int id)
        {
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return NotFound();
            }

            return Ok(agenda);
        }


        // PUT: api/AgendaApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAgenda(int id, Agenda agenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agenda.idagenda)
            {
                return BadRequest();
            }

            db.Entry(agenda).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgendaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AgendaApi
        [ResponseType(typeof(Agenda))]
        public IHttpActionResult PostAgenda(Agenda agenda)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Agenda.Add(agenda);
            db.SaveChanges();
            //RedirectToAction("Adicionar", "GoogleCalendar", new { idagenda = agenda.idagenda, date = agenda.dataevento, titulo = agenda.titulo, descricao = agenda.descricao, local = agenda.local });
            return CreatedAtRoute("DefaultApi", new { id = agenda.idagenda }, agenda);
        }

        // DELETE: api/AgendaApi/5
        [ResponseType(typeof(Agenda))]
        public IHttpActionResult DeleteAgenda(int id)
        {
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return NotFound();
            }

            db.Agenda.Remove(agenda);
            db.SaveChanges();

            return Ok(agenda);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AgendaExists(int id)
        {
            return db.Agenda.Count(e => e.idagenda == id) > 0;
        }
    }
}