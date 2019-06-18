Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract()> _
Public Class Budget

    Public Class MaxFieldLength
        Public Const UserID As Integer = 15
    End Class

    Private m_budget_id As Int64
    Private m_subcategory_id As Int64
    Private m_budget_amount As Decimal
    Private m_start_date As Date?
    Private m_user_id As String

    <DataMember()> _
    Public Property BudgetID() As Int64
        Get
            Return m_budget_id
        End Get
        Set(ByVal value As Int64)
            m_budget_id = value
        End Set
    End Property

    <DataMember()> _
    Public Property StartDate() As Date?
        Get
            Return m_start_date
        End Get
        Set(ByVal value As Date?)
            m_start_date = value
        End Set
    End Property

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
    Public Property Amount() As Decimal
        Get
            Return m_budget_amount
        End Get
        Set(ByVal value As Decimal)
            m_budget_amount = value
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
