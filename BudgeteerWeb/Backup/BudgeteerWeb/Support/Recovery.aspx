<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/GeneralMasterPage.Master" CodeBehind="Recovery.aspx.vb" Inherits="BudgeteerWeb.Index4" %>

<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">
    User Name and Password Recovery<br />    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="form2" runat="server" autocomplete="off">  
    Look, it happens to the best of us... but you're still a noob.<br /><br />
        <asp:label align="center" ID="lblError" runat="server" EnableViewState="False" CssClass="error" Width="100%" Height="30px" Visible="False" Font-Size="Medium"></asp:label>
    <div class="contentContainer">If you forgot your User Name, enter the email address you used to register with and click on the button below to receive an email with your User Name<br /><br />
        Email Address: <asp:TextBox ID="txtEmailAddress" runat="server" Width="250" Height="20" Font-Size="Medium"></asp:TextBox><br /><br />
        <asp:Button ID="ForgotUserName" runat="server" cssClass="bgtrButton" onClick="GetUserID" Text="Forgot User Name" ValidationGroup="Login1"  /></div><br />
    <div class="contentContainer">If you forgot your Password, click on the button below to verify your identity and reset your password<br /><br />
        <asp:Button ID="ForgotPassword" runat="server" cssClass="bgtrButton" onClick="RedirectToVerify" Text="Forgot Password" ValidationGroup="Login1" /></div>
    </form>
</asp:Content>

