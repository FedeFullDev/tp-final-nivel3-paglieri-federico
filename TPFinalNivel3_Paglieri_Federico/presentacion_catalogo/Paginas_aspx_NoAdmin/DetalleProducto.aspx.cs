using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion_catalogo.Paginas_aspx_NoAdmin
{
    public partial class DetalleProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo();

            if (Session["ArticuloSeleccionado"] != null)
            {
                articulo = (Articulo)Session["ArticuloSeleccionado"];
                tBoxNombre.Text = articulo.Nombre;
                tBoxPrecio.Text = articulo.Precio.ToString();
                tBoxCodigoArticulo.Text = articulo.CodigoArticulo;
                tBoxCategoria.Text = articulo.Categoria.Descripcion;
                tBoxMarca.Text = articulo.Marca.Descripcion;
                tBoxDescripcion.Text = articulo.Descripcion;
            }




        }
    }
}