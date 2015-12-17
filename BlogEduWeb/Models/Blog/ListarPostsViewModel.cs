using BlogEdu.DB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogEduWeb.Models.ContUsuario
{
    public class ListarPostsViewModel
    {
//  Registro por pagina = 10
        public List<Post> Posts { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalDePaginas { get; set; }
        public string Tag { get; set; }
        public List<string> Tags { get; set; }
        public string Pesquisa { get; set; }
    }
}