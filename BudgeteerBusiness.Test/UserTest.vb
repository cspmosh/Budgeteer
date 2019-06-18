Imports BudgeteerObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports BudgeteerBusiness
Imports System.Collections.Generic

'''<summary>
'''This is a test class for UserTest and is intended
'''to contain all UserTest Unit Tests
'''</summary>
<TestClass()> _
Public Class UserTest

    Private testContextInstance As TestContext

    '''<summary>
    '''Gets or sets the test context which provides
    '''information about and functionality for the current test run.
    '''</summary>
    Public Property TestContext() As TestContext
        Get
            Return testContextInstance
        End Get
        Set(ByVal value As TestContext)
            testContextInstance = value
        End Set
    End Property

#Region "Additional test attributes"
    '
    'You can use the following additional attributes as you write your tests:
    '
    'Use ClassInitialize to run code before running the first test in the class
    '<ClassInitialize()>  _
    'Public Shared Sub MyClassInitialize(ByVal testContext As TestContext)
    'End Sub
    '
    'Use ClassCleanup to run code after all tests in a class have run
    '<ClassCleanup()>  _
    'Public Shared Sub MyClassCleanup()
    'End Sub
    '
    'Use TestInitialize to run code before running each test
    '<TestInitialize()>  _
    'Public Sub MyTestInitialize()
    'End Sub
    '
    'Use TestCleanup to run code after each test has run
    '<TestCleanup()>  _
    'Public Sub MyTestCleanup()
    'End Sub
    '
#End Region

    '''<summary>
    '''A test for addUser
    '''</summary>
    <TestMethod()> _
    Public Sub addUserTest()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User
        Dim questions As List(Of BudgeteerObjects.UserSecurityQuestionAnswer) = New List(Of BudgeteerObjects.UserSecurityQuestionAnswer)
        Dim question1 As BudgeteerObjects.UserSecurityQuestionAnswer = New BudgeteerObjects.UserSecurityQuestionAnswer
        Dim question2 As BudgeteerObjects.UserSecurityQuestionAnswer = New BudgeteerObjects.UserSecurityQuestionAnswer
        Dim question3 As BudgeteerObjects.UserSecurityQuestionAnswer = New BudgeteerObjects.UserSecurityQuestionAnswer
        Dim password As String = ""

        question1.UserID = "JTurner"
        question1.SecurityQuestionID = 1
        question1.Answer = "answer1"

        question2.UserID = "JTurner"
        question2.SecurityQuestionID = 2
        question2.Answer = "answer2"

        question3.UserID = "JTurner"
        question3.SecurityQuestionID = 3
        question3.Answer = "answer3"

        questions.Add(question1)
        questions.Add(question2)
        questions.Add(question3)

        password = "hello"
        user.UserID = "JTurner"
        user.FirstName = "Josh"
        user.LastName = "Turner"
        user.EmailAddress = "cs.pmosh@gmail.com"

        Dim expected As String = ""
        Dim actual As String

        actual = target.addUser(user, password, questions)
        Assert.AreNotEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for addUser
    '''</summary>
    <TestMethod()> _
    Public Sub activateUserTest()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim registration As BudgeteerObjects.Registration = New BudgeteerObjects.Registration

        registration.UserID = "JTurner"
        registration.EmailAddress = "cs.pmosh@gmail.com"
        registration.RegistrationKey = "1d34092e-9ec9-4633-a05a-92895109044f"

        Dim expected As Integer = 1
        Dim actual As Integer

        actual = target.activateUser(registration.UserID, registration.EmailAddress, registration.RegistrationKey)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for addUser
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "User ID cannot be null")> _
    Public Sub addUserUserIDNothing()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

        user.UserID = Nothing
        user.Password = "password"
        user.FirstName = "Josh"
        user.LastName = "Turner"
        user.EmailAddress = user.UserID 'unique password

        'target.addUser(user)

    End Sub

    '''<summary>
    '''A test for addUser
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "User ID exceeded the maximum number of allowable characters")> _
    Public Sub addUserUserIDMaxLen()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

        user.UserID = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        user.Password = "password"
        user.FirstName = "Josh"
        user.LastName = "Turner"
        user.EmailAddress = user.UserID 'unique password

        'target.addUser(user)

    End Sub

    '''<summary>
    '''A test for addUser
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Password cannot be null")> _
    Public Sub addUserPasswordNothing()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

        user.UserID = Right(Trim(Date.UtcNow.ToString & Date.UtcNow.Millisecond.ToString), 15) 'unique user name
        user.Password = Nothing
        user.FirstName = "Josh"
        user.LastName = "Turner"
        user.EmailAddress = user.UserID 'unique password

        'target.addUser(user)

    End Sub

    '''<summary>
    '''A test for addUser
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Password exceeded the maximum number of allowable characters")> _
    Public Sub addUserPasswordMaxLen()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

        user.UserID = Right(Trim(Date.UtcNow.ToString & Date.UtcNow.Millisecond.ToString), 15) 'unique user name
        user.Password = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        user.FirstName = "Josh"
        user.LastName = "Turner"
        user.EmailAddress = user.UserID 'unique password

        'target.addUser(user)

    End Sub

    '''<summary>
    '''A test for addUser
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "First Name cannot be null")> _
    Public Sub addUserFirstNameNothing()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

        user.UserID = Right(Trim(Date.UtcNow.ToString & Date.UtcNow.Millisecond.ToString), 15) 'unique user name
        user.Password = "Password"
        user.FirstName = Nothing
        user.LastName = "Turner"
        user.EmailAddress = user.UserID 'unique password

        'target.addUser(user)

    End Sub

    '''<summary>
    '''A test for addUser
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "First Name exceeded the maximum number of allowable characters")> _
    Public Sub addUserFirstNameMaxLen()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

        user.UserID = Right(Trim(Date.UtcNow.ToString & Date.UtcNow.Millisecond.ToString), 15) 'unique user name
        user.Password = "Password"
        user.FirstName = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        user.LastName = "Turner"
        user.EmailAddress = user.UserID 'unique password

        'target.addUser(user)

    End Sub

    '''<summary>
    '''A test for addUser
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Last Name cannot be null")> _
    Public Sub addUserLastNameNothing()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

        user.UserID = Right(Trim(Date.UtcNow.ToString & Date.UtcNow.Millisecond.ToString), 15) 'unique user name
        user.Password = "Password"
        user.FirstName = "Josh"
        user.LastName = Nothing
        user.EmailAddress = user.UserID 'unique password

        'target.addUser(user)

    End Sub

    '''<summary>
    '''A test for addUser
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Last Name exceeded the maximum number of allowable characters")> _
    Public Sub addUserLastNameMaxLen()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

        user.UserID = Right(Trim(Date.UtcNow.ToString & Date.UtcNow.Millisecond.ToString), 15) 'unique user name
        user.Password = "Password"
        user.FirstName = "Josh"
        user.LastName = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        user.EmailAddress = user.UserID 'unique password

        'target.addUser(user)

    End Sub

    '''<summary>
    '''A test for addUser
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Email Address cannot be null")> _
    Public Sub addUserEmailAddressNothing()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

        user.UserID = Right(Trim(Date.UtcNow.ToString & Date.UtcNow.Millisecond.ToString), 15) 'unique user name
        user.Password = "Password"
        user.FirstName = "Josh"
        user.LastName = "Turner"
        user.EmailAddress = Nothing

        'target.addUser(user)

    End Sub

    '''<summary>
    '''A test for addUser
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Email Address exceeded the maximum number of allowable characters")> _
    Public Sub addUserEmailAddressMaxLen()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User

        user.UserID = Right(Trim(Date.UtcNow.ToString & Date.UtcNow.Millisecond.ToString), 15) 'unique user name
        user.Password = "Password"
        user.FirstName = "Josh"
        user.LastName = "Turner"
        user.EmailAddress = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        'target.addUser(user)

    End Sub

    '''<summary>
    '''A test for updateUserPassword
    '''</summary>
    <TestMethod()> _
    Public Sub updateUserPasswordTest()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User
        Dim result As Integer = Nothing
        Dim newPassword As String = "Updated Password"

        user.UserID = Right(Trim(Date.UtcNow.ToString & Date.UtcNow.Millisecond.ToString), 15) 'unique user name
        user.Password = "password"
        user.FirstName = "Josh"
        user.LastName = "Turner"
        user.EmailAddress = user.UserID 'unique email address

        'result = target.addUser(user)

        If result <> 1 Then
            Assert.Fail("Failed to add user")
        End If

        'result = target.updateUserPassword(user.UserID, newPassword)

        If result <> 1 Then
            Assert.Fail("Failed to update password")
        End If

        user = target.getUser(user.UserID)
        Assert.AreEqual(newPassword, user.Password)

    End Sub


    '''<summary>
    '''A test for updateUserEmail
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "User ID cannot be null or empty")> _
    Public Sub updateUserEmailAddressUserIDNothing()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim userID As String = Nothing
        Dim newEmail As String = "Updated Password"

        target.updateUserEmail(userID, newEmail)

    End Sub

    '''<summary>
    '''A test for updateUserEmail
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "User ID exceeded the maximum number of allowable characters")> _
    Public Sub updateUserEmailAddressUserIDMaxLen()

        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim userID As String = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        Dim newEmail As String = "email@address.com"

        target.updateUserEmail(userID, newEmail)

    End Sub

    '''<summary>
    '''A test for updateUserEmail
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Email Address cannot be null or empty")> _
    Public Sub updateUserEmailAddressEmailNothing()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim userID As String = "JTurner"
        Dim newEmail As String = Nothing

        target.updateUserEmail(userID, newEmail)

    End Sub

    '''<summary>
    '''A test for updateUserEmail
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Email Address exceeded the maximum number of allowable characters")> _
    Public Sub updateUserEmailAddressEmailMaxLen()

        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim userID As String = "JTurner"
        Dim newEmail As String = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."

        target.updateUserEmail(userID, newEmail)

    End Sub

    '''<summary>
    '''A test for getUser
    '''</summary>
    <TestMethod()> _
    Public Sub getUserTest()
        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim user As BudgeteerObjects.User = New BudgeteerObjects.User
        Dim result As Integer = Nothing
        Dim newEmail As String = Trim(Date.UtcNow.ToString & Date.UtcNow.Millisecond.ToString)
        Dim userID As String = Nothing

        user.UserID = "JTurner"
        user.Password = "password"
        user.FirstName = "Josh"
        user.LastName = "Turner"
        user.EmailAddress = "cs.pmosh@gmail.com"

        'result = target.addUser(user)

        'If result <> 1 Then
        '    Assert.Fail("Failed to add user")
        'End If
        userID = user.UserID
        user = Nothing
        user = target.getUser(userID)
        Assert.AreNotEqual(user, Nothing)
    End Sub

    '''<summary>
    '''A test for getUser
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "User ID cannot be null or empty")> _
    Public Sub getUserUserIDNothing()

        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim userID As String = Nothing

        target.getUser(userID)

    End Sub

    '''<summary>
    '''A test for getUser
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "User ID exceeded the maximum number of allowable characters")> _
    Public Sub getUserUserIDMaxLen()

        Dim target As BudgeteerBusiness.User = New BudgeteerBusiness.User
        Dim userID As String = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."

        target.getUser(userID)

    End Sub

End Class
