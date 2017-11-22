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
    public class AlunoesApiController : ApiController
    {
        private DadosBanco db = new DadosBanco();

        // GET: api/AlunoesApi
        public IQueryable<Aluno> GetAlunoes()
        {
            return db.Alunoes;
        }

        // GET: api/AlunoesApi/5
        [ResponseType(typeof(Aluno))]
        public IHttpActionResult PostLogin(Aluno aluno)
        {
            Aluno maluno = db.Alunoes.Where(a => a.email.Equals(aluno.email)).FirstOrDefault();

            if (maluno == null)
            {
                return NotFound();
            }

            return Ok(maluno);
        }
        //public IHttpActionResult GetAluno(int id)
        //{
        //    Aluno aluno = db.Alunoes.Find(id);
        //    if (aluno == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(aluno);
        //}

        // PUT: api/AlunoesApi/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutAluno(int id, Aluno aluno)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != aluno.idaluno)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(aluno).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!AlunoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/AlunoesApi
        //[ResponseType(typeof(Aluno))]
        //public IHttpActionResult PostAluno(Aluno aluno)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Alunoes.Add(aluno);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = aluno.idaluno }, aluno);
        //}

        //// DELETE: api/AlunoesApi/5
        //[ResponseType(typeof(Aluno))]
        //public IHttpActionResult DeleteAluno(int id)
        //{
        //    Aluno aluno = db.Alunoes.Find(id);
        //    if (aluno == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Alunoes.Remove(aluno);
        //    db.SaveChanges();

        //    return Ok(aluno);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlunoExists(int id)
        {
            return db.Alunoes.Count(e => e.idaluno == id) > 0;
        }
    }
}