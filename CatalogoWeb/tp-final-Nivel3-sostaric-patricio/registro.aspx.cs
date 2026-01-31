using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace tp_final_Nivel3_sostaric_patricio
{
    public partial class registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                try
                {
                    
                    if (Validacion.ValidaTextoVacio(txtEmail) || Validacion.ValidaTextoVacio(txtPassword))
                    {
                        Session.Add("error", "⚠ Email y contraseña son obligatorios.");
                        Response.Redirect("Error.aspx", false);
                        return;
                    }

                    Usuario user = new Usuario
                    {
                        Email = txtEmail.Text,
                        Pass = txtPassword.Text
                    };

                    UsuarioNegocio usuarioNegocio = new UsuarioNegocio();
                    EmailService emailService = new EmailService();

                    
                    user.Id = usuarioNegocio.insertarNuevo(user);

                    
                    Session.Add("Usuario", user);

                    // Enviar correo de bienvenida
                    emailService.armarCorreo(user.Email, "Bienvenido usuario", "Hola, te damos la bienvenida a la aplicación...");
                    emailService.enviarEmail();

                    Response.Redirect("Default.aspx", false);
                }
                catch (Exception ex)
                {
                    Session.Add("error", "Error en registro: " + ex.Message);
                    Response.Redirect("Error.aspx", false);
                }
            }


        }

        protected void cvEmail_ServerValidate(object source, ServerValidateEventArgs args)
        {
            EmailService emailService = new EmailService();
            args.IsValid = !string.IsNullOrWhiteSpace(txtEmail.Text) && !emailService.EmailYaRegistrado(txtEmail.Text);
        }
    }
}