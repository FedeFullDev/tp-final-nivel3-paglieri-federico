using Controlador;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion_catalogo.Paginas_aspx_Login
{
    public partial class Login : System.Web.UI.Page
    {
        Usuario usuario = null;
        public bool validarPermisosAdmin;

        public bool loginvalidado = true;

        public bool validarVisibilidadLabel;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    if (Session["validarVisibilidadLabel"] == null)
                    {
                        Session.Add("validarVisibilidadLabel", true);
                    }
                   

                    /*
                    Esto porque puede ser que ingrese a la página load pero no necesariamente porque haya venido de la página de registro.
                    Con el if siguientem e aseguro que se ha creado una cuenta, y quiero precargar los textBox con los datos dela cuenta creada.
                    */
                    if (Session["UsuarioCreado"] != null)
                    {
                        usuario = new Usuario();
                        usuario = (Usuario)Session["UsuarioCreado"];


                        /*
              Si bien el usuario creado se agregará a la base de datos, y eso será lo que se precargue en los textbox de esta página Login,
                        para asegurarme, por cualquier error, para que no se precargue algo que no se logró cargar en la base de datos.
                        (Es decir se agrega lo que el usuario agregó al registrarse para que no ingrese algo incorrecto. Aunque si intenta
                        hacer login con algo que no está en la base de datos no le permitirá loguearse).
              */

                        if (ConexionUsuario.validarLogin(usuario, true))
                        {
                            tBoxUsuario.Text = usuario.email;
                            
                        }
                    }
                }
                catch (Exception ex)
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


                /*
                La session UsuarioCreado solo la hice para poder traerme esos datos de la página Registro. Luego para que no reste rendimiento
                la elimino.
                */
                Session.Remove("UsuarioCreado");
            }
        }
        

        protected void btonLogin_Click(object sender, EventArgs e)
        {
            try

            {
                // Si es != null quiere decir que viene de la página de registro.
                if (usuario != null)
                {
                    Session.Add("UsuarioLogin", usuario);
                }

                /*
                Este otro bloque de código es importante ya que si la persona está registrada, el código no se ejecutará en las líneas de arriba.
                Entonces si la persona ya está registrada, vendrá directamente al login, y aquí no se precargarán los textBox, sino que deberá ingresar
                manualmente su correo y contraseña.
                */


                if (usuario == null)
                {
                    usuario = new Usuario();
                    usuario.email = tBoxUsuario.Text;
                    usuario.contraseña = tBoxContraseña.Text;



                    if (ConexionUsuario.validarLogin(usuario, true))
                    {
                        Session.Add("UsuarioLogin", usuario);




                        Response.Redirect("/Paginas_aspx_NoAdmin/Home.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();


                    }
                    else
                    {
                        Session["validarVisibilidadLabel"] = false;
                        loginvalidado = false;
                    }
                }
            }
            catch (Exception ex)
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


        }

        protected void linkRegistro_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistroCuenta.aspx");
            
        }
    }
}