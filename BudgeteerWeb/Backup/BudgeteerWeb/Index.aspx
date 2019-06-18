<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/GeneralMasterPage.Master" CodeBehind="Index.aspx.vb" Inherits="BudgeteerWeb.Index6" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"></asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">
        <h3 style="display: inline;">Simple. Flexible. <span style="color: #80A626;">Free!</span></h3>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<form id="form2" runat="server" autocomplete="off">  
    <table align="center">
        <tr>
            <td align="center"><img alt="" src="/images/adBankAccountsSS.png" /><br /><br /></td>
            <td align="center"><img alt="" src="/images/adBankAccountsText.png" /></td>
        </tr>
        <tr>
            <td align="center"><img alt="" src="/images/adTransactionsText.png" /></td>
            <td align="center"><img alt="" src="/images/adTransactionsSS.png" /></td>
        </tr>
        <tr>
            <td align="center"><img alt="" src="/images/adCategoriesSS.png" /><br /><br /></td>
            <td align="center"><img alt="" src="/images/adCategoriesText.png" /></td>
        </tr>
        <tr>
            <td align="center"><img alt="" src="/images/adBudgetsText.png" /><br /><br />
        <asp:Button ID="btnSignUp" runat="server" cssClass="bgtrButton" Text="Sign Up Now!"  PostBackUrl="/Registration/AccountRegistration.aspx" /></td>
            <td align="center"><img alt="" src="/images/adBudgetsSS.png" /></td>
        </tr>
    </table>
    </form>
</asp:Content>