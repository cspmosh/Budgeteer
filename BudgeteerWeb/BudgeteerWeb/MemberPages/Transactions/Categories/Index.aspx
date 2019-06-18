<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="Index.aspx.vb" Inherits="BudgeteerWeb.Index3" %>

<asp:Content ID="Header1" ContentPlaceHolderID="head" runat="server">
</asp:Content>   

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/Transactions/Index.aspx">Transactions</a> > <a href="/MemberPages/Transactions/Categories/Index.aspx">Categories</a></asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">Categories</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="width: 100%;">
        <asp:label ID="lblError" CssClass="error" runat="server" Visible="false"></asp:label>
        <asp:Button ID="Button1" CssClass="bgtrButton" runat="server" Text="Add Category" PostBackUrl="~/MemberPages/Transactions/Categories/New.aspx" />&nbsp; 
        <asp:Button ID="Button2" CssClass="bgtrButton" runat="server" Text="Add Subcategory" PostBackUrl="~/MemberPages/Transactions/Subcategories/New.aspx" /><br /><br /> 
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr class="repeaterHeader">
                <td style="padding-left: 10px;"><strong>Category</strong></td>
                <td><strong>Subcategory</strong></td>                        
                <td><strong>Sinking Fund</strong></td>
                <td><strong>Balance</strong></td>
                <td><strong>Active</strong></td>                          
                <td></td>  
                <td></td>                    
            </tr>
        <asp:Repeater ID="categoryRepeater" runat="server" DataSourceID="CategoryDataSource">
            <ItemTemplate>
                <tr class='evenRow'>                     
                    <td style="padding-left: 10px;"><%#DataBinder.Eval(Container.DataItem, "Description")%></td>                    
                    <td></td>                    
                    <td></td> 
                    <td></td>
                    <td><asp:CheckBox ID="CheckBox1" runat="server" checked='<%#DataBinder.Eval(Container.DataItem, "Active")%>' Enabled="False" /></td> 
                    <td align="center"><asp:ImageButton ID="btnEdit" runat="server" ImageUrl="/Images/pencil.png" ToolTip="Edit" PostBackUrl='<%# "~/MemberPages/Transactions/Categories/Edit.aspx?CategoryID=" & DataBinder.Eval(Container.dataitem, "CategoryID").toString%>'/></td>
                    <td align="center"><asp:ImageButton ID="btnDelete" runat="server" ImageUrl="/Images/delete.png" ToolTip="Delete" OnClick="deleteCategory" CommandArgument='<%# DataBinder.Eval(Container.dataitem, "CategoryID").toString%>'/></td>                  
                </tr>
                <asp:Repeater ID="subcategoryRepeater" runat="server" DataSource='<%#getSubcategories(DataBinder.Eval(Container.DataItem, "CategoryID"))%>'>
                <ItemTemplate>  
                <tr class='oddRow'>
                    <td></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "Description")%></td>                    
                    <td><asp:CheckBox ID="CheckBox2" runat="server" checked='<%#DataBinder.Eval(Container.DataItem, "SinkingFund")%>' Enabled="False" /></td>
                    <td><asp:label runat="server" Visible='<%#DataBinder.Eval(Container.DataItem, "SinkingFund")%>'><%#DataBinder.Eval(Container.DataItem, "Balance", "{0:c}")%></asp:label></td>
                    <td><asp:CheckBox ID="CheckBox1" runat="server" checked='<%#DataBinder.Eval(Container.DataItem, "Active")%>' Enabled="False" /></td>
                    <td align="center"><asp:ImageButton ID="btnEdit" runat="server" ImageUrl="/Images/pencil.png" ToolTip="Edit" PostBackUrl='<%# "~/MemberPages/Transactions/Subcategories/Edit.aspx?SubcategoryID=" & DataBinder.Eval(Container.dataitem, "SubcategoryID").toString%>'/></td>
                    <td align="center"><asp:ImageButton ID="btnDelete" runat="server" ImageUrl="/Images/delete.png" ToolTip="Delete" OnClick="deleteSubcategory" CommandArgument='<%# DataBinder.Eval(Container.dataitem, "SubcategoryID").toString%>'/></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>
            </ItemTemplate> 
            <FooterTemplate>
                <tr class="budgetTotal">
                    <td style="padding-left: 10px;" colspan="2"><strong>Total Sinking Fund Balance:</strong></td>
                    <td></td>
                    <td><strong><%#String.Format("{0:c}", BalanceTotal)%></strong></td>
                    <td colspan="3"></td>                    
                </tr>
            </FooterTemplate>           
        </asp:Repeater> 
        </table>   
    </div>     
    <br />
    <asp:Button ID="btnNewCategory" CssClass="bgtrButton" runat="server" Text="Add Category" PostBackUrl="~/MemberPages/Transactions/Categories/New.aspx" />&nbsp; 
    <asp:Button ID="btnNewSubcategory" CssClass="bgtrButton" runat="server" Text="Add Subcategory" PostBackUrl="~/MemberPages/Transactions/Subcategories/New.aspx" /> 
    
    <asp:ObjectDataSource ID="CategoryDataSource" runat="server" 
        SelectMethod="GetCategories" 
        TypeName="BudgeteerWeb.BudgeteerService.BudgeteerClient">     
    </asp:ObjectDataSource>    
       
</asp:Content>
