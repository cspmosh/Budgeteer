Imports System.ServiceModel

Partial Public Class Index
    Inherits System.Web.UI.Page

    Dim wcfProxy As BudgeteerService.BudgeteerClient = New BudgeteerService.BudgeteerClient
    Dim Paginator As BudgeteerService.Paginator = New BudgeteerService.Paginator
    Dim Transactions As List(Of BudgeteerService.Transaction) = New List(Of BudgeteerService.Transaction)
    Dim BankAccounts As List(Of BudgeteerService.BankAccount) = New List(Of BudgeteerService.BankAccount)
    Public balanceTotal As Decimal = Nothing

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim querystring As NameValueCollection = Request.QueryString
        Paginator.PageSize = 30
        wcfProxy = DirectCast(Session("Budgeteer.ClientProxy"), BudgeteerService.BudgeteerClient)
        If (Not (Request.IsAuthenticated)) Then
            Response.Redirect("~/login.aspx")
        End If
        If Not Page.IsPostBack Then
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
            RetrieveData()
        End If
    End Sub

    Protected Sub deleteAccount(ByVal sender As Object, ByVal e As EventArgs)
        Dim lnkBtn As ImageButton = CType(sender, ImageButton)
        If lnkBtn.CommandArgument <> "" Then
            If wcfProxy IsNot Nothing Then
                If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                    wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                    Try
                        wcfProxy.DeleteBankAccount(CType(lnkBtn.CommandArgument, Int64))
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
        End If
    End Sub

    Private Sub RetrieveData()

        If wcfProxy IsNot Nothing Then
            If wcfProxy.State <> ServiceModel.CommunicationState.Faulted And _
                wcfProxy.State <> ServiceModel.CommunicationState.Closed Then
                Try
                    BankAccounts = wcfProxy.GetPagedBankAccounts(balanceTotal, Paginator)
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

        accountRepeater.DataSource = BankAccounts
        accountRepeater.DataBind()

        InitializePaginators()

    End Sub

    Private Sub InitializePaginators()
        Session("BankAccounts.Paginator") = Paginator
        If Paginator.LastPage > 0 Then
            Paginator1.CurrentPageNumber = Paginator.PageNumber
            Paginator1.TotalPageCount = Paginator.LastPage
        Else
            Paginator1.CurrentPageNumber = 1
            Paginator1.TotalPageCount = 1
        End If
        Paginator1.PageCounterText = Paginator1.CurrentPageNumber.ToString & " of " & Paginator1.TotalPageCount.ToString
    End Sub

    Protected Sub Paginator1_PageClicked(ByVal sender As Object, ByVal e As PaginatorEventArgs)
        Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) & "?Page=" & e.page)
    End Sub

    Private Sub ShowError(ByVal msg As String)
        lblError.Text = msg
        lblError.Visible = True
    End Sub

End Class