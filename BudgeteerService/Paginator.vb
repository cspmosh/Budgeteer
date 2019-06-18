Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract()> _
Public Class Paginator

    Private m_pageSize As Integer
    Private m_pageNumber As Integer
    <DataMember(Name:="FirstPage")> _
    Private m_firstPage As Integer = 1
    <DataMember(Name:="LastPage")> _
    Private m_lastPage As Integer

    <DataMember()> _
    Public Property PageNumber() As Integer
        Get
            Return m_pageNumber
        End Get
        Set(ByVal value As Integer)
            m_pageNumber = value
        End Set
    End Property

    <DataMember()> _
    Public Property PageSize() As Integer
        Get
            Return m_pageSize
        End Get
        Set(ByVal value As Integer)
            m_pageSize = value
        End Set
    End Property

    Public ReadOnly Property FirstPage() As Integer
        Get
            Return m_firstPage
        End Get
    End Property

    Public ReadOnly Property LastPage() As Integer
        Get
            Return m_lastPage
        End Get
    End Property

    Public Sub SetLastPage(ByVal LastPage As Integer)
        m_lastPage = LastPage
    End Sub

    Public Sub SetFirstPage(ByVal PageNo As Integer)
        m_firstPage = PageNo
    End Sub

End Class
