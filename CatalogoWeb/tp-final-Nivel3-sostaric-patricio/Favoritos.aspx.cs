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
                Cargar();
            }






        }

        protected void btnEliminarFavorito_Click(object sender, EventArgs e)
        {
            // Usuario user = (Usuario)Session["Usuario"];
            Usuario user = ObtenerUsuario();
            ArticuloFavoritoNegocio negocio = new ArticuloFavoritoNegocio();

            // Obtener el IdArticuloFav del botón que se hizo click
           // Usuario userGuardado = user;
            int idArticulo = int.Parse(((Button)sender).CommandArgument);

            // Obtener el IdUser del usuario logueado

            int idUser = user.Id;

            // Eliminar el registro de la tabla FAVORITOS, solo si pertenece al usuario logueado
            negocio.eliminarFavorito(idArticulo, idUser);



            Cargar();
            //actualizar la pagina: 
            Page_Load(sender, e);




        }
        private void Cargar()
        {
            // Usuario user = (Usuario)Session["Usuario"];
            Usuario user = ObtenerUsuario();

            string id = Request.QueryString["Id"];


            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int idArticulo))
            {
                ArticuloFavoritoNegocio negocio = new ArticuloFavoritoNegocio();
                ArticuloFavorito nuevo = new ArticuloFavorito();

                nuevo.IdUser = user.Id;
                nuevo.IdArticulo = int.Parse(id);

                negocio.insertarNuevoFavorito(nuevo);
            }
            ListaArticulo = new List<Articulo>();

            if (user != null)
            {
                ArticuloFavoritoNegocio negocioart = new ArticuloFavoritoNegocio();
                List<int> idArticulosFavoritos = negocioart.listarFavUserId(user.Id);
                if (idArticulosFavoritos.Count > 0)
                {
                    ArticuloNegocio art = new ArticuloNegocio();
                    ListaArticulo = art.listarArtById(idArticulosFavoritos);
                    RepetidorFavorito.DataSource = ListaArticulo;
                    RepetidorFavorito.DataBind();
                }
                
                
            }
            else
            {
                Session.Add("error", "No se han podido cargar los articulos favoritos 😕");
                Response.Redirect("Error.aspx", false);
            }
              if (ListaArticulo.Count == 0)
                {
                    RepetidorFavorito.Visible=false;
                }
                else
                {
                    RepetidorFavorito.Visible = true;
                }


        }
        public Usuario ObtenerUsuario()
        {
            if (Session["Usuario"] != null)
            {
                return (Usuario)Session["Usuario"];
            }
            else
            {
                return null;
            }
        }
    }
}



