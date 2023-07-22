using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC1
{
    public class Login
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Adm { get; set; }

        public Login(string usuario, string senha, string adm)
        {
            Usuario = usuario;
            Senha = senha;
            Adm = adm;
        }
        public Login() { }
    }
}
