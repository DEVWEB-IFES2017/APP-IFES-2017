using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AppIFES.Models;

namespace AppIFES.Controllers
{
    public class DisciplinasController : Controller
    {
        private DadosBanco db = new DadosBanco();

        // GET: Disciplinas
        public ActionResult Index()
        {
            if ((Session["Userid"] != null) && Session["UserSupervisor"].Equals("1"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            var disciplinas = db.Disciplinas.Include(d => d.usuario);
            return View(disciplinas.ToList());
        }

        // GET: Disciplinas/Details/5
        public ActionResult Details(int? id)
        {
            if ((Session["Userid"] != null) && Session["UserSupervisor"].Equals("1"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disciplina disciplina = db.Disciplinas.Find(id);
            if (disciplina == null)
            {
                return HttpNotFound();
            }
            return View(disciplina);
        }

        // GET: Disciplinas/Create
        public ActionResult Create()
        {
            if ((Session["Userid"] != null) && Session["UserSupervisor"].Equals("1"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewBag.idusuario = new SelectList(db.Usuarios, "idusuario", "nome");
            return View();
        }

        // POST: Disciplinas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "iddisciplina,descricao,idusuario")] Disciplina disciplina)
        {
            if ((Session["Userid"] != null) && Session["UserSupervisor"].Equals("1"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (ModelState.IsValid)
            {
                db.Disciplinas.Add(disciplina);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idusuario = new SelectList(db.Usuarios, "idusuario", "nome", disciplina.idusuario);
            return View(disciplina);
        }

        // GET: Disciplinas/Edit/5
        public ActionResult Edit(int? id)
        {
            if ((Session["Userid"] != null) && Session["UserSupervisor"].Equals("1"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disciplina disciplina = db.Disciplinas.Find(id);
            if (disciplina == null)
            {
                return HttpNotFound();
            }
            ViewBag.idusuario = new SelectList(db.Usuarios, "idusuario", "nome", disciplina.idusuario);
            return View(disciplina);
        }

        // POST: Disciplinas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "iddisciplina,descricao,idusuario")] Disciplina disciplina)
        {
            if ((Session["Userid"] != null) && Session["UserSupervisor"].Equals("1"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (ModelState.IsValid)
            {
                db.Entry(disciplina).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idusuario = new SelectList(db.Usuarios, "idusuario", "nome", disciplina.idusuario);
            return View(disciplina);
        }

        // GET: Disciplinas/Delete/5
        public ActionResult Delete(int? id)
        {
            if ((Session["Userid"] != null) && Session["UserSupervisor"].Equals("1"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Disciplina disciplina = db.Disciplinas.Find(id);
            if (disciplina == null)
            {
                return HttpNotFound();
            }
            return View(disciplina);
        }

        // POST: Disciplinas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if ((Session["Userid"] != null) && Session["UserSupervisor"].Equals("1"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            Disciplina disciplina = db.Disciplinas.Find(id);
            db.Disciplinas.Remove(disciplina);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
