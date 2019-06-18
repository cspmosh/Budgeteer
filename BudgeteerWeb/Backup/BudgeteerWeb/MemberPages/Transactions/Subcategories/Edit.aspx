<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="Edit.aspx.vb" Inherits="BudgeteerWeb.Edit4" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/Transactions/Index.aspx">Transactions</a> > <a href="/MemberPages/Transactions/Categories/Index.aspx">Categories</a> > Edit Subcategory</asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">Edit Subcategory</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:label ID="lblError" cssClass="error" runat="server" Visible="false"></asp:label>
    <asp:ValidationSummary CssClass="error" ID="validationSummary" DisplayMode="BulletList" runat="server" ShowSummary="true" /> 
    <div class="contentContainer">               
        <asp:FormView ID="SubcategoryFormView" runat="server" 
            DefaultMode="Edit" DataSourceID="SubcategoryDataSource" 
            DataKeyNames="SubcategoryID,CategoryID,Description,UserID">       
            <EditItemTemplate>
                <table>
                    <tr>
                        <td align="right">Category:*</td>
                        <td>
                            <asp:DropDownList ID="ddlCategories"
                                runat="server"
                                DataSourceID="CategoryDataSource"
                                DataTextField="Description"
                                DataValueField="CategoryID"
                                AppendDataBoundItems="true"                                                 
                                Width="160px"/>      
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator2" 
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
                            <asp:TextBox ID="DescriptionTextBox" runat="server" 
                                Text='<%# Bind("Description") %>' MaxLength="50" Width="250" TextMode="SingleLine"/>
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator1" 
                                runat="server" 
                                ErrorMessage="Please enter a subcategory description" 
                                ControlToValidate="DescriptionTextBox" 
                                SetFocusOnError="True" 
                                Display="None"/>                        
                            <asp:CustomValidator 
                                ID="uniqueSubcatValidator" 
                                runat="server" 
                                ErrorMessage="Subcategory is already in use" 
                                ControlToValidate="DescriptionTextBox" 
                                SetFocusOnError="True" 
                                onservervalidate="ValidateSubcategoryDescriptionUniqueness" 
                                Display="None"/> 
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Type:*</td>
                        <td>
                            <asp:DropDownList ID="ddlType" runat="server">
                                <asp:ListItem Text="Expense" Value="Expense" />
                                <asp:ListItem Text="Income" Value="Income" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Sinking Fund:*</td>
                        <td>
                            <asp:CheckBox ID="cbxSinkingFund" runat="server" checked='<%# Bind("SinkingFund") %>' AutoPostBack="true" OnCheckedChanged="chk_CheckedChanged"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Balance:</td>
                        <td>
                            <asp:TextBox ID="BalanceTextBox" 
                                runat="server" 
                                placeholder="0.00"
                                Text='<%# Bind("Balance", "{0:0.00;-0.00;0.00}") %>' 
                                ToolTip="A total dollar amount for this subcategory sinking fund. Format example: 1234.00"/>                                                       
                            <asp:RegularExpressionValidator 
                                ID="RegularExpressionValidator2" 
                                runat="server" 
                                ErrorMessage="Invalid Format" 
                                ControlToValidate="BalanceTextBox"                         
                                ValidationExpression="^-{0,1}(\d{1,7})?(\.((\d{1})|(\d{2})))?$" 
                                SetFocusOnError="True" 
                                Display="None"/>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Active*:</td>
                        <td>
                            <asp:CheckBox ID="CheckBox2" runat="server" checked='<%# Bind("Active") %>' />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">Notes:</td>
                        <td>
                        <asp:TextBox ID="notesTextBox" MaxLength="255" runat="server" TextMode="MultiLine" Text='<%# Bind("Notes") %>' Width="250" Height="70" Wrap="true" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
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
    <asp:ObjectDataSource ID="SubcategoryDataSource" runat="server" 
        SelectMethod="GetSubcategory" 
        TypeName="BudgeteerWeb.BudgeteerService.BudgeteerClient" 
        DataObjectTypeName="BudgeteerWeb.BudgeteerService.Subcategory" 
        UpdateMethod="UpdateSubcategory">
        <SelectParameters>            
            <asp:QueryStringParameter Name="SubcategoryID" QueryStringField="SubcategoryID" Type="Int64" />
        </SelectParameters>        
    </asp:ObjectDataSource>
   
    <asp:ObjectDataSource ID="CategoryDataSource" runat="server" 
        SelectMethod="GetActiveCategories" 
        TypeName="BudgeteerWeb.BudgeteerService.BudgeteerClient">     
    </asp:ObjectDataSource>


</asp:Content>
