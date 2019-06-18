<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="ChangeEmail.aspx.vb" Inherits="BudgeteerWeb.ChangeEmail" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/UserAccount/Index.aspx">Account Information</a> > <a href="/MemberPages/UserAccount/ChangeEmail.aspx">Change Email</a></asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">Change Email</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Label ID="lblError" Visible="false" CssClass="error" runat="server"></asp:Label>
    <div class="contentContainer">
        <table>
            <tr>
                <td align="right">New Email: </td><td><asp:TextBox ID="txtNewEmail" runat="server" Width="250"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <br />                  
                    <asp:Button CssClass="bgtrButton" ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" CausesValidation="True" 
                        Text="Change Email" />&nbsp; 
                    <asp:Button CssClass="bgtrCancelButton" ID="UpdateCancelButton" runat="server" 
                        CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="UpdateCancelButton_Click"  />
                </td>                          
            </tr>
        </table>          
    </div>
</asp:Content>