
using BlogEdu.DB;
using BlogEduWeb.Models.Administracao;
using System;
using System.Web.Mvc;

using BlogEdu.DB.Classes;
using System.Linq;

namespace BlogEduWeb.Controllers.ContUsuario
{
    public class ContUsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadastrarUsuario()
        {
            var viewModel = new CadastrarUsuarioViewModel();

            return View(viewModel);
        }
        
        [HttpPost]
        public ActionResult CadastrarUsuario(CadastrarUsuarioViewModel viewModel)
        {
            if (ModelState.IsValid) {
                var conexao = new ConexaoBanco();
                var usuario = new Usuario();

                usuario.Login = viewModel.Login;
                usuario.Nome = viewModel.Nome;
                usuario.Senha = viewModel.Senha;

                var acho = conexao.Usuarios.Where(x => x.Nome == usuario.Nome).FirstOrDefault();

                if (acho == null)
                 {
                    try
                    {
                        conexao.Usuarios.Add(usuario);
                        conexao.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception exp)
                    {
                        ModelState.AddModelError("Erro Banco:", exp.Message);
                    }
                }
                else
                {
                    throw new Exception(string.Format("Usuario já Cadastrado"));
                }
            }
            return View(viewModel);
        }

        public ActionResult EditarUsuario(int id)
        {
            var viewModel = new CadastrarUsuarioViewModel();
            var conexao = new ConexaoBanco();
            var usuario = conexao.Usuarios.Where(x => x.Id == id).FirstOrDefault();

            if (usuario == null)
            {
                throw new Exception(string.Format("Posto com código {0} não encontrado.", id));
            }
            viewModel.Id = usuario.Id;
            viewModel.Login = usuario.Login;
            viewModel.Nome = usuario.Nome;
            viewModel.Senha = usuario.Senha;
            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditarUsuario(CadastrarUsuarioViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var usuarios = conexao.Usuarios.Where(p => p.id == viewModel.id).FirstOrDefault();

                usuarios.Id = viewModel.Id;
                usuarios.Login = viewModel.Login;
                usuarios.Nome = viewModel.Nome;
                usuarios.Senha = viewModel.Senha;

                //var acho = conexao.Usuarios.Where(x => x.sNome == viewModel.sNome && x.Id == viewModel.iId).FirstOrDefault();
                var acho = (from p in conexao.Usuarios where p.id != viewModel.id && p.Nome == viewModel.Nome  select p).FirstOrDefault();

                if (acho == null)
                {
                    try
                    {
                        //conexao.Usuarios.Add(usuarios);
                        conexao.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (Exception exp)
                    {
                        ModelState.AddModelError("Erro Banco:", exp.Message);
                    }                    
                }
                else
                {
                    throw new Exception(string.Format("Usuario já cadastrado!"));
                }
            }
            return View(viewModel);
        }

        #region
        public ActionResult ExcluirPost(int id)
        {
            var conexao = new ConexaoBanco();
            var usuario = (from p in conexao.Usuarios where p.Id == id select p).FirstOrDefault();

            if (usuario == null)
            {
                throw new Exception(string.Format("Post código {0} não exite.", id));
            }
            conexao.Usuarios.Remove(usuario);
            conexao.SaveChanges();

            return RedirectToAction("Index", "Administracao");
        }
        #endregion
    }
}