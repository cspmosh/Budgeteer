<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="Index.aspx.vb" Inherits="BudgeteerWeb.Index5" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/UserAccount/Index.aspx">Account Information</a></asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">Account Information</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:label ID="lblError" CssClass="error" runat="server" Visible="false"></asp:label>
    <div class="contentContainer">
        <asp:FormView ID="FormView1" runat="server" DataSourceID="accountDataSource">
            <ItemTemplate>
                <table>
                    <tr><td>&nbsp;</td><td style="width: 100px;">User Name: </td><td style="width: 150px;"><asp:Label ID="lblUserName" runat="server" Text='<%# Bind("userID") %>'></asp:Label></td><td></td></tr>
                    <tr><td>&nbsp;</td><td>First Name: </td><td><asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label></td><td></td></tr>
                    <tr><td>&nbsp;</td><td>Last Name: </td><td><asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>'></asp:Label></td><td></td></tr>
                    <tr><td>&nbsp;</td><td>Email Address: </td><td><asp:Label ID="lblEmailAddress" runat="server" Text='<%# Bind("EmailAddress") %>'></asp:Label></td><td><asp:Button ID="btnEmail" CssClass="bgtrButton" runat="server" onCommand="btn_Click" CommandName="Redirect" CommandArgument="changeEmail.aspx" Text="Change Email" Width="150"/></td></tr>
                    <tr><td>&nbsp;</td><td>Password: </td><td><asp:Label ID="lblPassword" runat="server" Text='*********'></asp:Label></td><td><asp:Button ID="btnPwd" CssClass="bgtrButton" runat="server" onCommand="btn_Click" CommandName="Redirect" CommandArgument="changePassword.aspx" Text="Change Password" Width="150"/></td></tr>
                </table>
            </ItemTemplate>
        </asp:FormView>
    </div>
    
    <asp:ObjectDataSource ID="accountDataSource" runat="server" 
        SelectMethod="GetUser" 
        TypeName="BudgeteerWeb.BudgeteerService.BudgeteerClient">     
    </asp:ObjectDataSource>   
</asp:Content>