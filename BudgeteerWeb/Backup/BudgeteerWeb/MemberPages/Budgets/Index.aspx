<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/MemberPages/MasterPage.Master" CodeBehind="Index.aspx.vb" Inherits="BudgeteerWeb.Index1" %>
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

<asp:Content ID="bc" ContentPlaceHolderID="breadcrumbs" runat="server"><a href="/MemberPages/Budgets/Index.aspx">Budgets</a></asp:Content>
<asp:Content ID="title" ContentPlaceHolderID="pageTitle" runat="server">
    Budgets
    <span style="padding-left: 20px;"><DayPilot:MonthPicker ID="MonthPicker1" runat="server" /></span>
    <asp:Button ID="btnMonthPicker" Text="Submit" runat="server" /> 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">     
  <asp:label ID="lblError" CssClass="error" runat="server" Visible="false"></asp:label>               
  <asp:Button ID="Button1" runat="server" Text="Add Budget" CssClass="bgtrButton" PostBackUrl="~/MemberPages/Budgets/New.aspx" />&nbsp;  
    <asp:Button ID="Button2" runat="server" Text="Copy Prior Budgets" CssClass="bgtrButton" onclick="copybudgets" ToolTip="Copies all budgets from previous month" /><br /><br />     
  <asp:Repeater ID="IncomeCategoryRepeater" runat="server">
            <HeaderTemplate>        
                <table border="0" cellpadding="0" cellspacing="0" style="width: 100%;">
                <tr><td class="tab">Income</td><td colspan="6"></td></tr>
                <tr class="repeaterHeader">             
                    <td style="padding-left: 10px;"><strong>Category</strong></td>      
                    <td><strong>Subcategory</strong></td>           
                    <td style="width: 390px;"><strong>Utilized Dollars</strong></td>
                    <td><strong>Available</strong></td>
                    <td><strong>Budgeted</strong></td>
                    <td colspan="2" style="width: 50px;"></td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="evenRow">                        
                    <td colspan="2" style="padding-left: 10px;"><%#DataBinder.Eval(Container.DataItem, "Description")%></td>
                    <td><uc1:ProgressBar                                                      
                            ID="ProgressBar1" 
                            runat="server" 
                            Height="17px" 
                            Width="350px" 
                            fontColor="#000000"                             
                            Maximum='<%#DataBinder.Eval(Container.DataItem, "TotalBudget", "{0:c}")%>'                   
                            Minimum="0"  
                            BorderWidth="1" 
                            BorderStyle="solid"
                            BorderColor="#888888" 
                            ProgressColor="#77bb22"
                            ProgressCompleteColor='<%#System.Drawing.ColorTranslator.FromHtml("#77bb22")%>'
                            ProgressText='<%#String.Format("{0:c}", DataBinder.Eval(Container.DataItem, "UtilizedAmount")) & "&nbsp;<em>of</em>&nbsp;" & String.Format("{0:c}", DataBinder.Eval(Container.DataItem, "TotalBudget")) %>' 
                            Value='<%#DataBinder.Eval(Container.DataItem, "UtilizedAmount")%>' 
                            BackColor="#CCCCCC" />
                    </td>
                    <td><%#DataBinder.Eval(Container.DataItem, "AvailableAmount", "{0:c}")%></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "MonthlyBudget", "{0:c}")%></td> 
                    <td colspan="3"></td>   
                </tr>                
                <asp:Repeater ID="IncomeSubcategoryRepeater" runat="server" DataSource='<%#GetIncomeBudgetSubcategories(DataBinder.Eval(Container.DataItem, "categoryID"))%>'>
                <ItemTemplate>
                <%#IIf(Container.ItemIndex Mod 2 = 0, "<tr class='oddRow'>", "<tr class='oddRow'>")%>  
                    <td></td>                      
                    <td><%#getSubcategoryDescription(DataBinder.Eval(Container.DataItem, "SubcategoryID"))%></td>
                    <td><uc1:ProgressBar 
                            ID="ProgressBar1" 
                            runat="server" 
                            Height="17px" 
                            Width="350px" 
                            fontColor="#000000"
                            Maximum='<%#String.Format("{0:c}", GetSubcategoryBudget(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Income"))%>'                   
                            Minimum="0"  
                            BorderWidth="1" 
                            BorderStyle="solid"
                            BorderColor="#888888" 
                            ProgressColor="#77bb22"
                            ProgressCompleteColor='<%#System.Drawing.ColorTranslator.FromHtml("#77bb22")%>'
                            ProgressText='<%#String.Format("{0:c}", GetSubcategoryUtilizedDollars(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Income")) & "&nbsp;<em>of</em>&nbsp;" & String.Format("{0:c}", GetSubcategoryBudget(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Income")) %>' 
                            Value='<%#GetSubcategoryUtilizedDollars(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Income")%>' 
                            BackColor="#CCCCCC" /></td>
                    <td><%#String.Format("{0:c}", getSubcategoryAvailableDollars(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Income"))%></td>
                    <td><%#String.Format("{0:c}", DataBinder.Eval(Container.DataItem, "Amount"))%></td>                             
                    <td align="center"><asp:ImageButton ID="btnEdit" runat="server" ImageUrl="/Images/pencil.png" ToolTip="Edit" PostBackUrl='<%# "~/MemberPages/Budgets/Edit.aspx?BudgetID=" & DataBinder.Eval(Container.dataitem, "BudgetID").toString%>'/></td>
                    <td align="center"><asp:ImageButton ID="btnDelete" runat="server" ImageUrl="/Images/delete.png" ToolTip="Delete" OnClick="deleteBudget" CommandArgument='<%# DataBinder.Eval(Container.dataitem, "BudgetID").toString%>'/></td>
                </tr>                
                </ItemTemplate>
                </asp:Repeater>                               
            </ItemTemplate>
            <FooterTemplate>
                <tr class="budgetTotal">
                    <td style="padding-left: 10px;" colspan="2"><strong>Total Income</strong></td>
                    <td></td>
                    <td><strong><%#String.Format("{0:c}", availableIncomeTotal)%></strong></td>
                    <td><strong><%#String.Format("{0:c}", IncomeTotal)%></strong></td>
                    <td></td>
                    <td></td>
                </tr>  
            </FooterTemplate>
        </asp:Repeater>  
            
        <tr><td colspan="8" style="background-color:#eeeeee; border: 1px solid #cccccc;"><uc2:Paginator 
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
                PageCounterCssClass="pageCounter" /></td></tr> 
                <tr><td colspan="8" style="height: 30px;"></td></tr>
                 
                 
        <asp:Repeater ID="ExpenseCategoryRepeater" runat="server">
            <HeaderTemplate>        
                <tr><td class="tab">Expenses</td><td colspan="6"></td></tr>
                <tr class="repeaterHeader">             
                    <td style="padding-left: 10px;"><strong>Category</strong></td>      
                    <td><strong>Subcategory</strong></td>           
                    <td><strong>Utilized Dollars</strong></td>
                    <td><strong>Available</strong></td>
                    <td><strong>Budgeted</strong></td>
                    <td colspan="2"></td>
                </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="evenRow" >                        
                    <td colspan="2" style="padding-left: 10px;"><%#DataBinder.Eval(Container.DataItem, "Description")%></td>
                    <td ><uc1:ProgressBar 
                            visible="true"
                            ID="ProgressBar1" 
                            runat="server" 
                            Height="17px" 
                            Width="350px" 
                            fontColor="#000000"
                            Maximum='<%#DataBinder.Eval(Container.DataItem, "TotalBudget")%>'                   
                            Minimum="0"  
                            BorderWidth="1" 
                            BorderStyle="solid"
                            BorderColor="#888888" 
                            ProgressColor="#77bb22"
                            ProgressCompleteColor='<%#System.Drawing.ColorTranslator.FromHtml("#ff6644")%>'
                            ProgressText='<%#String.Format("{0:c}", -DataBinder.Eval(Container.DataItem, "UtilizedAmount")) & "&nbsp;<em>of</em>&nbsp;" & String.Format("{0:c}", DataBinder.Eval(Container.DataItem, "TotalBudget")) %>' 
                            Value='<%#-DataBinder.Eval(Container.DataItem, "UtilizedAmount")%>' 
                            BackColor="#CCCCCC" />
                    </td>
                    <td><%#DataBinder.Eval(Container.DataItem, "AvailableAmount", "{0:c}")%></td>
                    <td><%#DataBinder.Eval(Container.DataItem, "MonthlyBudget", "{0:c}")%></td> 
                    <td colspan="3"></td>   
                </tr>                
                <asp:Repeater ID="ExpenseSubcategoryRepeater" runat="server" DataSource='<%#GetExpenseBudgetSubcategories(DataBinder.Eval(Container.DataItem, "categoryID"))%>'>
                <ItemTemplate>
                <%#IIf(Container.ItemIndex Mod 2 = 0, "<tr class='oddRow'>", "<tr class='oddRow'>")%>  
                    <td></td>                      
                    <td><%#getSubcategoryDescription(DataBinder.Eval(Container.DataItem, "SubcategoryID"))%></td>
                    <td><uc1:ProgressBar 
                            ID="ProgressBar1" 
                            runat="server" 
                            Height="17px" 
                            Width="350px" 
                            fontColor="#000000"
                            Maximum='<%#String.Format("{0:c}", GetSubcategoryBudget(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Expense"))%>'                   
                            Minimum="0"  
                            BorderWidth="1" 
                            BorderStyle="solid"
                            BorderColor="#888888" 
                            ProgressColor="#77bb22"
                            ProgressCompleteColor='<%#System.Drawing.ColorTranslator.FromHtml("#ff6644")%>'
                            ProgressText='<%#String.Format("{0:c}", GetSubcategoryUtilizedDollars(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Expense"))  & "&nbsp;<em>of</em>&nbsp;" & String.Format("{0:c}", GetSubcategoryBudget(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Expense"))%>' 
                            Value='<%#GetSubcategoryUtilizedDollars(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Expense")%>' 
                            BackColor="#CCCCCC" /></td>
                    <td><%#String.Format("{0:c}", getSubcategoryAvailableDollars(DataBinder.Eval(Container.DataItem, "SubcategoryID"), "Expense"))%></td>
                    <td><%#String.Format("{0:c}", DataBinder.Eval(Container.DataItem, "Amount"))%></td>                                
                    <td align="center"><asp:ImageButton ID="btnEdit" runat="server" ImageUrl="/Images/pencil.png" ToolTip="Edit" PostBackUrl='<%# "~/MemberPages/Budgets/Edit.aspx?BudgetID=" & DataBinder.Eval(Container.dataitem, "BudgetID").toString%>'/></td>
                    <td align="center"><asp:ImageButton ID="btnDelete" runat="server" ImageUrl="/Images/delete.png" ToolTip="Delete" OnClick="deleteBudget" CommandArgument='<%# DataBinder.Eval(Container.dataitem, "BudgetID").toString%>'/></td>
                </tr>
                </ItemTemplate>
                </asp:Repeater>                  
            </ItemTemplate>
            <FooterTemplate>
                <tr class="budgetTotal">
                    <td style="padding-left: 10px;" colspan="2"><strong>Total Expenses</strong></td>
                    <td></td>
                    <td><strong><%#String.Format("{0:c}", availableExpenseTotal)%></strong></td>
                    <td><strong><%#String.Format("{0:c}", ExpenseTotal)%></strong></td>
                    <td></td>
                    <td></td>
                </tr>
            </FooterTemplate>
        </asp:Repeater> 
                  
        <tr><td colspan="8" class="paginatorContainer"><uc2:Paginator 
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
            />  </td></tr>     
        </table>

        <br />
    <asp:Button ID="btnNew" runat="server" Text="Add Budget" CssClass="bgtrButton" PostBackUrl="~/MemberPages/Budgets/New.aspx" />&nbsp; 
    <asp:Button ID="btnCopy" runat="server" Text="Copy Prior Budgets" CssClass="bgtrButton" onclick="copybudgets" ToolTip="Copies all budgets from previous month" />
</asp:Content>

