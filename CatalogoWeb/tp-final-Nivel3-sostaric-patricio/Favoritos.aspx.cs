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

                // Validación  ID debe ser entero válido
                if (Validacion.ValidaEntero(argumento, out int idArticulo))
                {
                    EliminarFavorito(idArticulo);
                    Cargar(); // refresca la página
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

            List<Articulo> lista = (List<Articulo>)Session["ListaFavoritos"] ?? new List<Articulo>();
            RepetidorFavorito.DataSource = lista;
            RepetidorFavorito.DataBind();
            RepetidorFavorito.Visible = lista.Count > 0;
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
            try
            {
                negocio.eliminarFavorito(idArticulo, user.Id);

                List<Articulo> listaArticulos = (List<Articulo>)Session["ListaFavoritos"] ?? new List<Articulo>();
                listaArticulos.RemoveAll(a => a.Id == idArticulo);
                Session["ListaFavoritos"] = listaArticulos;

                Cargar();
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al eliminar favorito: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }



    }
}






