using Controlador;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosElementales;
using System.Web.WebSockets;

namespace presentacion_catalogo.Paginas_aspx_NoAdmin
{
    public partial class Home : System.Web.UI.Page
    {
        public bool FiltroAvanzadoActivo { get; set; }


        List<Articulo> articuloList;

        List<Articulo> CapturarProductosFavoritos;




        public bool validarListaIdPrecio
        {
            get { return Session["validarListaIdPrecio"] != null ? (bool)Session["validarListaIdPrecio"] : false; }
            set { Session["validarListaIdPrecio"] = value; }
        }

        public bool validarListaModelo
        {
            get { return Session["validarListaModelo"] != null ? (bool)Session["validarListaModelo"] : false; }
            set { Session["validarListaModelo"] = value; }
        }



        protected void Page_Load(object sender, EventArgs e)
        {

            



            ConexionArticulo conexion = new ConexionArticulo();
            try
            {
                if (!IsPostBack)
                {
                    FuncionesDeFormulario funciones = new FuncionesDeFormulario();

                    /*La siguiente validación es para evaluar si el contenedor del producto debe tener el icono de agregar producto 
                     * a favoritos o el icono de eliminar producto de favoritos*/
                    if (Session["UsuarioLogin"] != null)
                    {


                        var id = ((Usuario)Session["UsuarioLogin"]).id;

                        var listArticulosFavoritos = funciones.SelectProductosFavoritos(id);

                        var listArticulosFavoritosId = listArticulosFavoritos.Select(articulo => articulo.Id).ToList();

                        Session.Add("listArticulosFavoritosId", listArticulosFavoritosId);
                    }


                    repeaterProductosCatalogo.DataSource = conexion.ListaArticulo();
                    repeaterProductosCatalogo.DataBind();
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

        protected void CheckBoxFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxFiltroAvanzado.Checked)
            {
                FiltroAvanzadoActivo = true;
                if (Session["FiltroAvanzadoActivo"] == null)
                {
                    Session.Add("FiltroAvanzadoActivo", FiltroAvanzadoActivo);
                }
                else if (Session["FiltroAvanzadoActivo"] != null)
                {
                    Session["FiltroAvanzadoActivo"] = FiltroAvanzadoActivo;
                }

                string campoSeleccionado = dDownListCampo.Text;

                if (campoSeleccionado == "Código" || campoSeleccionado == "Modelo")
                {
                    validarListaModelo = true;
                }
                else
                {
                    validarListaIdPrecio = true;
                }
            }
            else
            {
                FiltroAvanzadoActivo = false;
                if (Session["FiltroAvanzadoActivo"] == null)
                {
                    Session.Add("FiltroAvanzadoActivo", FiltroAvanzadoActivo);
                }
                else if (Session["FiltroAvanzadoActivo"] != null)
                {
                    Session["FiltroAvanzadoActivo"] = FiltroAvanzadoActivo;
                }
            }




        }

        protected void btonDesplegarDetalleProducto_Click(object sender, EventArgs e)
        {

            int.TryParse(((Button)sender).CommandArgument, out int id);

            ConexionArticulo conexion = new ConexionArticulo();
            try
            {
                Articulo articuloSeleccionado = conexion.FiltrarRegistroPorId(id);

                Session.Add("ArticuloSeleccionado", articuloSeleccionado);
                Response.Redirect("DetalleProducto.aspx", false);
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





        protected void dDownListCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (HiddenFieldNavBarResponsiveJSValidationHome.Value == "true")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "setupNavBar", "setupNavBar();", true);
            }

            string campoSeleccionado = dDownListCampo.Text;
            if (campoSeleccionado == "Precio")
            {

                validarListaIdPrecio = true;
                validarListaModelo = false;

                if (Session["validarListaIdPrecio"] == null)
                {
                    Session.Add("validarListaIdPrecio", validarListaIdPrecio);
                }
                else if (Session["validarListaIdPrecio"] != null)
                {
                    Session["validarListaIdPrecio"] = validarListaIdPrecio;
                }

                if (Session["validarListaModelo"] == null)
                {
                    Session.Add("validarListaModelo", validarListaModelo);
                }
                else if (Session["validarListaModelo"] != null)
                {
                    Session["validarListaModelo"] = validarListaModelo;
                }
            }
            else if (campoSeleccionado == "Código" || campoSeleccionado == "Modelo")
            {

                validarListaModelo = true;
                validarListaIdPrecio = false;

                if (Session["validarListaIdPrecio"] == null)
                {
                    Session.Add("validarListaIdPrecio", validarListaIdPrecio);
                }
                else if (Session["validarListaIdPrecio"] != null)
                {
                    Session["validarListaIdPrecio"] = validarListaIdPrecio;
                }

                if (Session["validarListaModelo"] == null)
                {
                    Session.Add("validarListaModelo", validarListaModelo);
                }
                else if (Session["validarListaModelo"] != null)
                {
                    Session["validarListaModelo"] = validarListaModelo;
                }
            }

        }

        protected void btonFiltrar_Click(object sender, EventArgs e)
        {
            if (HiddenFieldNavBarResponsiveJSValidationHome.Value == "true")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "setupNavBar", "setupNavBar();", true);
            }


            string campo = dDownListCampo.Text;
            string criterio = "";

            if (validarListaIdPrecio)
            {
                criterio = dDownListCriterio.Text;
            }
            else if (validarListaModelo)
            {
                criterio = dDownListCriterio1.Text;
            }

            string filtro = tBoxBusqueda.Text;

            FuncionesDeFormulario funcion = new FuncionesDeFormulario();

            ConexionArticulo conexion = new ConexionArticulo();
            List<Articulo> listArticulosExistentes;


            try
            {
                listArticulosExistentes = conexion.ListaArticulo();

                List<Articulo> list = new List<Articulo>();

                if (filtro != string.Empty)
                {
                    if (campo == "Código")
                    {
                        list = listArticulosExistentes.FindAll(articulos => articulos.CodigoArticulo.ToLower().Contains(filtro.ToLower()));
                    }
                    if (campo == "Modelo")
                    {
                        list = listArticulosExistentes.FindAll(articulos => articulos.Nombre.ToLower().Contains(filtro.ToLower()));
                    }
                    if (campo == "Precio")
                    {
                        if (criterio == "Menor a")
                        {
                            list = listArticulosExistentes.FindAll(articulos => articulos.Precio < int.Parse(filtro));
                        }
                        else if (criterio == "Mayor a")
                        {
                            list = listArticulosExistentes.FindAll(articulos => articulos.Precio > int.Parse(filtro));
                        }
                        else if (criterio == "Igual a")
                        {
                            list = listArticulosExistentes.FindAll(articulos => articulos.Precio == int.Parse(filtro));
                        }

                    }
                }




                if (list.Count > 0)
                {
                    repeaterProductosCatalogo.DataSource = funcion.Filtrar(campo, criterio, filtro);
                    repeaterProductosCatalogo.DataBind();
                }
                else if (list.Count == 0 && tBoxBusqueda.Text == string.Empty.Trim().Replace(" ", ""))
                {

                    repeaterProductosCatalogo.DataSource = conexion.ListaArticulo();
                    repeaterProductosCatalogo.DataBind();
                }
                else if (list.Count == 0 && !(tBoxBusqueda.Text == string.Empty.Trim().Replace(" ", "")))
                {
                    repeaterProductosCatalogo.DataSource = list;
                    repeaterProductosCatalogo.DataBind();
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

        protected void tBoxFiltroRapido_TextChanged(object sender, EventArgs e)
        {


            /* vector que contendrá todos los caracteres de modelo, nombre y precio de cada producto del catálogo. De esta manera, con
            el filtro rápido, lo que el usuario ingrese será buscado por el método FindAll en el vector 
           */

            //List<char> listaCaracteresTotalesProducuto = new List<char>();

            string caracteresTotales = string.Empty;

            try
            {
                if (Session["list"] != null)
                {
                    ConexionArticulo conexion = new ConexionArticulo();
                    List<Articulo> list = Session["list"] as List<Articulo>;
                    list = conexion.ListaArticulo();

                    foreach (Articulo articulo in list)
                    {
                        foreach (char codArticulo in articulo.CodigoArticulo)
                        {
                            if (!caracteresTotales.ToLower().Contains(codArticulo.ToString().ToLower()))
                            {
                                caracteresTotales += articulo.CodigoArticulo;
                            }
                        }

                        foreach (char nombre in articulo.Nombre)
                        {
                            if (!caracteresTotales.ToLower().Contains(nombre.ToString().ToLower()))
                            {
                                caracteresTotales += articulo.Nombre;
                            }
                        }

                        foreach (char precio in articulo.Precio.ToString())
                        {
                            if (!caracteresTotales.ToLower().Contains(precio.ToString().ToLower()))
                            {
                                caracteresTotales += articulo.Precio;
                            }
                        }
                    }



                    foreach (char caracter in caracteresTotales)
                    {
                        if (tBoxFiltroRapido.Text.ToLower().Contains(caracter.ToString().ToLower()))
                        {
                            repeaterProductosCatalogo.DataSource = list.FindAll(articulo => articulo.CodigoArticulo.ToLower().Contains(tBoxFiltroRapido.Text.ToLower()) ||
                            articulo.Nombre.ToLower().Contains(tBoxFiltroRapido.Text.ToLower()) || articulo.Precio.ToString().ToLower().Contains(tBoxFiltroRapido.Text.ToLower()));
                            repeaterProductosCatalogo.DataBind();
                        }

                    }


                    if (tBoxFiltroRapido.Text == string.Empty)
                    {
                        repeaterProductosCatalogo.DataSource = list;
                        repeaterProductosCatalogo.DataBind();
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

            try
            {
                if (Session["list"] == null)
                {
                    ConexionArticulo conexion = new ConexionArticulo();
                    List<Articulo> list = new List<Articulo>();
                    list = conexion.ListaArticulo();

                    Session["list"] = list;

                    foreach (Articulo articulo in list)
                    {
                        foreach (char codArticulo in articulo.CodigoArticulo)
                        {
                            if (!caracteresTotales.ToLower().Contains(codArticulo.ToString().ToLower()))
                            {
                                caracteresTotales += articulo.CodigoArticulo;
                            }
                        }

                        foreach (char nombre in articulo.Nombre)
                        {
                            if (!caracteresTotales.ToLower().Contains(nombre.ToString().ToLower()))
                            {
                                caracteresTotales += articulo.Nombre;
                            }
                        }

                        foreach (char precio in articulo.Precio.ToString())
                        {
                            if (!caracteresTotales.ToLower().Contains(precio.ToString().ToLower()))
                            {
                                caracteresTotales += articulo.Precio;
                            }
                        }
                    }



                    foreach (char caracter in caracteresTotales)
                    {
                        if (tBoxFiltroRapido.Text.ToLower().Contains(caracter.ToString().ToLower()))
                        {
                            repeaterProductosCatalogo.DataSource = list.FindAll(articulo => articulo.CodigoArticulo.ToLower().Contains(tBoxFiltroRapido.Text.ToLower()) ||
                            articulo.Nombre.ToLower().Contains(tBoxFiltroRapido.Text.ToLower()) || articulo.Precio.ToString().ToLower().Contains(tBoxFiltroRapido.Text.ToLower()));
                            repeaterProductosCatalogo.DataBind();
                        }

                    }



                    if (tBoxFiltroRapido.Text == string.Empty)
                    {
                        repeaterProductosCatalogo.DataSource = list;
                        repeaterProductosCatalogo.DataBind();
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


        protected void btonAgregarProductoFavorito_Click(object sender, EventArgs e)
        {
            string idArticulo = ((LinkButton)sender).CommandArgument;

            if (Session["UsuarioLogin"] != null)
            {
                FuncionesDeFormulario funciones = new FuncionesDeFormulario();
                int idUsuario = ((Usuario)Session["UsuarioLogin"]).id;

                funciones.AgregarProdutoFavorito(Convert.ToInt32(idArticulo), idUsuario);



                Response.Redirect($"~/Paginas_aspx_NoAdmin/ProductoFavorito.aspx", true);

            }
        }

        protected void btonEliminarProductoFavorito_Click(object sender, EventArgs e)
        {

            string idArticulo = ((LinkButton)sender).CommandArgument;

            if (Session["UsuarioLogin"] != null)
            {

                FuncionesDeFormulario funciones = new FuncionesDeFormulario();

                int idUsuario = ((Usuario)Session["UsuarioLogin"]).id;

                funciones.EliminarProductoFavorito(Convert.ToInt32(idArticulo), idUsuario);

                Response.Redirect($"~/Paginas_aspx_NoAdmin/ProductoFavorito.aspx", true);


            }


        }
    }
}
