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
    public partial class FormularioArticulo : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            txtId.Enabled = false;


            try
            {
                if (!IsPostBack)
                {
                    
                    cargarCombos();

                    
                    string idStr = Request.QueryString["id"];
                    if (!string.IsNullOrEmpty(idStr) && int.TryParse(idStr, out int idArticulo))
                    {
                        ArticuloNegocio negocio = new ArticuloNegocio();
                        Articulo seleccionado = negocio.BuscarPorId(idArticulo);

                        if (seleccionado != null)
                        {
                            txtId.Text = seleccionado.Id.ToString();
                            txtNombre.Text = seleccionado.Nombre;
                            txtCodigo.Text = seleccionado.Codigo;
                            txtDescripcion.Text = seleccionado.Descripcion;
                            txtPrecio.Text = seleccionado.Precio.ToString("0.00");
                            txtImagenUrl.Text = seleccionado.ImagenUrl;
                            imgArticulo.ImageUrl = seleccionado.ImagenUrl;

                            ddlCategoria.SelectedValue = seleccionado.TipoCategoria.Id.ToString();
                            ddlMarca.SelectedValue = seleccionado.TipoMarca.Id.ToString();
                        }
                        else
                        {
                            lblMensaje.Text = "⚠ El ID del artículo no es válido.";
                            lblMensaje.Visible = true;
                            panelFormulario.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al cargar formulario: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }


        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    lblMensaje.Text = "⚠ Debe ingresar un código y un nombre.";
                    lblMensaje.Visible = true;
                    return;
                }

                // Construir objeto desde el formulario
                Articulo nuevo = new Articulo
                {
                    Codigo = txtCodigo.Text.Trim(),
                    Nombre = txtNombre.Text.Trim(),
                    Descripcion = txtDescripcion.Text.Trim(),
                    ImagenUrl = txtImagenUrl.Text.Trim()
                };

                // Validar y asignar precio
                if (!Validacion.ValidaDecimal(txtPrecio, out decimal precio))
                {
                    lblMensaje.Text = "⚠ El precio debe ser un número válido.";
                    lblMensaje.Visible = true;
                    return;
                }
                nuevo.Precio = precio;

                // Validar selección de combos
                if (!Validacion.ValidaDropDown(ddlCategoria) || !Validacion.ValidaDropDown(ddlMarca))
                {
                    lblMensaje.Text = "⚠ Debe seleccionar una categoría y una marca.";
                    lblMensaje.Visible = true;
                    return;
                }

                nuevo.TipoCategoria = new Categoria { Id = int.Parse(ddlCategoria.SelectedValue) };
                nuevo.TipoMarca = new Marca { Id = int.Parse(ddlMarca.SelectedValue) };

                ArticuloNegocio negocio = new ArticuloNegocio();

                // Alta o edición → usar solo QueryString
                string idStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idStr) && int.TryParse(idStr, out int idArticulo))
                {
                    // Edición
                    nuevo.Id = idArticulo;
                    negocio.Modificar(nuevo);
                }
                else
                {
                    // Alta
                    negocio.Agregar(nuevo);
                }

                // Si todo salió bien → volver a la lista
                Response.Redirect("ArticuloLista.aspx", false);
            }
            catch (Exception ex)
            {
                // Feedback claro si es duplicado
                if (ex.Message.Contains("código") || ex.Message.Contains("nombre"))
                {
                    lblMensaje.Text = "⚠ Ya existe otro artículo con ese código o nombre.";
                    lblMensaje.Visible = true;
                    return;
                }

                // Errores inesperados → redirigir a Error.aspx
                Session.Add("error", "Error al guardar artículo: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }



        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            //  Siempre presente en el markup, no dinámico
            imgArticulo.ImageUrl = string.IsNullOrWhiteSpace(txtImagenUrl.Text)
                ? "https://grupoact.com.ar/wp-content/uploads/2020/04/placeholder.png"
                : txtImagenUrl.Text;


        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
            chkConfirmaEliminacion.Visible = true;
            btnConfirmaEliminar.Visible = true;



        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!chkConfirmaEliminacion.Checked)
                {
                    lblMensaje.Text = "⚠ Debe confirmar la eliminación antes de continuar.";
                    lblMensaje.Visible = true;
                    return;
                }

                // Usar directamente el QueryString
                string idStr = Request.QueryString["id"];
                if (string.IsNullOrEmpty(idStr) || !int.TryParse(idStr, out int idArticulo))
                {
                    lblMensaje.Text = "⚠ El ID del artículo no es válido.";
                    lblMensaje.Visible = true;
                    return;
                }

                ArticuloNegocio negocio = new ArticuloNegocio();
                negocio.Eliminar(idArticulo);

                Response.Redirect("ArticuloLista.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al eliminar artículo: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }


        }
        private void cargarCombos()
        {
            CategoriaNegocio catNegocio = new CategoriaNegocio();
            ddlCategoria.DataSource = catNegocio.listar();
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataTextField = "Descripcion";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Seleccione una categoría", ""));

            MarcaNegocio marcaNegocio = new MarcaNegocio();
            ddlMarca.DataSource = marcaNegocio.listar();
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataTextField = "Descripcion";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("Seleccione una marca", ""));


        }

    }
}