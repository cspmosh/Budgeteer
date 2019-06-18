<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="IndexOld.aspx.vb" Inherits="BudgeteerWeb.IndexOld" %>
<%@ Register Assembly="DayPilot.MonthPicker" Namespace="DayPilot.Web.UI" TagPrefix="DayPilot" %>
<%@ Register src="../../Controls/ProgressBar.ascx" tagname="ProgressBar" tagprefix="uc1" %>   
<%@ Register src="../../Controls/Paginator.ascx" tagname="Paginator" tagprefix="uc2" %>

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
<asp:Label ID="lblError" runat="server"></asp:Label>
<DayPilot:MonthPicker ID="MonthPicker1" runat="server" />  
<asp:Button ID="btnMonthPicker" Text="Submit" runat="server" />                
   
<div style="width: 900px;">
                
<asp:Repeater ID="Repeater1" runat="server">
    <HeaderTemplate>        
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr><td colspan="8"><h2>Budgets - <%#MonthPicker1.StartDate.ToString("MMMM")%></h2></td></tr>
        <tr style="background: #000055; color: White;">                 
            <td><strong>Subcategory</strong></td>
            <td></td>
            <td></td>
            <td><strong>Frequency</strong></td>
            <td></td>
            <td></td>
        </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <%#IIf(Container.ItemIndex Mod 2 = 0, "<tr class='evenRow'>", "<tr class='oddRow'>")%>                        
            <td><%#getCategorySubcategoryDescription(DataBinder.Eval(Container.DataItem, "SubcategoryID"))%></td>
            <td><uc1:ProgressBar 
                    ID="ProgressBar1" 
                    runat="server" 
                    Height="17px" 
                    Width="350px" 
                    Maximum='<%#DataBinder.Eval(Container.DataItem, "Amount", "{0:c}")%>'                   
                    Minimum="0"  
                    BorderWidth="1" 
                    BorderStyle="solid"
                    BorderColor="#888888" 
                    ProgressColor="#99FF33"
                    ProgressCompleteColor='<%#System.Drawing.ColorTranslator.FromHtml("#FF402B")%>'
                    ProgressText='<%#String.Format("{0:c}", GetSubcategoryUtilizedDollars(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Expense"))%>' 
                    Value='<%#GetSubcategoryUtilizedDollars(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Expense")%>' 
                    BackColor="#CCCCCC" />
            <td><%#DataBinder.Eval(Container.DataItem, "Amount", "{0:c}")%></td>    
            </td>
            <td><%#GetFrequency(DataBinder.Eval(Container.DataItem, "FrequencyID"))%></td>
            <td align="center"><asp:ImageButton ID="btnEdit" runat="server" ImageUrl="/Images/pencil.png" ToolTip="Edit" PostBackUrl='<%# "~/MemberPages/Budgets/Edit.aspx?BudgetID=" & DataBinder.Eval(Container.dataitem, "BudgetID").toString%>'/></td>
            <td align="center"><asp:ImageButton ID="btnDelete" runat="server" ImageUrl="/Images/delete.png" ToolTip="Delete" OnClick="deleteBudget" CommandArgument='<%# DataBinder.Eval(Container.dataitem, "BudgetID").toString%>'/></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>      
<uc2:Paginator 
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
    PageCounterCssClass="pageCounter" 
    onPageClicked="Paginator1_PageClicked"
    />
        
<br /><br />

<asp:Repeater ID="Repeater2" runat="server">
    <HeaderTemplate>        
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
        <tr style="background: #000055; color: White;">             
            <td><strong>Subcategory</strong></td>
            <td></td>
            <td></td>
            <td><strong>Frequency</strong></td>
            <td></td>
            <td></td>
        </tr>
    </HeaderTemplate>
    <ItemTemplate>
        <%#IIf(Container.ItemIndex Mod 2 = 0, "<tr class='evenRow'>", "<tr class='oddRow'>")%>                        
            <td><%#getCategorySubcategoryDescription(DataBinder.Eval(Container.DataItem, "SubcategoryID"))%></td>
            <td><uc1:ProgressBar 
                    ID="ProgressBar1" 
                    runat="server" 
                    Height="17px" 
                    Width="350px" 
                    Maximum='<%#DataBinder.Eval(Container.DataItem, "Amount", "{0:c}")%>'                   
                    Minimum="0"  
                    BorderWidth="1" 
                    BorderStyle="solid"
                    BorderColor="#888888" 
                    ProgressColor="#99FF33"
                    ProgressCompleteColor='<%#System.Drawing.ColorTranslator.FromHtml("#8CF93C")%>'
                    ProgressText='<%#String.Format("{0:c}", GetSubcategoryUtilizedDollars(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Income"))%>' 
                    Value='<%#GetSubcategoryUtilizedDollars(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Income")%>' 
                    BackColor="#CCCCCC" />
            <td><%#DataBinder.Eval(Container.DataItem, "Amount", "{0:c}")%></td>    
            </td>
            <td><%#GetFrequency(DataBinder.Eval(Container.DataItem, "FrequencyID"))%></td>
            <td><asp:HyperLink ID="lnkEdit" runat="server" NavigateUrl='<%# "~/MemberPages/Budgets/Edit.aspx?BudgetID=" & DataBinder.Eval(Container.dataitem, "BudgetID").toString%>'>Edit</asp:HyperLink></td>
            <td><asp:LinkButton ID="lnkBtnDelete" runat="server" OnClick="deleteBudget" CommandArgument='<%# DataBinder.Eval(Container.dataitem, "BudgetID").toString%>'>Delete</asp:LinkButton></td>
        </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater> 
<uc2:Paginator 
        ID="Paginator2" 
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
        onPageClicked="Paginator2_PageClicked"      
        PageCounterCssClass="pageCounter" />
        
</div>
        
<asp:Button ID="btnNew" runat="server" Text="New Budget" PostBackUrl="~/MemberPages/Budgets/New.aspx" /> 
<asp:Button ID="btnEditFrequencies" runat="server" Text="Edit Frequencies" PostBackUrl="~/MemberPages/Budgets/Frequencies/Index.aspx" /> 
    
</asp:Content>

