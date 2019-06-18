Imports System.ServiceModel
Imports System.Runtime.Serialization


<DataContract()> _
Public Class ApplicationSetting

    Public Class MaxFieldLength
        Public Const Setting As Integer = 30
        Public Const Value As Integer = 30
        Public Const UserID As Integer = 15
    End Class

    Private m_setting As String
    Private m_value As String
    Private m_user_id As String

    <DataMember()> _
    Public Property Setting() As String
        Get
            Return m_setting
        End Get
        Set(ByVal value As String)
            m_setting = value
        End Set
    End Property

    <DataMember()> _
    Public Property Value() As String
        Get
            Return m_value
        End Get
        Set(ByVal value As String)
            m_value = value
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
