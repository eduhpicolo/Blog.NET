using BlogEdu.DB;
using BlogEduWeb.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogEduWeb.Controllers.Login
{
    //Comando para bloquar o acesso a usuarios nao habilitados
    [Authorize]
    public class LoginController : Controller
    {
        // GET: Login
        #region Index
        //  Para ter usuarios sem login
        [AllowAnonymous]
        public ActionResult Index(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel viewModel, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var conexao = new ConexaoBanco();
            var usuario = (from p in conexao.Usuarios
                           where p.Login.ToUpper() == viewModel.Login.ToUpper() &&
                           p.Senha == viewModel.Senha
                           select p).FirstOrDefault();
            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuário e/ou senha estão incorretos.");
                return View(viewModel);
            }

            FormsAuthentication.SetAuthCookie(viewModel.Login, viewModel.Lembrar);

            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }
            return RedirectToAction("Index", "Blog");
        }
        #endregion

        #region Sair
        public ActionResult Sair()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        #endregion
    }
}