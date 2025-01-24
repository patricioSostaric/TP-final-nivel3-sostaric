<%@ Page Title="" Language="C#" MasterPageFile="~/MiMasterpage.Master" AutoEventWireup="true" CodeBehind="ArticuloLista.aspx.cs" Inherits="tp_final_Nivel3_sostaric_patricio.ArticuloLista" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h1 class ="CentrarTitulo"> grilla de articulos</h1>

    <div class="row">
    <div class="col-6">
        <div class="mb-3">
            <asp:Label Text="Buscar por nombre" CssClass="Margen" runat="server" />
        
            <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" />
        
        </div>
    </div>

    <div class="col-6" style="display: flex; flex-direction: column; justify-content: flex-end;">
    <div class="mb-3">
        <asp:CheckBox runat="server" Text="Filtro Avanzado" CssClass="form-control" ID="chkFiltroAvanzado" AutoPostBack="true"  OnCheckedChanged="chkFiltroAvanzado_CheckedChanged" />
    </div>
</div>
  <%if (chkFiltroAvanzado.Checked)
      { %>
  <div class="row">
      <div class="col-3">
          <div class="mb-3">
              <asp:Label Text="Campo" ID="lblCampo" runat="server" />
              <asp:DropDownList runat="server" AutoPostBack="true" CssClass="form-control" id="ddlCampo" OnSelectedIndexChanged ="ddlCampo_SelectedIndexChanged">
                  <asp:ListItem Text="Codigo" />
                  <asp:ListItem Text="Precio" />
                 
              </asp:DropDownList>
          </div>
      </div>
      <div class="col-3">
          <div class="mb-3">
              <asp:Label Text="Criterio" runat="server" />
              <asp:DropDownList runat="server" ID="ddlCriterio" CssClass="form-control"></asp:DropDownList>
          </div>
      </div>
      <div class="col-3">
          <div class="mb-3">
              <asp:Label Text="Filtro" runat="server" />
              <asp:TextBox runat="server" ID="txtFiltroAvanzado" CssClass="form-control" />
          </div>
      </div>
      
  </div>
  <div class="row">
      <div class="col-3">
          <div class="mb-3">
              <asp:Button Text="Buscar" runat="server" CssClass="btn btn-primary" id="btnBuscar" OnClick="btnBuscar_Click"/>
          </div>
      </div>
  </div>
  <%} %>
    </div>
    <div class="Mi-tabla" >

     <asp:GridView ID="dgvArticulos" CssClass="table table-striped table-hover table-m" AutoGenerateColumns="false" DataKeyNames="Id"
     OnSelectedIndexChanged="dgvArticulos_SelectedIndexChanged"
     OnPageIndexChanging="dgvArticulos_PageIndexChanging"
     AllowPaging="true" PageSize="5" runat="server">
     <Columns>
         <asp:BoundField HeaderText="Código" DataField="Codigo" />
         <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
         <asp:BoundField HeaderText="Descripción" DataField="Descripcion" />
         <asp:BoundField HeaderText="Marca" DataField="TipoMarca.Descripcion" />
         <asp:BoundField HeaderText="Categoría" DataField="TipoCategoria.Descripcion" />
         <asp:BoundField HeaderText="Precio" DataField="Precio" />
         <asp:CommandField HeaderText="Acción" ShowSelectButton="true" SelectText="✏️" />
     </Columns>
 </asp:GridView>
</div> 
    <div class="boton">
 <a  href="FormularioArticulo.aspx" class="btn btn-secondary"> Nuevo articulo</a>
  
</div>

</asp:Content>
