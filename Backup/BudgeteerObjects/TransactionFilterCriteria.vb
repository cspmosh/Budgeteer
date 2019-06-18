Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract()> _
Public Class TransactionFilterCriteria
    Public Class MaxFieldLength
        Public Const Description As Integer = 255
    End Class

    Private m_transaction_id As Int64
    Private m_year As Integer
    Private m_month As Integer
    Private m_day As Integer
    Private m_check_number As Integer
    Private m_description As String
    Private m_amount As Decimal
    Private m_tax_amount As Decimal
    Private m_subcategory_id As Int64
    Private m_account_id As Int64
    Private m_user_id As String

    <DataMember()> _
    Public Property TransactionID() As Int64
        Get
            Return m_transaction_id
        End Get
        Set(ByVal value As Int64)
            m_transaction_id = value
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
    Public Property Amount() As Decimal
        Get
            Return m_amount
        End Get
        Set(ByVal value As Decimal)
            m_amount = value
        End Set
    End Property

    <DataMember()> _
    Public Property TaxAmount() As Decimal
        Get
            Return m_tax_amount
        End Get
        Set(ByVal value As Decimal)
            m_tax_amount = value
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
    Public Property CheckNumber() As Integer
        Get
            Return m_check_number
        End Get
        Set(ByVal value As Integer)
            m_check_number = value
        End Set
    End Property

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
    Public Property TransactionYear() As Integer
        Get
            Return m_year
        End Get
        Set(ByVal value As Integer)
            m_year = value
        End Set
    End Property

    <DataMember()> _
    Public Property TransactionMonth() As Integer
        Get
            Return m_month
        End Get
        Set(ByVal value As Integer)
            m_month = value
        End Set
    End Property

    <DataMember()> _
    Public Property TransactionDay() As Integer
        Get
            Return m_day
        End Get
        Set(ByVal value As Integer)
            m_day = value
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
