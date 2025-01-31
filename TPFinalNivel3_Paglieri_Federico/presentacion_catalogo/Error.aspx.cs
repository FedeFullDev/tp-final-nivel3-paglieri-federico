using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace presentacion_catalogo
{
    public partial class Error : System.Web.UI.Page
    {
        public string errorMessage = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["Error"] != null)
            {
                errorMessage = Session["Error"].ToString();
                if (Session["errorMessage"] == null)
                {
                    Session.Add("errorMessage", errorMessage);
                }
                else
                {
                    Session["errorMessage"] = errorMessage;
                }




            }


        }
    }
}