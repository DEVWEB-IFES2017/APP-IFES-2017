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
    public class AgendaController : Controller
    {
        private DadosBanco db = new DadosBanco();

        // GET: Agenda
        public ActionResult Index()
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            int idusuario = int.Parse(Session["Userid"].ToString());
            var agenda = db.Agenda.Include(a => a.Disciplina).Where(a =>a.Disciplina.idusuario == idusuario);
            return View(agenda.ToList());
        }

        // GET: Agenda/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // GET: Agenda/Create
        public ActionResult Create()
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewBag.iddisciplina = new SelectList(db.Disciplinas, "iddisciplina", "descricao");
            return View();
        }

        // POST: Agenda/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idagenda,iddisciplina,dataevento,titulo,descricao,local,idevento")] Agenda agenda)
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (ModelState.IsValid)
            {
                db.Agenda.Add(agenda);
                db.SaveChanges();

                return RedirectToAction("Adicionar", "GoogleCalendar", new { idagenda = agenda.idagenda, date = agenda.dataevento, titulo = agenda.titulo, descricao = agenda.descricao, local = agenda.local });
            }

            ViewBag.iddisciplina = new SelectList(db.Disciplinas, "iddisciplina", "descricao", agenda.iddisciplina);
            return View(agenda);
        }

        // GET: Agenda/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            ViewBag.iddisciplina = new SelectList(db.Disciplinas, "iddisciplina", "descricao", agenda.iddisciplina);
            return View(agenda);
        }

        // POST: Agenda/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idagenda,iddisciplina,dataevento,titulo,descricao,local,idevento")] Agenda agenda)
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (ModelState.IsValid)
            {
                db.Entry(agenda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Alterar", "GoogleCalendar", new { idagenda = agenda.idagenda, date = agenda.dataevento, titulo = agenda.titulo, descricao = agenda.descricao, local = agenda.local });
            }
            ViewBag.iddisciplina = new SelectList(db.Disciplinas, "iddisciplina", "descricao", agenda.iddisciplina);
            return View(agenda);
        }

        // GET: Agenda/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = db.Agenda.Find(id);
            if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // POST: Agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            Agenda agenda = db.Agenda.Find(id);
            string idEvento = agenda.idevento;

            db.Agenda.Remove(agenda);
            db.SaveChanges();

            return RedirectToAction("Apagar", "GoogleCalendar", new { idevento = idEvento });
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
