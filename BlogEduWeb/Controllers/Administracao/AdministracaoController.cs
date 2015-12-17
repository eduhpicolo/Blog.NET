using BlogEdu.DB;
using BlogEdu.DB.Classes;
using BlogEduWeb.Models.Administracao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogEduWeb.Controllers.Administracao
{
    public class AdministracaoController : Controller
    {
        //// GET: Administracao
        public ActionResult index()
        {
            return View();
        }

        public ActionResult CadastrarPost()
        {
            var viewModel = new CadastrarPostViewModel();
            viewModel.Autor = "anonymos";
            viewModel.DataPublicacao = DateTime.Now;
            viewModel.HoraPublicacao = DateTime.Now;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CadastrarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var post = new Post();

                //DateTime dDataPublicacao = DateTime.Now.Date;
                //DateTime dHoraPublicacao = DateTime.Now;
                //DateTime dConcatenar = dDataPublicacao + dHoraPublicacao;
                var dDataPublicao = new DateTime(
                        viewModel.DataPublicacao.Year,
                        viewModel.DataPublicacao.Month,
                        viewModel.DataPublicacao.Day,
                        viewModel.HoraPublicacao.Hour,
                        viewModel.HoraPublicacao.Minute,
                        viewModel.HoraPublicacao.Second
                    );

                post.Autor = viewModel.Autor;
                post.DataPublicacao = dDataPublicao;
                post.Descricao = viewModel.Descricao;
                post.Resumo = viewModel.Resumo;
                post.Titulo = viewModel.Titulo;
                post.Visivel = viewModel.Visivel;
                post.PostTags = new List<TagPost>();

                if(viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexao.Tags where p.IdTag.ToLower() == item.ToLower() select p).Any();

                        if (!tagExiste)
                        {
                            var tagClass = new Tag();
                            tagClass.IdTag = item;
                            conexao.Tags.Add(tagClass);
                        }

                        var postTag = new TagPost();
                        postTag.IdTag = item;
                        post.PostTags.Add(postTag);
                    }
                }

                try { 

                conexao.Posts.Add(post);                
                conexao.SaveChanges();
                return RedirectToAction("Index");
                }
                catch (Exception exp)
                {
                    ModelState.AddModelError("Erro Banco:", exp.Message);
                }

            }
            return View(viewModel);
        }


        public ActionResult EditarPost(int id)
        {
            var viewModel = new CadastrarPostViewModel();
            var conexao = new ConexaoBanco();
            var post = conexao.Posts.Where(x => x.Id == id).FirstOrDefault();

            if (post == null)
            {
                throw new Exception(string.Format("Post com código {0} não encontrado.", id));
            }

            viewModel.Titulo = post.Titulo;
            viewModel.Autor = post.Autor;
            viewModel.DataPublicacao = post.DataPublicacao;
            viewModel.HoraPublicacao = post.DataPublicacao;
            viewModel.Visivel = post.Visivel;
            viewModel.Descricao = post.Descricao;
            viewModel.Resumo = post.Resumo;
            viewModel.id = post.Id;

            viewModel.Tags = (from p in post.PostTags select p.IdTag).ToList();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult EditarPost(CadastrarPostViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var conexao = new ConexaoBanco();
                var post = conexao.Posts.Where(x => x.Id == viewModel.id).FirstOrDefault();

                post.Titulo = viewModel.Titulo;
                post.Autor = viewModel.Autor;
                var DataPublicacao = new DateTime(
                    viewModel.DataPublicacao.Year,
                    viewModel.DataPublicacao.Month,
                    viewModel.DataPublicacao.Day,
                    viewModel.HoraPublicacao.Hour,
                    viewModel.HoraPublicacao.Minute,
                    viewModel.HoraPublicacao.Second
                    );
                post.DataPublicacao = DataPublicacao;
                post.Visivel = viewModel.Visivel;
                post.Descricao = viewModel.Descricao;
                post.Resumo = viewModel.Resumo;
                var postsTagsAtuais = post.PostTags.ToList();

                foreach (var item in postsTagsAtuais)
                {
                    conexao.TagPosts.Remove(item);
                }

                if (viewModel.Tags != null)
                {
                    foreach (var item in viewModel.Tags)
                    {
                        var tagExiste = (from p in conexao.Tags where p.IdTag.ToLower() == item.ToLower() select p).Any();

                        if (!tagExiste)
                        {
                            var tagClass = new Tag();
                            tagClass.IdTag = item;
                            conexao.Tags.Add(tagClass);
                        }

                        var postTag = new TagPost();
                        postTag.IdTag = item;
                        post.PostTags.Add(postTag);
                    }
                }

                try
                {
                    conexao.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception exp) {
                    ModelState.AddModelError("Erro Banco: ", exp.Message);
                }
            }
            return View(viewModel);
        }

        #region
        public ActionResult ExcluirPost(int id)
        {
            var conexao = new ConexaoBanco();
            var post = (from p in conexao.Posts where p.Id == id select p).FirstOrDefault();

            if (post == null)
            {
                throw new Exception(string.Format("Post código {0} não exite.", id));
            }
            conexao.Posts.Remove(post);
            conexao.SaveChanges();

            return RedirectToAction("Index", "Blog");
        }
        #endregion

        #region ExcluirComentario
        public ActionResult ExcluirComentario(int id)
        {
            var conexaoBanco = new ConexaoBanco();
            var comentario = (from p in conexaoBanco.Comentarios
                              where p.Id == id
                              select p).FirstOrDefault();
            if (comentario == null)
            {
                throw new Exception(string.Format("Comentário código {0} não foi encontrado.", id));
            }
            conexaoBanco.Comentarios.Remove(comentario);
            conexaoBanco.SaveChanges();

            var post = (from p in conexaoBanco.Posts
                        where p.Id == comentario.IdPost
                        select p).First();
            return Redirect(Url.Action("Post", "Blog", new
            {
                ano = post.DataPublicacao.Year,
                mes = post.DataPublicacao.Month,
                dia = post.DataPublicacao.Day,
                titulo = post.Titulo,
                id = post.Id
            }) + "#comentarios");
        }
       
        #endregion
    }
}