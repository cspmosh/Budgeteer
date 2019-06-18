<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/GeneralMasterPage.Master" CodeBehind="Login.aspx.vb" Inherits="BudgeteerWeb._Default" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"></asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">
    Account Login     
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <form id="form2" runat="server" autocomplete="off">  
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error" Width="100%" DisplayMode="BulletList" ShowSummary="True" ShowMessageBox="False" ValidationGroup="Login1" />  
    <asp:label align="center" ID="lblFailureText" runat="server" EnableViewState="False" CssClass="error" Width="100%" Height="30px" Visible="False" Font-Size="Medium"></asp:label>
    
    <div align="center" class="contentContainer" >     
        <asp:Login ID="Login1" runat="server" TitleText="Please Log In &amp;nbsp;"           
            VisibleWhenLoggedIn="False" 
            FailureText="Invalid User Name or Password" 
            DestinationPageUrl="~/MemberPages/BankAccounts/Index.aspx">           
            <LayoutTemplate>
                <table border="0" cellpadding="1" cellspacing="0" 
                    style="border-collapse:collapse;">
                    <tr>
                        <td>

                            <table border="0" cellpadding="0" cellspacing="8">
                                <tr>
                                    <td align="right" colspan="2">
                                        Please Log In</td>
                                       <td rowspan="4" style="padding: 10px; border-left: 1px solid #cccccc;">
                                            <asp:Button ID="LoginButton" runat="server" cssClass="bgtrButton" CommandName="Login" Text="Log In" ValidationGroup="Login1" />
                                       </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:TextBox ID="UserName" runat="server" Width="225" Height="20" Font-Size="Medium" AutoCompleteType="Disabled"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" 
                                            ControlToValidate="UserName" ErrorMessage="User Name is required." 
                                            Font-Names="LuzSans-Book" Display="None" ToolTip="User Name is required." 
                                            ValidationGroup="Login1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:TextBox ID="Password" runat="server" TextMode="Password" Width="225" Height="20" Font-Size="Medium"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" 
                                            ControlToValidate="Password" Display="None" ErrorMessage="Password is required." 
                                            Font-Names="LuzSans-Book" ToolTip="Password is required." 
                                            ValidationGroup="Login1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right">
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="/Support/Recovery.aspx">Forgot your User Name or Password?</asp:HyperLink></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>            
        </asp:Login>        
    </div>
    </form>
</asp:Content>
