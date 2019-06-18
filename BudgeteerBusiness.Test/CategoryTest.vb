Imports System.Collections.Generic

Imports BudgeteerObjects

Imports Microsoft.VisualStudio.TestTools.UnitTesting

Imports BudgeteerBusiness



'''<summary>
'''This is a test class for CategoryTest and is intended
'''to contain all CategoryTest Unit Tests
'''</summary>
<TestClass()> _
Public Class CategoryTest


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
    '''A test for getCategories
    '''</summary>
    <TestMethod()> _
    Public Sub getCategoriesTest()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim userID As String = "JTurner"
        Dim expected As List(Of BudgeteerObjects.Category) = Nothing
        Dim actual As List(Of BudgeteerObjects.Category)
        actual = target.getCategories(userID)
        Assert.AreNotEqual(expected, actual)

        userID = "NonExisting"
        actual = target.getCategories(userID)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for getCategories
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null user ID was inappropriately allowed")> _
    Public Sub getCategoriesUserIDNothing()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim userID As String = Nothing
        target.getCategories(userID)
    End Sub



    '''<summary>
    '''A test for addCategory
    '''</summary>
    <TestMethod()> _
    Public Sub addCategoryTest()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim category As BudgeteerObjects.Category = New BudgeteerObjects.Category
        Dim row_inserted As Integer

        category.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        category.UserID = "JTurner"

        row_inserted = target.addCategory(category)

        Assert.IsTrue(row_inserted > 0)
    End Sub

    '''<summary>
    '''A test for addCategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "An null category was inappriopriately allowed")> _
    Public Sub addCategoryNothing()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim category As BudgeteerObjects.Category = Nothing
        target.addCategory(category)
    End Sub

    '''<summary>
    '''A test for addCategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "An null category description was inappriopriately allowed")> _
    Public Sub addCategoryDescriptionNothing()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim category As BudgeteerObjects.Category = New BudgeteerObjects.Category
        category.Description = Nothing
        category.UserID = "JTurner"
        target.addCategory(category)
    End Sub

    '''<summary>
    '''A test for addCategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "Description exeeceded the maximum length of characters allowed")> _
    Public Sub addCategoryDescriptionMaxLen()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim category As BudgeteerObjects.Category = New BudgeteerObjects.Category
        category.Description = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
        category.UserID = "JTurner"
        target.addCategory(category)
    End Sub

    '''<summary>
    '''A test for addCategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "An null user ID was inappriopriately allowed")> _
    Public Sub addCategoryUserIDNothing()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim category As BudgeteerObjects.Category = New BudgeteerObjects.Category
        category.Description = "Something"
        category.UserID = Nothing
        target.addCategory(category)
    End Sub


    '''<summary>
    '''A test for updateCategory
    '''</summary>
    <TestMethod()> _
    Public Sub updateCategoryTest()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim category As BudgeteerObjects.Category = New BudgeteerObjects.Category
        Dim expected As Integer = 1
        Dim actual As Integer

        category.CategoryID = 1
        category.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        category.UserID = "JTurner"

        actual = target.updateCategory(category)
        Assert.AreEqual(expected, actual)

        category.UserID = "NonExisting"
        expected = 0
        actual = target.updateCategory(category)
        Assert.AreEqual(expected, actual)

    End Sub

    '''<summary>
    '''A test for updateCategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null category was inappriopriately allowed")> _
    Public Sub updateCategoryNothing()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim category As BudgeteerObjects.Category = Nothing
        target.updateCategory(category)
    End Sub

    '''<summary>
    '''A test for updateCategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null category ID was inappriopriately allowed")> _
    Public Sub updateCategoryCategoryIDNothing()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim category As BudgeteerObjects.Category = New BudgeteerObjects.Category
        category.CategoryID = Nothing
        category.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        category.UserID = "JTurner"
        target.updateCategory(category)
    End Sub

    '''<summary>
    '''A test for updateCategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null description was inappriopriately allowed")> _
    Public Sub updateCategoryDescriptionNothing()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim category As BudgeteerObjects.Category = New BudgeteerObjects.Category
        category.CategoryID = 1
        category.Description = Nothing
        category.UserID = "JTurner"
        target.updateCategory(category)
    End Sub

    '''<summary>
    '''A test for updateCategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "An null user ID was inappriopriately allowed")> _
    Public Sub updateCategoryUserIDNothing()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim category As BudgeteerObjects.Category = New BudgeteerObjects.Category
        category.CategoryID = 1
        category.Description = "Something"
        category.UserID = Nothing
        target.updateCategory(category)
    End Sub


    '''<summary>
    '''A test for deleteCategory
    '''</summary>
    <TestMethod()> _
    Public Sub deleteCategoryTest()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim category As BudgeteerObjects.Category = New BudgeteerObjects.Category
        Dim categoryID As ULong = 0
        Dim expected As Integer = 1
        Dim actual As Integer
        Dim inserted_row As Integer = 0

        category.Description = Date.UtcNow.ToString & " " & Date.UtcNow.Millisecond.ToString
        category.UserID = "JTurner"
        Try
            inserted_row = target.addCategory(category)
        Catch ex As Exception
            Assert.Fail("Could not insert new category")
        End Try

        If inserted_row > 0 Then
            actual = target.deleteCategory(inserted_row)
            Assert.AreEqual(expected, actual)
        Else
            Assert.Fail("Could not insert new category")
        End If
        
    End Sub

    '''<summary>
    '''A test for deleteCategory
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "An null category ID was inappriopriately allowed")> _
    Public Sub deleteCategoryCategoryIDNothing()
        Dim target As BudgeteerBusiness.Category = New BudgeteerBusiness.Category
        Dim categoryID As Int64 = Nothing
        target.deleteCategory(categoryID)
    End Sub

End Class
