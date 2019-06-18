Imports System.Drawing
Imports System
Imports System.Linq
Imports System.Web.UI.WebControls
Imports System.Collections.Specialized
Imports System.ServiceModel

Partial Public Class Index1
    Inherits System.Web.UI.Page

    Dim budgetDate As Date?
    Dim ExpenseBudgets As List(Of BudgeteerService.Budget) = New List(Of BudgeteerService.Budget)
    Dim IncomeBudgets As List(Of BudgeteerService.Budget) = New List(Of BudgeteerService.Budget)
    Dim UtilizedExpenseDollars As List(Of BudgeteerService.UtilizedDollars) = New List(Of BudgeteerService.UtilizedDollars)
    Dim UtilizedIncomeDollars As List(Of BudgeteerService.UtilizedDollars) = New List(Of BudgeteerService.UtilizedDollars)
    Dim ExpensePaginator As BudgeteerService.Paginator = New BudgeteerService.Paginator
    Dim IncomePaginator As BudgeteerService.Paginator = New BudgeteerService.Paginator
    Dim Categories As List(Of BudgeteerService.Category) = New List(Of BudgeteerService.Category)
    Dim Subcategories As List(Of BudgeteerService.Subcategory) = New List(Of BudgeteerService.Subcategory)
    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient
    Dim firstBudgetYear As Integer = 2011
    Dim ExpenseCategoryBudgets As List(Of CategoryBudget) = New List(Of CategoryBudget)
    Dim IncomeCategoryBudgets As List(Of CategoryBudget) = New List(Of CategoryBudget)
    Public IncomeTotal As Decimal
    Public ExpenseTotal As Decimal
    Public utilizedIncomeTotal As Decimal
    Public utilizedExpenseTotal As Decimal
    Public budgetedIncomeTotal As Decimal
    Public budgetedExpenseTotal As Decimal
    Public availableIncomeTotal As Decimal
    Public availableExpenseTotal As Decimal

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim querystring As NameValueCollection = Request.QueryString

        If (Not (Request.IsAuthenticated)) Then
            Response.Redirect("~/login.aspx")
        End If

        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
        'Set the page size here
        ExpensePaginator.PageSize = 50
        IncomePaginator.PageSize = 30
        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    firstBudgetYear = wcfProxy.GetBudgetFirstYear()
                Catch timeoutEx As TimeoutException
                    ShowError(timeoutEx.Message)
                    wcfProxy.Abort()
                Catch ex As FaultException
                    ShowError(ex.Message)
                Catch commEx As CommunicationException
                    ShowError(commEx.Message)
                    wcfProxy.Abort()
                End Try
            Else
                Response.Redirect("~/login.aspx")
            End If
        End If
        If firstBudgetYear = Nothing Then
            firstBudgetYear = Date.Today.Year
        End If
        MonthPicker1.YearStart = firstBudgetYear
        MonthPicker1.YearEnd = Date.Today.Year

        If Not Page.IsPostBack Then
            budgetDate = Session("Budgets.BudgetMonth")
            If budgetDate Is Nothing Then
                budgetDate = MonthPicker1.StartDate
                Session("Budgets.BudgetMonth") = budgetDate
            Else
                MonthPicker1.StartDate = budgetDate
            End If
            If querystring.HasKeys Then
                Dim expensePage As Integer = querystring.Get("ExpensePage")
                Dim incomePage As Integer = querystring.Get("IncomePage")
                If expensePage = Nothing Then
                    ExpensePaginator.PageNumber = 1
                Else
                    If expensePage < 1 Then
                        expensePage = 1
                    End If
                    ExpensePaginator.PageNumber = expensePage
                End If
                If incomePage = Nothing Then
                    IncomePaginator.PageNumber = 1
                Else
                    If incomePage < 1 Then
                        incomePage = 1
                    End If
                    IncomePaginator.PageNumber = incomePage
                End If
            Else
                ExpensePaginator.PageNumber = 1
                IncomePaginator.PageNumber = 1
            End If
            RetrieveData()
        End If
    End Sub

    Protected Function getCategoryDescription(ByVal CategoryID As Int64) As String
        Dim description As String = ""
        If Categories IsNot Nothing Then
            For Each category As BudgeteerService.Category In Categories
                If category.CategoryID = CategoryID Then
                    description = category.Description
                    Exit For
                End If
            Next
        End If
        Return description
    End Function

    Protected Function getSubcategoryDescription(ByVal subcategoryID As Int64) As String
        Dim description As String = ""
        If Subcategories IsNot Nothing Then
            For Each subcategory As BudgeteerService.Subcategory In Subcategories
                If subcategory.SubcategoryID = subcategoryID Then
                    description = subcategory.Description
                    Exit For
                End If
            Next
        End If
        Return description
    End Function

    Private Function getCategories() As List(Of BudgeteerService.Category)
        Dim Categories As List(Of BudgeteerService.Category) = New List(Of BudgeteerService.Category)
        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    Categories = wcfProxy.GetCategories()
                Catch timeoutEx As TimeoutException
                    ShowError(timeoutEx.Message)
                    wcfProxy.Abort()
                Catch ex As FaultException
                    ShowError(ex.Message)
                Catch commEx As CommunicationException
                    ShowError(commEx.Message)
                    wcfProxy.Abort()
                End Try
            Else
                Response.Redirect("~/login.aspx")
            End If
        End If
        Return Categories
    End Function

    Protected Function getSubcategories() As List(Of BudgeteerService.Subcategory)
        Dim Subcategories As List(Of BudgeteerService.Subcategory) = New List(Of BudgeteerService.Subcategory)
        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    Subcategories = wcfProxy.GetSubcategories()
                Catch timeoutEx As TimeoutException
                    ShowError(timeoutEx.Message)
                    wcfProxy.Abort()
                Catch ex As FaultException
                    ShowError(ex.Message)
                Catch commEx As CommunicationException
                    ShowError(commEx.Message)
                    wcfProxy.Abort()
                End Try
            Else
                Response.Redirect("~/login.aspx")
            End If
        End If
        Return Subcategories
    End Function

    Protected Sub deleteBudget(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkBtn As ImageButton = CType(sender, ImageButton)
        If lnkBtn.CommandArgument <> "" Then
            Dim result As Integer
            If wcfProxy IsNot Nothing Then
                If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                    wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                    Try
                        result = wcfProxy.DeleteBudget(CType(lnkBtn.CommandArgument, Int64))
                        ExpensePaginator = DirectCast(Session("Budgets.ExpensePaginator"), BudgeteerService.Paginator)
                        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) & "?ExpensePage=" & Paginator1.CurrentPageNumber() & "&IncomePage=" & Paginator2.CurrentPageNumber())
                    Catch timeoutEx As TimeoutException
                        ShowError(timeoutEx.Message)
                        wcfProxy.Abort()
                    Catch ex As FaultException
                        ShowError(ex.Message)
                    Catch commEx As CommunicationException
                        ShowError(commEx.Message)
                        wcfProxy.Abort()
                    End Try
                Else
                    Response.Redirect("~/login.aspx")
                End If
            End If
        End If
    End Sub

    Protected Function getSubcategoryBudget(ByVal subcategoryID As Int64, ByVal budgetType As String) As Decimal
        Select Case budgetType
            Case "Expense"
                For Each dollars As BudgeteerService.UtilizedDollars In UtilizedExpenseDollars
                    If dollars.SubcategoryID = subcategoryID Then
                        Return dollars.Budget
                    End If
                Next
            Case "Income"
                For Each dollars As BudgeteerService.UtilizedDollars In UtilizedIncomeDollars
                    If dollars.SubcategoryID = subcategoryID Then
                        Return dollars.Budget
                    End If
                Next
        End Select
    End Function

    Protected Function getSubcategoryUtilizedDollars(ByVal subcategoryID As Int64, ByVal budgetType As String) As Decimal
        Select Case budgetType
            Case "Expense"
                For Each dollars As BudgeteerService.UtilizedDollars In UtilizedExpenseDollars
                    If dollars.SubcategoryID = subcategoryID Then
                        Return -dollars.Amount
                    End If
                Next
            Case "Income"
                For Each dollars As BudgeteerService.UtilizedDollars In UtilizedIncomeDollars
                    If dollars.SubcategoryID = subcategoryID Then
                        Return dollars.Amount
                    End If
                Next
        End Select
    End Function

    Protected Function getSubcategoryAvailableDollars(ByVal subcategoryID As Int64, ByVal budgetType As String) As Decimal
        Select Case budgetType
            Case "Expense"
                For Each dollars As BudgeteerService.UtilizedDollars In UtilizedExpenseDollars
                    If dollars.SubcategoryID = subcategoryID Then
                        Return dollars.Available
                    End If
                Next
            Case "Income"
                For Each dollars As BudgeteerService.UtilizedDollars In UtilizedIncomeDollars
                    If dollars.SubcategoryID = subcategoryID Then
                        Return dollars.Available
                    End If
                Next
        End Select
    End Function

    Protected Sub btnMonthPicker_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMonthPicker.Click
        budgetDate = MonthPicker1.StartDate
        Session("Budgets.BudgetMonth") = budgetDate
        ExpensePaginator.PageNumber = 1
        IncomePaginator.PageNumber = 1
        RetrieveData()
    End Sub

    Private Sub RetrieveData()

        Categories = getCategories()
        Subcategories = getSubcategories()

        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    ' Get Expense Budgets and Utilized Dollars
                    ExpenseBudgets = wcfProxy.GetBudgetsWithCriteria(budgetDate, "Expense", ExpenseTotal, ExpensePaginator)
                    UtilizedExpenseDollars = wcfProxy.GetUtilizedDollarsWithCriteria(budgetDate, "Expense", utilizedExpenseTotal, budgetedExpenseTotal, availableExpenseTotal, ExpensePaginator)
                    ExpenseCategoryBudgets = GetBudgetCategoryTotals(ExpenseBudgets, UtilizedExpenseDollars)

                    ' Get Income Budgets and Utilized Dollars
                    IncomeBudgets = wcfProxy.GetBudgetsWithCriteria(budgetDate, "Income", IncomeTotal, IncomePaginator)
                    UtilizedIncomeDollars = wcfProxy.GetUtilizedDollarsWithCriteria(budgetDate, "Income", utilizedIncomeTotal, budgetedIncomeTotal, availableIncomeTotal, IncomePaginator)
                    IncomeCategoryBudgets = GetBudgetCategoryTotals(IncomeBudgets, UtilizedIncomeDollars)

                    ExpenseCategoryRepeater.DataSource = ExpenseCategoryBudgets
                    ExpenseCategoryRepeater.DataBind()

                    IncomeCategoryRepeater.DataSource = IncomeCategoryBudgets
                    IncomeCategoryRepeater.DataBind()

                    InitializePaginators()
                Catch timeoutEx As TimeoutException
                    ShowError(timeoutEx.Message)
                    wcfProxy.Abort()
                Catch ex As FaultException
                    ShowError(ex.Message)
                Catch commEx As CommunicationException
                    ShowError(commEx.Message)
                    wcfProxy.Abort()
                End Try
            Else
                Response.Redirect("~/login.aspx")
            End If
        End If
    End Sub

    Private Function getCategoryID(ByVal subcategoryID As Int64) As Int64
        If Subcategories IsNot Nothing Then
            For Each subcategory As BudgeteerService.Subcategory In Subcategories
                If subcategory.SubcategoryID = subcategoryID Then
                    Return subcategory.CategoryID
                End If
            Next
        End If
        Return Nothing
    End Function

    Public Function GetExpenseBudgetSubcategories(ByVal categoryID As Int64) As List(Of BudgeteerService.Budget)
        Dim budgets As List(Of BudgeteerService.Budget) = New List(Of BudgeteerService.Budget)
        If ExpenseBudgets IsNot Nothing Then
            For Each budget As BudgeteerService.Budget In ExpenseBudgets
                If categoryID = getCategoryID(budget.SubcategoryID) Then
                    budgets.Add(budget)
                End If
            Next
        End If
        Return budgets
    End Function

    Public Function GetIncomeBudgetSubcategories(ByVal categoryID As Int64) As List(Of BudgeteerService.Budget)
        Dim budgets As List(Of BudgeteerService.Budget) = New List(Of BudgeteerService.Budget)
        If IncomeBudgets IsNot Nothing Then
            For Each budget As BudgeteerService.Budget In IncomeBudgets
                If categoryID = getCategoryID(budget.SubcategoryID) Then
                    budgets.Add(budget)
                End If
            Next
        End If
        Return budgets
    End Function

    Private Function GetBudgetCategoryTotals(ByVal SubcategoryBudgets As List(Of BudgeteerService.Budget), ByVal UtilizedDollars As List(Of BudgeteerService.UtilizedDollars)) As List(Of CategoryBudget)
        Dim budgetCategories As List(Of CategoryBudget) = New List(Of CategoryBudget)
        Dim lastCategoryID As Int64 = Nothing
        If (UtilizedDollars IsNot Nothing) And (SubcategoryBudgets IsNot Nothing) Then
            For i = 0 To UtilizedDollars.Count - 1
                If UtilizedDollars(i).CategoryID <> lastCategoryID Then
                    Dim budget As CategoryBudget = New CategoryBudget
                    budget.categoryID = UtilizedDollars(i).CategoryID
                    budget.description = getCategoryDescription(UtilizedDollars(i).CategoryID)
                    budget.monthlyBudget = SubcategoryBudgets(i).Amount
                    budget.totalBudget = UtilizedDollars(i).Budget
                    budget.utilizedAmount = UtilizedDollars(i).Amount
                    budget.availableAmount = UtilizedDollars(i).Available
                    budgetCategories.Add(budget)
                Else
                    budgetCategories(budgetCategories.Count - 1).monthlyBudget += SubcategoryBudgets(i).Amount
                    budgetCategories(budgetCategories.Count - 1).totalBudget += UtilizedDollars(i).Budget
                    budgetCategories(budgetCategories.Count - 1).utilizedAmount += UtilizedDollars(i).Amount
                    budgetCategories(budgetCategories.Count - 1).availableAmount += UtilizedDollars(i).Available
                End If
                lastCategoryID = UtilizedDollars(i).CategoryID
            Next
        End If
        Return budgetCategories
    End Function

    Private Sub InitializePaginators()
        Session("Budgets.ExpensePaginator") = ExpensePaginator
        Session("Budgets.IncomePaginator") = IncomePaginator

        If ExpensePaginator.LastPage > 0 Then
            Paginator1.CurrentPageNumber = ExpensePaginator.PageNumber
            Paginator1.TotalPageCount = ExpensePaginator.LastPage
        Else
            Paginator1.CurrentPageNumber = 1
            Paginator1.TotalPageCount = 1
        End If

        Paginator1.PageCounterText = Paginator1.CurrentPageNumber.ToString & " of " & Paginator1.TotalPageCount.ToString

        If IncomePaginator.LastPage > 0 Then
            Paginator2.CurrentPageNumber = IncomePaginator.PageNumber
            Paginator2.TotalPageCount = IncomePaginator.LastPage
        Else
            Paginator2.CurrentPageNumber = 1
            Paginator2.TotalPageCount = 1
        End If

        Paginator2.PageCounterText = Paginator2.CurrentPageNumber.ToString & " of " & Paginator2.TotalPageCount.ToString

    End Sub

    Protected Sub Paginator1_PageClicked(ByVal sender As Object, ByVal e As PaginatorEventArgs)
        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) & "?ExpensePage=" & e.page & "&IncomePage=" & Paginator2.CurrentPageNumber())
    End Sub

    Protected Sub Paginator2_PageClicked(ByVal sender As Object, ByVal e As PaginatorEventArgs)
        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) & "?ExpensePage=" & Paginator1.CurrentPageNumber() & "&IncomePage=" & e.page)
    End Sub

    Protected Sub CopyBudgets()
        budgetDate = MonthPicker1.StartDate
        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    wcfProxy.CopyBudgets(DateAdd(DateInterval.Month, -1, budgetDate.Value), budgetDate)
                    Response.Redirect("Index.aspx")
                Catch timeoutEx As TimeoutException
                    ShowError(timeoutEx.Message)
                    wcfProxy.Abort()
                Catch ex As FaultException
                    ShowError(ex.Message)
                Catch commEx As CommunicationException
                    ShowError(commEx.Message)
                    wcfProxy.Abort()
                End Try
            Else
                Response.Redirect("~/login.aspx")
            End If
        End If
    End Sub

    Private Sub ShowError(ByVal msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

End Class

Public Class CategoryBudget

    Private m_category_id As Int64
    Private m_description As String
    Private m_budget_amount As Decimal
    Private m_budget_total As Decimal
    Private m_utilized_amount As Decimal
    Private m_available_amount As Decimal

    Public Property categoryID() As Int64
        Get
            Return m_category_id
        End Get
        Set(ByVal value As Int64)
            m_category_id = value
        End Set
    End Property

    Public Property description() As String
        Get
            Return m_description
        End Get
        Set(ByVal value As String)
            m_description = value
        End Set
    End Property

    Public Property monthlyBudget() As Decimal
        Get
            Return m_budget_amount
        End Get
        Set(ByVal value As Decimal)
            m_budget_amount = value
        End Set
    End Property

    Public Property totalBudget() As Decimal
        Get
            Return m_budget_total
        End Get
        Set(ByVal value As Decimal)
            m_budget_total = value
        End Set
    End Property

    Public Property utilizedAmount() As Decimal
        Get
            Return m_utilized_amount
        End Get
        Set(ByVal value As Decimal)
            m_utilized_amount = value
        End Set
    End Property

    Public Property availableAmount() As Decimal
        Get
            Return m_available_amount
        End Get
        Set(ByVal value As Decimal)
            m_available_amount = value
        End Set
    End Property

End Class
