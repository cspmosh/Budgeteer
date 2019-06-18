Imports System.ServiceModel

Partial Public Class Index2
    Inherits System.Web.UI.Page

    Const ReallyRidiculousNumber As Decimal = 9999999999

    Dim Paginator As BudgeteerService.Paginator = New BudgeteerService.Paginator
    Dim SearchCriteria As BudgeteerService.TransactionFilterCriteria = New BudgeteerService.TransactionFilterCriteria
    Dim Categories As List(Of BudgeteerService.Category) = New List(Of BudgeteerService.Category)
    Dim Subcategories As List(Of BudgeteerService.Subcategory) = New List(Of BudgeteerService.Subcategory)
    Dim BankAccounts As List(Of BudgeteerService.BankAccount) = New List(Of BudgeteerService.BankAccount)
    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient
    Dim Transactions As List(Of BudgeteerService.Transaction) = New List(Of BudgeteerService.Transaction)
    Dim firstTransactionYear As Integer

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
        'Set the page size here
        Paginator.PageSize = 20
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim querystring As NameValueCollection = Request.QueryString

        If (Not (Request.IsAuthenticated)) Then
            Response.Redirect("~/login.aspx")
        End If

        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    firstTransactionYear = wcfProxy.GetFirstTransactionYear()
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

        If firstTransactionYear = Nothing Then
            firstTransactionYear = Date.Today.Year
        End If

        Categories = getCategories()
        Subcategories = getSubcategories()
        BankAccounts = getBankAccounts()

        If Not Page.IsPostBack Then
            InitializeFilters()
            If querystring.HasKeys Then
                Dim page As Integer = querystring.Get("Page")
                If page = Nothing Then
                    Paginator.PageNumber = 1
                Else
                    If page < 1 Then
                        page = 1
                    End If
                    Paginator.PageNumber = page
                End If
            Else
                Paginator.PageNumber = 1
            End If
            ApplyFilters()
            RetrieveData()
        End If

    End Sub

    Protected Sub deleteTransaction(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkBtn As ImageButton = CType(sender, ImageButton)
        If lnkBtn.CommandArgument <> "" Then
            Dim result As Integer
            If wcfProxy IsNot Nothing Then
                If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                    wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                    Try
                        result = wcfProxy.DeleteTransaction(CType(lnkBtn.CommandArgument, Int64))
                        Paginator = DirectCast(Session("Transactions.Paginator"), BudgeteerService.Paginator)
                        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) & "?Page=" & Paginator1.CurrentPageNumber())
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

    Protected Sub Paginator1_PageClicked(ByVal sender As Object, ByVal e As PaginatorEventArgs)
        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) & "?Page=" & e.page)
    End Sub

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

    Protected Function getSubcategory(ByVal subcategoryID As Int64) As String
        If Subcategories IsNot Nothing Then
            For Each subcategory As BudgeteerService.Subcategory In Subcategories
                If subcategory.SubcategoryID = subcategoryID Then
                    Return getCategory(subcategory.CategoryID) & " - " & subcategory.Description
                End If
            Next
        End If
        Return ""
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

    Private Function getCategory(ByVal CategoryID As Int64) As String
        If Categories IsNot Nothing Then
            For Each category As BudgeteerService.Category In Categories
                If category.CategoryID = CategoryID Then
                    Return category.Description
                End If
            Next
        End If
        Return String.Empty
    End Function

    Protected Function getBankAccounts() As List(Of BudgeteerService.BankAccount)
        Dim BankAccounts As List(Of BudgeteerService.BankAccount) = New List(Of BudgeteerService.BankAccount)
        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    BankAccounts = wcfProxy.GetBankAccounts()
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
        Return BankAccounts
    End Function

    Protected Function getBankAccount(ByVal accoundID As Int64) As String
        If BankAccounts IsNot Nothing Then
            For Each account As BudgeteerService.BankAccount In BankAccounts
                If account.AccountID = accoundID Then
                    If account.Number = Nothing Then
                        Return account.Name
                    Else
                        Return account.Name & " (" & account.Number & ")"
                    End If
                End If
            Next
        End If
        Return ""
    End Function

    Private Sub RetrieveData()
        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    Transactions = wcfProxy.GetTransactionsWithCriteria(SearchCriteria, Paginator)
                    Repeater1.DataSource = Transactions
                    Repeater1.DataBind()
                    InitializePaginator()
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

    Private Sub InitializePaginator()
        Session("Transactions.Paginator") = Paginator
        If Paginator.LastPage > 0 Then
            Paginator1.CurrentPageNumber = Paginator.PageNumber
            Paginator1.TotalPageCount = Paginator.LastPage
        Else
            Paginator1.CurrentPageNumber = 0
            Paginator1.TotalPageCount = 0
        End If
        Paginator1.PageCounterText = Paginator1.CurrentPageNumber.ToString & " of " & Paginator1.TotalPageCount.ToString
    End Sub

    Private Sub InitializeFilters()
        ' Initialize month drop down list
        ddlMonth.Items.Add(New ListItem("", 0))
        ddlMonth.Items.Add(New ListItem("January", 1))
        ddlMonth.Items.Add(New ListItem("February", 2))
        ddlMonth.Items.Add(New ListItem("March", 3))
        ddlMonth.Items.Add(New ListItem("April", 4))
        ddlMonth.Items.Add(New ListItem("May", 5))
        ddlMonth.Items.Add(New ListItem("June", 6))
        ddlMonth.Items.Add(New ListItem("July", 7))
        ddlMonth.Items.Add(New ListItem("August", 8))
        ddlMonth.Items.Add(New ListItem("September", 9))
        ddlMonth.Items.Add(New ListItem("October", 10))
        ddlMonth.Items.Add(New ListItem("November", 11))
        ddlMonth.Items.Add(New ListItem("December", 12))

        ' Initialize day drop down list
        ddlDay.Items.Add(New ListItem("", 0))
        ddlDay.Items.Add(New ListItem("1", 1))
        ddlDay.Items.Add(New ListItem("2", 2))
        ddlDay.Items.Add(New ListItem("3", 3))
        ddlDay.Items.Add(New ListItem("4", 4))
        ddlDay.Items.Add(New ListItem("5", 5))
        ddlDay.Items.Add(New ListItem("6", 6))
        ddlDay.Items.Add(New ListItem("7", 7))
        ddlDay.Items.Add(New ListItem("8", 8))
        ddlDay.Items.Add(New ListItem("9", 9))
        ddlDay.Items.Add(New ListItem("10", 10))
        ddlDay.Items.Add(New ListItem("11", 11))
        ddlDay.Items.Add(New ListItem("12", 12))
        ddlDay.Items.Add(New ListItem("13", 13))
        ddlDay.Items.Add(New ListItem("14", 14))
        ddlDay.Items.Add(New ListItem("15", 15))
        ddlDay.Items.Add(New ListItem("16", 16))
        ddlDay.Items.Add(New ListItem("17", 17))
        ddlDay.Items.Add(New ListItem("18", 18))
        ddlDay.Items.Add(New ListItem("19", 19))
        ddlDay.Items.Add(New ListItem("20", 20))
        ddlDay.Items.Add(New ListItem("21", 21))
        ddlDay.Items.Add(New ListItem("22", 22))
        ddlDay.Items.Add(New ListItem("23", 23))
        ddlDay.Items.Add(New ListItem("24", 24))
        ddlDay.Items.Add(New ListItem("25", 25))
        ddlDay.Items.Add(New ListItem("26", 26))
        ddlDay.Items.Add(New ListItem("27", 27))
        ddlDay.Items.Add(New ListItem("28", 28))
        ddlDay.Items.Add(New ListItem("29", 29))
        ddlDay.Items.Add(New ListItem("30", 30))
        ddlDay.Items.Add(New ListItem("31", 31))

        ' Initialize year drop down list
        ddlYear.Items.Add(New ListItem("", 0))
        For yr As Integer = Date.Today.Year To firstTransactionYear Step -1
            ddlYear.Items.Add(New ListItem(yr.ToString, yr))
        Next

        'Initialize category/subcategory drop down list
        ddlCatSubcat.Items.Add(New ListItem("", 0))

        If Categories IsNot Nothing Then
            For Each category As BudgeteerService.Category In Categories
                If Subcategories IsNot Nothing Then
                    For Each subcategory As BudgeteerService.Subcategory In Subcategories
                        If subcategory.CategoryID = category.CategoryID Then
                            ddlCatSubcat.Items.Add(New ListItem(category.Description & " - " & subcategory.Description, subcategory.SubcategoryID))
                        End If
                    Next
                End If
            Next
        End If

        ' Initialize bank account drop down list
        ddlBankAccount.Items.Add(New ListItem("", 0))
        If BankAccounts IsNot Nothing Then
            For Each bankaccount As BudgeteerService.BankAccount In BankAccounts
                If bankaccount.Number <> Nothing Then
                    ddlBankAccount.Items.Add(New ListItem(bankaccount.Name & " (" & bankaccount.Number & ")", bankaccount.AccountID))
                Else
                    ddlBankAccount.Items.Add(New ListItem(bankaccount.Name, bankaccount.AccountID))
                End If
            Next
        End If

        ' The first time this page is initilized, set the year and month to todays year and month
        If Session("Transactions.FirstInitialize") = Nothing Then
            ddlYear.SelectedValue = Date.Today.Year
            ddlMonth.SelectedValue = Date.Today.Month
        Else
            ddlYear.SelectedValue = Session("Transactions.Filter.Year")
            ddlMonth.SelectedValue = Session("Transactions.Filter.Month")
        End If
        ddlDay.SelectedValue = Session("Transactions.Filter.Day")
        txtDescription.Text = Session("Transactions.Filter.Description")
        txtAmount.Text = Session("Transactions.Filter.Amount")
        ddlCatSubcat.SelectedValue = Session("Transactions.Filter.CatSubcat")
        ddlBankAccount.SelectedValue = Session("Transactions.Filter.BankAccount")
        ' Used to prevent year and month from being reset to todays date on every page refresh
        Session("Transactions.FirstInitialize") = "No"

    End Sub

    Protected Sub ApplyFilters()
        ' Populate Search Criteria Object with filter items
        SearchCriteria.TransactionYear = ddlYear.SelectedValue
        SearchCriteria.TransactionMonth = ddlMonth.SelectedValue
        SearchCriteria.TransactionDay = ddlDay.SelectedValue
        SearchCriteria.Description = txtDescription.Text
        If String.IsNullOrEmpty(Trim(txtAmount.Text)) Then
            SearchCriteria.Amount = Nothing
        Else
            Try
                SearchCriteria.Amount = CType(txtAmount.Text, Decimal)
            Catch ex As Exception
                SearchCriteria.Amount = ReallyRidiculousNumber
            End Try
        End If
        SearchCriteria.SubcategoryID = ddlCatSubcat.SelectedValue
        SearchCriteria.AccountID = ddlBankAccount.SelectedValue

        ' Save the search criteria in session 
        Session("Transactions.Filter.Year") = SearchCriteria.TransactionYear
        Session("Transactions.Filter.Month") = SearchCriteria.TransactionMonth
        Session("Transactions.Filter.Day") = SearchCriteria.TransactionDay
        Session("Transactions.Filter.Description") = SearchCriteria.Description
        If SearchCriteria.Amount = Nothing Then
            Session("Transactions.Filter.Amount") = ""
        Else
            If SearchCriteria.Amount = ReallyRidiculousNumber Then
                Session("Transactions.Filter.Amount") = ""
            Else
                Session("Transactions.Filter.Amount") = SearchCriteria.Amount.ToString
            End If
        End If
        Session("Transactions.Filter.CatSubcat") = SearchCriteria.SubcategoryID
        Session("Transactions.Filter.BankAccount") = SearchCriteria.AccountID
    End Sub

    Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFilter.Click
        Paginator.PageNumber = 1 'reset paging
        ApplyFilters()
        RetrieveData()
    End Sub

    Protected Sub clearFilter()
        ddlYear.SelectedIndex = 0
        ddlMonth.SelectedIndex = 0
        ddlDay.SelectedIndex = 0
        txtDescription.Text = ""
        txtAmount.Text = ""
        ddlCatSubcat.SelectedIndex = 0
        ddlBankAccount.SelectedIndex = 0
    End Sub

    Private Sub ShowError(ByVal msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

End Class