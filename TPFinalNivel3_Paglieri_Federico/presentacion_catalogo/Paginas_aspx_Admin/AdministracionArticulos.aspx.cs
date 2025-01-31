using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Controlador;
using MetodosElementales;
using Modelo;

namespace presentacion_catalogo.Paginas_aspx_Admin
{
    public partial class AdministracionArticulos : System.Web.UI.Page
    {
        ConexionArticulo conexion = new ConexionArticulo();
        ConexionCategoria conexionCategoria = new ConexionCategoria();
        ConexionMarca conexionMarca = new ConexionMarca();
        FuncionesDeFormulario funciones = new FuncionesDeFormulario();

        public bool validarTipoUsuarioAdmin;

        public bool validarEliminarArticulo;

        public bool validarAgregarModificarArticulo;

        public bool validarMaxCaracteresNombre;
        public bool validarMaxCaracteresCodigoArticulo;
        public bool validarMaxCaracteresDescripcion;
        public bool validarMaxCaracteresPrecio;

        public string imgUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {



            try
            {
                if (!IsPostBack)
                {



                    btonEliminar.Enabled = false;

                    dDownListMarca.DataSource = conexionMarca.ListaMarca();
                    dDownListMarca.DataValueField = "Id";
                    dDownListMarca.DataTextField = "Descripcion";
                    dDownListMarca.DataBind();

                    dDownListCategoria.DataSource = conexionCategoria.ListaCategoria();
                    dDownListCategoria.DataValueField = "Id";
                    dDownListCategoria.DataTextField = "Descripcion";
                    dDownListCategoria.DataBind();



                    if (Request.QueryString["id"] != null)
                    {
                        Articulo articuloSeleccionado = new Articulo();
                        btonEliminar.Enabled = true;


                        btonAgregar.Text = "Modificar";


                        tBoxId.Text = Request.QueryString["id"];


                        /*
                           Para precargar los demás cotroles voy a filtrar la tabla de la base de datos según el id que se haya
                           eleccionado. Esto porque además de no mostrar todos los campos de la tabla en el Grid, por temas de orden y 
                           importancia, no podría campturar esos datos de otra manera, o sería muy poco óptimo.

                           Usaré un método para obtener el registro de la tabla con ese id.
                        */


                        articuloSeleccionado = conexion.FiltrarRegistroPorId(int.Parse(tBoxId.Text));

                        tBoxNombre.Text = articuloSeleccionado.Nombre;
                        tBoxCodigoArticulo.Text = articuloSeleccionado.CodigoArticulo;
                        tBoxDescripcion.Text = articuloSeleccionado.Descripcion;

                        // no se carga el drop down list
                        dDownListMarca.SelectedValue = articuloSeleccionado.Marca.Id.ToString();
                        dDownListCategoria.SelectedValue = articuloSeleccionado.Categoria.Id.ToString();

                        //ImgArticulo.Style.Add("background-image", $"{articuloSeleccionado.ImagenUrl}");
                        //ImgArticulo.Style.Add("background-color", "white");
                        imgUrl = articuloSeleccionado.ImagenUrl;
                        Session.Add("urlImagen", articuloSeleccionado.ImagenUrl);



                        tBoxPrecio.Text = articuloSeleccionado.Precio.ToString();

                    }
                    else
                    {
                        btonAgregar.Text = "Agregar";


                    }

                    // validar si es admin o no para deshabilitar o no los  permisos de  administrador en la página

                    if (Session["UsuarioLogin"] != null)
                    {
                        if (((Usuario)Session["UsuarioLogin"]).TIPOUSUARIO == TIPOUSUARIO.si_admin)
                        {

                            validarTipoUsuarioAdmin = true;
                            if (Session["validarTipoUsuarioAdmin"] == null)
                            {
                                Session.Add("validarTipoUsuarioAdmin", validarTipoUsuarioAdmin);
                            }
                            else
                            {
                                Session["validarTipoUsuarioAdmin"] = validarTipoUsuarioAdmin;
                            }

                        }
                        else
                        {
                            validarTipoUsuarioAdmin = false;
                            if (Session["validarTipoUsuarioAdmin"] == null)
                            {
                                Session.Add("validarTipoUsuarioAdmin", validarTipoUsuarioAdmin);
                            }
                            else
                            {
                                Session["validarTipoUsuarioAdmin"] = validarTipoUsuarioAdmin;
                            }
                        }
                    }

                }
                else if (IsPostBack)
                {


                    //validación para el maxLenght de los iput de Nombre, Código de Artículo y Descripción

                    if (Session["validarMaxCaracteresCodigoArticulo"] != null)
                    {
                        Session["validarMaxCaracteresCodigoArticulo"] = Session["validarMaxCaracteresCodigoArticulo"];
                    }

                    if (Session["validarMaxCaracteresNombre"] != null)
                    {
                        Session["validarMaxCaracteresNombre"] = Session["validarMaxCaracteresNombre"];
                    }

                    if (Session["validarMaxCaracteresDescripcion"] != null)
                    {
                        Session["validarMaxCaracteresDescripcion"] = Session["validarMaxCaracteresDescripcion"];
                    }

                    if (Session["validarMaxCaracteresPrecio"] != null)
                    {
                        Session["validarMaxCaracteresPrecio"] = Session["validarMaxCaracteresPrecio"];
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

        protected void btonAgregar_Click(object sender, EventArgs e)
        {
            Articulo Articulo = new Articulo();
            try
            {

                if (!string.IsNullOrEmpty(tBoxId.Text))
                {
                    Articulo.Id = int.Parse(tBoxId.Text);
                }

                Articulo.Nombre = tBoxNombre.Text;
                Articulo.CodigoArticulo = tBoxCodigoArticulo.Text;
                Articulo.Descripcion = tBoxDescripcion.Text;
                Articulo.Marca = new Marca();
                Articulo.Marca.Id = int.Parse(dDownListMarca.SelectedValue);
                Articulo.Categoria = new Categoria();
                Articulo.Categoria.Id = int.Parse(dDownListCategoria.SelectedValue);
                if (Session["urlImagen"] != null)
                {
                    Articulo.ImagenUrl = Session["urlImagen"] as string;
                }
                else
                {
                    Articulo.ImagenUrl = "";
                }

                if (!string.IsNullOrEmpty(tBoxPrecio.Text))
                {
                    Articulo.Precio = decimal.Parse(tBoxPrecio.Text);
                }
                else
                {
                    Articulo.Precio = 0;
                }


               
                    if (Request.QueryString["id"] == null)
                    {
                        funciones.AgregarconStoreProcedure(Articulo);
                    }
                    else if (Request.QueryString["id"] != null)
                    {
                        funciones.Modificar(Articulo);
                    }

                    Response.Redirect("TablaArticulos.aspx", false);
                    Session["validarAgregarModificarArticulo"] = null;
                    Context.ApplicationInstance.CompleteRequest();



                


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



        protected void btonEliminar_Click(object sender, EventArgs e)
        {
            try
            {


                funciones.Eliminar(int.Parse(tBoxId.Text));
                Response.Redirect("TablaArticulos.aspx", false);
                Session["validarEliminarArticulo"] = null;
                Context.ApplicationInstance.CompleteRequest();


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

        
        protected void tBoxCodigoArticulo_TextChanged(object sender, EventArgs e)
        {
            if (tBoxCodigoArticulo.Text.Length == 50)
            {

                if (Session["validarMaxCaracteresCodigoArticulo"] == null)
                {
                    Session.Add("validarMaxCaracteresCodigoArticulo", true);
                    validarMaxCaracteresCodigoArticulo = (bool)Session["validarMaxCaracteresCodigoArticulo"];
                }
                else
                {
                    Session["validarMaxCaracteresCodigoArticulo"] = true;
                    validarMaxCaracteresCodigoArticulo = (bool)Session["validarMaxCaracteresCodigoArticulo"];
                }
            }
            else
            {
                if (Session["validarMaxCaracteresCodigoArticulo"] == null)
                {
                    Session.Add("validarMaxCaracteresCodigoArticulo", false);
                    validarMaxCaracteresCodigoArticulo = (bool)Session["validarMaxCaracteresCodigoArticulo"];
                }
                else
                {
                    Session["validarMaxCaracteresCodigoArticulo"] = false;

                    validarMaxCaracteresCodigoArticulo = (bool)Session["validarMaxCaracteresCodigoArticulo"];
                }



            }

        }

        protected void tBoxNombre_TextChanged(object sender, EventArgs e)
        {
            if (tBoxNombre.Text.Length == 50)
            {

                if (Session["validarMaxCaracteresNombre"] == null)
                {
                    Session.Add("validarMaxCaracteresNombre", true);
                    validarMaxCaracteresNombre = (bool)Session["validarMaxCaracteresNombre"];
                }
                else
                {
                    Session["validarMaxCaracteresNombre"] = true;
                    validarMaxCaracteresNombre = (bool)Session["validarMaxCaracteresNombre"];
                }
            }
            else
            {
                if (Session["validarMaxCaracteresNombre"] == null)
                {
                    Session.Add("validarMaxCaracteresNombre", false);
                    validarMaxCaracteresNombre = (bool)Session["validarMaxCaracteresNombre"];
                }
                else
                {
                    Session["validarMaxCaracteresNombre"] = false;

                    validarMaxCaracteresNombre = (bool)Session["validarMaxCaracteresNombre"];
                }



            }

        }

        protected void tBoxDescripcion_TextChanged(object sender, EventArgs e)
        {
            if (tBoxDescripcion.Text.Length == 150)
            {

                if (Session["validarMaxiCaracteresDescripcion"] == null)
                {
                    Session.Add("validarMaxCaracteresDescripcion", true);
                    validarMaxCaracteresDescripcion = (bool)Session["validarMaxCaracteresDescripcion"];
                }
                else
                {
                    Session["validarMaxCaracteresDescripcion"] = true;
                    validarMaxCaracteresDescripcion = (bool)Session["validarMaxCaracteresDescripcion"];
                }
            }
            else
            {
                if (Session["validarMaxCaracteresDescripcion"] == null)
                {
                    Session.Add("validarMaxCaracteresDescripcion", false);
                    validarMaxCaracteresDescripcion = (bool)Session["validarMaxCaracteresDescripcion"];
                }
                else
                {
                    Session["validarMaxCaracteresDescripcion"] = false;

                    validarMaxCaracteresDescripcion = (bool)Session["validarMaxCaracteresDescripcion"];
                }



            }

        }

        protected void tBoxPrecio_TextChanged(object sender, EventArgs e)
        {

            decimal minMoney = -922337203685477.5808M;
            decimal maxMoney = 922337203685477.5807M;
            string input = tBoxPrecio.Text;

            if (decimal.TryParse(input, out decimal value))
            {
                if (value < minMoney || value > maxMoney)
                {
                    // Manejo del error: Número fuera del rango
                    lblError.Text = "El número está fuera del rango permitido para un tipo MONEY.";
                    lblError.Visible = true;
                    PanelButtonsAgregarEliminar.Visible = false;

                    if (Session["EstadoLabelError"] == null)
                    {
                        Session.Add("EstadoLabelError", lblError.Visible);
                    }
                    else
                    {
                        Session["EstadoLabelError"] = lblError.Visible;
                    }
                }
                else
                {
                    // Valor válido
                    lblError.Visible = false;
                    PanelButtonsAgregarEliminar.Visible = true;

                    if (Session["EstadoLabelError"] == null)
                    {
                        Session.Add("EstadoLabelError", lblError.Visible);
                    }
                    else
                    {
                        Session["EstadoLabelError"] = lblError.Visible;
                    }
                }
            }
            else
            {
                // Manejo del error: Formato inválido
                lblError.Text = "Por favor ingrese un número válido.";
                lblError.Visible = true;

                PanelButtonsAgregarEliminar.Visible = false;

                if (Session["EstadoLabelError"] == null)
                {
                    Session.Add("EstadoLabelError", lblError.Visible);
                }
                else
                {
                    Session["EstadoLabelError"] = lblError.Visible;
                }
            }



            if (tBoxPrecio.Text.Length == 20)
            {

                if (Session["validarMaxCaracteresPrecio"] == null)
                {
                    Session.Add("validarMaxCaracteresPrecio", true);
                    validarMaxCaracteresPrecio = (bool)Session["validarMaxCaracteresPrecio"];
                }
                else
                {
                    Session["validarMaxCaracteresPrecio"] = true;
                    validarMaxCaracteresPrecio = (bool)Session["validarMaxCaracteresPrecio"];
                }
            }
            else
            {
                if (Session["validarMaxCaracteresPrecio"] == null)
                {
                    Session.Add("validarMaxCaracteresPrecio", false);
                    validarMaxCaracteresPrecio = (bool)Session["validarMaxCaracteresPrecio"];
                }
                else
                {
                    Session["validarMaxCaracteresPrecio"] = false;

                    validarMaxCaracteresPrecio = (bool)Session["validarMaxCaracteresPrecio"];
                }



            }
        }



    }
}
