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
    public class UsuariosController : Controller
    {
        private DadosBanco db = new DadosBanco();

        // GET: Usuarios
        public ActionResult Index()
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            else
            {
                var usuarios = db.Usuarios.Include(u => u.simnao);
                return View(usuarios.ToList());
            }
        }

        // GET: Usuarios/Details/5
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
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            ViewBag.supervisor = new SelectList(db.Simnaos, "ativo", "descricao");
            return View();
        }

        // POST: Usuarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "idusuario,nome,email,senha,supervisor")] Usuario usuario)
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.supervisor = new SelectList(db.Simnaos, "ativo", "descricao", usuario.supervisor);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
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
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            ViewBag.supervisor = new SelectList(db.Simnaos, "ativo", "descricao", usuario.supervisor);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "idusuario,nome,email,senha,supervisor")] Usuario usuario)
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.supervisor = new SelectList(db.Simnaos, "ativo", "descricao", usuario.supervisor);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
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
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Userid"] == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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
