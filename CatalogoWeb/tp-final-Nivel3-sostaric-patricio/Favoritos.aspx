<%@ Page Title="" Language="C#" MasterPageFile="~/MiMasterpage.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="tp_final_Nivel3_sostaric_patricio.Favoritos" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Mis articulos favoritos</h2>
    <br /> 
     
   <%--<asp:Label ID="lblSinFavoritos" runat="server" 
           Text="No tienes artículos favoritos." 
           CssClass="sin-favoritos" 
           Visible="false" />--%>

   <%-- <div class="container">
    <div class="row justify-content-center">
        <div class="col-auto text-center">
            <svg xmlns="http://www.w3.org/2000/svg" 
                 viewBox="0 0 24 24" 
                 width="32" height="32" 
                 fill="gold" 
                 class="me-2">
                <polygon points="12 2 15 9 22 9 17 14 
                                 19 21 12 17 5 21 
                                 7 14 2 9 9 9"/>
            </svg>
            <asp:Label ID="lblSinFavoritos" runat="server" 
                       Text="No tienes artículos favoritos." 
                       CssClass="text-muted" 
                       Visible="false" />
        </div>
    </div>
</div>--%>
    <div class="container d-flex justify-content-center mt-5">
    <div class="card shadow-sm p-4 text-center">
        <svg xmlns="http://www.w3.org/2000/svg" 
             viewBox="0 0 24 24" width="40" height="40" 
             fill="gold" class="mb-3">
            <polygon points="12 2 15 9 22 9 17 14 
                             19 21 12 17 5 21 
                             7 14 2 9 9 9"/>
        </svg>
        <asp:Label ID="lblSinFavoritos" runat="server" 
                   Text="No tienes artículos favoritos." 
                   CssClass="text-muted fs-5" 
                   Visible="false" />
    </div>
</div>
    <asp:ScriptManager ID="Scriptmanajer1" runat="server"></asp:ScriptManager>
    

    <div class="row row-cols-1 row-cols-md-3 g-4">

     
        <asp:Repeater ID="RepetidorFavorito" runat="server">
    <ItemTemplate>
        <div class="col">
            <div class="card card-unificada">
                <img src="<%#Eval("ImagenUrl") %>" class="card-img-top tamanio-imagen" alt="Imagen del articulo"
                     onerror="this.src='https://www.mansor.com.uy/wp-content/uploads/2020/06/imagen-no-disponible2.jpg'">

                <div class="card-body">
                    <h5 class="card-title"><%#Eval("Nombre") %></h5>
                    <p class="card-text descripcion"><%#Eval("Descripcion") %></p>
                    <p class="card-text"><%#Eval("Precio", "{0:F2}") %></p>
                    <a href="DetalleArticulo.aspx?id=<%#Eval("Id") %>">Ver detalle</a>
                    <asp:Button ID="btnEliminarFavorito" CssClass="btn btn-outline-danger btn-sm"
                        runat="server" Text="❌"
                        CommandName="ArticuloId" CommandArgument='<%#Eval("Id")%>' OnClick="btnEliminarFavorito_Click" />
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>


    </div>


</asp:Content>
