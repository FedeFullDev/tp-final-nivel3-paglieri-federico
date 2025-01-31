using Controlador;
using MetodosElementales;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion_catalogo.Paginas_aspx_Login
{
    public partial class RegistroCuenta : System.Web.UI.Page
    {

        public bool UsuarioRepetido;
        public bool UsuarioIngresado;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btonAgregarUsuario_Click(object sender, EventArgs e)
        {
            Usuario nuevoUsuario = new Usuario();
           
            nuevoUsuario.email = tBoxUsuario.Text;
            nuevoUsuario.contraseña = tBoxContraseña.Text;

            try
            {
                if (ConexionUsuario.validarLogin(nuevoUsuario, false))
                {
                    UsuarioRepetido = true;
                    tBoxUsuario.Text = string.Empty;
                    tBoxContraseña.Text = string.Empty;
                }
                else
                {

                    FuncionesDeFormulario funciones = new FuncionesDeFormulario();
                    funciones.AgregarUsuario(nuevoUsuario);


                    Session.Add("UsuarioCreado", nuevoUsuario);

                    Response.Redirect("Login.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();




                }
            }
            catch(Exception ex)
            {
                if (Session["Error"] == null)
                {
                    Session.Add("Error", ex.Message);
                }
                else if (Session["Error"] != null)
                {
                    Session["Error"] = ex.Message;
                }
                Response.Redirect("~/Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();

            }

;
        }
    }
}