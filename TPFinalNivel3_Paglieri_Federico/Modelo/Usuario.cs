using Microsoft.SqlServer.Server;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{

    public enum TIPOUSUARIO
    {
        no_admin = 0,
        si_admin = 1
    }

     public class Usuario
    {




        public int id { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string email { get; set; }

        public string contraseña { get; set; }

        public string UrlImagen { get; set; }


        public TIPOUSUARIO TIPOUSUARIO { get; set; }


      




    }
}
