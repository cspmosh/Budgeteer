<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/GeneralMasterPage.Master" CodeBehind="Confirmation.aspx.vb" Inherits="BudgeteerWeb.Confirmation" %>

<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">
    Registration Confirmation<br />    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="contentContainer">
        <asp:Label ID="lblMessage" runat="server" Text="Label"></asp:Label>
    </div>
    <br />
    <a href="/login.aspx">Back to Login</a>
</asp:Content>

