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
            
            if (Session["error"] != null)
            {
                lblError.Text = Session["error"].ToString();
                Session.Remove("error");
            }
            else if (Session["duplicado"] != null)
            {
                lblError.Text = Session["duplicado"].ToString();
                Session.Remove("duplicado");
            }


        }
    }
}