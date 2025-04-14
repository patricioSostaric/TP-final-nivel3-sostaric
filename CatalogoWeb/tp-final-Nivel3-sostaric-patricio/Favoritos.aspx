<%@ Page Title="" Language="C#" MasterPageFile="~/MiMasterpage.Master" AutoEventWireup="true" CodeBehind="Favoritos.aspx.cs" Inherits="tp_final_Nivel3_sostaric_patricio.Favoritos" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Mis articulos favoritos</h2>
    <br />
    <asp:ScriptManager ID="Scriptmanajer1" runat="server"></asp:ScriptManager>
    
    <div class="row row-cols-1 row-cols-md-3 g-4">
        
                <asp:Repeater ID="RepetidorFavorito" runat="server">
                    <ItemTemplate>
                        <div class="col">
                            <div class="card">
                                <img src="<%#Eval("ImagenUrl") %>" class="card-img-top" alt="Imagen del articulo" style="max-width: 500px; max-height: 600px;"
                                    onerror="this.src='https://www.mansor.com.uy/wp-content/uploads/2020/06/imagen-no-disponible2.jpg'">

                                <div class="card-body">
                                    <h5 class="card-title"><%#Eval("Nombre") %></h5>
                                    <p class="card-text"><%#Eval("Descripcion") %></p>
                                    <a href="DetalleArticulo.aspx?id=<%#Eval("Id") %>">Ver detalle</a>
                                    <asp:Button ID="btnEliminarFavorito" CssClass="btn" runat="server" Text="❌"
                                        CommandName="ArticuloId" CommandArgument='<%#Eval("Id")%>' OnClick="btnEliminarFavorito_Click" />
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
          
    </div>

</asp:Content>
