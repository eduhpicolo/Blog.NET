using BlogEdu.DB.Classes.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogEdu.DB.Classes
{
    public class TagPost : ClasseBase
    {
//        public int idTagPost { get; set; }
        public string IdTag { get; set; }
        public int IdPost { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
