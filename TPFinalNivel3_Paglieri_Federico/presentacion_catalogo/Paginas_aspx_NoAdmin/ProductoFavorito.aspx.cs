using Controlador;
using MetodosElementales;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion_catalogo.Paginas_aspx_NoAdmin
{
    public partial class ProductoFavorito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UsuarioLogin"] != null)
            {
                FuncionesDeFormulario funciones = new FuncionesDeFormulario();

                var idUsuario = ((Usuario)Session["UsuarioLogin"]).id;

                var listArticulosFavoritos = funciones.SelectProductosFavoritos(idUsuario);
                Session.Add("listArticulosFavoritos", listArticulosFavoritos);

                if (listArticulosFavoritos != null)
                {

                
                    GridViewArticulosFavoritos.DataSource = listArticulosFavoritos;
                    GridViewArticulosFavoritos.DataBind();
                }
            }



        }

        protected void GridViewArticulosFavoritos_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            int.TryParse(((GridView)sender).DataKeys[((GridView)sender).SelectedIndex].Value.ToString(), out id);

            Response.Redirect("Home.aspx?producto=" + id);
        }
    }
    
}