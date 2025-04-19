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
                if (Session["ListaFavoritos"] == null)
                {
                    ArticuloFavoritoNegocio negocioart = new ArticuloFavoritoNegocio();
                    List<int> idArticulosFavoritos = negocioart.listarFavUserId(user.Id);
                    Session["ListaFavoritos"] = idArticulosFavoritos.Count > 0
                        ? new ArticuloNegocio().listarArtById(idArticulosFavoritos)
                        : new List<Articulo>();
                }
                Cargar();
            }





        }

        protected void btnEliminarFavorito_Click(object sender, EventArgs e)
        {


            // Obtener el IdArticuloFav del botón que se hizo click
            int idArticulo = int.Parse(((Button)sender).CommandArgument);
            EliminarFavorito(idArticulo);
            //actualizar la pagina:
            Cargar();

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
            return (Usuario)Session["Usuario"] ?? null;
        }
        private void EliminarFavorito(int idArticulo)
        {

            Usuario user = ObtenerUsuario();
            ArticuloFavoritoNegocio negocio = new ArticuloFavoritoNegocio();
            negocio.eliminarFavorito(idArticulo, user.Id);
            List<Articulo> listaArticulos = (List<Articulo>)Session["ListaFavoritos"] ?? new List<Articulo>();
            listaArticulos.RemoveAll(a => a.Id == idArticulo);
            Session["ListaFavoritos"] = listaArticulos;
            Cargar();

        }
    }
}



