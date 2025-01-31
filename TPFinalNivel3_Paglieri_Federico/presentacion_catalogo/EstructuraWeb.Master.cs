using MetodosElementales;
using Modelo;
using presentacion_catalogo.Paginas_aspx_Admin;
using presentacion_catalogo.Paginas_aspx_Login;
using presentacion_catalogo.Paginas_aspx_NoAdmin;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace presentacion_catalogo
{
    public partial class EstructuraWeb : System.Web.UI.MasterPage
    {


        public bool validarIngresoPaginasAdministrador = false;
        public bool validarIngresoPaginasNoAdministrador = false;

        public bool validarPaginaLoginRegistroCuenta = true;


      

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                (bool, int?) validador;

                validador = ((bool, int?))Helper.validarLogin.ValidarPermisoPaginas(Session["UsuarioLogin"]);


                if (!(Page is Paginas_aspx_Login.Login) && !(Page is RegistroCuenta) && !(Page is Home))
                {


                    if (validador.Item1 == false && validador.Item2 == null)
                    {


                        Response.Redirect("/Paginas_aspx_Login/Login.aspx", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }



                if (Session["UsuarioLogin"] != null)
                {


                    if (((Usuario)Session["UsuarioLogin"]).TIPOUSUARIO == TIPOUSUARIO.si_admin)
                    {
                        validarIngresoPaginasAdministrador = true;


                    }
                    if (((Usuario)Session["UsuarioLogin"]).TIPOUSUARIO == TIPOUSUARIO.no_admin)
                    {
                        validarIngresoPaginasNoAdministrador = true;

                    }

                    validarPaginaLoginRegistroCuenta = true;
                }
                else if (Session["UsuarioLogin"] == null)
                {

                    validarPaginaLoginRegistroCuenta = false;

                }

                string uniqueParam = DateTime.Now.Ticks.ToString();

                if ((validador.Item1 = true && validador.Item2 == 1) || (validador.Item1 = true && validador.Item2 == 0))
                {
                   

                    // Ese if de arriba quire decir que hay una cuenta ingresada, que hay un login. Ya sea administrador o no.
                    if (!string.IsNullOrEmpty(((Usuario)Session["UsuarioLogin"]).UrlImagen))
                    {
                       
                        imgAvatar.ImageUrl = "~/Imagenes/" + ((Usuario)Session["UsuarioLogin"]).UrlImagen + "?" + uniqueParam;
                    }
                    else
                    {
                        imgAvatar.ImageUrl = "~/Imagenes/LoginDefault.png";
                    }

                }
                else
                {
                    imgAvatar.ImageUrl = "~/Imagenes/LoginDefault.png";
                }

                if ((validador.Item1 = true && validador.Item2 == 1) || (validador.Item1 = true && validador.Item2 == 0))
                {
                    
                    // Ese if de arriba quire decir que hay una cuenta ingresada, que hay un login. Ya sea administrador o no.
                    if (!string.IsNullOrEmpty(((Usuario)Session["UsuarioLogin"]).UrlImagen))
                    {
                        
                        Image1.ImageUrl = "~/Imagenes/" + ((Usuario)Session["UsuarioLogin"]).UrlImagen + "?" + uniqueParam;
                    }
                    else
                    {
                        Image1.ImageUrl = "~/Imagenes/LoginDefault.png";
                    }

                }
                else
                {
                    Image1.ImageUrl = "~/Imagenes/LoginDefault.png";
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

        protected void btonCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("UsuarioLogin");
                Response.Redirect("/Paginas_aspx_Login/Login.aspx", true);
               
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
    }
}