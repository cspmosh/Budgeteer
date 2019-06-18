<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="ChangePassword.aspx.vb" Inherits="BudgeteerWeb.ChangePassword" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/UserAccount/Index.aspx">Account Information</a> > <a href="/MemberPages/UserAccount/ChangePassword.aspx">Change Password</a></asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">Change Password</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Label ID="lblError" Visible="false" CssClass="error" runat="server"></asp:Label>
    <div class="contentContainer">
        <table>
            <tr>
                <td align="right">Old Password: </td><td><asp:TextBox ID="txtOldPassword" runat="server" Width="250" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">New Password: </td><td><asp:Textbox ID="txtNewPassword" runat="server" Width="250" TextMode="Password"></asp:Textbox></td>
            </tr>
            <tr>
                <td align="right">Confirm Password: </td><td><asp:TextBox ID="txtConfirmPassword" runat="server" Width="250" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <br />                  
                    <asp:Button CssClass="bgtrButton" ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" CausesValidation="True" 
                        Text="Change Password" />&nbsp; 
                    <asp:Button CssClass="bgtrCancelButton" ID="UpdateCancelButton" runat="server" 
                        CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="UpdateCancelButton_Click"  />
                </td>
            </tr>
        </table>           
    </div>
</asp:Content>
