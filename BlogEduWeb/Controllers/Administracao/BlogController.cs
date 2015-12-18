using BlogEdu.DB;
using BlogEdu.DB.Classes;
using BlogEduWeb.Models.Administracao;
using BlogEduWeb.Models.ContUsuario;
using BlogEduWeb.Models.Detalhes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEduWeb.Controllers.Administracao
{
    public class BlogController : Controller
    {
        // GET: Blog, string aceita nulo com padrão,
        public ActionResult Index(int? pagina, string tag, string pesquisa)
        {
            var conexao = new ConexaoBanco();

//  Se não for nulo
            var paginaCorreta = pagina.GetValueOrDefault(1);
            var registroPorPagina = 10;
            var posts = (from p in conexao.Posts where p.Visivel select p);

//  Verificando se é nulo a tag
            if (!string.IsNullOrEmpty(tag))
            {
                //  Any acha o primiero valor, 
                posts = (from p in posts where p.PostTags.Any(x => x.IdTag.ToUpper() == tag.ToUpper()) select p);
            }

//  Verifica quando não é nulo e nem vazio a pesquisa
            if (!string.IsNullOrEmpty(pesquisa))
            {
                posts = (from p in posts where p.Titulo.ToUpper().Contains(pesquisa.ToUpper())
                        ||  p.Resumo.ToUpper().Contains(pesquisa.ToUpper())
                        ||  p.Descricao.ToUpper().Contains(pesquisa.ToUpper())
                         select p);
            }

            var qtdeRegistros = posts.Count();
            var indiceDaPagina = paginaCorreta - 1;
            var qtdeRegistrosPular = (indiceDaPagina * registroPorPagina);
            //  Arendonta para cima
            var qtdePaginas = Math.Ceiling((decimal)qtdeRegistros / (decimal)registroPorPagina);

            var viewModel = new ListarPostsViewModel();
            viewModel.Posts = (from p in posts orderby p.DataPublicacao descending select p).Skip(qtdeRegistrosPular).Take(registroPorPagina).ToList();
            viewModel.PaginaAtual = paginaCorreta;
            viewModel.TotalDePaginas = (int)qtdePaginas;
            viewModel.Tag = tag;

            viewModel.Tags = (from p in conexao.Tags where conexao.TagPosts.Any(x => x.IdTag == p.IdTag) orderby p.IdTag ascending select p.IdTag).ToList();

            viewModel.Pesquisa = pesquisa;
            return View(viewModel);
        }

        public ActionResult _Paginacao()
        {
            return PartialView();
        }

        #region
        public ActionResult Post(int id)
        {
            var conexao = new ConexaoBanco();
            var viewModel = new DetalhesPostViewModel();
            var post = conexao.Posts.Where(x => x.Id == id).FirstOrDefault();

            viewModel.id = post.Id;
            viewModel.Titulo = post.Titulo;
            viewModel.Autor = post.Autor;
            viewModel.DataPublicacao = post.DataPublicacao;
            viewModel.HoraPublicacao = post.DataPublicacao;
            viewModel.Visivel = post.Visivel;
            viewModel.Descricao = post.Descricao;
            viewModel.Resumo = post.Resumo;

            viewModel.Tags = (from p in post.PostTags select p.IdTag).ToList();

            return View(viewModel);
        }
        #endregion
       
    }

    public ActionResult _PaginacaoPost()
    {
        return PartialView();
    }

}