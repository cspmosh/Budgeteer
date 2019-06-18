Imports System.ComponentModel

<DefaultEvent("PageClicked")> _
<ToolboxData("<{0}:Paginator runat=""server""> </{0}:Paginator>")> _
Partial Public Class Paginator
    Inherits System.Web.UI.UserControl

    <Browsable(True)> _
    <Category("Data")> _
    Public Event PageClicked As PaginatorEventHandler

    <Category("Data")> _
    Public Property CurrentPageNumber() As Integer
        Get
            Return CType(ViewState("Paginator.PageNumber"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("Paginator.PageNumber") = value
        End Set
    End Property

    <Category("Data")> _
    Public Property TotalPageCount() As Integer
        Get
            Return CType(ViewState("Paginator.TotalPages"), Integer)
        End Get
        Set(ByVal value As Integer)
            ViewState("Paginator.TotalPages") = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property CssClass() As String
        Get
            Return paginator1.CssClass
        End Get
        Set(ByVal value As String)
            paginator1.CssClass = value
        End Set
    End Property

    <Category("Layout")> _
    Public Property HorizontalAlign() As HorizontalAlign
        Get
            Return innerTable.HorizontalAlign
        End Get
        Set(ByVal value As HorizontalAlign)
            innerTable.HorizontalAlign = value
        End Set
    End Property

    <Category("Layout")> _
    Public Property Width() As Unit
        Get
            Return paginator1.Width
        End Get
        Set(ByVal value As Unit)
            paginator1.Width = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property FirstPageText() As String
        Get
            Return btnFirst.Text
        End Get
        Set(ByVal value As String)
            btnFirst.Text = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property FirstPageImage() As String
        Get
            Return imgFirst.ImageUrl
        End Get
        Set(ByVal value As String)
            imgFirst.ImageUrl = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property PreviousPageText() As String
        Get
            Return btnPrevious.Text
        End Get
        Set(ByVal value As String)
            btnPrevious.Text = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property PreviousPageImage() As String
        Get
            Return imgPrevious.ImageUrl
        End Get
        Set(ByVal value As String)
            imgPrevious.ImageUrl = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property NextPageText() As String
        Get
            Return btnNext.Text
        End Get
        Set(ByVal value As String)
            btnNext.Text = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property NextPageImage() As String
        Get
            Return imgNext.ImageUrl
        End Get
        Set(ByVal value As String)
            imgNext.ImageUrl = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property LastPageText() As String
        Get
            Return btnLast.Text
        End Get
        Set(ByVal value As String)
            btnLast.Text = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property LastPageImage() As String
        Get
            Return imgLast.ImageUrl
        End Get
        Set(ByVal value As String)
            imgLast.ImageUrl = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property PageCounterCssClass() As String
        Get
            Return pageCounter.CssClass
        End Get
        Set(ByVal value As String)
            pageCounter.CssClass = value
        End Set
    End Property

    <Category("Appearance")> _
    Public Property PageCounterText() As String
        Get
            Return Label1.Text
        End Get
        Set(ByVal value As String)
            Label1.Text = value
        End Set
    End Property

    Protected Sub nextPage()
        If CurrentPageNumber() < TotalPageCount() Then
            CurrentPageNumber() += 1
        End If
        RaiseEvent PageClicked(Me, New PaginatorEventArgs(CurrentPageNumber()))
    End Sub

    Protected Sub LastPage()
        CurrentPageNumber() = TotalPageCount()
        RaiseEvent PageClicked(Me, New PaginatorEventArgs(CurrentPageNumber()))
    End Sub

    Protected Sub FirstPage()
        CurrentPageNumber() = 1
        RaiseEvent PageClicked(Me, New PaginatorEventArgs(CurrentPageNumber()))
    End Sub

    Protected Sub PreviousPage()
        If CurrentPageNumber > 1 Then
            CurrentPageNumber -= 1
        End If
        RaiseEvent PageClicked(Me, New PaginatorEventArgs(CurrentPageNumber()))
    End Sub

End Class

Public Delegate Sub PaginatorEventHandler(ByVal sender As Object, ByVal e As PaginatorEventArgs)

Public Class PaginatorEventArgs
    Inherits EventArgs
    Private _page As Integer
    Public Sub New(ByVal page As Integer)
        Me._page = page
    End Sub
    Public ReadOnly Property page() As Integer
        Get
            Return _page
        End Get
    End Property
End Class