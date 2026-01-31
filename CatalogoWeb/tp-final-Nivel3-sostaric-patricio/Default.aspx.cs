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
            ListaArticulo = negocio.Listar();

            if (!IsPostBack)
            {
                Session.Add("listaArticulos", negocio.Listar());
                repRepetidor.DataSource = Session["listaArticulos"];
                repRepetidor.DataBind();
            }
        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Si el filtro está vacío → mostrar todos
                if (string.IsNullOrWhiteSpace(txtFiltro.Text))
                {
                    if (Session["listaArticulos"] != null)
                    {
                        lblMensaje.Visible = false;
                        repRepetidor.DataSource = (List<Articulo>)Session["listaArticulos"];
                        repRepetidor.DataBind();
                    }
                    return;
                }

                // Validación: sesión nula
                if (Session["listaArticulos"] == null)
                {
                    lblMensaje.Text = "⚠ No se pudo acceder a la lista de artículos.";
                    lblMensaje.CssClass = "alert alert-danger d-block";
                    lblMensaje.Visible = true;
                    return;
                }

                List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
                List<Articulo> listaFiltrada = lista.FindAll(x =>
                    x.Nombre.ToUpper().Contains(txtFiltro.Text.Trim().ToUpper()));

                if (listaFiltrada.Count == 0)
                {
                    lblMensaje.Text = "⚠ No se encontraron artículos con ese nombre.";
                    lblMensaje.CssClass = "alert alert-danger d-block";
                    lblMensaje.Visible = true;

                    repRepetidor.DataSource = null;
                    repRepetidor.DataBind();
                }
                else
                {
                    lblMensaje.Visible = false;
                    repRepetidor.DataSource = listaFiltrada;
                    repRepetidor.DataBind();
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }




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
            AgregarFavorito(idArticulo, (Button)sender);
        }
       
        private void AgregarFavorito(int idArticulo, Button btnAgregarFavorito)
        {
            try
            {
                Usuario user = (Usuario)Session["Usuario"];
                ArticuloFavoritoNegocio negocio = new ArticuloFavoritoNegocio();
                if (negocio.ExisteFavorito(idArticulo, user.Id))
                {
                    Session.Add("duplicado", "el articulo ya estaba agregado a favoritos");
                    Response.Redirect("Error.aspx", false);
                }
                else
                {
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
                    Articulo articulo = artNegocio.BuscarPorId(idArticulo);
                    if (articulo != null)
                    {
                        listaArticulos.Add(articulo);
                    }
                    Session["ListaFavoritos"] = listaArticulos;
                    Response.Redirect("Favoritos.aspx", false);
                }
            }
            catch (Exception ex)
            {
                
                Session.Add("error", "Error al agregar artículo a favoritos: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }
        }


    }
}