<%@ Page Title="" Language="C#" MasterPageFile="~/MiMasterpage.Master" AutoEventWireup="true" CodeBehind="registro.aspx.cs" Inherits="tp_final_Nivel3_sostaric_patricio.registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div class="row">
        <div class="col-4">
            <h2>Creá tu perfil de usuario</h2>

            <asp:ValidationSummary ID="vsRegistro" runat="server"
                CssClass="alert alert-danger" HeaderText="Errores:" />

            <!-- Campo Email -->
            <div class="mb-3">
                <label class="form-label">Email</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtEmail" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                    ControlToValidate="txtEmail" ErrorMessage="El email es obligatorio"
                    CssClass="text-danger" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server"
                    ControlToValidate="txtEmail" ErrorMessage="Formato de email inválido"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    CssClass="text-danger" />
                <asp:CustomValidator ID="cvEmail" runat="server"
                    ControlToValidate="txtEmail"
                    ErrorMessage="El email ya está siendo utilizado por otro usuario"
                    OnServerValidate="cvEmail_ServerValidate" CssClass="text-danger" />
            </div>

            <!-- Campo Password -->
            <div class="mb-3">
                <label class="form-label">Password</label>
                <asp:TextBox runat="server" CssClass="form-control" ID="txtPassword" TextMode="Password" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                    ControlToValidate="txtPassword" ErrorMessage="La contraseña es obligatoria"
                    CssClass="text-danger" />
            </div>

            <!-- Botones -->
            <asp:Button Text="Registrarse" CssClass="btn btn-primary" ID="btnRegistrarse" OnClick="btnRegistrarse_Click" runat="server" />
            <a href="/" class="btn btn-link">Cancelar</a>
        </div>
    </div>


</asp:Content>
