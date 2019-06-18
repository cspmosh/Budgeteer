Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract()> _
Public Class User

    Public Class MaxFieldLength
        Public Const UserID As Integer = 30
        Public Const Password As Integer = 52
        Public Const Salt As Integer = 8
        Public Const FirstName As Integer = 50
        Public Const LastName As Integer = 50
        Public Const EmailAddress As Integer = 50
    End Class

    Private m_userID As String
    Private m_password As String
    Private m_firstName As String
    Private m_lastName As String
    Private m_emailAddress As String
    Private m_active As Boolean
    Private m_salt As String

    <DataMember()> _
    Public Property UserID() As String
        Get
            Return m_userID
        End Get
        Set(ByVal value As String)
            m_userID = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return m_password
        End Get
        Set(ByVal value As String)
            m_password = value
        End Set
    End Property

    Public Property Salt() As String
        Get
            Return m_salt
        End Get
        Set(ByVal value As String)
            m_salt = value
        End Set
    End Property

    <DataMember()> _
    Public Property FirstName() As String
        Get
            Return m_firstName
        End Get
        Set(ByVal value As String)
            m_firstName = value
        End Set
    End Property

    <DataMember()> _
    Public Property LastName() As String
        Get
            Return m_lastName
        End Get
        Set(ByVal value As String)
            m_lastName = value
        End Set
    End Property

    <DataMember()> _
    Public Property EmailAddress() As String
        Get
            Return m_emailAddress
        End Get
        Set(ByVal value As String)
            m_emailAddress = value
        End Set
    End Property

    Public Property Active() As Boolean
        Get
            Return m_active
        End Get
        Set(ByVal value As Boolean)
            m_active = value
        End Set
    End Property

End Class
