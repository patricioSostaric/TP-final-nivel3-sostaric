<%@ Page Title="" Language="C#" MasterPageFile="~/MiMasterpage.Master" AutoEventWireup="true" CodeBehind="registro.aspx.cs" Inherits="tp_final_Nivel3_sostaric_patricio.registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="row">
      <div class="col-4">
          <h2>Creá tu perfil de usuario</h2>
          <div class="mb-3">
              <label class="form-label">Email</label>
              <asp:TextBox runat="server" cssclass="form-control" ID="txtEmail"/>
          </div>
          <div class="mb-3">
              <label class="form-label">Password</label>
              <asp:TextBox runat="server" cssclass="form-control" ID="txtPassword" TextMode="Password"/>
          </div>
          <asp:Button Text="Registrarse" cssclass="btn btn-primary" ID="btnRegistrarse" OnClick="btnRegistrarse_Click" runat="server" />
          <a href="/">Cancelar</a>

      </div>
  </div>
</asp:Content>
