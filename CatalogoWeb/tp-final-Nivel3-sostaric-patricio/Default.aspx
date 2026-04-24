<%@ Page Title="" Language="C#" MasterPageFile="~/MiMasterpage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp_final_Nivel3_sostaric_patricio.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
     <h1 class="CentrarTitulo">¡Bienvenido/a!</h1>
    <p>Te encuentras en la web de artículos.</p>
    <h3>Lista de Artículos</h3>

    <div class="row">
        <div class="Margen">
            <div class="col-6">
                <div class="mb-3">
                    <asp:Label Text="Buscar" runat="server" />
                    <asp:TextBox runat="server" ID="txtFiltro" CssClass="form-control"
                                 AutoPostBack="true" OnTextChanged="txtFiltro_TextChanged" />
                </div>
            </div>
        </div>
    </div>

    <div class="mb-3">
        <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
    </div>

    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="repRepetidor" runat="server" OnItemDataBound="repRepetidor_ItemDataBound">
            <ItemTemplate>
                <div class="col">
                    <div class="card h-100 card-unificada">
                       
                        <asp:Image ID="imgArticulo" runat="server"
                                   CssClass="img-fluid rounded detalle-img"
                                   AlternateText="Imagen del artículo" />

                        <div class="card-body">
                            
                            <asp:Label ID="lblNombre" runat="server" CssClass="card-title h5"></asp:Label>

                            
                            <asp:Label ID="lblDescripcion" runat="server" CssClass="card-text d-block"></asp:Label>

                            
                            <asp:Label ID="lblPrecio" runat="server" CssClass="card-text d-block"></asp:Label>

                            
                            <asp:HyperLink ID="lnkDetalle" runat="server" Text="Ver detalle"></asp:HyperLink>

                            
                            <asp:Button ID="btnComprar" runat="server"
                                        Text="Comprar"
                                        CssClass="btn btn-secondary mt-2"
                                        OnClick="btnComprar_Click" />

                            
                            <asp:Button ID="btnFavorito" runat="server"
                                        CssClass="btn btn-sm mt-2 ms-2"
                                        OnClick="btnAgregarFavorito_Click" />
                        </div>
                       
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>

