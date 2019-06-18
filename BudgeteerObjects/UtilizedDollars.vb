Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract()> _
Public Class UtilizedDollars

    Private m_date As Date?
    Private m_categoryID As Int64
    Private m_subcategoryID As Int64
    Private m_amount As Decimal
    Private m_budget As Decimal
    Private m_available As Decimal

    <DataMember()> _
    Public Property BudgetDate() As Date?
        Get
            Return m_date
        End Get
        Set(ByVal value As Date?)
            m_date = value
        End Set
    End Property

    <DataMember()> _
    Public Property CategoryID() As Int64
        Get
            Return m_categoryID
        End Get
        Set(ByVal value As Int64)
            m_categoryID = value
        End Set
    End Property

    <DataMember()> _
    Public Property SubcategoryID() As Int64
        Get
            Return m_subcategoryID
        End Get
        Set(ByVal value As Int64)
            m_subcategoryID = value
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
    Public Property Budget() As Decimal
        Get
            Return m_budget
        End Get
        Set(ByVal value As Decimal)
            m_budget = value
        End Set
    End Property

    <DataMember()> _
    Public Property Available() As Decimal
        Get
            Return m_available
        End Get
        Set(ByVal value As Decimal)
            m_available = value
        End Set
    End Property

End Class
