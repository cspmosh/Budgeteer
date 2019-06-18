<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="New.aspx.vb" Inherits="BudgeteerWeb._New1" %>
<%@ Register Assembly="DayPilot.MonthPicker" Namespace="DayPilot.Web.UI" TagPrefix="DayPilot" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/Budgets/Index.aspx">Budgets</a> > Add Budget</asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">Add Budget</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:label ID="lblError" CssClass="error" runat="server" Visible="false"></asp:label>
    <asp:ValidationSummary CssClass="error" ID="validationSummary" DisplayMode="BulletList" runat="server" ShowSummary="true" />
    <div class="contentContainer">
        <asp:FormView ID="FormView1" runat="server" 
            DefaultMode="Insert" DataSourceID="BudgetDataSource" 
            DataKeyNames="budgetID,UserID" CellPadding="4" >
            <InsertItemTemplate>
                <table>
                    <tr>
                        <td align="right">Type:*</td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server">
                                <asp:ListItem Text="Expense" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Income" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Month:*</td>
                        <td><DayPilot:MonthPicker ID="MonthPicker1" runat="server" StartDate='<%# Bind("startDate") %>' YearStart='<%# Date.Today.Year - 3 %>' yearEnd='<%# Date.Today.Year + 1 %>'/> </td>
                    </tr>
                    <tr>
                        <td align="right">Amount:*</td>
                        <td>
                            <asp:TextBox ID="AmountTextBox" 
                                runat="server" 
                                placeholder="0.00"
                                Text='<%# Bind("Amount", "{0:0.00;0.00;0.00}") %>' 
                                ToolTip="A total amount for this budget"/>                        
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
                        <td align="right">Category:*</td>
                        <td>
                            <asp:DropDownList ID="ddlCategories"
                                runat="server"
                                DataSourceID="CategoryDataSource"
                                DataTextField="Description"
                                DataValueField="CategoryID"
                                AppendDataBoundItems="true" 
                                OnSelectedIndexChanged="ddlCategories_SelectedIndexChanged"
                                AutoPostBack="true" Width="160px"><asp:ListItem Text="-- Choose --" Value="" Selected="True"></asp:ListItem>                     
                            </asp:DropDownList>  
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator1" 
                                runat="server" 
                                ErrorMessage="Please select a category" 
                                ControlToValidate="ddlCategories" 
                                SetFocusOnError="True" 
                                Display="None"/>                    
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Subcategory:*</td>
                        <td>
                            <asp:DropDownList ID="ddlSubcategories" 
                                runat="server" 
                                DataSourceID="SubcategoryDataSource" 
                                DataTextField="Description"      
                                OnDataBound="subcategoryListDataBound"                   
                                DataValueField="SubcategoryID" Width="160px" Enabled="False"><asp:ListItem Text="-- Choose --" Value="" Selected="True"></asp:ListItem>                                         
                            </asp:DropDownList>  
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator3" 
                                runat="server" 
                                ErrorMessage="Please select a subcategory" 
                                ControlToValidate="ddlSubcategories" 
                                SetFocusOnError="True" 
                                Display="None"/>                        
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <br />                            
                            <asp:Button CssClass="bgtrButton" ID="InsertButton" runat="server" CausesValidation="True" 
                                CommandName="Insert" Text="Add" />&nbsp;
                            <asp:Button CssClass="bgtrCancelButton" ID="InsertCancelButton" runat="server" 
                                CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="InsertCancelButton_Click" />&nbsp;
                        </td>
                    </tr>
                </table> 
            </InsertItemTemplate>    
        </asp:FormView>
    </div>
        
    <asp:ObjectDataSource ID="BudgetDataSource" runat="server"        
        TypeName="BudgeteerWeb.BudgeteerService.BudgeteerClient" 
        DataObjectTypeName="BudgeteerWeb.BudgeteerService.Budget" 
        InsertMethod="AddBudget">              
    </asp:ObjectDataSource>
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
</asp:Content>
