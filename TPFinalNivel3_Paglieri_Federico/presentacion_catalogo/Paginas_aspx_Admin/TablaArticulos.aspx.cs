using Controlador;
using Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion_catalogo.Paginas_aspx_Admin
{
    public partial class TablaArticulos : System.Web.UI.Page
    {

        public bool validarTipoUsuarioAdmin;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConexionArticulo conexion = new ConexionArticulo();
                try
                {
                    GridViewArticulos.DataSource = conexion.ListaArticulo();
                    GridViewArticulos.DataBind();
                }catch(Exception ex)
                {
                    if (Session["Error"] == null)
                    {
                        Session.Add("Error", ex.Message);
                    }
                    else if (Session["Error"] != null)
                    {
                        Session["Error"] = ex.Message;
                    }
                    Response.Redirect("~/Error", false);
                    Context.ApplicationInstance.CompleteRequest();

                }




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
        }


        protected void GridViewArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
           
                int capturarIdArticulo = (int)GridViewArticulos.SelectedDataKey.Value;



                Response.Redirect($"AdministracionArticulos.aspx?id={capturarIdArticulo}", false);
                Context.ApplicationInstance.CompleteRequest();
           
            
        }

        protected void btonAgregarArticulo_Click(object sender, EventArgs e)
        {
           
                Response.Redirect($"AdministracionArticulos.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
           
           
        }

      
    }
}