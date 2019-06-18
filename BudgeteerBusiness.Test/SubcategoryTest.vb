Imports System
Imports System.Collections.Generic
Imports BudgeteerObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports BudgeteerBusiness

'''<summary>
'''This is a test class for SubcategoryTest and is intended
'''to contain all SubcategoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class SubcategoryTest

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
    '''A test for getSubcategories
    '''</summary>
    <TestMethod()> _
    Public Sub getSubcategoriesTest()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim UserID As String = "JTurner"
        Dim expected As List(Of BudgeteerObjects.Subcategory) = Nothing
        Dim actual As List(Of BudgeteerObjects.Subcategory)
        actual = target.GetSubcategories(UserID)
        Assert.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for getSubcategories
    '''</summary>
    <TestMethod()> _
    Public Sub getSubcategoriesByCategoryID()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim UserID As String = "JTurner"
        Dim CategoryID As Int64 = 1
        Dim expected As List(Of BudgeteerObjects.Subcategory) = Nothing
        Dim actual As List(Of BudgeteerObjects.Subcategory)
        actual = target.GetSubcategoriesByCategoryID(CategoryID, UserID)
        Assert.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for getSubcategories
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null User ID was inappropriately allowed")> _
    Public Sub getSubcategoriesUserIDNothing()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim UserID As String = Nothing
        target.GetSubcategories(UserID)
    End Sub


    '''<summary>
    '''A test for getSubcategory
    '''</summary>
    <TestMethod()> _
    Public Sub getSubcategoryTest()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategoryID As ULong = 1
        Dim expected As BudgeteerObjects.Subcategory = Nothing
        Dim actual As BudgeteerObjects.Subcategory
        actual = target.GetSubcategory(subcategoryID)
        Assert.AreNotEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for getSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null Subcategory ID was inappropriately allowed")> _
    Public Sub getSubcategorySubcategoryIDNothing()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategoryID As ULong = Nothing
        target.GetSubcategory(subcategoryID)
    End Sub

    '''<summary>
    '''A test for addSubcategory
    '''</summary>
    <TestMethod()> _
    Public Sub addSubcategoryTest()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        Dim inserted_row As Integer
        subcategory.CategoryID = 1
        subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = " "
        subcategory.UserID = "JTurner"
        inserted_row = target.AddSubcategory(subcategory)
        Assert.IsTrue(inserted_row > 0)
    End Sub

    '''<summary>
    '''A test for addSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null subcategory was inappropriately allowed")> _
    Public Sub addSubcategorySubcategoryNothing()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = Nothing
        target.AddSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for addSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null Category ID was inappropriately allowed")> _
    Public Sub addSubcategoryCategoryIDNothing()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        subcategory.CategoryID = Nothing
        subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = Nothing
        subcategory.UserID = "JTurner"
        target.AddSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for addSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null Description was inappropriately allowed")> _
    Public Sub addSubcategoryDescriptionNothing()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        subcategory.CategoryID = 1
        subcategory.Description = Nothing
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = Nothing
        subcategory.UserID = "JTurner"
        target.AddSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for addSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A Description which exceeded the maximum character length was inappropriately allowed")> _
    Public Sub addSubcategoryDescriptionMaxLen()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        subcategory.CategoryID = 1
        subcategory.Description = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = Nothing
        subcategory.UserID = "JTurner"
        target.AddSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for addSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A sinking fund value other than 'Y' or 'N' was inappropriately allowed")> _
    Public Sub addSubcategorySinkingFundNotYOrN()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        subcategory.CategoryID = 1
        subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        subcategory.SinkingFund = Nothing
        subcategory.Balance = Nothing
        subcategory.Notes = Nothing
        subcategory.UserID = "JTurner"
        target.AddSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for addSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Notes exeeceded the maximum length of characters allowed")> _
    Public Sub addSubcategoryNotesMaxLen()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        subcategory.CategoryID = 1
        subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        subcategory.UserID = "JTurner"
        target.AddSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for addSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null User ID was inappropriately allowed")> _
    Public Sub addSubcategoryUserIDNothing()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        subcategory.CategoryID = 1
        subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = Nothing
        subcategory.UserID = Nothing
        target.AddSubcategory(subcategory)
    End Sub


    '''<summary>
    '''A test for updateSubcategory
    '''</summary>
    <TestMethod()> _
    Public Sub updateSubcategoryTest()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        Dim expected As Integer = 1
        Dim actual As Integer

        subcategory.SubcategoryID = 1
        subcategory.CategoryID = 1
        subcategory.Description = "Savannah Allowance"
        subcategory.Type = "Expense"
        subcategory.SinkingFund = True
        subcategory.Balance = 5
        subcategory.Notes = Nothing
        subcategory.UserID = "JTurner"

        actual = target.UpdateSubcategory(subcategory)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for updateSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null subcategory was inappropriately allowed")> _
    Public Sub updateSubcategorySubcategoryNothing()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = Nothing
        target.UpdateSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for updateSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null subcategory ID was inappropriately allowed")> _
    Public Sub updateSubcategorySubcategoryIDNothing()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory

        subcategory.SubcategoryID = Nothing
        subcategory.CategoryID = 1
        subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = Nothing

        target.UpdateSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for updateSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null category ID was inappropriately allowed")> _
    Public Sub updateSubcategoryCategoryIDNothing()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        subcategory.SubcategoryID = 1
        subcategory.CategoryID = Nothing
        subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = Nothing
        target.UpdateSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for updateSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null Description was inappropriately allowed")> _
    Public Sub updateSubcategoryDescriptionNothing()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        subcategory.SubcategoryID = 1
        subcategory.CategoryID = 1
        subcategory.Description = Nothing
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = Nothing
        target.UpdateSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for updateSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Description exceeded the maximum allowable characters")> _
    Public Sub updateSubcategoryDescriptionMaxLen()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        subcategory.SubcategoryID = 1
        subcategory.CategoryID = 1
        subcategory.Description = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = Nothing
        target.UpdateSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for updateSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A sinking fund value other than 'Y' or 'N' was inappropriately allowed")> _
    Public Sub updateSubcategorySinkingFundNotYOrN()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        subcategory.SubcategoryID = 1
        subcategory.CategoryID = 1
        subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        subcategory.SinkingFund = Nothing
        subcategory.Balance = Nothing
        subcategory.Notes = Nothing
        target.UpdateSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for updateSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Notes exeeceded the maximum length of characters allowed")> _
    Public Sub updateSubcategoryNotesMaxLen()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        subcategory.CategoryID = 1
        subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        subcategory.UserID = "JTurner"
        target.UpdateSubcategory(subcategory)
    End Sub

    '''<summary>
    '''A test for deleteSubcategory
    '''</summary>
    <TestMethod()> _
    Public Sub deleteSubcategoryTest()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
        Dim expected As Integer = 1
        Dim actual As Integer
        Dim inserted_row As Integer = 0

        subcategory.CategoryID = 1
        subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        subcategory.SinkingFund = False
        subcategory.Balance = Nothing
        subcategory.Notes = Nothing
        subcategory.UserID = "JTurner"

        Try
            inserted_row = target.AddSubcategory(subcategory)
            If inserted_row = 0 Then
                Assert.Fail("Failed to insert new subcategory")
            End If
        Catch ex As Exception
            Assert.Fail("Failed to insert new subcategory")
        End Try

        actual = target.DeleteSubcategory(inserted_row)
        Assert.AreEqual(expected, actual) 'Deleted successfully

    End Sub

    '''<summary>
    '''A test for deleteSubcategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null Subcategory ID was inappropriately allowed")> _
    Public Sub deleteSubcategorySubcategoryIDNothing()
        Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
        Dim subcategoryID As Int64 = Nothing
        target.DeleteSubcategory(subcategoryID)
    End Sub

    ''''<summary>
    ''''A test for adjustSubcategoryBalance
    ''''</summary>
    '<TestMethod()> _
    'Public Sub adjustSubcategoryBalancePositiveAmount()
    '    Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
    '    Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
    '    Dim amount As [Decimal] = 5.13
    '    Dim expected As [Decimal] = 8.0
    '    Dim actual As [Decimal]

    '    subcategory.SubcategoryID = 1
    '    subcategory.CategoryID = 1
    '    subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
    '    subcategory.SinkingFund = "N"
    '    subcategory.Balance = 2.87
    '    subcategory.Notes = Nothing
    '    subcategory.UserID = "JTurner"

    '    target.adjustSubcategoryBalance(subcategory, amount)

    '    'Re-Retrieve the subcategory
    '    subcategory = target.GetSubcategory(subcategory.SubcategoryID)
    '    actual = subcategory.Balance
    '    Assert.AreEqual(expected, actual)

    'End Sub

    ''''<summary>
    ''''A test for adjustSubcategoryBalance
    ''''</summary>
    '<TestMethod()> _
    'Public Sub adjustSubcategoryBalanceNegativeAmount()
    '    Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
    '    Dim subcategory As BudgeteerObjects.Subcategory = New BudgeteerObjects.Subcategory
    '    Dim amount As [Decimal] = -3
    '    Dim expected As [Decimal] = -7
    '    Dim actual As [Decimal]

    '    subcategory.SubcategoryID = 1
    '    subcategory.CategoryID = 1
    '    subcategory.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
    '    subcategory.SinkingFund = "N"
    '    subcategory.Balance = -4
    '    subcategory.Notes = Nothing
    '    subcategory.UserID = "JTurner"

    '    target.adjustSubcategoryBalance(subcategory, amount)

    '    'Re-Retrieve the subcategory
    '    subcategory = target.GetSubcategory(subcategory.SubcategoryID)
    '    actual = subcategory.Balance
    '    Assert.AreEqual(expected, actual)

    'End Sub

    ''''<summary>
    ''''A test for adjustSubcategoryBalance
    ''''</summary>
    '<TestMethod()> _
    '<ExpectedException(GetType(ArgumentException), "A null subcategory was inappropriately allowed")> _
    'Public Sub adjustSubcategorySubcategoryNothing()
    '    Dim target As BudgeteerBusiness.Subcategory = New BudgeteerBusiness.Subcategory
    '    Dim subcategory As BudgeteerObjects.Subcategory = Nothing
    '    Dim amount As Decimal = Nothing
    '    target.adjustSubcategoryBalance(subcategory, amount)
    'End Sub

End Class
