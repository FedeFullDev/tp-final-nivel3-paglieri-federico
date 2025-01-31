using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;

namespace Helper
{
    public class validarLogin
    {
        public static (bool, int?) ValidarPermisoPaginas(object sessionUsuario)
        {
            Usuario usuarioActivo = sessionUsuario != null ? (Usuario)sessionUsuario : null;

            if (usuarioActivo != null && usuarioActivo.id > 0)
            {
                if (usuarioActivo.TIPOUSUARIO == TIPOUSUARIO.si_admin)
                {
                    return (true, 1);
                }else if (usuarioActivo.TIPOUSUARIO == TIPOUSUARIO.no_admin)
                {
                    return (true, 0);
                }
              
            }

            return (false, null);

        }
    }
}
