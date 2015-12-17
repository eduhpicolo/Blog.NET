using BlogEdu.DB.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;	
using System.Linq;
using System.Web;

namespace BlogEduWeb.Models.Detalhes
{
    public class DetalhesPostViewModel
    {

        public int id { get; set; }

        public string Titulo { get; set; }

        public string Autor { get; set; }

        public string Descricao { get; set; }

        public string Resumo { get; set; }

        public DateTime DataPublicacao { get; set; }

        public DateTime HoraPublicacao { get; set; }

        public int QtdComentarios { get; set; }

        public bool Visivel { get; set; }

        public List<string> Tags { get; set; }
		
		//* Cadastrar Cometário*/
        [DisplayName("Nome")]
        [StringLength(100, ErrorMessage = "O campo nome deve possuir no máximo {1}, Caracteres!")]
        [Required(ErrorMessage = "O campo Nome é Obrigatório!")]
        public string ComentarioNome { get; set; }

        [DisplayName("E-mail")]
        [StringLength(100, ErrorMessage = "O campo e-mail deve possuir no máximo {1}, Caracteres!")]
        [Required(ErrorMessage = "E-mail inválido")]
        public string ComentarioEmail { get; set; }

        [DisplayName("Descrição")]        
        [Required(ErrorMessage = "O campo Descrição é Obrigatorio ")]
        public string ComentarioDescricao { get; set; }

        [DisplayName("Página Web")]
        [StringLength(100, ErrorMessage = "O campo Página Web deve possuir no máximo {1} caracteres!")]        
        public string ComentarioPaginaWeb { get; set; }
    }
}