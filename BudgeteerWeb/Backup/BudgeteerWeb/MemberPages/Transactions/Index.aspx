<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="Index.aspx.vb" Inherits="BudgeteerWeb.Index2" %>
<%@ Register Assembly="DayPilot.MonthPicker" Namespace="DayPilot.Web.UI" TagPrefix="DayPilot" %>
<%@ Register src="../../Controls/Paginator.ascx" tagname="Paginator" tagprefix="uc1" %>

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/Transactions/Index.aspx">Transactions</a></asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">Transactions</asp:Content>

<asp:Content ID="Header1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        // preload images 
        ProgressBarHighlightImage = new Image(1, 1);
        ProgressBarHighlightImage.src = "/Images/ProgressBarHighlight.png";
        PaginatorImage01 = new Image(1, 1);
        PaginatorImage01.src = "/Images/page_first.png";
        PaginatorImage02 = new Image(1, 1);
        PaginatorImage02.src = "/Images/page_previous.png";
        PaginatorImage03 = new Image(1, 1);
        PaginatorImage03.src = "/Images/page_next.png";
        PaginatorImage04 = new Image(1, 1);
        PaginatorImage04.src = "/Images/page_last.png";      
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<asp:label ID="lblError" CssClass="error" runat="server" Visible="false"></asp:label>
<asp:Button ID="Button1" runat="server" cssClass="bgtrButton" Text="Add Transaction" PostBackUrl="~/MemberPages/Transactions/New.aspx" />&nbsp; 
<asp:Button ID="Button3" runat="server" cssClass="bgtrButton" Text="Edit Categories" PostBackUrl="~/MemberPages/Transactions/Categories/Index.aspx" /><br /><br />
          
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;" > 
            <tr class="repeaterHeader">
                <td style="width: 200px; padding-left: 10px;"><strong>Date</strong></td>
                <td><strong>Description</strong></td>
                <td align="center" style="width: 75px;"><strong>Amount</strong></td>
                <td style="width: 200px;"><strong>Category / Subcategory</strong></td>
                <td style="width: 150px;"><strong>Bank Account</strong></td>
                <td style="width: 35px;"></td>
                <td style="width: 15px;"></td>
            </tr>         
            <tr>
                <td><asp:DropDownList ID="ddlMonth" runat="server" style="height:22px;"></asp:DropDownList><asp:DropDownList ID="ddlDay" runat="server" style="height:22px;"></asp:DropDownList><asp:DropDownList ID="ddlYear" runat="server" style="height:22px;"></asp:DropDownList></td>
                <td><div style="display: block; padding-right: 8px;"><asp:TextBox ID="txtDescription" runat="server" style="width: 100%;"></asp:TextBox></div></td>
                <td><div style="display: block; padding-right: 8px;"><asp:TextBox ID="txtAmount" runat="server" style="width: 100%;"></asp:TextBox></div></td>
                <td><div style="display: block; padding-right: 4px;"><asp:DropDownList ID="ddlCatSubcat" runat="server" style="width: 100%; height: 22px;"></asp:DropDownList></div></td>
                <td><div style="display: block; padding-right: 4px;"><asp:DropDownList ID="ddlBankAccount" runat="server" style="width: 100%; height: 22px;"></asp:DropDownList></div></td>
                <td align="center"><div style="display: block; padding-right: 8px;"><asp:ImageButton ID="btnClear" runat="server" ImageUrl="/Images/clear.png" tooltip="Clear filters" OnClick="clearFilter" /></div></td>
                <td><asp:Button ID="btnFilter" Text="Filter" runat="server" /></td>
            </tr>          
            
<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
            <%#IIf(Container.ItemIndex Mod 2 = 0, "<tr class='evenRow'>", "<tr class='oddRow'>")%>
                <td style="padding-left: 5px;"><%#DataBinder.Eval(Container.DataItem, "TransactionDate", "{0:MMMM dd, yyyy (ddd)}")%></td> 
                <td><%#DataBinder.Eval(Container.DataItem, "Description")%></td>
                <td align="right" style="padding-right: 8px;"><%#DataBinder.Eval(Container.DataItem, "Amount", "{0:c}")%></td>
                <td><%#GetSubcategory(DataBinder.Eval(Container.DataItem, "SubcategoryID"))%></td>
                <td><%#GetBankAccount(DataBinder.Eval(Container.DataItem, "AccountID"))%></td>
                <td align="center"><asp:ImageButton ID="btnEdit" runat="server" ImageUrl="/Images/pencil.png" ToolTip="Edit" PostBackUrl='<%# "~/MemberPages/Transactions/Edit.aspx?TransactionID=" & DataBinder.Eval(Container.dataitem, "TransactionID").toString%>'/></td>
                <td align="center"><asp:ImageButton ID="btnDelete" runat="server" ImageUrl="/Images/delete.png" ToolTip="Delete" OnClick="deleteTransaction" CommandArgument='<%# DataBinder.Eval(Container.dataitem, "TransactionID").toString%>'/></td>
            </tr>
    </ItemTemplate>
</asp:Repeater> 
            <tr>
                <td colspan="7" class="paginatorContainer">
                    <uc1:Paginator 
                        ID="Paginator1" 
                        runat="server" 
                        cssClass="paginator"  
                        HorizontalAlign="Center"
                        EnableViewState="true"        
                        FirstPageText="First" 
                        FirstPageImage="/Images/page_first.png"
                        PreviousPageText="Previous"
                        PreviousPageImage="/Images/page_previous.png"
                        NextPageText="Next"  
                        NextPageImage="/Images/page_next.png"
                        LastPageText="Last"
                        LastPageImage="/Images/page_last.png"  
                        onPageClicked="Paginator1_PageClicked"      
                        PageCounterCssClass="pageCounter" />  
                </td>
            </tr>
        </table>


    <br />
    <asp:Button ID="btnNew" runat="server" cssClass="bgtrButton" Text="Add Transaction" PostBackUrl="~/MemberPages/Transactions/New.aspx" />&nbsp; 
    <asp:Button ID="btnEditCategories" runat="server" cssClass="bgtrButton" Text="Edit Categories" PostBackUrl="~/MemberPages/Transactions/Categories/Index.aspx" />&nbsp;
       
</asp:Content>
