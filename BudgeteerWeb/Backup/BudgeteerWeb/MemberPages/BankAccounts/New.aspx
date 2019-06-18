<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="New.aspx.vb" Inherits="BudgeteerWeb._New2" %>
<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/BankAccounts/Index.aspx">Bank Accounts</a> > Add Bank Account</asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">Add Bank Account</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:label ID="lblError" CssClass="error" runat="server" Visible="false"></asp:label>
    <asp:ValidationSummary CssClass="error" ID="validationSummary" DisplayMode="BulletList" runat="server" ShowSummary="true" />
    <div class="contentContainer">             
        <asp:FormView ID="BankAccountFormView" runat="server" 
            DefaultMode="Insert" DataSourceID="BankAccountDataSource" 
            DataKeyNames="AccountID,Number,Name,UserID" >
            <InsertItemTemplate>   
               <table>
                    <tr>
                        <td style="padding-left: 10px;" align="right">Number:</td>
                        <td>
                            <asp:TextBox ID="txtNumber" runat="server" 
                            Text='<%# Bind("Number") %>' MaxLength="255" Width="400" TextMode="SingleLine" />
                            <asp:RegularExpressionValidator 
                            ID="RegularExpressionValidator1" 
                            runat="server" 
                            ErrorMessage="Invalid format for account number. Please specify a 4 digit number" 
                            ControlToValidate="txtNumber" 
                            ValidationExpression="^([0-9]{4}){0,1}$" 
                            SetFocusOnError="True"                             
                            Display="None" />                        
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px;" align="right">Name:*</td>
                        <td>
                            <asp:TextBox ID="txtName" runat="server" 
                                Text='<%# Bind("Name") %>' MaxLength="50" Width="400" TextMode="SingleLine"/>
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator2" 
                                runat="server" 
                                ErrorMessage="Please enter an account name" 
                                ControlToValidate="txtName"
                                SetFocusOnError="True" 
                                Display="None"/>                       
                            <asp:CustomValidator ID="CustomValidator1" 
                                runat="server" 
                                ErrorMessage="Bank account name and number is already in use" 
                                ControlToValidate="txtName" 
                                onservervalidate="ValidateBankAccountUniqueness" 
                                Display="None"/>                                                                         
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px;" align="right">Type:</td>
                        <td>
                            <asp:TextBox ID="txtType" runat="server" 
                            Text='<%# Bind("Type") %>' MaxLength="20" Width="400" TextMode="SingleLine" ToolTip='IE "Checking", "Savings", "Credit Card"'/>                   
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px;" align="right">Balance:</td>
                        <td>
                            <asp:TextBox ID="txtBalance" 
                            runat="server" 
                            placeholder="0.00"
                            Text='<%# Bind("Balance", "{0:0.00;-0.00;0.00}") %>' 
                            ToolTip="A total dollar amount for this bank account. Format example: 1234.00"/>                                                     
                            <asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator2" 
                                runat="server" 
                                ErrorMessage="Invalid Format for Balance" 
                                ControlToValidate="txtBalance"                         
                                ValidationExpression="^-{0,1}(\d{1,7})?(\.((\d{1})|(\d{2})))?$" 
                                SetFocusOnError="True" 
                                Display="None"/>                        
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px;" align="right">Active:*</td>
                        <td><asp:CheckBox ID="cbxActive" runat="server" checked='<%# Bind("Active") %>'/></td>
                    </tr>                                        
                    <tr>
                        <td align="center" colspan="2">
                            <br />                          
                            <asp:Button cssClass="bgtrButton" ID="InsertButton" runat="server" CausesValidation="True" 
                                CommandName="Insert" Text="Add" />&nbsp;              
                            <asp:Button cssClass="bgtrCancelButton" ID="InsertCancelButton" runat="server" 
                                CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="InsertCancelButton_Click"  />                            
                        </td>
                    </tr>
                </table>  
            </InsertItemTemplate>       
        </asp:FormView>
    </div>
    <asp:ObjectDataSource ID="BankAccountDataSource" runat="server" 
        SelectMethod="GetBankAccount" 
        TypeName="BudgeteerWeb.BudgeteerService.BudgeteerClient" 
        DataObjectTypeName="BudgeteerWeb.BudgeteerService.BankAccount" 
        InsertMethod="AddBankAccount">
        <SelectParameters>            
            <asp:QueryStringParameter Name="AccountID" QueryStringField="AccountID" Type="Int64" />
        </SelectParameters>        
    </asp:ObjectDataSource>



</asp:Content>
