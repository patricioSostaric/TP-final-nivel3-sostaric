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


            if (!IsPostBack)
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                ListaArticulo = negocio.Listar();

                Session["listaArticulos"] = ListaArticulo;
                repRepetidor.DataSource = Session["listaArticulos"];

                if (Session["Usuario"] != null)
                {
                    Usuario user = (Usuario)Session["Usuario"];
                    ArticuloFavoritoNegocio favNegocio = new ArticuloFavoritoNegocio();
                    Session["ListaFavoritos"] = favNegocio.ListarFavoritosPorUsuario(user.Id);
                }

                repRepetidor.DataSource = Session["listaArticulos"];
                repRepetidor.DataBind();


            }

            if (Session["Usuario"] != null)
            {
                Usuario user = (Usuario)Session["Usuario"];
                ArticuloFavoritoNegocio favNegocio = new ArticuloFavoritoNegocio();
                List<Articulo> favoritos = favNegocio.ListarFavoritosPorUsuario(user.Id);

                Session["ListaFavoritos"] = favoritos;
            }
            // Rebind SIEMPRE, así el ItemDataBound se ejecuta con la info actualizada
            //repRepetidor.DataSource = Session["listaArticulos"];
            // repRepetidor.DataBind();



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
            Response.Redirect("Error.aspx", false);
        }

        protected void btnAgregarFavorito_Click(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("Login.aspx",false);
                Context.ApplicationInstance.CompleteRequest();

                return;
            }

            int idArticulo = int.Parse(((Button)sender).CommandArgument);
            Usuario user = (Usuario)Session["Usuario"];
            ArticuloFavoritoNegocio negocio = new ArticuloFavoritoNegocio();

            try
            {
                if (negocio.ExisteFavorito(idArticulo, user.Id))
                    negocio.eliminarFavorito(idArticulo, user.Id);
                else
                    negocio.insertarNuevoFavorito(new ArticuloFavorito { IdUser = user.Id, IdArticulo = idArticulo });

                // refrescar favoritos y rebind
                Session["ListaFavoritos"] = negocio.ListarFavoritosPorUsuario(user.Id);
                repRepetidor.DataSource = Session["listaArticulos"];
                repRepetidor.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al manejar favoritos: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }


        }



        protected void repRepetidor_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Articulo articulo = (Articulo)e.Item.DataItem;
                Button btnFavorito = (Button)e.Item.FindControl("btnFavorito");

                if (Session["Usuario"] != null && Session["ListaFavoritos"] != null)
                {
                    var favoritos = (List<Articulo>)Session["ListaFavoritos"];
                    bool yaFavorito = favoritos.Any(f => f.Id == articulo.Id);

                    btnFavorito.Text = yaFavorito ? "⭐ Quitar de favoritos" : "♡ Agregar a favoritos";
                    btnFavorito.CssClass = yaFavorito ? "btn btn-warning btn-sm mt-2 ms-2" : "btn btn-outline-warning btn-sm mt-2 ms-2";
                }
                else
                {
                    btnFavorito.Visible = false;
                }
            }



        }
    }
}