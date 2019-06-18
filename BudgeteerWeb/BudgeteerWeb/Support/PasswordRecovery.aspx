<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/GeneralMasterPage.Master" CodeBehind="PasswordRecovery.aspx.vb" Inherits="BudgeteerWeb.PasswordRecovery" %>

<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">
    Password Recovery<br />        
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<form id="form2" runat="server" autocomplete="off">  
<asp:label ID="lblError" runat="server" EnableViewState="False" CssClass="error" Visible="False" Font-Size="Medium"></asp:label>
    <asp:panel ID="instructionPanel" runat="server" Visible="true"><div class="contentContainer"><strong>Step <asp:Label ID="lblStep" runat="server"></asp:Label> of 3: <asp:Label ID="lblStepInstructions" runat="server"></asp:Label></strong></div></asp:panel><br />
    <div class="contentContainer">
        <asp:Label ID="lblSuccess" runat="server" Visible="false" Text="Your password has been reset. Please <a href='\login.aspx'>Click Here</a> to go back to the login page"></asp:Label>
        <asp:Panel ID="pnlVerify" runat="server" Visible="false"
            HorizontalAlign="left">
            <table>
                <tr>
                    <td align="right">User Name:</td>
                    <td><asp:TextBox ID="txtUserName" runat="server" Height="20" Width="250" Font-Size="Medium" AutoCompleteType="Disabled"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">First Name:</td>
                    <td><asp:TextBox ID="txtFirstName" runat="server" Height="20" Width="250" Font-Size="Medium" AutoCompleteType="Disabled"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">Last Name:</td>
                    <td><asp:TextBox ID="txtLastName" runat="server" Height="20" Width="250" Font-Size="Medium" AutoCompleteType="Disabled"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">Email Address:</td>
                    <td><asp:TextBox ID="txtEmailAddress" runat="server" Height="20" Width="250" Font-Size="Medium" AutoCompleteType="Disabled"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2"><br /><asp:Button id="btnVerify" runat="server" CssClass="bgtrButton" onClick="VerifyUser" Text="Verify Account" /></td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlConfirm" runat="server" Visible="false" 
            HorizontalAlign="Left">
            <table>
                <asp:Repeater ID="securityQuestionRepeater" runat="server">  
                    <ItemTemplate>              
                        <tr><td><asp:HiddenField ID="hiddenQuestionID" runat="server" Value='<%#DataBinder.Eval(Container.DataItem, "SecurityQuestionID")%>' /><asp:Label ID="lblSecurityQuestion" runat="server" Font-Size="Medium" Text='<%#DataBinder.Eval(Container.DataItem, "Question")%>'></asp:Label></td></tr>
                        <tr><td><asp:TextBox ID="txtSecurityQuestionAnswer" runat="server" height="20" Font-Size="Medium" Width="100%" AutoCompleteType="Disabled"></asp:TextBox></td></tr>                           
                    </ItemTemplate>            
                </asp:Repeater>
                    <tr><td><br /><asp:Button ID="btnConfirm" runat="server" CssClass="bgtrButton" onClick="ConfirmUser" Text="Confirm Account" /></td></tr> 
            </table>           
        </asp:Panel>
        <asp:Panel ID="pnlReset" runat="server" Visible="false" 
            HorizontalAlign="Left">
            <table>
                <tr>
                    <td align="right">New Password:</td>
                    <td align="right"><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="250" Height="20" Font-size="Medium"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="right">Confirm Password:</td>
                    <td align="right"><asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Width="250" Height="20" Font-size="Medium"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2"><br /><asp:button ID="btnReset" runat="server" CssClass="bgtrButton" onclick="resetPassword" Text="Reset Password" /></td>
                </tr>
            </table>
        </asp:Panel>        
    </div>
    </form>
</asp:Content>
