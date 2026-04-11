<%@ Page Title="" Language="C#" MasterPageFile="~/MiMasterpage.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" Inherits="tp_final_Nivel3_sostaric_patricio.DetalleArticulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
   <div class="row">
    <div class="col-8 text-center mb-4">
        <h1 class="titulo-detalle">Detalle del artículo</h1>
    </div>
</div>


<div class="row align-items-start">
    <!-- Columna izquierda: datos del producto -->
    <div class="col-6 mt-3">
        <p class="text-muted"><strong>Nombre </strong>
            <asp:Label runat="server" ID="lblNombre" /></p>
        <p class="text-muted"><strong>Marca:</strong>
            <asp:Label runat="server" ID="lblMarca" /></p>
        <p class="text-muted"><strong>Categoría:</strong>
            <asp:Label runat="server" ID="lblCategoria" /></p>
        <p class="text-muted"><strong>Precio </strong>$<asp:Label runat="server" ID="lblPrecio" /></p>
        <p class="text-muted"><strong>Descripcion</strong>
            <asp:Label runat="server" ID="lblDescripcion" /></p>

        <a href="Default.aspx" class="btn btn-secondary mt-3">Volver al inicio</a>
    </div>

    <!-- Columna derecha: imagen -->
    <div class="col-6 d-flex justify-content-start">
        <asp:Image runat="server" ID="imgArticulo" CssClass="img-fluid rounded detalle-img" />
    </div>
</div>

</asp:Content>
