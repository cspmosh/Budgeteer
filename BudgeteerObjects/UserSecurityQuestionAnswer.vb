Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract()> _
Public Class UserSecurityQuestionAnswer

    Public Class MaxFieldLength
        Public Const UserID As Integer = 30
        Public Const Answer As Integer = 50
    End Class

    Private m_user_id As String
    Private m_security_question_id As Int64
    Private m_answer As String

    <DataMember()> _
    Public Property UserID() As String
        Get
            Return m_user_id
        End Get
        Set(ByVal value As String)
            m_user_id = value
        End Set
    End Property

    <DataMember()> _
    Public Property SecurityQuestionID() As Int64
        Get
            Return m_security_question_id
        End Get
        Set(ByVal value As Int64)
            m_security_question_id = value
        End Set
    End Property

    <DataMember()> _
    Public Property Answer() As String
        Get
            Return m_answer
        End Get
        Set(ByVal value As String)
            m_answer = value
        End Set
    End Property

End Class
