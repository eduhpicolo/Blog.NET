using BlogEdu.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEdu.DB.Classes
{
    public class Usuario : ClasseBase
    {
        public object id;

        public string Login { get; set; }
        public string Nome { get; set; }
        public string Senha { get; set; }

    }
}
