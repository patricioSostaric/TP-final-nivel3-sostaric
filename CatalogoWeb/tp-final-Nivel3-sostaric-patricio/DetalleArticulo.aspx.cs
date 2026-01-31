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
                    MarcaNegocio marcanegocio = new MarcaNegocio();
                    ddlMarca.DataSource = marcanegocio.listar();
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                    CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                    ddlCategoria.DataSource = categoriaNegocio.listar();
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();
                }

                // Configuración si estamos modificando
                string idParam = Request.QueryString["id"];
                if (!string.IsNullOrWhiteSpace(idParam) && !IsPostBack)
                {
                    if (int.TryParse(idParam, out int idArticulo))
                    {
                        ArticuloNegocio negocio = new ArticuloNegocio();
                        var lista = negocio.Listar(idParam);

                        if (lista != null && lista.Count > 0)
                        {
                            Articulo seleccionado = lista[0];

                            txtId.Text = idParam;
                            txtId.ReadOnly = true;

                            txtCodigo.Text = seleccionado.Codigo ?? string.Empty;
                            txtCodigo.ReadOnly = true;

                            txtNombre.Text = seleccionado.Nombre ?? string.Empty;
                            txtNombre.ReadOnly = true;

                            txtDescripcion.Text = seleccionado.Descripcion ?? string.Empty;
                            txtDescripcion.ReadOnly = true;

                            txtPrecio.Text = seleccionado.Precio.ToString("0.00");
                            txtPrecio.ReadOnly = true;

                            txtImagenUrl.Text = seleccionado.ImagenUrl ?? string.Empty;
                            txtImagenUrl.ReadOnly = true;

                            ddlMarca.SelectedValue = seleccionado.TipoMarca?.Id.ToString();
                            ddlMarca.Enabled = false;

                            ddlCategoria.SelectedValue = seleccionado.TipoCategoria?.Id.ToString();
                            ddlCategoria.Enabled = false;

                            txtImagenUrl_TextChanged(sender, e);
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

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
           
            if (!string.IsNullOrWhiteSpace(txtImagenUrl.Text))
                imgArticulo.ImageUrl = txtImagenUrl.Text;
           

        }
    }
}