using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controlador;
using MetodosElementales;
using Modelo;

namespace presentacion_catalogo.Paginas_aspx_Login
{

    public partial class PerfilUsuario : System.Web.UI.Page
    {
        public Usuario Usuario;

        public bool checkBoxChecked;

        public bool validarGuardarCambios; /*variable para que el botón de guardar cambos se deshabilte si la contraseña que se
          desea modificar no existe*/

        public bool validarRecargarPaginaMaestra;



        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
               


                if (Session["ValidarGuardarCambios"] != null)
                {
                    Session["ValidarGuardarCambios"] = true;
                }
                else
                {
                    Session.Add("ValidarGuardarCambios", true);
                }

                ConexionUsuario conexionUsuario = new ConexionUsuario();
                if (!IsPostBack || (IsPostBack && tBoxNombre.Text == string.Empty && tBoxApellido.Text == string.Empty))
                {
                    Usuario usuario = new Usuario();
                    if (Session["UsuarioLogin"] != null)
                    {
                        usuario.id = ((Usuario)Session["UsuarioLogin"]).id;

                        Usuario Usuarioo = Usuarioo = conexionUsuario.SeleccionarUsuarioId(usuario.id);

                        tBoxNombre.Text = Usuarioo.nombre;
                        tBoxApellido.Text = Usuarioo.apellido;
                        tBoxEmail.Text = Usuarioo.email;
                        if (!string.IsNullOrEmpty(Usuarioo.UrlImagen))
                        {
                            string uniqueParam = DateTime.Now.Ticks.ToString();

                            imgPerfil.ImageUrl = "~/Imagenes/" + Usuarioo.UrlImagen + "?" + uniqueParam;
                        }



                    }
                }

                if (IsPostBack && HiddenFieldEliminarImagen.Value == "true")
                {
                    Usuario usuario = new Usuario();
                    if (Session["UsuarioLogin"] != null)
                    {
                        usuario.id = ((Usuario)Session["UsuarioLogin"]).id;

                        Usuario Usuarioo = Usuarioo = conexionUsuario.SeleccionarUsuarioId(usuario.id);

                        string rutaFisicaImagenes = Server.MapPath("~/Imagenes/");
                        string archivoImagen = Path.Combine(rutaFisicaImagenes, Usuarioo.UrlImagen); // Usa la URL guardada

                        // Verificar si el archivo existe antes de intentar eliminarlo
                        if (File.Exists(archivoImagen))
                        {
                            File.Delete(archivoImagen); // Elimina la imagen del servidor
                        }

                        ConexionUsuario Modificar = new ConexionUsuario();

                        Usuarioo.UrlImagen = string.Empty;

                        ((Usuario)Session["UsuarioLogin"]).UrlImagen = Usuarioo.UrlImagen;
                        //if (!string.IsNullOrEmpty(Usuario.UrlImagen))
                        //{
                        Modificar.ModificarCampoUsuario(Usuarioo, "urlImagenPerfil");
                        //}
                        // Actualiza la propiedad del usuario para reflejar que no hay imagen


                        // Actualiza el control de imagen para mostrar un placeholder
                        imgPerfil.ImageUrl = "~/Imagenes/DefaultImage.png";

                        // Actualiza también el avatar en la barra de navegación
                        Image imageLoginBarraNavegacion1 = (Image)Master.FindControl("imgAvatar");
                        Image imagenLoginBarraNavegacion2 = (Image)Master.FindControl("Image1");
                        if (imageLoginBarraNavegacion1 != null)
                        {
                            if (imageLoginBarraNavegacion1.ImageUrl != "~/Imagenes/LoginDefault.png")
                                imageLoginBarraNavegacion1.ImageUrl = "~/Imagenes/LoginDefault.png";
                        }
                        if (imagenLoginBarraNavegacion2 != null)
                        {
                            if (imagenLoginBarraNavegacion2.ImageUrl != "~/Imagenes/LoginDefault.png")
                                imagenLoginBarraNavegacion2.ImageUrl = "~/Imagenes/LoginDefault.png";
                        }

                        // Restablece el HiddenField para futuras interacciones
                        HiddenFieldEliminarImagen.Value = "false";
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

        protected void btonGuardarCambios_Click(object sender, EventArgs e)
        {


            ConexionUsuario Modificar = new ConexionUsuario();

            Usuario usuario = new Usuario();
            if (Session["UsuarioLogin"] != null)
            {
                Usuario = new Usuario();

                usuario.id = ((Usuario)Session["UsuarioLogin"]).id;

                Usuario = Modificar.SeleccionarUsuarioId(usuario.id);
            }
            //Usuario = (Usuario)Session["UsuarioLogin"];

            try
            {


                if (InputFile.HasFile)
                {

                    if (!string.IsNullOrEmpty(InputFile.PostedFile.FileName))
                    {
                        string rutaFisicaImagenes = Server.MapPath("~/Imagenes/");

                        //repito la linea siguiente de codigo ya que al no ser una sesion,la precargaDatosUsuario realizada en la línea 27,
                        // no se tomará en cuenta y se perderán los datos. Y necesito que precargaDatosUsuario tenga el id del registro,
                        //ya que la funcion de ModificarCampoUsuario en las líneas siguientes, necesitan el id.


                        Usuario.UrlImagen = "PerfilIMG-" + Usuario.id + ".jpg";
                        InputFile.PostedFile.SaveAs(rutaFisicaImagenes + Usuario.UrlImagen);

                        ((Usuario)Session["UsuarioLogin"]).UrlImagen = Usuario.UrlImagen;
                    }



                }

                if (!string.IsNullOrEmpty(Usuario.UrlImagen))
                {
                    Modificar.ModificarCampoUsuario(Usuario, "urlImagenPerfil");
                }


                Usuario.nombre = tBoxNombre.Text;
                Modificar.ModificarCampoUsuario(Usuario, "nombre");
                Usuario.apellido = tBoxApellido.Text;
                Modificar.ModificarCampoUsuario(Usuario, "apellido");


                // Agregar un parámetro único a la URL de la imagen
                string uniqueParam = DateTime.Now.Ticks.ToString();

                Image imagenLoginBarraNavegacion1 = (Image)Master.FindControl("imgAvatar");
                if (!string.IsNullOrEmpty(Usuario.UrlImagen))
                {
                    imagenLoginBarraNavegacion1.ImageUrl = "~/Imagenes/" + Usuario.UrlImagen + "?" + uniqueParam;
                }

                Image imageLoginBarraNavegacion2 = (Image)Master.FindControl("Image1");
                if (!string.IsNullOrEmpty(Usuario.UrlImagen))
                {
                    imageLoginBarraNavegacion2.ImageUrl = "~/Imagenes/" + Usuario.UrlImagen + "?" + uniqueParam;
                }

                imgPerfil.ImageUrl = "~/Imagenes/" + Usuario.UrlImagen + "?" + uniqueParam;


                //Image imagenLoginBarraNavegacion1 = (Image)Master.FindControl("imgAvatar");

                //if (!string.IsNullOrEmpty(Usuario.UrlImagen))
                //{
                //    imagenLoginBarraNavegacion1.ImageUrl = "~/Imagenes/" + Usuario.UrlImagen;
                //}
               

                //Image imageLoginBarraNavegacion2 = (Image)Master.FindControl("Image1");

                //if (!string.IsNullOrEmpty(Usuario.UrlImagen))
                //{
                //    imageLoginBarraNavegacion2.ImageUrl = "~/Imagenes/" + Usuario.UrlImagen;
                //}
                

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


        protected void cBoxCambiarContraseña_CheckedChanged(object sender, EventArgs e)
        {


            checkBoxChecked = cBoxCambiarContraseña.Checked;
            if (Session["CheckBoxChecked"] == null)
            {
                Session.Add("CheckBoxChecked", checkBoxChecked);
            }
            else
            {
                Session["CheckBoxChecked"] = checkBoxChecked;
            }

            if ((bool)Session["CheckBoxChecked"])
            {
                btonGuardarCambios.Visible = false;
            }
            else
            {
                btonGuardarCambios.Visible = true;
            }

            //// Actualizar navbar dependiendo del estado del checkbox.
            //if (checkBoxChecked && HiddenFieldNavBarResponsiveJSValidationPerfilUsuario.Value == "true")
            //{
            //    // Si el checkbox está marcado, actualizamos navbar
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "refreshHeaderChecked", "setupNavBar();", true);


            //}
            //else if ((!checkBoxChecked) && HiddenFieldNavBarResponsiveJSValidationPerfilUsuario.Value == "true")
            //{
            //    // Si el checkbox no está marcado, actualizamos navbar
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "refreshHeaderUnChecked", "setupNavBar();", true);


            //}



        }

        protected void btonEliminarCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                (bool, int?) validarUsuarioActivo = Helper.validarLogin.ValidarPermisoPaginas(Session["UsuarioLogin"]);

                if ((validarUsuarioActivo.Item1 = true && validarUsuarioActivo.Item2 == 1) || (validarUsuarioActivo.Item1 = true && validarUsuarioActivo.Item2 == 0))
                {
                    Usuario = ((Usuario)Session["UsuarioLogin"]);

                    int idUsuario = Usuario.id;

                    FuncionesDeFormulario funciones = new FuncionesDeFormulario();
                    funciones.EliminarCuenta(idUsuario);

                    Session.Remove("UsuarioLogin");

                    Response.Redirect("Login.aspx");
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


        protected void btonGuardarContraseaña_Click(object sender, EventArgs e)
        {
            //Codigo para validar el cambio de contraseña



            try
            {


                if (Session["RecuperarContraseñaActualTemporalmente"] == null)
                {
                    Session.Add("RecuperarContraseñaActualTemporalmente", tBoxContraseñaActual.Text);
                }
                else if (Session["RecuperarContraseñaActualTemporalmente"] != null)
                {
                    Session["RecuperarContraseñaActualTemporalmente"] = tBoxContraseñaActual.Text;
                }

                if (!string.IsNullOrEmpty(tBoxContraseñaActual.Text))
                {
                    FuncionesDeFormulario funciones = new FuncionesDeFormulario();
                    string contraseñaActual = Session["RecuperarContraseñaActualTemporalmente"].ToString();
                    string contraseñaNueva = string.Empty;

                    if (!string.IsNullOrEmpty(tBoxContraseñaNueva.Text))
                    {
                        contraseñaNueva = tBoxContraseñaNueva.Text;

                    }

                    bool validarContraseñaActual = funciones.validarContraseña(contraseñaActual);

                    if (validarContraseñaActual)
                    {
                        validarGuardarCambios = true;
                        if (Session["ValidarGuardarCambios"] != null)
                        {
                            Session["ValidarGuardarCambios"] = validarGuardarCambios;
                        }
                        else
                        {
                            Session.Add("ValidarGuardarCambios", validarGuardarCambios);
                        }

                        funciones.ModificarContraseña(contraseñaActual, contraseñaNueva);

                        Session["CheckBoxChecked"] = false;
                        if (!(bool)Session["CheckBoxChecked"])
                        {
                            cBoxCambiarContraseña.Checked = false;
                            btonGuardarCambios.Visible = true;
                        }
                    }
                    else
                    {
                        validarGuardarCambios = false;
                        if (Session["ValidarGuardarCambios"] != null)
                        {
                            Session["ValidarGuardarCambios"] = validarGuardarCambios;
                        }
                        else
                        {
                            Session.Add("ValidarGuardarCambios", validarGuardarCambios);
                        }

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
    }
}


