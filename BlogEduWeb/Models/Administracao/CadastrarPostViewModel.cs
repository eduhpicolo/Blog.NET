using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogEduWeb.Models.Administracao
{
    public class CadastrarPostViewModel
    {
        [DisplayName("ID")]
        [Required(ErrorMessage = "O campo ID é obrigatório.")]
        public int id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage ="O campo Título é obrigatório.")]
        [StringLength(100, MinimumLength = 2, 
        ErrorMessage = "A quantidade de caracteres no campo título deve ser entre {2} e {1}.")]
        public string Titulo { get; set; }

        [DisplayName("Autor")]
        [Required(ErrorMessage = "O campo Autor é obrigatório.")]
        [StringLength(100, MinimumLength = 2,
        ErrorMessage = "A quantidade de caracteres no campo Autor deve ser entre {2} e {1}.")]
        public string Autor { get; set; }

        [DisplayName("Descrição")]
        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        public string Descricao { get; set; }

        [DisplayName("Resumo")]
        [Required(ErrorMessage = "O campo Resumo é obrigatório.")]
        [StringLength(100, MinimumLength = 2,
        ErrorMessage = "A quantidade de caracteres no campo Resumo deve ser entre {2} e {1}.")]
        public string Resumo { get; set; }

        [DisplayName("Data de Publicação")]
        [Required(ErrorMessage = "O campo Data Publicação é obrigatório.")]
        public DateTime DataPublicacao { get; set; }

        [DisplayName("Hora de Publicação")]
        [Required(ErrorMessage = "O campo Hora de Publicação é obrigatório.")]
        public DateTime HoraPublicacao { get; set; }

        [DisplayName("Visível")]
        [Required(ErrorMessage = "O campo Visível é obrigatório.")]
        public bool Visivel { get; set; }

        public List<string> Tags { get; set; }
    }
}