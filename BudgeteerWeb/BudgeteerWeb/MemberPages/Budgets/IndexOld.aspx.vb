Imports System.Drawing
Imports System
Imports System.Linq
Imports System.Web.UI.WebControls
Imports System.Collections.Specialized

Partial Public Class IndexOld
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
    Dim Frequencies As List(Of BudgeteerService.Frequency) = New List(Of BudgeteerService.Frequency)
    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient
    Dim firstBudgetYear As Integer = 2011
    Dim subcatExpenseDollars As NameValueCollection = New NameValueCollection
    Dim catExpenseDollars As NameValueCollection = New NameValueCollection
    Dim subcatIncomeDollars As NameValueCollection = New NameValueCollection
    Dim catIncomeDollars As NameValueCollection = New NameValueCollection

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
        'Set the page size here
        ExpensePaginator.PageSize = 30
        IncomePaginator.PageSize = 30
        Try
            firstBudgetYear = wcfProxy.GetBudgetFirstYear()
        Catch ex As Exception
            lblError.Text = ex.Message()
        End Try
        If firstBudgetYear = Nothing Then
            firstBudgetYear = Date.Today.Year
        End If
        MonthPicker1.YearStart = firstBudgetYear
        MonthPicker1.YearEnd = Date.Today.Year
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim querystring As NameValueCollection = Request.QueryString

        If (Not (Request.IsAuthenticated)) Then
            Response.Redirect("~/login.aspx")
        End If

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

    Protected Sub deleteBudget(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkBtn As ImageButton = CType(sender, ImageButton)
        If lnkBtn.CommandArgument <> "" Then
            Dim result As Integer
            Try
                result = wcfProxy.DeleteBudget(CType(lnkBtn.CommandArgument, Int64))
                ExpensePaginator = DirectCast(Session("Budgets.ExpensePaginator"), BudgeteerService.Paginator)
                Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) & "?ExpensePage=" & Paginator1.CurrentPageNumber() & "&IncomePage=" & Paginator2.CurrentPageNumber())
            Catch ex As Exception
                ' Catch error.... and do something
            End Try
        End If
    End Sub

    Protected Function getCategorySubcategoryDescription(ByVal subcategoryID As Int64) As String
        If Subcategories IsNot Nothing Then
            For Each subcategory As BudgeteerService.Subcategory In Subcategories
                If subcategory.SubcategoryID = subcategoryID Then
                    Return getCategoryDescription(subcategory.CategoryID) & " - " & subcategory.Description
                End If
            Next
        End If
        Return subcategoryID.ToString()
    End Function

    Protected Function getSubcategories() As List(Of BudgeteerService.Subcategory)
        Dim Subcategories As List(Of BudgeteerService.Subcategory) = New List(Of BudgeteerService.Subcategory)
        Try
            Subcategories = wcfProxy.GetSubcategories()
        Catch ex As Exception
            ' Catch error.... and do something
        End Try
        Return Subcategories
    End Function

    Private Function getCategoryDescription(ByVal CategoryID As Int64) As String
        If Categories IsNot Nothing Then
            For Each category As BudgeteerService.Category In Categories
                If category.CategoryID = CategoryID Then
                    Return category.Description
                End If
            Next
        End If
        Return String.Empty
    End Function

    Private Function getCategories() As List(Of BudgeteerService.Category)
        Dim Categories As List(Of BudgeteerService.Category) = New List(Of BudgeteerService.Category)
        Try
            Categories = wcfProxy.GetCategories()
        Catch ex As Exception
            ' Catch error.... and do something
        End Try
        Return Categories
    End Function

    Protected Function getFrequency(ByVal FrequencyID As Int64) As String
        If Frequencies IsNot Nothing Then
            For Each frequency As BudgeteerService.Frequency In Frequencies
                If frequency.FrequencyID = FrequencyID Then
                    Return frequency.Description
                End If
            Next
        End If
        Return String.Empty
    End Function

    Private Function getFrequencies() As List(Of BudgeteerService.Frequency)
        Dim Frequencies As List(Of BudgeteerService.Frequency) = New List(Of BudgeteerService.Frequency)
        Try
            Frequencies = wcfProxy.GetFrequencies()
        Catch ex As Exception
            ' Catch error.... and do something
        End Try
        Return Frequencies
    End Function

    Protected Function getCategoryUtilizedDollars(ByVal categoryID As Int64, ByVal budgetType As String) As Decimal
        Select Case budgetType
            Case "Expense"
                Return -CType(catExpenseDollars(categoryID.ToString), Decimal)
            Case "Income"
                Return CType(catIncomeDollars(categoryID.ToString), Decimal)
        End Select
    End Function

    Protected Function getSubcategoryUtilizedDollars(ByVal subcategoryID As Int64, ByVal budgetType As String) As Decimal
        Select Case budgetType
            Case "Expense"
                Return -CType(subcatExpenseDollars(subcategoryID.ToString), Decimal)
            Case "Income"
                Return CType(subcatIncomeDollars(subcategoryID.ToString), Decimal)
        End Select
    End Function

    Protected Sub storeUtilizedExpenseDollars(ByVal startDate As Date, ByRef catDollars As NameValueCollection, ByRef subcatDollars As NameValueCollection)
        Dim categoryDollars As Decimal
        Dim subcategoryDollars As Decimal
        catDollars.Clear()
        subcatDollars.Clear()
        If UtilizedExpenseDollars IsNot Nothing Then
            For Each dollars As BudgeteerService.UtilizedDollars In UtilizedExpenseDollars
                categoryDollars = Nothing
                subcategoryDollars = Nothing
                If (dollars.BudgetDate = startDate) Then
                    If catDollars(dollars.CategoryID.ToString) IsNot Nothing Then
                        catDollars(dollars.CategoryID.ToString) = (CType(catDollars(dollars.CategoryID.ToString), Decimal) + dollars.Amount).ToString
                    Else
                        catDollars.Add(dollars.CategoryID.ToString, dollars.Amount.ToString)
                    End If
                    If subcatDollars(dollars.SubcategoryID.ToString) IsNot Nothing Then
                        subcatDollars(dollars.SubcategoryID.ToString) = (CType(subcatDollars(dollars.SubcategoryID.ToString), Decimal) + dollars.Amount).ToString()
                    Else
                        subcatDollars.Add(dollars.SubcategoryID.ToString, dollars.Amount.ToString)
                    End If
                End If
            Next
        End If
    End Sub

    Protected Sub storeUtilizedIncomeDollars(ByVal startDate As Date, ByRef catDollars As NameValueCollection, ByRef subcatDollars As NameValueCollection)
        Dim categoryDollars As Decimal
        Dim subcategoryDollars As Decimal
        catDollars.Clear()
        subcatDollars.Clear()
        If UtilizedIncomeDollars IsNot Nothing Then
            For Each dollars As BudgeteerService.UtilizedDollars In UtilizedIncomeDollars
                categoryDollars = Nothing
                subcategoryDollars = Nothing
                If (dollars.BudgetDate = startDate) Then
                    If catDollars(dollars.CategoryID.ToString) IsNot Nothing Then
                        catDollars(dollars.CategoryID.ToString) = (CType(catDollars(dollars.CategoryID.ToString), Decimal) + dollars.Amount).ToString
                    Else
                        catDollars.Add(dollars.CategoryID.ToString, dollars.Amount.ToString)
                    End If
                    If subcatDollars(dollars.SubcategoryID.ToString) IsNot Nothing Then
                        subcatDollars(dollars.SubcategoryID.ToString) = (CType(subcatDollars(dollars.SubcategoryID.ToString), Decimal) + dollars.Amount).ToString()
                    Else
                        subcatDollars.Add(dollars.SubcategoryID.ToString, dollars.Amount.ToString)
                    End If
                End If
            Next
        End If
    End Sub

    Protected Sub btnMonthPicker_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnMonthPicker.Click
        budgetDate = MonthPicker1.StartDate
        Session("Budgets.BudgetMonth") = budgetDate
        ExpensePaginator.PageNumber = 1
        IncomePaginator.PageNumber = 1
        RetrieveData()
    End Sub

    Private Sub RetrieveData()

        ExpenseBudgets = wcfProxy.GetBudgetsWithCriteria(budgetDate, Nothing, Nothing, "Expense", ExpensePaginator)
        IncomeBudgets = wcfProxy.GetBudgetsWithCriteria(budgetDate, Nothing, Nothing, "Income", IncomePaginator)

        UtilizedExpenseDollars = wcfProxy.GetUtilizedDollarsWithCriteria(budgetDate, Nothing, Nothing, "Expense", ExpensePaginator)
        UtilizedIncomeDollars = wcfProxy.GetUtilizedDollarsWithCriteria(budgetDate, Nothing, Nothing, "Income", IncomePaginator)

        storeUtilizedExpenseDollars(budgetDate, catExpenseDollars, subcatExpenseDollars)
        storeUtilizedIncomeDollars(budgetDate, catIncomeDollars, subcatIncomeDollars)

        Categories = getCategories()
        Subcategories = getSubcategories()
        Frequencies = getFrequencies()

        Repeater1.DataSource = ExpenseBudgets
        Repeater1.DataBind()

        Repeater2.DataSource = IncomeBudgets
        Repeater2.DataBind()

        InitializePaginators()

    End Sub

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

End Class

