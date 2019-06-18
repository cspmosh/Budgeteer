Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract()> _
Public Class Subcategory

    Public Class MaxFieldLength
        Public Const Type As Integer = 10
        Public Const Description As Integer = 50
        Public Const Notes As Integer = 255
    End Class

    Private m_subcategory_id As Int64
    Private m_category_id As Int64
    Private m_description As String
    Private m_type As String
    Private m_sinking_fund As Boolean
    Private m_balance As Decimal
    Private m_notes As String
    Private m_active As Boolean
    Private m_user_id As String

    <DataMember()> _
    Public Property SubcategoryID() As Int64
        Get
            Return m_subcategory_id
        End Get
        Set(ByVal value As Int64)
            m_subcategory_id = value
        End Set
    End Property

    <DataMember()> _
    Public Property CategoryID() As Int64
        Get
            Return m_category_id
        End Get
        Set(ByVal value As Int64)
            m_category_id = value
        End Set
    End Property

    <DataMember()> _
    Public Property Description() As String
        Get
            Return m_description
        End Get
        Set(ByVal value As String)
            m_description = value
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
    Public Property SinkingFund() As Boolean
        Get
            Return m_sinking_fund
        End Get
        Set(ByVal value As Boolean)
            m_sinking_fund = value
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
    Public Property Notes() As String
        Get
            Return m_notes
        End Get
        Set(ByVal value As String)
            m_notes = value
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
