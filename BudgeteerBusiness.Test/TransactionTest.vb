Imports System.Collections.Generic
Imports BudgeteerObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports BudgeteerBusiness


'''<summary>
'''This is a test class for TransactionTest and is intended
'''to contain all TransactionTest Unit Tests
'''</summary>
<TestClass()> _
Public Class TransactionTest

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

    '''<summary>
    '''A test for getTransactionFirstYear
    '''</summary>
    <TestMethod()> _
    Public Sub getTransactionFirstYear()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim userID As String = "JTurner"
        Dim expected As Integer = 2008
        Dim actual As Integer
        actual = target.getTransactionFirstYear(userID)
        Assert.AreEqual(expected, actual)

        userID = "NonExisting"
        actual = target.getTransactionFirstYear(userID)
        Assert.AreEqual(Nothing, actual)

    End Sub

    '''<summary>
    '''A test for getTransactions
    '''</summary>
    <TestMethod()> _
    Public Sub getTransactionsTest()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim userID As String = "JTurner"
        Dim expected As List(Of BudgeteerObjects.Transaction) = Nothing
        Dim actual As List(Of BudgeteerObjects.Transaction)
        actual = target.getTransactions(userID)
        Assert.AreNotEqual(expected, actual)

        userID = "NonExisting"
        actual = target.getTransactions(userID)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for getTransactions
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null user ID was inappropriately allowed")> _
    Public Sub getTransactionsUserIDNothing()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim userID As String = Nothing
        target.getTransactions(userID)
    End Sub


    '''<summary>
    '''A test for addTransaction
    '''</summary>
    <TestMethod()> _
    Public Sub addTransactionTest()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction
        Dim row As Integer

        transaction.TransactionDate = Date.Today()
        transaction.Description = "New Transaction!"
        transaction.Amount = Nothing '0
        transaction.TaxAmount = Nothing '0
        transaction.CheckNumber = Nothing 'Null
        transaction.SubcategoryID = Nothing
        transaction.AccountID = Nothing
        transaction.UserID = "JTurner"

        row = target.addTransaction(transaction)
        Assert.IsTrue(row > 0)

    End Sub

    '''<summary>
    '''A test for addTransaction
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null transaction was inappropriately allowed")> _
    Public Sub addTransactionTransactionNothing()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = Nothing
        target.addTransaction(transaction)
    End Sub

    '''<summary>
    '''A test for addTransaction
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null date was inappropriately allowed")> _
    Public Sub addTransactionDateNothing()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction

        transaction.TransactionDate = Nothing
        transaction.Description = "New Transaction!"
        transaction.Amount = Nothing '0
        transaction.TaxAmount = Nothing '0
        transaction.CheckNumber = Nothing 'Null
        transaction.SubcategoryID = Nothing
        transaction.AccountID = Nothing
        transaction.UserID = "JTurner"

        target.addTransaction(transaction)

    End Sub

    '''<summary>
    '''A test for addTransaction
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null description was inappropriately allowed")> _
    Public Sub addTransactionDescriptionNothing()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction

        transaction.TransactionDate = Date.Today()
        transaction.Description = Nothing 'Error: Required Field
        transaction.Amount = Nothing '0
        transaction.TaxAmount = Nothing '0
        transaction.CheckNumber = Nothing 'Null
        transaction.SubcategoryID = Nothing 'Null
        transaction.AccountID = Nothing 'Null
        transaction.UserID = "JTurner"

        target.addTransaction(transaction)
    End Sub

    '''<summary>
    '''A test for addTransaction
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Description exceeded the maximum length of characters allowed")> _
    Public Sub addTransactionDescriptionMaxLen()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction

        transaction.TransactionDate = Date.Today()
        transaction.Description = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        transaction.Amount = Nothing '0
        transaction.TaxAmount = Nothing '0
        transaction.CheckNumber = Nothing 'Null
        transaction.SubcategoryID = Nothing 'Null
        transaction.AccountID = Nothing 'Null
        transaction.UserID = "JTurner"

        target.addTransaction(transaction)
    End Sub

    '''<summary>
    '''A test for addTransaction
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null user ID was inappropriately allowed")> _
    Public Sub addTransactionUserIDNothing()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction

        transaction.TransactionDate = Date.Today()
        transaction.Description = "New Description, Today!"
        transaction.Amount = Nothing '0
        transaction.TaxAmount = Nothing '0
        transaction.CheckNumber = Nothing 'Null
        transaction.SubcategoryID = Nothing 'Null
        transaction.AccountID = Nothing 'Null
        transaction.UserID = Nothing

        target.addTransaction(transaction)
    End Sub

    '''<summary>
    '''A test for addTransaction
    '''</summary>
    <TestMethod()> _
    Public Sub addTransactionAccountAdjust()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction
        Dim row As Integer

        Dim accountRow As Integer = Nothing
        Dim accountBusiness As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount

        account.Name = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        account.Number = Nothing
        account.Type = Nothing
        account.Balance = Nothing
        account.Active = "Y"
        account.UserID = "JTurner"

        Try
            accountRow = accountBusiness.AddAccount(account)
        Catch ex As Exception
            Assert.Fail("Could not add a new account")
        End Try

        If accountRow > 0 Then

            transaction.TransactionDate = Date.Today()
            transaction.Description = "New Transaction!"
            transaction.Amount = 9.15
            transaction.TaxAmount = Nothing '0
            transaction.CheckNumber = Nothing 'Null
            transaction.SubcategoryID = Nothing
            transaction.AccountID = accountRow 'Assign account ID of new account to transaction
            transaction.UserID = "JTurner"

            Try
                row = target.addTransaction(transaction)
            Catch ex As Exception
                Assert.Fail("Could not add new transaction")
            End Try

            If row > 0 Then
                ' Re-retrieve the account and verify the account balance updated
                account = Nothing
                account = accountBusiness.GetAccount(accountRow)
                If account IsNot Nothing Then
                    Assert.AreEqual(account.Balance, transaction.Amount)
                Else
                    ' Unable to re-retrieve account
                    Assert.Fail("Unable to re-retrieve account")
                End If
            Else
                Assert.Fail("Failed to insert a new transaction")
            End If

        Else
            Assert.Fail("Failed to insert a new account")
        End If

    End Sub

    '''<summary>
    '''A test for addTransaction
    '''</summary>
    <TestMethod()> _
    Public Sub addTransactionSFSubcategoryAdjust()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction
        Dim row As Integer

        Dim subcategoryRow As Integer = Nothing
        Dim subcategoryBusiness As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory

        subcategory.CategoryID = 1
        subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        subcategory.SinkingFund = "y"
        subcategory.Balance = Nothing
        subcategory.Notes = " "
        subcategory.UserID = "JTurner"

        Try
            subcategoryRow = subcategoryBusiness.addSubcategory(subcategory)
        Catch ex As Exception
            Assert.Fail("Failed to insert a new subcategory")
        End Try

        If subcategoryRow > 0 Then

            transaction.TransactionDate = Date.Today()
            transaction.Description = "New Transaction! Sinking Fund Subcategory added"
            transaction.Amount = 9.15
            transaction.TaxAmount = Nothing '0
            transaction.CheckNumber = Nothing 'Null
            transaction.SubcategoryID = subcategoryRow 'Assign subcategory ID of new subcategory to transaction
            transaction.AccountID = Nothing
            transaction.UserID = "JTurner"

            Try
                row = target.addTransaction(transaction)
            Catch ex As Exception
                Assert.Fail("Could not add new transaction")
            End Try

            If row > 0 Then
                ' Re-retrieve the account and verify the account balance updated
                subcategory = Nothing
                subcategory = subcategoryBusiness.getSubcategory(subcategoryRow)
                If subcategory IsNot Nothing Then
                    Assert.AreEqual(subcategory.Balance, transaction.Amount)
                Else
                    ' Unable to re-retrieve account
                    Assert.Fail("Unable to re-retrieve account")
                End If
            Else
                Assert.Fail("Failed to insert a new transaction")
            End If

        Else
            Assert.Fail("Failed to insert a new subcategory")
        End If

    End Sub

    '''<summary>
    '''A test for updateTransaction
    '''</summary>
    <TestMethod()> _
    Public Sub updateTransactionTest()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction

        'Represents a transaction already in the database with modified fields
        transaction.TransactionID = 1
        transaction.TransactionDate = Date.Today()
        transaction.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        transaction.Amount = Nothing '0
        transaction.TaxAmount = Nothing '0
        transaction.CheckNumber = Nothing 'Null
        transaction.SubcategoryID = Nothing 'Null
        transaction.AccountID = Nothing 'Null
        transaction.UserID = "JTurner"

        Dim expected As Integer = 1
        Dim actual As Integer
        actual = target.updateTransaction(transaction)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for updateTransaction
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null transaction was inappropriately allowed")> _
    Public Sub updateTransactionTransactionNothing()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = Nothing
        target.updateTransaction(transaction)
    End Sub

    '''<summary>
    '''A test for updateTransaction
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null transaction ID was inappropriately allowed")> _
    Public Sub updateTransactionTransactionIDNothing()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction

        'Represents a transaction already in the database with modified fields
        transaction.TransactionID = Nothing
        transaction.TransactionDate = Date.Today()
        transaction.Description = "Updated Description"
        transaction.Amount = Nothing '0
        transaction.TaxAmount = Nothing '0
        transaction.CheckNumber = Nothing 'Null
        transaction.SubcategoryID = Nothing 'Null
        transaction.AccountID = Nothing 'Null

        target.updateTransaction(transaction)
    End Sub

    '''<summary>
    '''A test for updateTransaction
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null date was inappropriately allowed")> _
    Public Sub updateTransactionDateNothing()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction

        'Represents a transaction already in the database with modified fields
        transaction.TransactionID = 1
        transaction.TransactionDate = Nothing
        transaction.Description = "Updated Description"
        transaction.Amount = Nothing '0
        transaction.TaxAmount = Nothing '0
        transaction.CheckNumber = Nothing 'Null
        transaction.SubcategoryID = Nothing 'Null
        transaction.AccountID = Nothing 'Null

        target.updateTransaction(transaction)
    End Sub

    '''<summary>
    '''A test for updateTransaction
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null description was inappropriately allowed")> _
    Public Sub updateTransactionDescriptionNothing()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction

        'Represents a transaction already in the database with modified fields
        transaction.TransactionID = 1
        transaction.TransactionDate = Date.Today()
        transaction.Description = Nothing
        transaction.Amount = Nothing '0
        transaction.TaxAmount = Nothing '0
        transaction.CheckNumber = Nothing 'Null
        transaction.SubcategoryID = Nothing 'Null
        transaction.AccountID = Nothing 'Null

        target.updateTransaction(transaction)
    End Sub

    '''<summary>
    '''A test for updateTransaction
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Description exceeded the maximum length of characters allowed")> _
    Public Sub updateTransactionDescriptionMaxLen()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction

        'Represents a transaction already in the database with modified fields
        transaction.TransactionID = 1
        transaction.TransactionDate = Date.Today()
        transaction.Description = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        transaction.Amount = Nothing '0
        transaction.TaxAmount = Nothing '0
        transaction.CheckNumber = Nothing 'Null
        transaction.SubcategoryID = Nothing 'Null
        transaction.AccountID = Nothing 'Null

        target.updateTransaction(transaction)
    End Sub

    '''<summary>
    '''A test for updateTransaction
    '''</summary>
    <TestMethod()> _
    Public Sub updateTransactionAccountAdjust()

        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction
        Dim row As Integer

        Dim accountRow As Integer = Nothing
        Dim accountBusiness As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount
        Dim expected As Decimal

        account.Name = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        account.Number = Nothing
        account.Type = Nothing
        account.Balance = 10
        account.Active = "Y"
        account.UserID = "JTurner"

        Try
            accountRow = accountBusiness.AddAccount(account)
        Catch ex As Exception
            Assert.Fail("Could not add a new account")
        End Try

        If accountRow > 0 Then

            transaction.TransactionID = 1
            transaction.TransactionDate = Date.Today()
            transaction.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
            transaction.Amount = 5
            transaction.TaxAmount = Nothing '0
            transaction.CheckNumber = Nothing 'Null
            transaction.SubcategoryID = Nothing
            transaction.AccountID = accountRow 'Assign account ID of new account to transaction
            transaction.UserID = "JTurner"

            expected = account.Balance + transaction.Amount

            Try
                row = target.updateTransaction(transaction)
            Catch ex As Exception
                Assert.Fail("Could not update new transaction")
            End Try

            If row > 0 Then
                ' Re-retrieve the account and verify the account balance updated
                account = Nothing
                account = accountBusiness.GetAccount(accountRow)
                If account IsNot Nothing Then
                    Assert.AreEqual(account.Balance, transaction.Amount)
                Else
                    ' Unable to re-retrieve account
                    Assert.Fail("Unable to re-retrieve account")
                End If
            Else
                Assert.Fail("Failed to update the transaction")
            End If

        Else
            Assert.Fail("Failed to insert a new account")
        End If

    End Sub

    '''<summary>
    '''A test for updaTransaction
    '''</summary>
    <TestMethod()> _
    Public Sub updateTransactionSFSubcategoryAdjust()

        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction
        Dim row As Integer

        Dim subcategoryRow As Integer = Nothing
        Dim subcategoryBusiness As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory

        subcategory.CategoryID = 1
        subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = " "
        subcategory.UserID = "JTurner"

        Try
            subcategoryRow = subcategoryBusiness.AddSubcategory(subcategory)
        Catch ex As Exception
            Assert.Fail("Failed to insert a new subcategory")
        End Try

        If subcategoryRow > 0 Then

            transaction.TransactionID = 1
            transaction.TransactionDate = Date.Today()
            transaction.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
            transaction.Amount = 9.15
            transaction.TaxAmount = Nothing '0
            transaction.CheckNumber = Nothing 'Null
            transaction.SubcategoryID = subcategoryRow 'Assign subcategory ID of new subcategory to transaction
            transaction.AccountID = Nothing
            transaction.UserID = "JTurner"

            Try
                row = target.updateTransaction(transaction)
            Catch ex As Exception
                Assert.Fail("Could not update the transaction")
            End Try

            If row > 0 Then
                ' Re-retrieve the account and verify the account balance updated
                subcategory = Nothing
                subcategory = subcategoryBusiness.GetSubcategory(subcategoryRow)
                If subcategory IsNot Nothing Then
                    Assert.AreEqual(subcategory.Balance, transaction.Amount)
                Else
                    ' Unable to re-retrieve account
                    Assert.Fail("Unable to re-retrieve account")
                End If
            Else
                Assert.Fail("Failed to update the transaction")
            End If

        Else
            Assert.Fail("Failed to insert a new subcategory")
        End If

    End Sub

    '''<summary>
    '''A test for deleteTransaction
    '''</summary>
    <TestMethod()> _
    Public Sub deleteTransactionTest()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction
        Dim row As Integer

        transaction.TransactionDate = Date.Today()
        transaction.Description = "New Transaction!"
        transaction.Amount = Nothing '0
        transaction.TaxAmount = Nothing '0
        transaction.CheckNumber = Nothing 'Null
        transaction.SubcategoryID = Nothing
        transaction.AccountID = Nothing
        transaction.UserID = "JTurner"

        row = target.addTransaction(transaction)
        transaction = Nothing
        transaction = target.getTransaction(row)
        ' Verify that the transaction was created
        Assert.AreNotEqual(transaction, Nothing)

        ' Delete the transaction
        target.deleteTransaction(row)
        transaction = target.getTransaction(row)
        ' Verify that the transaction was deleted
        Assert.AreEqual(transaction, Nothing)

    End Sub

    '''<summary>
    '''A test for deleteTransaction
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null transaction ID was inappropriately allowed")> _
    Public Sub deleteTransactionTransactionIDNothing()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transactionID As Integer = Nothing

        ' Delete the transaction
        target.deleteTransaction(transactionID)

    End Sub

    '''<summary>
    '''A test for deleteTransaction
    '''</summary>
    <TestMethod()> _
    Public Sub deleteTransactionAccountAdjust()
        Dim target As BudgeteerBusiness.Transaction = New BudgeteerBusiness.Transaction
        Dim transaction As BudgeteerObjects.Transaction = New BudgeteerObjects.Transaction
        Dim row As Integer

        Dim accountRow As Integer = Nothing
        Dim accountBusiness As BudgeteerBusiness.BankAccount = New BudgeteerBusiness.BankAccount
        Dim account As BudgeteerObjects.BankAccount = New BudgeteerObjects.BankAccount

        account.Name = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        account.Number = Nothing
        account.Type = Nothing
        account.Balance = Nothing
        account.Active = "Y"
        account.UserID = "JTurner"

        Try
            accountRow = accountBusiness.AddAccount(account)
        Catch ex As Exception
            Assert.Fail("Could not add a new account")
        End Try

        transaction.TransactionDate = Date.Today()
        transaction.Description = "New Transaction!"
        transaction.Amount = 5.0
        transaction.TaxAmount = Nothing '0
        transaction.CheckNumber = Nothing 'Null
        transaction.SubcategoryID = Nothing
        transaction.AccountID = accountRow
        transaction.UserID = "JTurner"

        row = target.addTransaction(transaction)
        transaction = Nothing
        transaction = target.getTransaction(row)
        ' Verify that the transaction was created
        Assert.AreNotEqual(transaction, Nothing)

        account = accountBusiness.GetAccount(accountRow)
        Assert.AreEqual(account.Balance, transaction.Amount)

        ' Delete the transaction
        target.deleteTransaction(row)
        transaction = target.getTransaction(row)
        ' Verify that the transaction was deleted
        Assert.AreEqual(transaction, Nothing)

        account = accountBusiness.GetAccount(accountRow)
        Assert.AreEqual(account.Balance, 0D)

    End Sub

End Class
