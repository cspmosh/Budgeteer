Imports System

Imports System.Collections.Generic
Imports BudgeteerObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports BudgeteerBusiness

'''<summary>
'''This is a test class for BudgetTest and is intended
'''to contain all BudgetTest Unit Tests
'''</summary>
<TestClass()> _
Public Class BudgetTest


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

    '''<summary>
    '''A test for GetBudgets
    '''</summary>
    <TestMethod()> _
    Public Sub GetBudgetsTest()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim userID As String = "JTurner"
        Dim expected As List(Of BudgeteerObjects.Budget) = Nothing
        Dim actual As List(Of BudgeteerObjects.Budget)
        actual = target.GetBudgets(userID)
        Assert.AreNotEqual(expected, actual)

        userID = "NONUSER"
        actual = target.GetBudgets(userID)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for GetBudgets
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null user ID was inappropriately allowed")> _
    Public Sub GetBudgetsUserIDBlank()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim userID As String = Nothing
        target.GetBudgets(userID)
    End Sub

    '''<summary>
    '''A test for GetBudgets
    '''</summary>
    <TestMethod()> _
    Public Sub GetBudgetsByCriteriaTest()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim userID As String = "JTurner"
        Dim myDate As Date = Date.Today
        Dim expected As List(Of BudgeteerObjects.Budget) = Nothing
        Dim actual As List(Of BudgeteerObjects.Budget)
        actual = target.GetBudgetsWithCriteria(userID, myDate, Nothing)
        Assert.AreNotEqual(expected, actual)

        userID = "NONUSER"
        actual = target.GetBudgets(userID)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for AddBudget
    '''</summary>
    <TestMethod()> _
    Public Sub AddBudgetTest()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
        Dim expected As Integer = 1
        Dim actual As Integer

        budget.StartDate = "2015-02-01"
        budget.SubcategoryID = 3
        budget.Amount = 0
        budget.UserID = "JTurner"

        Try
            actual = target.AddBudget(budget)
        Catch ex As Exception
            'Budget probably already exists... 
            Assert.Inconclusive("Initialize budget to start date and subcategory not already in the database")
        End Try
        Assert.IsTrue(actual > 0)

    End Sub

    '''<summary>
    '''A test for AddBudget
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null budget inappropriately allowed")> _
    Public Sub AddBudgetBudgetNothing()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budget As BudgeteerObjects.Budget = Nothing
        target.AddBudget(budget)
    End Sub

    '''<summary>
    '''A test for AddBudget
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A blank start date was inappropriately allowed")> _
    Public Sub AddBudgetStartDateBlank()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget

        budget.StartDate = Nothing
        budget.SubcategoryID = 1
        budget.Amount = 300.0
        budget.UserID = "JTurner"
        target.AddBudget(budget)

    End Sub

    '''<summary>
    '''A test for AddBudget
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A blank subcategory was inappropriately allowed")> _
    Public Sub AddBudgetSubcategoryBlank()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget

        budget.StartDate = "2011-06-01"
        budget.SubcategoryID = Nothing
        budget.Amount = 300.0
        budget.UserID = "JTurner"
        target.AddBudget(budget)
    End Sub


    '''<summary>
    '''A test for AddBudget
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A blank user ID was inappropriately allowed")> _
    Public Sub AddBudgetUserIDBlank()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget

        budget.StartDate = "2011-06-01"
        budget.SubcategoryID = 2
        budget.Amount = 250.0
        budget.UserID = String.Empty
        target.AddBudget(budget)
    End Sub


    '''<summary>
    '''A test for UpdateBudget
    '''</summary>
    <TestMethod()> _
    Public Sub UpdateBudgetTest()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
        Dim expected As Integer = 1
        Dim actual As Integer

        Const MIN As Double = 1.0
        Const MAX As Double = 10000.0
        Const DEC_PLACES As Integer = 2

        Dim rnd As New Random

        'Generate a random number X where MIN <= X < MAX and then round to DEC_PLACES decimal places.
        Dim dbl As Double = Math.Round(rnd.NextDouble() * (MAX - MIN) + MIN, DEC_PLACES)

        'budget.BudgetID = 100
        'budget.Type = "Expense"
        'budget.SubcategoryID = 8
        'budget.StartDate = "2011-09-01"
        'budget.Amount = 25
        'budget.UserID = "JTurner"

        'actual = target.UpdateBudget(budget)
        'Assert.AreEqual(expected, actual)

        'expected = 0 ' Not updated
        'actual = target.UpdateBudget(budget)
        'Assert.AreEqual(expected, actual)

        budget.BudgetID = 1
        budget.SubcategoryID = 2
        budget.StartDate = "2011-04-01"
        budget.Amount = System.Convert.ToDecimal(dbl)

        actual = target.UpdateBudget(budget)
        Assert.AreEqual(expected, actual)

        expected = 0 ' Not updated
        actual = target.UpdateBudget(budget)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for UpdateBudget
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A blank budget ID was inappropriately allowed")> _
    Public Sub UpdateBudgetBudgetNothing()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budget As BudgeteerObjects.Budget = Nothing
        target.UpdateBudget(budget)
    End Sub

    '''<summary>
    '''A test for UpdateBudget
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A blank budget ID was inappropriately allowed")> _
    Public Sub UpdateBudgetBudgetIDBlank()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
        budget.BudgetID = Nothing
        budget.SubcategoryID = 2
        budget.StartDate = "2011-04-01"
        budget.Amount = 3.0
        target.UpdateBudget(budget)
    End Sub

    '''<summary>
    '''A test for UpdateBudget
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A blank subcategory ID was inappropriately allowed")> _
    Public Sub UpdateBudgetSubcategoryIDBlank()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
        budget.BudgetID = 1
        budget.SubcategoryID = Nothing
        budget.StartDate = "2011-04-01"
        budget.Amount = 3.0
        target.UpdateBudget(budget)
    End Sub


    '''<summary>
    '''A test for UpdateBudget
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A blank start date was inappropriately allowed")> _
    Public Sub UpdateBudgetStartDateBlank()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
        budget.BudgetID = 1
        budget.SubcategoryID = 2
        budget.StartDate = Nothing
        budget.Amount = 3.0
        target.UpdateBudget(budget)
    End Sub

    '''<summary>
    '''A test for deleteBudget
    '''</summary>
    <TestMethod()> _
    Public Sub deleteBudgetTest()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budget As BudgeteerObjects.Budget = New BudgeteerObjects.Budget
        Dim budgetID As ULong = 1024
        Dim expected As Integer = 1
        Dim actual As Integer
        Dim row As Long = Nothing

        budgetID = 1024
        budget.StartDate = Date.Today
        budget.SubcategoryID = 8
        budget.Amount = 0
        budget.UserID = "JTurner"

        'budget.Type = "Expense"
        'budget.StartDate = "2014-02-01"
        'budget.SubcategoryID = 4
        'budget.FrequencyID = 1
        'budget.Amount = 0
        'budget.UserID = "JTurner"

        'Try
        '    row = target.AddBudget(budget)
        'Catch ex As Exception
        '    'Budget probably already exists... 
        '    Assert.Inconclusive("Initialize budget to start date and subcategory not already in the database")
        'End Try
        'If row > 0 Then
        actual = target.deleteBudget(budgetID)
        Assert.AreEqual(expected, actual)
        'End If


    End Sub

    '''<summary>
    '''A test for deleteBudget
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A blank budget ID was inappropriately allowed")> _
    Public Sub deleteBudgetBudgetIDBlank()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim budgetID As ULong = Nothing
        target.deleteBudget(budgetID)
    End Sub



    '''<summary>
    '''A test for GetUtilizedDollarsByDate
    '''</summary>
    <TestMethod()> _
    Public Sub GetUtilizedDollarsByDateTest()
        Dim target As Budget = New Budget ' TODO: Initialize to an appropriate value
        Dim userID As String = "JTurner"
        Dim budgetDate As Nullable(Of DateTime) = Date.Today
        Dim subcategoryType As String = "Expense"
        Dim expected As List(Of UtilizedDollars) = Nothing ' TODO: Initialize to an appropriate value
        Dim actual As List(Of UtilizedDollars) = Nothing
        actual = target.GetUtilizedDollarsByDate(userID, budgetDate, subcategoryType)
        Assert.AreNotEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub

    '''<summary>
    '''A test for CopyBudgets
    '''</summary>
    <TestMethod()> _
    Public Sub CopyBudgetsTest()
        Dim target As BudgeteerBusiness.Budget = New BudgeteerBusiness.Budget
        Dim UserID As String = "JTurner"
        Dim startDate As Date = DateAdd(DateInterval.Month, -1, Date.Today)
        Dim endDate As Date = Date.Today
        Dim expected As Integer = 0
        Dim actual As Integer
        actual = target.CopyBudgets(UserID, startDate, endDate)
        Assert.AreNotEqual(expected, actual)
        Assert.Inconclusive("Verify the correctness of this test method.")
    End Sub
End Class
