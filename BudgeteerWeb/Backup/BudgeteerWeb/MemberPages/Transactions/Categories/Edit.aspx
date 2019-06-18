<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="Edit.aspx.vb" Inherits="BudgeteerWeb.Edit2" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/Transactions/Index.aspx">Transactions</a> > <a href="/MemberPages/Transactions/Categories/Index.aspx">Categories</a> > Edit Category</asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">Edit Category</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:label ID="lblError" cssClass="error" runat="server" Visible="false"></asp:label>
    <asp:ValidationSummary CssClass="error" ID="validationSummary" DisplayMode="BulletList" runat="server" ShowSummary="true" /> 
    <div class="contentContainer">                
        <asp:FormView ID="CategoryFormView" runat="server" 
            DefaultMode="Edit" DataSourceID="CategoryDataSource" 
            DataKeyNames="CategoryID,Description,UserID">
            <EditItemTemplate>                                   
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
    <asp:ObjectDataSource ID="CategoryDataSource" runat="server" 
        SelectMethod="GetCategory" 
        TypeName="BudgeteerWeb.BudgeteerService.BudgeteerClient" 
        DataObjectTypeName="BudgeteerWeb.BudgeteerService.Category" 
        UpdateMethod="UpdateCategory">
        <SelectParameters>            
            <asp:QueryStringParameter Name="CategoryID" QueryStringField="CategoryID" Type="Int64" />
        </SelectParameters>        
    </asp:ObjectDataSource>

</asp:Content>