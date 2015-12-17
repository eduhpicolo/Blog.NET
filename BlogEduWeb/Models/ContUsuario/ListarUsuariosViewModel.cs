using BlogEdu.DB.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogEduWeb.Models.ContUsuario
{
    public class ListarUsuariosViewModel
    {
        public List<Usuario> Usuarios { get; set; }
        public int PaginaAtual { get; set; }
        public int TotalDePaginas { get; set; }
    }
}