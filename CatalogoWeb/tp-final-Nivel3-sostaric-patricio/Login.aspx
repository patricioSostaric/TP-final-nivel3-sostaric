﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MiMasterpage.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="tp_final_Nivel3_sostaric_patricio.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="row">
      <div class="col-4">
          <h2>Login</h2>
          <div class="mb-3">
              <label class="form-label">Email</label>
              <asp:TextBox runat="server" ID="txtEmail" cssclass="form-control" TextMode="Email" REQUIRED ="txtEmail"/>
          </div>
          <div class="mb-3">
              <label class="form-label">Password</label>
              <asp:TextBox runat="server" cssclass="form-control" ID="txtPassword" TextMode="Password"/>
          </div>
          <asp:Button Text="Ingresar" cssclass="btn btn-primary" ID="btnLogin" OnClick="btnLogin_Click" runat="server" />
          <a href="/">Cancelar</a>

      </div>
  </div>
   
</asp:Content>
