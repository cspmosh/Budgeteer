<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="New.aspx.vb" Inherits="BudgeteerWeb._New3" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/Transactions/Index.aspx">Transactions</a> > <a href="/MemberPages/Transactions/Categories/Index.aspx">Categories</a> > Add Category</asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">Add Category</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:label ID="lblError" cssClass="error" runat="server" Visible="false"></asp:label>
    <asp:ValidationSummary CssClass="error" ID="validationSummary" DisplayMode="BulletList" runat="server" ShowSummary="true" />   
    <div class="contentContainer">            
        <asp:FormView ID="CategoryFormView" runat="server" 
            DefaultMode="Insert" DataSourceID="CategoryDataSource" 
            DataKeyNames="CategoryID,Description,UserID">
            <InsertItemTemplate>                                
                <table>
                    <tr>
                        <td align="right">Category:*</td>
                        <td>
                            <asp:TextBox ID="DescriptionTextBox" runat="server" 
                                Text='<%# Bind("Description") %>' 
                                MaxLength="255" 
                                Width="400" 
                                TextMode="SingleLine"/>
                            <asp:RequiredFieldValidator 
                                ID="RequiredFieldValidator1" 
                                runat="server" 
                                ErrorMessage="Please enter a category description" 
                                ControlToValidate="DescriptionTextBox" 
                                SetFocusOnError="True" 
                                Display="None"/>                                    
                            <asp:CustomValidator 
                                ID="uniqueCatValidator" 
                                runat="server" 
                                ErrorMessage="Category is already in use" 
                                ControlToValidate="DescriptionTextBox" 
                                onservervalidate="ValidateCategoryDescriptionUniqueness" 
                                Display="None"/>                                   
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Active*:</td>
                        <td>
                            <asp:CheckBox ID="cbxActive" runat="server" checked='<%# Bind("Active") %>'/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center"><br />                  
                            <asp:Button CssClass="bgtrButton" ID="InsertButton" runat="server" CausesValidation="True" 
                                CommandName="Insert" Text="Add" />&nbsp;
                            <asp:Button CssClass="bgtrCancelButton" ID="InsertCancelButton" runat="server" 
                                CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="InsertCancelButton_Click"  />
                        </td>                                
                    </tr>
                </table>                                               
            </InsertItemTemplate>        
        </asp:FormView>
    </div>
    
    <asp:ObjectDataSource ID="CategoryDataSource" runat="server" 
        SelectMethod="GetCategory" 
        TypeName="BudgeteerWeb.BudgeteerService.BudgeteerClient" 
        DataObjectTypeName="BudgeteerWeb.BudgeteerService.Category" 
        InsertMethod="AddCategory">
        <SelectParameters>            
            <asp:QueryStringParameter Name="CategoryID" QueryStringField="CategoryID" Type="Int64" />
        </SelectParameters>        
    </asp:ObjectDataSource>



</asp:Content>