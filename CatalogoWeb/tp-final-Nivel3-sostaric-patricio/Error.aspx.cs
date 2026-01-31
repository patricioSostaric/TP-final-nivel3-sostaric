using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_final_Nivel3_sostaric_patricio
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mensaje = null;

            if (Session["error"] != null)
            {
                mensaje = Session["error"].ToString();
                Session.Remove("error");
            }
            else if (Session["duplicado"] != null)
            {
                mensaje = Session["duplicado"].ToString();
                Session.Remove("duplicado");
            }

            lblError.Text = !string.IsNullOrWhiteSpace(mensaje)
                ? mensaje
                : "⚠ Ha ocurrido un error desconocido.";



        }
    }
}