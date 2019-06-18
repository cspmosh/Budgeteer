Imports System
Imports System.Collections.Generic
Imports BudgeteerObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports BudgeteerService

'''<summary>
'''This is a test class for BudgeteerTest and is intended
'''to contain all BudgeteerTest Unit Tests
'''</summary>
<TestClass()> _
Public Class BudgeteerTest


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
            testContextInstance = Value
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
    '''A test for UpdateUserPassword
    '''</summary>
    <TestMethod()> _
    <Owner("Josh Turner")> _
    Public Sub UpdateUserPasswordTest()
        Dim target As IBudgeteer = New Budgeteer
        Dim userID As String = "JTurner"
        Dim newPassword As String = "Test"
        Dim expected As Integer = 1
        Dim actual As Integer
        actual = target.UpdateUserPassword(userID, newPassword)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for UpdateUserEmailAddress
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateUserEmailAddressTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim userID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim newEmail As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.UpdateUserEmailAddress(userID, newEmail)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateTransaction
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateTransactionTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim transaction As Transaction = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.UpdateTransaction(transaction)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateSubcategory
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateSubcategoryTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim subcategory As Subcategory = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.UpdateSubcategory(subcategory)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateFrequency
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateFrequencyTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim frequency As Frequency = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.UpdateFrequency(frequency)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateCategory
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateCategoryTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim category As Category = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.UpdateCategory(category)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateBudget
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateBudgetTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim budget As Budget = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.UpdateBudget(budget)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for UpdateBankAccount
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateBankAccountTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim account As Account = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.UpdateBankAccount(account)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for SetApplicationSetting
    '''</summary>
    <TestMethod()> _
    Public Sub SetApplicationSettingTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim userID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim setting As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim value As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.SetApplicationSetting(userID, setting, value)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetTransactions
    '''</summary>
    <TestMethod()> _
    Public Sub GetTransactionsTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim userID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As List(Of Transaction) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of Transaction)
        actual = target.GetTransactions(userID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetSubcategories
    '''</summary>
    <TestMethod()> _
    Public Sub GetSubcategoriesTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim userID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As List(Of Subcategory) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of Subcategory)
        actual = target.GetSubcategories(userID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetFrequencies
    '''</summary>
    <TestMethod()> _
    Public Sub GetFrequenciesTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim userID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As List(Of Frequency) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of Frequency)
        actual = target.GetFrequencies(userID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetCategories
    '''</summary>
    <TestMethod()> _
    Public Sub GetCategoriesTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim userID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As List(Of Category) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of Category)
        actual = target.GetCategories(userID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetBudgets
    '''</summary>
    <TestMethod()> _
    Public Sub GetBudgetsTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim userID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As List(Of Budget) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of Budget)
        actual = target.GetBudgets(userID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetBankAccounts
    '''</summary>
    <TestMethod()> _
    Public Sub GetBankAccountsTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim userID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As List(Of Account) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of Account)
        actual = target.GetBankAccounts(userID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetApplicationSetting
    '''</summary>
    <TestMethod()> _
    Public Sub GetApplicationSettingTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim userID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim setting As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim defaultVal As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim actual As String
        actual = target.GetApplicationSetting(userID, setting, defaultVal)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteTransaction
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteTransactionTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim transactionID As ULong = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.DeleteTransaction(transactionID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteSubcategory
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteSubcategoryTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim subcategoryID As ULong = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.DeleteSubcategory(subcategoryID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for deleteFrequency
    '''</summary>
    <TestMethod()> _
    Public Sub deleteFrequencyTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim frequencyID As ULong = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.deleteFrequency(frequencyID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteCategory
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteCategoryTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim categoryID As ULong = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.DeleteCategory(categoryID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteBudget
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteBudgetTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim budgetID As ULong = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.DeleteBudget(budgetID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for DeleteBankAccount
    '''</summary>
    <TestMethod()> _
    Public Sub DeleteBankAccountTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim accountID As ULong = 0 ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.DeleteBankAccount(accountID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AuthenticateUser
    '''</summary>
    <TestMethod()> _
    Public Sub AuthenticateUserTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim userID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim password As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.AuthenticateUser(userID, password)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AdjustSubcategoryBalance
    '''</summary>
    <TestMethod()> _
    Public Sub AdjustSubcategoryBalanceTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim subcategory As Subcategory = Nothing ' TODO: Initialize to an appropriate value
        Dim amount As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.AdjustSubcategoryBalance(subcategory, amount)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AdjustBankAccountBalance
    '''</summary>
    <TestMethod()> _
    Public Sub AdjustBankAccountBalanceTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim account As Account = Nothing ' TODO: Initialize to an appropriate value
        Dim amount As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        Dim expected As [Decimal] = New [Decimal] ' TODO: Initialize to an appropriate value
        Dim actual As [Decimal]
        actual = target.AdjustBankAccountBalance(account, amount)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddTransaction
    '''</summary>
    <TestMethod()> _
    Public Sub AddTransactionTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim transaction As Transaction = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.AddTransaction(transaction)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddSubcategory
    '''</summary>
    <TestMethod()> _
    Public Sub AddSubcategoryTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim subcategory As Subcategory = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.AddSubcategory(subcategory)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddFrequency
    '''</summary>
    <TestMethod()> _
    Public Sub AddFrequencyTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim frequency As Frequency = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.AddFrequency(frequency)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddCategory
    '''</summary>
    <TestMethod()> _
    Public Sub AddCategoryTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim category As Category = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.AddCategory(category)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddBudget
    '''</summary>
    <TestMethod()> _
    Public Sub AddBudgetTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim budget As Budget = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.AddBudget(budget)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for AddBankAccount
    '''</summary>
    <TestMethod()> _
    Public Sub AddBankAccountTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim account As Account = Nothing ' TODO: Initialize to an appropriate value
        Dim expected As Integer = 0 ' TODO: Initialize to an appropriate value
        Dim actual As Integer
        actual = target.AddBankAccount(account)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for GetApplicationSettings
    '''</summary>
    <TestMethod()> _
    Public Sub GetApplicationSettingsTest()
        Dim target As IBudgeteer = New Budgeteer ' TODO: Initialize to an appropriate value
        Dim userID As String = String.Empty ' TODO: Initialize to an appropriate value
        Dim expected As List(Of ApplicationSetting) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of ApplicationSetting)
        actual = target.GetApplicationSettings(userID)
        Assert.AreEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub
End Class
