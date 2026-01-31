using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_final_Nivel3_sostaric_patricio
{
    public partial class MiPerfil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (Seguridad.sesionActiva(Session["Usuario"]))
                    {
                        Usuario user = (Usuario)Session["Usuario"];
                        txtEmail.Text = user.Email;
                        txtEmail.ReadOnly = true;
                        txtNombre.Text = user.Nombre;
                        txtApellido.Text = user.Apellido;

                        if (!string.IsNullOrEmpty(user.UrlImagenPerfil))
                        {
                            imgNuevoPerfil.ImageUrl = "~/Images/" + user.UrlImagenPerfil + "?v=" + DateTime.Now.Ticks;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error en MiPerfil: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }



        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Page.Validate();
                if (!Page.IsValid)
                    return;

                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario user = (Usuario)Session["Usuario"]; 

                // Guardar imagen si se subió
                if (txtImagen.PostedFile != null && txtImagen.PostedFile.ContentLength > 0)
                {
                    string ruta = Server.MapPath("~/Images/");
                    string nombreArchivo = "perfil-" + user.Id + ".jpg";
                    string rutaCompleta = Path.Combine(ruta, nombreArchivo);

                    txtImagen.PostedFile.SaveAs(rutaCompleta);
                    user.UrlImagenPerfil = nombreArchivo;
                }

                // Actualizar datos
                user.Nombre = txtNombre.Text;
                user.Apellido = txtApellido.Text;
                negocio.actualizar(user);

                // Refrescar avatar en MasterPage
                Image img = Master.FindControl("imgAvatar") as Image;
                if (img != null && !string.IsNullOrEmpty(user.UrlImagenPerfil))
                {
                    img.ImageUrl = "~/Images/" + user.UrlImagenPerfil + "?v=" + DateTime.Now.Ticks;
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al guardar perfil: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }



        }


    }
    
}