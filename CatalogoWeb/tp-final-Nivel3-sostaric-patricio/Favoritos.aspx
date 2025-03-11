<%@ Page Title="" Language="C#" MasterPageFile="~/MiMasterpage.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="tp_final_Nivel3_sostaric_patricio.Favoritos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <h2>Mis articulos favoritos</h2>
  <br />

      <div class="row row-cols-1 row-cols-md-3 g-4">

          <% foreach (Dominio.Articulo articuloFavorito in ListaFavorito) { %>
  <div class="col">
    <div class="card">
      <img src="<%: articuloFavorito.ImagenUrl %>" class="card-img-top" alt="...">
      <div class="card-body">
        <h5 class="card-title"><%: articuloFavorito.Nombre %></h5>
        <p class="card-text"><%: articuloFavorito.Descripcion %></p>
      <asp:Button ID="EliminarFavorito" runat="server" Text="Eliminar" OnClick="EliminarFavorito_Click" CommandArgument="<%:articuloFavorito.id %>"
      />
          <asp:Button ID="eliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger"  OnCommand="eliminar_Command" />
/>
         </div> 
    </div>
  </div>
<% } %>
           
          </div>

  
</asp:Content>
