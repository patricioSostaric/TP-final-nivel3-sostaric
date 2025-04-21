<%@ Page Title="" Language="C#" MasterPageFile="~/MiMasterpage.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="tp_final_Nivel3_sostaric_patricio.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="display: flex; justify-content: center;">

    <div class="card  mb-3" style="width: 18rem; text-align: center; justify-content: center">
        <img src="https://encrypted-tbn1.gstatic.com/images?q=tbn:ANd9GcRtzzmlsYuIdB5XTVAnM8iyP9HJUwDXwxHwXWcVxxAH803XPRVg" class="card-img-top" alt="Error">
        <div class="card-body">
            <h5 class="card-title">Hola, hubo un error!</h5>
            <asp:Label Text="" ID="lblError"  runat="server" CssClass="card-text" />
            <div style="padding: 20px;">
                <a href="Default.aspx" class="btn btn-secondary">Home</a>
            </div>
        </div>
    </div>

</div>
</asp:Content>
