<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="Edit.aspx.vb" Inherits="BudgeteerWeb.EditTransaction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/Transactions/Index.aspx">Transactions</a> > Edit Transaction</asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">Edit Transaction</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:label ID="lblError" cssClass="error" runat="server" Visible="false"></asp:label>
    <asp:ValidationSummary CssClass="error" ID="validationSummary" DisplayMode="BulletList" runat="server" ShowSummary="true" /> 
    <div class="contentContainer">  
        <asp:FormView ID="FormView1" runat="server"
            DefaultMode="Edit" DataSourceID="TransactionDataSource" 
            DataKeyNames="TransactionID,UserID">
            <EditItemTemplate>
                <table>
                    <tr>
                        <td align="right">TransactionDate:*</td>
                        <td>
                            <asp:TextBox 
                                ID="TransactionDateTextBox" 
                                runat="server" 
                               Text='<%# Bind("TransactionDate") %>'  
                               Width="70" 
                               MaxLength="10" EnableViewState="true" />
                            <asp:Image ID="imgCalendar" runat="server" ImageUrl="~/Images/calendar.png" />                         
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator3" 
                                runat="server" 
                                ErrorMessage="Please enter a date" 
                                ControlToValidate="TransactionDateTextBox" 
                                SetFocusOnError="True" 
                                Display="None"/>                        
                            <asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator4" 
                                runat="server" 
                                ErrorMessage="Invalid Format" 
                                Display="None" 
                                ControlToValidate="TransactionDateTextBox" 
                                ValidationExpression="^(\d{1,2})/(\d{1,2})/(\d{4})$"/>                                
                            <asp:CalendarExtender 
                                ID="CalendarExtender1" 
                                runat="server" 
                                DaysModeTitleFormat="MMMM yyyy" 
                                Enabled="True" 
                                FirstDayOfWeek="Sunday" 
                                EnableViewState="false"                        
                                SelectedDate='<%# Eval("TransactionDate") %>' TargetControlID="TransactionDateTextBox" PopupButtonID="imgCalendar" Format="MM/dd/yyyy"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">CheckNumber:</td>
                        <td>
                            <asp:TextBox ID="CheckNumberTextBox" runat="server" 
                                Text='<%# Bind("CheckNumber", "{0:#;#;}") %>'/>
                            <asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator1" 
                                runat="server" 
                                ErrorMessage="Invalid Format" 
                                ControlToValidate="CheckNumberTextBox" 
                                ValidationExpression="^[0-9]{0,10}$" 
                                SetFocusOnError="True" 
                                Display="None"/>                       
                        </td>
                    </tr>                    
                    <tr>
                        <td align="right">Description:*</td>
                        <td>
                            <asp:TextBox ID="DescriptionTextBox" runat="server" 
                                Text='<%# Bind("Description") %>' MaxLength="255" Width="400" TextMode="SingleLine"/>
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator1" 
                                runat="server" 
                                ErrorMessage="Please enter a description" 
                                ControlToValidate="DescriptionTextBox" 
                                SetFocusOnError="True" 
                                Display="None"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Amount:*</td>
                        <td>
                            <asp:TextBox ID="AmountTextBox" 
                                runat="server" 
                                placeholder="0.00"
                                Text='<%# Bind("Amount", "{0:###0.00;###0.00;###0.00}") %>' 
                                ToolTip="A total amount for this transaction, including any tax. Format example: 1234.00"/> 
                            <asp:DropDownList ID="ddlType" runat="server">
                                <asp:ListItem Text="Expense" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Deposit" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator2" 
                                runat="server" 
                                ErrorMessage="Please enter an amount" 
                                ControlToValidate="AmountTextBox" 
                                SetFocusOnError="True" 
                                Display="None"/>
                            <asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator2" 
                                runat="server" 
                                ErrorMessage="Invalid Format" 
                                ControlToValidate="AmountTextBox"                         
                                ValidationExpression="^(\d{1,7})?(\.((\d{1})|(\d{2})))?$" 
                                SetFocusOnError="True" 
                                Display="None"/>                        
                        </td>
                    </tr>
                    <tr>
                        <td align="right">TaxAmount:</td>
                        <td>
                            <asp:TextBox ID="TaxAmountTextBox" runat="server" 
                                placeholder="0.00"
                                Text='<%# Bind("TaxAmount", "{0:###0.00;###0.00;###0.00}") %>' 
                                ToolTip="The total amount of sales tax. Format example: 1234.00"/>
                            <asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator3" 
                                runat="server" 
                                ErrorMessage="Invalid Format" 
                                ControlToValidate="TaxAmountTextBox"                         
                                ValidationExpression="^(\d{1,7})?(\.((\d{1})|(\d{2})))?$" 
                                SetFocusOnError="True" 
                                Display="None"/>                        
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Category:</td>
                        <td>
                            <asp:DropDownList ID="ddlCategories"
                                runat="server"
                                DataSourceID="CategoryDataSource"
                                DataTextField="Description"
                                DataValueField="CategoryID"
                                AppendDataBoundItems="true" 
                                OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged"
                                AutoPostBack="true" Width="160px"/>                          
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Subcategory:</td>
                        <td>
                            <asp:DropDownList ID="ddlSubcategories" 
                                runat="server" 
                                DataSourceID="SubcategoryDataSource" 
                                OnDataBound="subcategoryListDataBound" 
                                DataTextField="Description"                         
                                DataValueField="SubcategoryID" Width="160px"><asp:ListItem Text="(None)" Value="0"></asp:ListItem>                                             
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator4" 
                                runat="server" 
                                ErrorMessage="A category must have a subcategory" 
                                ControlToValidate="ddlSubcategories" 
                                SetFocusOnError="True" 
                                Display="None"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Bank Account: </td>
                        <td>
                            <asp:DropDownList ID="ddlBankAccounts" 
                                runat="server" 
                                DataSourceID="AccountDataSource" 
                                DataTextField="Name" 
                                DataValueField="AccountID"
                                SelectedValue='<%# Bind("AccountID") %>'
                                AppendDataBoundItems="true" Width="160px"/>                    
                        </td>
                    </tr>
                    <tr>
                        <td align="right"></td>
                        <td>
                            <br />  
                            <asp:Button CssClass="bgtrButton" ID="UpdateButton" runat="server" CausesValidation="True" 
                                CommandName="Update" Text="Update" />&nbsp;
                            <asp:Button CssClass="bgtrCancelButton" ID="UpdateCancelButton" runat="server" 
                                CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="UpdateCancelButton_Click"  />
                        </td>
                    </tr>
                </table>                       
            </EditItemTemplate>       
        </asp:FormView>
    </div>
    <asp:ObjectDataSource ID="TransactionDataSource" runat="server" 
        SelectMethod="GetTransaction" 
        TypeName="BudgeteerWeb.BudgeteerService.BudgeteerClient" 
        DataObjectTypeName="BudgeteerWeb.BudgeteerService.Transaction" 
        UpdateMethod="UpdateTransaction">
        <SelectParameters>            
            <asp:QueryStringParameter Name="transactionID" QueryStringField="transactionID" Type="Int64" />
        </SelectParameters>        
    </asp:ObjectDataSource>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="SubcategoryDataSource" runat="server" 
        SelectMethod="GetActiveSubcategoriesByCategoryID"
        TypeName="BudgeteerWeb.BudgeteerService.BudgeteerClient">        
        <SelectParameters>
            <asp:Parameter Name="CategoryID"/>
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="CategoryDataSource" runat="server" 
        SelectMethod="GetActiveCategories" 
        TypeName="BudgeteerWeb.BudgeteerService.BudgeteerClient">     
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="AccountDataSource" runat="server" 
        SelectMethod="GetActiveBankAccounts" 
        TypeName="BudgeteerWeb.BudgeteerService.BudgeteerClient"        
        DataObjectTypeName="List (Of BudgeteerWeb.BudgeteerService.BankAccount)">
    </asp:ObjectDataSource>

</asp:Content>