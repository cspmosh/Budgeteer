Imports System.Collections.Generic
Imports System
Imports BudgeteerObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports BudgeteerBusiness

'''<summary>
'''This is a test class for AccountTest and is intended
'''to contain all AccountTest Unit Tests
'''</summary>
<TestClass()> _
Public Class BankAccountTest

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
    '''A test for GetAccounts
    '''</summary>
    <TestMethod()> _
    Public Sub GetAccountsTest()
        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim userID As String = "JTurner"
        Dim expected As List(Of BudgeteerObjects.BankAccount) = Nothing
        Dim actual As List(Of BudgeteerObjects.BankAccount)
        actual = target.GetAccounts(userID)
        Assert.AreNotEqual(expected, actual)

        userID = "NonExistingAcc"
        actual = target.GetAccounts(userID)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for GetAccounts
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null user ID was inappropriately allowed")> _
    Public Sub GetAccountsUserIDNothing()
        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim userID As String = Nothing
        Dim expected As List(Of BudgeteerObjects.BankAccount) = Nothing
        Dim actual As List(Of BudgeteerObjects.BankAccount)
        actual = target.GetAccounts(userID)
    End Sub

    '''<summary>
    '''A test for GetAccount
    '''</summary>
    <TestMethod()> _
    Public Sub GetAccountTest()
        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim accountID As Int64 = 1
        Dim expected As BudgeteerObjects.BankAccount = Nothing
        Dim actual As BudgeteerObjects.BankAccount
        actual = target.GetAccount(accountID)
        Assert.AreNotEqual(expected, actual)
        accountID = -1
        actual = target.GetAccount(accountID)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetAccount
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null account ID was inappropriately allowed")> _
    Public Sub GetAccountAccountIDNothing()
        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim accountID As Int64 = Nothing
        Dim expected As BudgeteerObjects.BankAccount = Nothing
        Dim actual As BudgeteerObjects.BankAccount
        actual = target.GetAccount(accountID)
    End Sub

    '''<summary>
    '''A test for AddAccount
    '''</summary>
    <TestMethod()> _
    Public Sub AddAccountTest()
        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim expected As Integer = 1
        Dim actual As Integer

        account.Number = Nothing
        account.Name = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        account.Type = Nothing
        account.Balance = Nothing
        account.Active = "Y"
        account.UserID = "JTurner"

        actual = target.AddAccount(account)
        Assert.IsTrue(actual > 0)
    End Sub

    '''<summary>
    '''A test for AddAccount
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Account of Nothing was inappropriately allowed")> _
    Public Sub AddAccountAccountNothing()
        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = Nothing
        target.AddAccount(account)
    End Sub

    '''<summary>
    '''A test for AddAccount
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Account Name exeeceded the maximum length of characters allowed")> _
    Public Sub AddAccountAccountNumberMaxLen()
        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount

        account.Number = "12345"
        account.Name = "Test Account"
        account.Type = "Test Type"
        account.Balance = 0.0
        account.Active = "Y"
        account.UserID = "JTurner"
        target.AddAccount(account)
    End Sub

    '''<summary>
    '''A test for AddAccount
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null Account Name was inappropriately allowed")> _
    Public Sub AddAccountAccountNameNothing()
        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount

        account.Number = ""
        account.Name = Nothing
        account.Type = "Test Type"
        account.Balance = 0.0
        account.Active = "Y"
        account.UserID = "JTurner"
        target.AddAccount(account)
    End Sub

    '''<summary>
    '''A test for AddAccount
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Account Name exeeceded the maximum length of characters allowed")> _
    Public Sub AddAccountAccountNameMaxLen()
        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount

        account.Number = ""
        account.Name = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        account.Type = "Test Type"
        account.Balance = 0.0
        account.Active = "Y"
        account.UserID = "JTurner"
        target.AddAccount(account)
    End Sub

    '''<summary>
    '''A test for AddAccount
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Account Name exeeceded the maximum length of characters allowed")> _
    Public Sub AddAccountAccountTypeMaxLen()
        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount

        account.Number = ""
        account.Name = "Account Name"
        account.Type = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        account.Balance = 0.0
        account.Active = "Y"
        account.UserID = "JTurner"
        target.AddAccount(account)
    End Sub

    '''<summary>
    '''A test for AddAccount
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Account Type not 'Y' or 'N'")> _
    Public Sub AddAccountActiveStatusNotYN()
        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount

        account.Number = ""
        account.Name = "Account Name"
        account.Type = "Account Type"
        account.Balance = 0.0
        account.Active = "A" 'Not Y or N
        account.UserID = "JTurner"
        target.AddAccount(account)

    End Sub

    '''<summary>
    '''A test for AddAccount
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Account User ID set to nothing")> _
    Public Sub AddAccountUserIDNothing()
        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount

        account.Number = ""
        account.Name = "Account Name"
        account.Type = "Account Type"
        account.Balance = 0.0
        account.Active = "Y"
        account.UserID = Nothing
        target.AddAccount(account)

    End Sub

    '''<summary>
    '''A test for AccountUpdate
    '''</summary>
    <TestMethod()> _
    Public Sub AccountUpdateTest()
        Dim target As BankAccount = New BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim expecting As Integer = 1
        Dim actual As Integer

        account.AccountID = 1
        account.Number = "6115"
        account.Name = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        account.Type = "Credit Card"
        account.Balance = 4.22
        account.Active = "Y"
        account.UserID = "JTurner"

        actual = target.UpdateAccount(account)
        Assert.AreEqual(expecting, actual)

        expecting = 0
        actual = target.UpdateAccount(account)
        Assert.AreEqual(expecting, actual)

    End Sub

    '''<summary>
    '''A test for AccountUpdate
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "An account of nothing was inappropriately allowed")> _
    Public Sub AccountUpdateAccountNothing()

        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = Nothing
        Dim actual As Integer

        actual = target.UpdateAccount(account)

    End Sub

    '''<summary>
    '''A test for AccountUpdate
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "An accountID of nothing was inappropriately allowed")> _
    Public Sub AccountUpdateAccountIDNothing()

        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim actual As Integer

        account.AccountID = Nothing
        account.Number = "6115"
        account.Name = "Amazon CC"
        account.Type = "Credit Card"
        account.Balance = 4.22
        account.Active = "Y"
        account.UserID = "JTurner"

        actual = target.UpdateAccount(account)

    End Sub

    '''<summary>
    '''A test for AccountUpdate
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "An accountID of nothing was inappropriately allowed")> _
    Public Sub AccountUpdateAccountNumberMaxLen()

        Dim target As BankAccount = New BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim expecting As Integer = 0
        Dim actual As Integer

        account.AccountID = 1
        account.Number = "61157" 'Account Number should only be 4 digits
        account.Name = "Amazon CC"
        account.Type = "Credit Card"
        account.Balance = 4.22
        account.Active = "Y"
        account.UserID = "JTurner"

        actual = target.UpdateAccount(account)

    End Sub

    '''<summary>
    '''A test for AccountUpdate
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A blank account name was inappropriately allowed")> _
    Public Sub AccountUpdateAccountNameNothing()

        Dim target As BankAccount = New BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim expecting As Integer = 0
        Dim actual As Integer

        account.AccountID = 1
        account.Number = "6115"
        account.Name = Nothing
        account.Type = "Credit Card"
        account.Balance = 4.22
        account.Active = "Y"
        account.UserID = "JTurner"

        actual = target.UpdateAccount(account)

    End Sub

    '''<summary>
    '''A test for AccountUpdate
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Account Name exeeceded the maximum length of characters allowed")> _
    Public Sub AccountUpdateAccountNameMaxLen()

        Dim target As BankAccount = New BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim expecting As Integer = 0
        Dim actual As Integer

        account.AccountID = 1
        account.Number = "6115"
        account.Name = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        account.Type = "Credit Card"
        account.Balance = 4.22
        account.Active = "Y"
        account.UserID = "JTurner"

        actual = target.UpdateAccount(account)

    End Sub

    '''<summary>
    '''A test for AccountUpdate
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Account Name exeeceded the maximum length of characters allowed")> _
    Public Sub AccountUpdateAccountTypeMaxLen()

        Dim target As BankAccount = New BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim expecting As Integer = 0
        Dim actual As Integer

        account.AccountID = 1
        account.Number = "6115"
        account.Name = "Amazon CC"
        account.Type = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        account.Balance = 4.22
        account.Active = "Y"
        account.UserID = "JTurner"

        actual = target.UpdateAccount(account)

    End Sub

    '''<summary>
    '''A test for AccountUpdate
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "An active status other than Y or N was inappropriately allowed")> _
    Public Sub AccountUpdateActiveStatusNotYN()

        Dim target As BankAccount = New BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim expecting As Integer = 0
        Dim actual As Integer

        account.AccountID = 1
        account.Number = "6115"
        account.Name = "Amazon CC"
        account.Type = "Credit Card"
        account.Balance = 4.22
        account.Active = Nothing 'Something other than Y or N
        account.UserID = "JTurner"

        actual = target.UpdateAccount(account)

    End Sub

    '''<summary>
    '''A test for DeleteAccount
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteAccountTest()

        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim expected As Integer = 1
        Dim actual As Integer

        account.Number = "1234"
        account.Name = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        account.Type = "Test Type"
        account.Balance = 0.0
        account.Active = "Y"
        account.UserID = "JTurner"

        ' First add the account
        Dim accountID As Long = target.AddAccount(account)

        If accountID = 0 Then
            Assert.Fail() 'Couldn't add account
        End If

        actual = target.DeleteAccount(accountID)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for DeleteAccount
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "An account ID of nothing was inappropriately allowed")> _
    Public Sub DeleteAccountAccountIDNothing()

        Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim accountID As Long = Nothing
        Dim expected As Integer = 1
        Dim actual As Integer

        actual = target.DeleteAccount(accountID)

    End Sub

    'No longer a public function

    ''''<summary> 
    ''''A test for AdjustBalance
    ''''</summary>
    '<TestMethod()> _
    'Public Sub AdjustBalancePositiveAmount()
    '    Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
    '    Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
    '    Dim amount As [Decimal] = New [Decimal](5.5)
    '    Dim expected As [Decimal] = New [Decimal](65.5)
    '    Dim actual As [Decimal] = New [Decimal]

    '    account.AccountID = 1
    '    account.Number = "6115"
    '    account.Name = "Amazon CC"
    '    account.Type = "Credit Card"
    '    account.Balance = 60.0
    '    account.Active = "Y"
    '    account.UserID = "JTurner"

    '    Using dTran As BudgeteerDAL.DistributedTransaction = New BudgeteerDAL.DistributedTransaction(Utils.ConnectionString)
    '        target.AdjustBalance(dTran, account, amount)
    '    End Using

    '    'Re-Retrieve the account
    '    account = target.GetAccount(account.AccountID)
    '    actual = account.Balance
    '    Assert.AreEqual(expected, actual)

    'End Sub

    'No longer a public function

    ''''<summary>
    ''''A test for AdjustBalance
    ''''</summary>
    '<TestMethod()> _
    'Public Sub AdjustBalanceNegativeAmount()
    '    Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
    '    Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
    '    Dim amount As [Decimal] = New [Decimal](-5.5)
    '    Dim expected As [Decimal] = New [Decimal](54.5)
    '    Dim actual As [Decimal] = New [Decimal]

    '    account.AccountID = 1
    '    account.Number = "6115"
    '    account.Name = "Amazon CC"
    '    account.Type = "Credit Card"
    '    account.Balance = 60.0
    '    account.Active = "Y"
    '    account.UserID = "JTurner"

    '    target.AdjustBalance(account, amount)

    '    'Re-Retrieve the account
    '    account = target.GetAccount(account.AccountID)
    '    actual = account.Balance
    '    Assert.AreEqual(expected, actual)

    'End Sub

    'No longer a public function

    ''''<summary>
    ''''A test for AdjustBalance
    ''''</summary>
    '<TestMethod()> _
    '<ExpectedException(GetType(ArgumentException), "An empty account was inappropriately allowed")> _
    '    Public Sub AdjustBalanceAccountNothing()
    '    Dim target As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
    '    Dim account As BudgeteerObjects.BankAccount = Nothing
    '    Dim amount As [Decimal] = New [Decimal]
    '    target.AdjustBalance(account, amount)
    'End Sub

End Class
