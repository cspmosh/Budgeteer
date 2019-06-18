Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract()> _
Public Class BankAccount

    Public Class MaxFieldLength
        Public Const Name As Integer = 50
        Public Const Number As Integer = 4
        Public Const Type As Integer = 20
        Public Const UserID As Integer = 15
    End Class

    Private m_account_id As Int64
    Private m_number As String
    Private m_name As String
    Private m_type As String
    Private m_balance As Decimal
    Private m_active As Boolean
    Private m_user_id As String

    <DataMember()> _
    Public Property AccountID() As Int64
        Get
            Return m_account_id
        End Get
        Set(ByVal value As Int64)
            m_account_id = value
        End Set
    End Property

    <DataMember()> _
    Public Property Number() As String
        Get
            Return m_number
        End Get
        Set(ByVal value As String)
            m_number = value
        End Set
    End Property

    <DataMember()> _
    Public Property Name() As String
        Get
            Return m_name
        End Get
        Set(ByVal value As String)
            m_name = value
        End Set
    End Property

    <DataMember()> _
    Public Property Type() As String
        Get
            Return m_type
        End Get
        Set(ByVal value As String)
            m_type = value
        End Set
    End Property

    <DataMember()> _
    Public Property Balance() As Decimal
        Get
            Return m_balance
        End Get
        Set(ByVal value As Decimal)
            m_balance = value
        End Set
    End Property

    <DataMember()> _
    Public Property Active() As Boolean
        Get
            Return m_active
        End Get
        Set(ByVal value As Boolean)
            m_active = value
        End Set
    End Property

    <DataMember()> _
    Public Property UserID() As String
        Get
            Return m_user_id
        End Get
        Set(ByVal value As String)
            m_user_id = value
        End Set
    End Property

End Class
