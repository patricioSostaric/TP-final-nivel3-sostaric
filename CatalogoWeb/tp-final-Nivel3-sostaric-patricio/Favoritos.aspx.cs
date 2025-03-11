using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_final_Nivel3_sostaric_patricio
{
    public partial class Favoritos : System.Web.UI.Page
    {
        public List<Articulo> ListaFavorito { get; set; }
        protected void Page_Load(object sender, EventArgs e)


        {
            EliminarFavorito.CommandArgument = "IdArticulo";//establece el valor de command argument
            Usuario user = (Usuario)Session["Usuario"];
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out int idArticulo))
            {
                ArticuloFavoritoNegocio negocio = new ArticuloFavoritoNegocio();
                ArticuloFavorito nuevo = new ArticuloFavorito();
                nuevo.IdUser = user.Id;
                nuevo.IdArticulo = int.Parse(id);
                negocio.insertarNuevoFavorito(nuevo);
            }
            ListaFavorito = new List<Articulo>();
            if (user != null)
            {
                ArticuloFavoritoNegocio negocioart = new ArticuloFavoritoNegocio();
                List<int> idArticulosFavoritos = negocioart.listarFavUserId(user.Id);
                ArticuloNegocio art = new ArticuloNegocio();
                ListaFavorito = art.listarArtById(idArticulosFavoritos);
            }
        }

        protected void EliminarFavorito_Click(object sender, EventArgs e)
        {
            // Button btn = (Button)sender;
            //int idArticulo = int.Parse(btn.CommandArgument);
            // int idUser = (int)Session["Usuario"];
            // ArticuloFavoritoNegocio favoritoNegocio = new ArticuloFavoritoNegocio();
            // favoritoNegocio.eliminarFavorito(idArticulo, idUser);
            //  Response.Redirect("Favoritos.aspx");
            // Button btn = (Button)sender;
            //Button boton = (Button)sender;
            //int idArticulo = int.Parse(boton.ToString());

            //int idUser = (int)HttpContext.Current.Session["Usuario"];
            //  ArticuloFavoritoNegocio favoritoNegocio = new ArticuloFavoritoNegocio();
            // favoritoNegocio.eliminarFavorito(idArticulo, idUser);
            // Response.Redirect("Favoritos.aspx");

            Button btn = (Button)sender;

            if (int.TryParse(btn.CommandArgument, out int idArticulo))
            {
                // El valor es un número entero, puedes utilizar idArticulo
                int idUser = (int)HttpContext.Current.Session["Usuario"];
                ArticuloFavoritoNegocio favoritoNegocio = new ArticuloFavoritoNegocio();
                favoritoNegocio.eliminarFavorito(idArticulo, idUser);
                Response.Redirect("Favoritos.aspx",false);
            }
            else
            {
                // El valor no es un número entero, puedes mostrar un error
                Response.Write("Error: El ID del artículo no es válido.");
            }
        }

        protected void eliminar_Command(object sender, CommandEventArgs e)
        {
            Response.Write("Valor de CommandArgument: " + e.CommandArgument);

            // Verifica si el valor es un número válido
            if (int.TryParse(e.CommandArgument.ToString(), out int idArticulo))
            {
                // Si es un número válido, ejecuta el código para eliminar el artículo
                ArticuloFavoritoNegocio favoritoNegocio = new ArticuloFavoritoNegocio();
                favoritoNegocio.eliminarFavorito(idArticulo, (int)HttpContext.Current.Session["usuario"]);
                Response.Redirect("Favoritos.aspx");
            }
            else
            {
                // Si no es un número válido, muestra un mensaje de error
                Response.Write("Error: El valor de CommandArgument no es un número válido.");
            }
        }
    }
}
        //protected void Page_Init(object sender, EventArgs e)
       // {
           // ClientScriptManager cs = Page.ClientScript;
           // cs.RegisterForEventValidation(typeof(System.Web.UI.WebControls.Button).ToString(), "EliminarFav");

           
       // }

       /* protected void btnEliminarFav_Click(object sender, EventArgs e)
        {
            Usuario user = (Usuario)Session["usuario"];
            ArticuloFavoritoNegocio negocio = new ArticuloFavoritoNegocio();

            // Obtener el IdArticuloFav del botón que se hizo click
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);

            int idArticulo = int.Parse(((Button)sender).CommandArgument);

            // Obtener el IdUser del usuario logueado
            int idUser = int.Parse(Session["UserId"].ToString());
            //int idUser = user.Id;

            // Eliminar el registro de la tabla FAVORITOS, solo si pertenece al usuario logueado
            negocio.eliminarFavorito(idArticulo, idUser);
            //negocio.eliminarFav(id);

            //actualizar la pagina: 
            Response.Redirect("Favoritos.aspx", false);

        }*/
    
