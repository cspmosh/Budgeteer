Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract()> _
Public Class Category

    Public Class MaxFieldLength
        Public Const Description As Integer = 50
    End Class

    Private m_category_id As Int64
    Private m_description As String
    Private m_active As Boolean
    Private m_user_id As String

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
