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
            ConfirmaEliminacion = false;
            try
            {
                //configuración inicial de la pantalla.
                if (!IsPostBack)
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    List<Categoria> lista = negocio.listar();

                    MarcaNegocio negocio1 = new MarcaNegocio();
                    List<Marca> lista1 = negocio1.listar();

                    ddlCategoria.DataSource = lista;
                    ddlCategoria.DataValueField = "Id";
                    ddlCategoria.DataTextField = "Descripcion";
                    ddlCategoria.DataBind();

                    ddlMarca.DataSource = lista1;
                    ddlMarca.DataValueField = "Id";
                    ddlMarca.DataTextField = "Descripcion";
                    ddlMarca.DataBind();

                }
                //configuración si estamos modificando.
                string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
                if (id != "" && !IsPostBack)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    // Usamos BuscarPorId porque devuelve directamente un solo artículo

                    Articulo seleccionado = negocio.BuscarPorId(int.Parse(id));

                    //guardo Articulo seleccionado en session
                    Session.Add("ArticuloSeleccionado", seleccionado);

                    //pre cargar todos los campos...
                    txtId.Text = id;
                    txtNombre.Text = seleccionado.Nombre;
                    txtDescripcion.Text = seleccionado.Descripcion;
                    txtImagenUrl.Text = seleccionado.ImagenUrl;
                    txtCodigo.Text = seleccionado.Codigo.ToString();
                    txtPrecio.Text = seleccionado.Precio.ToString("0.00");


                    ddlCategoria.SelectedValue = seleccionado.TipoCategoria.Id.ToString();
                    ddlMarca.SelectedValue = seleccionado.TipoMarca.Id.ToString();
                    txtImagenUrl_TextChanged(sender, e);
                }
            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                Response.Redirect("Error.aspx");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Articulo nuevo = new Articulo();
                

                nuevo.Codigo = txtCodigo.Text;
                nuevo.Nombre = txtNombre.Text;
                nuevo.Descripcion = txtDescripcion.Text;
                nuevo.ImagenUrl = txtImagenUrl.Text;

                // Validar y asignar precio
                if (!Validacion.ValidaDecimal(txtPrecio, out decimal precio))
                {
                    Session.Add("error", "⚠ El precio debe ser un número válido.");
                    Response.Redirect("Error.aspx", false);
                    return;
                }
                nuevo.Precio = precio;
                // Validar selección de combos
                if (!Validacion.ValidaDropDown(ddlCategoria) || !Validacion.ValidaDropDown(ddlMarca))
                {
                    Session.Add("error", "⚠ Debe seleccionar una categoría y una marca.");
                    Response.Redirect("Error.aspx", false);
                    return;
                }

                nuevo.TipoCategoria = new Categoria { Id = int.Parse(ddlCategoria.SelectedValue) };
                nuevo.TipoMarca = new Marca { Id = int.Parse(ddlMarca.SelectedValue) };

                ArticuloNegocio negocio = new ArticuloNegocio();

                if (Request.QueryString["id"] != null && Validacion.ValidaEntero(txtId.Text, out int idArticulo))
                {
                    nuevo.Id = idArticulo;
                    negocio.Modificar(nuevo);
                }
                else
                {
                    negocio.Agregar(nuevo);
                }

                Response.Redirect("ArticuloLista.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al guardar artículo: " + ex.Message);
                Response.Redirect("Error.aspx", false);
            }


        }

        protected void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            imgArticulo.ImageUrl = txtImagenUrl.Text;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnConfirmaEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!chkConfirmaEliminacion.Checked)
                {
                    Session.Add("error", "⚠ Debe confirmar la eliminación antes de continuar.");
                    Response.Redirect("Error.aspx", false);
                    return;
                }

                if (!Validacion.ValidaEntero(txtId.Text, out int idArticulo))
                {
                    Session.Add("error", "⚠ El ID del artículo no es válido.");
                    Response.Redirect("Error.aspx", false);
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
    }
}