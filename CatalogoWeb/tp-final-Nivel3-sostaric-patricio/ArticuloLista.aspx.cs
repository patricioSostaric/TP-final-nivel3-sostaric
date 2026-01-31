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
    public partial class ArticuloLista : System.Web.UI.Page
    {
        public bool FiltroAvanzado { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Seguridad.esAdmin(Session["Usuario"]))
            {
                Session.Add("error", "Se requiere permisos de admin para acceder a esta pantalla");
                Response.Redirect("Error.aspx");
            }
            if (!IsPostBack)
            {

                ArticuloNegocio negocio = new ArticuloNegocio();
                Session.Add("listaArticulos", negocio.Listar());
                dgvArticulos.DataSource = Session["listaArticulos"];
                dgvArticulos.DataBind();


                CargarCriterios();

            }

        }

        protected void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            //  List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
            //List<Articulo> listaFiltrada = lista.FindAll(x => x.Nombre.ToUpper().Contains(txtFiltro.Text.ToUpper()));
            //dgvArticulos.DataSource = listaFiltrada;
            //dgvArticulos.DataBind();
            try
            {
                string filtro = txtFiltro.Text.Trim();

                // Validación: filtro vacío
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    lblMensaje.Text = "⚠ Ingresá un valor para buscar por nombre.";
                    lblMensaje.CssClass = "alert alert-warning d-block";
                    lblMensaje.Visible = true;

                    dgvArticulos.DataSource = null;
                    dgvArticulos.DataBind();
                    return;
                }

                if (Session["listaArticulos"] == null)
                {
                    lblMensaje.Text = "⚠ No se pudo acceder a la lista de artículos.";
                    lblMensaje.CssClass = "alert alert-danger d-block";
                    lblMensaje.Visible = true;
                    return;
                }

                List<Articulo> lista = (List<Articulo>)Session["listaArticulos"];
                List<Articulo> listaFiltrada = lista.FindAll(x =>
                    x.Nombre.ToUpper().Contains(filtro.ToUpper()));


                dgvArticulos.DataSource = listaFiltrada;
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }


        }

        protected void chkFiltroAvanzado_CheckedChanged(object sender, EventArgs e)
        {
            FiltroAvanzado = chkFiltroAvanzado.Checked;
            txtFiltro.Enabled = !FiltroAvanzado;

            if (FiltroAvanzado)
            {
                // Limpia el filtro común al activar el avanzado
                txtFiltro.Text = string.Empty;
                lblMensaje.Visible = false; // oculta mensajes previos
            }

            if (!FiltroAvanzado)
            {

                dgvArticulos.DataSource = Session["listaArticulos"];
                dgvArticulos.DataBind();
            }


        }

        protected void ddlCampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarCriterios();

            // limpia el filtro para evitar valores inválidos
            txtFiltroAvanzado.Text = string.Empty;

            // Ajusta el TextMode dinámicamente según el campo
            if (ddlCampo.SelectedValue == "Precio")
            {
                // Solo acepta números/decimales
                txtFiltroAvanzado.TextMode = TextBoxMode.Number;
            }
            else
            {
                // Campos alfanuméricos: texto libre
                txtFiltroAvanzado.TextMode = TextBoxMode.SingleLine;
            }



        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
           
        
            try
            {
                string campo = ddlCampo.SelectedValue;
                string criterio = ddlCriterio.SelectedValue;
                string filtro = txtFiltroAvanzado.Text.Trim();

                // Validación: filtro vacío
                if (string.IsNullOrWhiteSpace(filtro))
                {
                    lblMensaje.Text = "⚠ Ingresá un valor para filtrar.";
                    lblMensaje.CssClass = "alert alert-warning d-block";
                    lblMensaje.Visible = true;
                    return; // corta la ejecución, no llama a Filtrar
                }

                // Validación: filtro no numérico en campo Precio
                if (campo == "Precio" && !decimal.TryParse(filtro, out _))
                {
                    lblMensaje.Text = "⚠ Ingresá un número válido para el campo Precio.";
                    lblMensaje.CssClass = "alert alert-warning d-block";
                    lblMensaje.Visible = true;
                    return;
                }

                // Ejecución del filtro
                ArticuloNegocio negocio = new ArticuloNegocio();
                var listaFiltrada = negocio.Filtrar(campo, criterio, filtro);

                if (listaFiltrada.Count == 0)
                {
                    lblMensaje.Text = "⚠ No se encontraron artículos con ese criterio.";
                    lblMensaje.CssClass = "alert alert-danger d-block";
                    lblMensaje.Visible = true;
                }
                else
                {
                    lblMensaje.Visible = false;
                }

                dgvArticulos.DataSource = listaFiltrada;
                dgvArticulos.DataBind();
            }
            catch (Exception ex)
            {
                Session.Add("error", ex.ToString());
                Response.Redirect("Error.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        


        }

        protected void dgvArticulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvArticulos.SelectedDataKey.Value.ToString();
            Response.Redirect("FormularioArticulo.aspx?id=" + id);
        }

        protected void dgvArticulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvArticulos.PageIndex = e.NewPageIndex;
            if (Session["listaArticulos"] != null)
            {

                dgvArticulos.DataSource = Session["listaArticulos"];
                dgvArticulos.DataBind();
            }
        }

        private void CargarCriterios()
        {
            ddlCriterio.Items.Clear();
            //string campoSeleccionado = ddlCampo.SelectedItem?.Text;
            string campoSeleccionado = ddlCampo.SelectedValue;



            switch (campoSeleccionado)
            {

                case "Codigo":
                case "Nombre":
                case "Marca":
                case "Categoria":

                    ddlCriterio.Items.Add("Contiene");
                    ddlCriterio.Items.Add("Comienza con");
                    ddlCriterio.Items.Add("Termina con");
                    break;

                case "Precio":
                    ddlCriterio.Items.Add("Igual a");
                    ddlCriterio.Items.Add("Mayor a");
                    ddlCriterio.Items.Add("Menor a");
                    break;

                default:

                    ddlCriterio.Items.Add("Contiene");
                    break;
            }


            ddlCriterio.SelectedIndex = 0;
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            // Limpia controles
            txtFiltroAvanzado.Text = string.Empty;
            ddlCampo.SelectedIndex = 0;
            CargarCriterios();
            lblMensaje.Visible = false;

            // Desmarca el filtro avanzado y oculta la sección
            chkFiltroAvanzado.Checked = false;
           
            // Recarga la grilla completa
            ArticuloNegocio negocio = new ArticuloNegocio();
            dgvArticulos.DataSource = negocio.Listar(); 
            dgvArticulos.DataBind();


        }
    }
}