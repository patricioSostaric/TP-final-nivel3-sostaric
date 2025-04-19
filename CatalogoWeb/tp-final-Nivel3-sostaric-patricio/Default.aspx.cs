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
    public partial class Default : System.Web.UI.Page
    {
        public List<Articulo> ListaArticulo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            

            ArticuloNegocio negocio = new ArticuloNegocio();
            ListaArticulo = negocio.listarConSP();

            if (!IsPostBack)
            {
                Session.Add("listaArticulos", negocio.listarConSP());
                repRepetidor.DataSource = Session["listaArticulos"];
                repRepetidor.DataBind();
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
            List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            repRepetidor.DataSource = listaFiltrada;
            repRepetidor.DataBind();
        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            string valor = ((Button)sender).CommandArgument;

            Session.Add("error", "Disponible próximamente 😁");
            Response.Redirect("Error.aspx",false);
        }

        protected void btnAgregarFavorito_Click(object sender, EventArgs e)
        {
            int idArticulo = int.Parse(((Button)sender).CommandArgument);
            AgregarFavorito(idArticulo);
        }
        private void AgregarFavorito(int idArticulo)
        {
            try
            {
                Usuario user = (Usuario)Session["Usuario"];
                ArticuloFavoritoNegocio negocio = new ArticuloFavoritoNegocio();
                ArticuloFavorito nuevoFavorito = new ArticuloFavorito();
                nuevoFavorito.IdUser = user.Id;
                nuevoFavorito.IdArticulo = idArticulo;
                negocio.insertarNuevoFavorito(nuevoFavorito);
                List<Articulo> listaArticulos = (List<Articulo>)Session["ListaFavoritos"];
                if (listaArticulos == null)
                {
                    listaArticulos = new List<Articulo>();
                }
                ArticuloNegocio artNegocio = new ArticuloNegocio();
                Articulo articulo = artNegocio.listarArtById(new List<int> { idArticulo }).FirstOrDefault();
                if (articulo != null)
                {
                    listaArticulos.Add(articulo);
                }
                Session["ListaFavoritos"] = listaArticulos;
                Response.Redirect("Favoritos.aspx", false);
            }
            catch (Exception ex)
            {
                // Manejar la excepción, por ejemplo, mostrar un mensaje de error
                Session.Add("error", "Error al agregar artículo a favoritos: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }
    }
}