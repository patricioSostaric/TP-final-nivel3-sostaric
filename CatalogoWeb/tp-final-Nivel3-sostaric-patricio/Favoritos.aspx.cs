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
    public partial class Favoritos : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        protected void Page_Load(object sender, EventArgs e)

        {

            if (!IsPostBack)
            {
                Usuario user = ObtenerUsuario();
                if (user == null)
                {
                    Session.Add("error", "⚠ Debe iniciar sesión para ver favoritos.");
                    Response.Redirect("Error.aspx", false);
                    return;
                }

                ArticuloFavoritoNegocio negocioart = new ArticuloFavoritoNegocio();
                Session["ListaFavoritos"] = negocioart.ListarFavoritosPorUsuario(user.Id);
                Cargar();
            }




        }

        protected void btnEliminarFavorito_Click(object sender, EventArgs e)
        {

            try
            {
                string argumento = ((Button)sender).CommandArgument;
                System.Diagnostics.Debug.WriteLine("CommandArgument recibido: " + argumento);

                if (int.TryParse(argumento, out int idArticulo) && idArticulo > 0)
                {
                    EliminarFavorito(idArticulo);
                    Cargar();
                }
                else
                {
                    Session.Add("error", "⚠ El ID del artículo no es válido.");
                    Response.Redirect("Error.aspx", false);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al eliminar favorito: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }





        }
        private void Cargar()
        {
            Usuario user = ObtenerUsuario();
            if (user == null) return;

            ArticuloFavoritoNegocio negocio = new ArticuloFavoritoNegocio();
            var lista = negocio.ListarFavoritosPorUsuario(user.Id);

            RepetidorFavorito.DataSource = lista;
            RepetidorFavorito.DataBind();


            
            if (lista.Count > 0)
            {
                RepetidorFavorito.DataSource = lista;
                RepetidorFavorito.DataBind();
                RepetidorFavorito.Visible = true;
                lblSinFavoritos.Visible = false;
            }
            else
            {
                RepetidorFavorito.Visible = false;
                lblSinFavoritos.Visible = true;
            }
            
        }


        public Usuario ObtenerUsuario()
        {
            return (Usuario)Session["Usuario"] ;
           
        }
        private void EliminarFavorito(int idArticulo)
        {
            Usuario user = ObtenerUsuario();
            if (user == null)
            {
                Session.Add("error", "⚠ Usuario no válido.");
                Response.Redirect("Error.aspx", false);
                return;
            }

            ArticuloFavoritoNegocio negocio = new ArticuloFavoritoNegocio();
            negocio.eliminarFavorito(idArticulo, user.Id);

            Cargar();


        }



    }
}






