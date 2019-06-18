Public Class Registration

    Public Class MaxFieldLength
        Public Const UserID As Integer = 30
        Public Const EmailAddress As Integer = 50
        Public Const RegistrationKey As Integer = 36
    End Class

    Private m_registration_key As String
    Private m_user_iD As String
    Private m_email_address As String

    Public Property RegistrationKey() As String
        Get
            Return m_registration_key
        End Get
        Set(ByVal value As String)
            m_registration_key = value
        End Set
    End Property

    Public Property UserID() As String
        Get
            Return m_user_iD
        End Get
        Set(ByVal value As String)
            m_user_iD = value
        End Set
    End Property

    Public Property EmailAddress() As String
        Get
            Return m_email_address
        End Get
        Set(ByVal value As String)
            m_email_address = value
        End Set
    End Property

End Class
