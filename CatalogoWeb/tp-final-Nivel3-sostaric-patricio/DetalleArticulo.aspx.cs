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
    public partial class DetalleArticulo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                if (!IsPostBack)
                {
                    string idParam = Request.QueryString["id"];
                    if (!string.IsNullOrWhiteSpace(idParam) && int.TryParse(idParam, out int idArticulo))
                    {
                        ArticuloNegocio negocio = new ArticuloNegocio();
                        var lista = negocio.Listar(idParam);

                        if (lista != null && lista.Count > 0)
                        {
                            Articulo seleccionado = lista[0];

                            // Asignar valores a Labels
                            lblNombre.Text = seleccionado.Nombre ?? string.Empty;
                            lblMarca.Text = seleccionado.TipoMarca?.Descripcion ?? string.Empty;
                            lblCategoria.Text = seleccionado.TipoCategoria?.Descripcion ?? string.Empty;
                            lblPrecio.Text = seleccionado.Precio.ToString("0.00");
                            lblDescripcion.Text = seleccionado.Descripcion ?? string.Empty;

                            // Imagen
                            imgArticulo.ImageUrl = seleccionado.ImagenUrl ?? string.Empty;
                        }
                        else
                        {
                            Session.Add("error", "⚠ No se encontró el artículo solicitado.");
                            Response.Redirect("Error.aspx", false);
                        }
                    }
                    else
                    {
                        Session.Add("error", "⚠ El parámetro ID no es válido.");
                        Response.Redirect("Error.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error en DetalleArticulo: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }




        }


    }
}