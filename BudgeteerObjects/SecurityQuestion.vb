Imports System.ServiceModel
Imports System.Runtime.Serialization

<DataContract()> _
Public Class SecurityQuestion

    Public Class MaxFieldLength
        Public Const Question As Integer = 255
    End Class

    Private m_security_question_id As Int64
    Private m_question As String

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
    Public Property Question() As String
        Get
            Return m_question
        End Get
        Set(ByVal value As String)
            m_question = value
        End Set
    End Property

End Class
