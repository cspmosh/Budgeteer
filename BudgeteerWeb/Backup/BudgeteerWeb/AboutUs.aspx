<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/GeneralMasterPage.Master" CodeBehind="AboutUs.aspx.vb" Inherits="BudgeteerWeb.AboutUs" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"></asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">
    About Us... errr Me
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    Budgeteer is a personal finance web application that I (Josh Turner) developed in my spare time with the intent of sharing my passion for budgeting with the world. While there are many free budgeting applications available on the web, I wanted to develop something that gives more control over the budgeting process. For this reason, Budgeteer is intentionally not very automated. It doesn't automatically connect to your bank accounts and pull down transactions and assign pre-defined budget categories as some applications may. Budgeting is a very intentional and control driven concept, which is why I personally feel that every dollar has a name on it and that name is completely dependent on what you give it in Budgeteer.
    <br /><br />
    If you like what you see and you wish to support Budgeteer, feel free to donate some money to help with maintenance and hosting expenses via the donate button below... but don't forget to record that expense in your transactions! If you have any questions or you just want to get in touch, send an email to <a href="mailto:budgeteer.app@gmail.com">budgeteer.app@gmail.com</a>. Happy Budgeteering!<br /><br />
    <form action="https://www.paypal.com/cgi-bin/webscr" method="post">
        <input type="hidden" name="cmd" value="_s-xclick"/>
        <input type="hidden" name="hosted_button_id" value="9VVA9NSSKGPK8"/>
        <input type="image" src="https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!"/>
        <img alt="" border="0" src="https://www.paypalobjects.com/en_US/i/scr/pixel.gif" width="1" height="1"/>
    </form>
</asp:Content>
