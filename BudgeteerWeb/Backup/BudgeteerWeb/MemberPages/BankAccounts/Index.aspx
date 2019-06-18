<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="Index.aspx.vb" Inherits="BudgeteerWeb.Index" %>
<%@ Register src="../../Controls/Paginator.ascx" tagname="Paginator" tagprefix="uc1" %>

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

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/BankAccounts/Index.aspx">Bank Accounts</a></asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">Bank Accounts</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
        <asp:Label ID="lblError" runat="server" cssClass="error" Visible="false"></asp:Label>        
        <asp:Button ID="Button1" CssClass="bgtrButton" runat="server" Text="Add Bank Account" PostBackUrl="~/MemberPages/BankAccounts/New.aspx" /><br /><br />
        
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">                
                <tr class="repeaterHeader">
                   <td style="padding-left: 10px;"><strong>Number</strong></td>
                   <td><strong>Name</strong></td>
                   <td><strong>Type</strong></td>
                   <td><strong>Balance</strong></td>
                   <td><strong>Active</strong></td>
                   <td></td>
                   <td></td>
               </tr>  
        <asp:Repeater ID="accountRepeater" runat="server">
            <ItemTemplate>                       
                <%#IIf(Container.ItemIndex Mod 2 = 0, "<tr class='evenRow'>", "<tr class='oddRow'>")%>
                    <td style="padding-left: 10px;"><%#DataBinder.Eval(Container.DataItem, "Number")%></td> 
                    <td><%#DataBinder.Eval(Container.DataItem, "Name")%></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "Type")%></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "Balance", "{0:c}")%></td>
                    <td><asp:CheckBox ID="cbxActive" runat="server" Enabled="false" checked='<%#DataBinder.Eval(Container.DataItem, "Active")%>'/></td>
                    <td><asp:ImageButton ID="btnEdit" runat="server" ImageUrl="/Images/pencil.png" ToolTip="Edit" PostBackUrl='<%# "~/MemberPages/BankAccounts/Edit.aspx?AccountID=" & DataBinder.Eval(Container.dataitem, "AccountID").toString%>'/></td>
                    <td><asp:ImageButton ID="btnDelete" runat="server" ImageUrl="/Images/delete.png" ToolTip="Delete" OnClick="deleteAccount" CommandArgument='<%# DataBinder.Eval(Container.dataitem, "AccountID").toString%>'/></td>
                </tr>
            </ItemTemplate> 
            <FooterTemplate>
                <tr class="budgetTotal">
                    <td style="padding-left: 10px;" colspan="2"><strong>Total Balance</strong></td>
                    <td></td>                    
                    <td><strong><%#String.Format("{0:c}", balanceTotal)%></strong></td>                    
                    <td colspan="3"></td>                    
                </tr>  
            </FooterTemplate>           
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
    <asp:Button ID="btnNew" CssClass="bgtrButton" runat="server" Text="Add Bank Account" PostBackUrl="~/MemberPages/BankAccounts/New.aspx" />
        
</asp:Content>