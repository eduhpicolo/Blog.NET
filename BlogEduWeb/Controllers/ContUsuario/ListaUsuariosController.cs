using BlogEdu.DB;
using BlogEduWeb.Models.ContUsuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEduWeb.Controllers.ContUsuario
{
    public class ListaUsuariosController : Controller
    {
        // GET: ListaUsuarios
        public ActionResult Index(int? pagina)
        {
            var conexao = new ConexaoBanco();

            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registroPorPagina = 10;
            var usuarios = (from p in conexao.Usuarios orderby p.Id select p);

            var qtdRegistros = usuarios.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdRegistroPular = (indiceDaPagina * registroPorPagina);

            var qtdPaginas = Math.Ceiling((decimal)qtdRegistros / (decimal)registroPorPagina);

            var viewModel = new ListarUsuariosViewModel();            
            viewModel.Usuarios = usuarios.Skip(qtdRegistroPular).Take(registroPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalDePaginas = (int)qtdPaginas;
            return View(viewModel);
        }

        public ActionResult _Paginacao()
        {
            return PartialView();
        }
    }
}