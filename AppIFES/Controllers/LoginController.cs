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
    public class LoginController : Controller
    {
        private DadosBanco db = new DadosBanco();

        // GET: Loginadm
        public ActionResult Login()
        {
            if ((Session["Userid"] != null) && Session["UserSupervisor"].Equals("1"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Usuario usuario)
        {
            var nusuario = db.Usuarios.Where(a => a.email.Equals(usuario.email) && a.senha.Equals(usuario.senha)).FirstOrDefault();
            if (nusuario != null)
            {
                Session["Userid"] = nusuario.idusuario.ToString();
                Session["UserSupervisor"] = nusuario.supervisor.ToString();
                Session["UserNome"] = nusuario.nome.ToString();
                return RedirectToAction("AfterLogin");
            }
            else
                ViewBag.MsgInvalida = "Usuário ou Senha invalidos!";

            return View(usuario);
        }

        public ActionResult AfterLogin()
        {
            if ((Session["Userid"] != null) && Session["UserSupervisor"].Equals("1"))
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return RedirectToAction("Login");
        }

        public ActionResult LoginOff()
        {
            Session["Userid"] = null;
            Session["UserSupervisor"] = null;
            Session["UserNome"] = null;
            return RedirectToAction("Index", "Home", new { area = "" });
            
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
