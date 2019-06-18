<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/GeneralMasterPage.Master"  CodeBehind="AccountRegistration.aspx.vb" Inherits="BudgeteerWeb.Registration" %>

<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">
    Account Registration<br />    
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Label ID="lblSuccess" runat="server" Text="Thank you for your registration. You will receieve an email shortly with information on completing the registration process" Visible="False"></asp:Label>
    
    <form id="form2" runat="server" autocomplete="off">  
    <asp:label align="center" ID="lblError" runat="server" EnableViewState="False" CssClass="error" Width="100%" Height="30px" Visible="False" Font-Size="Medium"></asp:label>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="error" Width="100%" DisplayMode="BulletList" ShowSummary="True" ShowMessageBox="False" />  
  
    <asp:Panel ID="panel1" runat="server">            
        <div class="contentContainer">
            <table border="0">
                <tr>
                    <td align="right">User Name:</td>
                    <td><asp:TextBox ID="txtUserID" runat="server" Height="20" Width="250" Font-Size="medium" AutoCompleteType="Disabled"></asp:TextBox> (No spaces)<asp:RequiredFieldValidator ID="reqUserID" runat="server" ErrorMessage="Please choose a login name"  CssClass="error" ControlToValidate="txtUserID" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">First Name:</td>
                    <td><asp:TextBox ID="txtFirstName" runat="server" Height="20" Width="250" Font-Size="medium" AutoCompleteType="Disabled"></asp:TextBox><asp:RequiredFieldValidator ID="reqFirstName" runat="server" ErrorMessage="Please enter your first name" Display="None" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">Last Name:</td>
                    <td><asp:TextBox ID="txtLastName" runat="server" Height="20" Width="250" Font-Size="medium" AutoCompleteType="Disabled"></asp:TextBox><asp:RequiredFieldValidator ID="reqLastName" runat="server" ErrorMessage="Please enter your last name" Display="None" ControlToValidate="txtLastName"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">Email Address: </td>
                    <td><asp:TextBox ID="txtEmailAddress" runat="server" Height="20" Width="250" Font-Size="medium" AutoCompleteType="Disabled"></asp:TextBox><asp:RequiredFieldValidator ID="reqEmail" runat="server" ErrorMessage="Please enter a valid email address" Display="None" ControlToValidate="txtEmailAddress"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">Password: </td>
                    <td><asp:TextBox ID="txtPassword" runat="server" MaxLength="30" TextMode="Password" Height="20" Width="250" Font-Size="medium"></asp:TextBox> (At least 7 characters, one of which must be a number or special character)<asp:RequiredFieldValidator ID="reqPassword" runat="server" ErrorMessage="Please choose a password" Display="None" ControlToValidate="txtPassword"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">Confirm Password: </td>
                    <td><asp:TextBox ID="txtPasswordConfirm" runat="server" MaxLength="30" TextMode="Password" Height="20" Width="250" Font-Size="medium"></asp:TextBox><asp:RequiredFieldValidator ID="reqPasswordConfirm" runat="server" ErrorMessage="Please confirm your password" Display="None" ControlToValidate="txtPasswordConfirm"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">Security Question 1:</td>
                    <td><asp:DropDownList ID="ddlSecurityQuestion1" runat="server" Height="25" Width="100%" DataTextField="question" DataValueField="securityQuestionId" Font-Size="medium"></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidatorSQ1" runat="server" ErrorMessage="Please choose a security question"  ControlToValidate="ddlSecurityQuestion1" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">Answer:</td>
                    <td><asp:TextBox ID="txtAnswer1" runat="server" MaxLength="50" Height="20" Width="100%" Font-Size="medium" AutoCompleteType="Disabled"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorSA1" runat="server" ErrorMessage="Please answer security question #1"  ControlToValidate="txtAnswer1" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">Security Question 2:</td>
                    <td><asp:DropDownList ID="ddlSecurityQuestion2" runat="server" Height="25" Width="100%" DataTextField="question" DataValueField="securityQuestionId" Font-Size="medium"></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidatorSQ2" runat="server" ErrorMessage="Please choose a security question"  ControlToValidate="ddlSecurityQuestion2" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">Answer:</td>
                    <td><asp:TextBox ID="txtAnswer2" runat="server" MaxLength="50" Height="20" Width="100%"  Font-Size="medium" AutoCompleteType="Disabled"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorSA2" runat="server" ErrorMessage="Please answer security question #2"  ControlToValidate="txtAnswer2" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">Security Question 3:</td>
                    <td><asp:DropDownList ID="ddlSecurityQuestion3" runat="server" Height="25" Width="100%" DataTextField="question" DataValueField="securityQuestionId" Font-Size="medium"></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidatorSQ3" runat="server" ErrorMessage="Please choose a security question"  ControlToValidate="ddlSecurityQuestion3" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="right">Answer:</td>
                    <td><asp:TextBox ID="txtAnswer3" runat="server" MaxLength="50" Height="20" Width="100%"  Font-Size="medium" AutoCompleteType="Disabled"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidatorSA3" runat="server" ErrorMessage="Please answer security question #3"  ControlToValidate="txtAnswer3" SetFocusOnError="True" Display="None"></asp:RequiredFieldValidator></td>                   
                </tr>
                <tr>
                    <td align="right"></td>
                    <td><br /><asp:Button ID="btnRegister" runat="server" Text="Create Account" OnClick="registerUser" CssClass="bgtrButton" /></asp:TextBox></td>                   
                </tr>
            </table>
        </div>        
     </asp:Panel>
     </form>
</asp:Content>
