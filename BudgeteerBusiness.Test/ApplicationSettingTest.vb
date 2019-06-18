Imports System.Collections.Generic
Imports BudgeteerObjects
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports BudgeteerBusiness


'''<summary>
'''This is a test class for ApplicationSettingTest and is intended
'''to contain all ApplicationSettingTest Unit Tests
'''</summary>
<TestClass()> _
Public Class ApplicationSettingTest

    Private testContextInstance As TestContext
    Private longText As String = "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT." & _
                         "THIS IS GOING TO BE AN ABNOXIOUSLY LONG STRING OF TEXT."
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
    '''A test for getSettings
    '''</summary>
    <TestMethod()> _
    Public Sub getSettingsTest()
        Dim target As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim userID As String = "JTurner"
        Dim expected As List(Of BudgeteerObjects.ApplicationSetting) = Nothing
        Dim actual As List(Of BudgeteerObjects.ApplicationSetting)
        actual = target.getSettings(userID)
        Assert.AreNotEqual(expected, actual)

        userID = "NonExistingAcc"
        actual = target.getSettings(userID)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for getSettings
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null user ID was inappropriately allowed")> _
        Public Sub getSettingsUserIDBlank()
        Dim target As ApplicationSetting = New ApplicationSetting
        Dim userID As String = Nothing
        target.getSettings(userID)
    End Sub

    '''<summary>
    '''A test for getSetting
    '''</summary>
    <TestMethod()> _
    Public Sub getSettingTest()
        Dim target As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim userID As String = "JTurner"
        Dim setting As String = "Auto-Copy Budgets"
        Dim defaultVal As String = "DEFAULT VALUE"
        Dim expected As String = "ON"
        Dim actual As String

        actual = target.getSetting(userID, setting, defaultVal)
        Assert.AreEqual(expected, actual)

        setting = "Non-Existing Setting"
        expected = "DEFAULT VALUE"
        actual = target.getSetting(userID, setting, defaultVal)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for getSetting
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null user ID was inappropriately allowed")> _
    Public Sub getSettingUserIDBlank()
        Dim target As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim userID As String = Nothing
        Dim setting As String = "Auto-Copy Budgets"
        Dim defaultVal As String = "DEFAULT VALUE"
        target.getSetting(userID, setting, defaultVal)
    End Sub

    '''<summary>
    '''A test for getSetting
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null setting was inappropriately allowed")> _
    Public Sub getSettingSettingBlank()
        Dim target As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim userID As String = "JTurner"
        Dim setting As String = Nothing
        Dim defaultVal As String = "DEFAULT VALUE"
        target.getSetting(userID, setting, defaultVal)
    End Sub

    '''<summary>
    '''A test for setSetting
    '''</summary>
    <TestMethod()> _
    Public Sub setSettingTest()
        Dim target As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim userID As String = "JTurner"
        Dim setting As String = "Test Setting"
        Dim value As String = "on"
        Dim expected As Integer = 1
        Dim actual As Integer
        actual = target.setSetting(userID, setting, value)
        Assert.AreEqual(expected, actual)
    End Sub

    '''<summary>
    '''A test for setSetting
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null user ID was inappropriately allowed")> _
    Public Sub setSettingUserIDBlank()
        Dim target As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim userID As String = Nothing
        Dim setting As String = "Test Setting"
        Dim value As String = "Test Value"
        target.setSetting(userID, setting, value)
    End Sub

    '''<summary>
    '''A test for setSetting
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "A null setting was inappropriately allowed")> _
    Public Sub setSettingSettingBlank()
        Dim target As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim userID As String = "JTurner"
        Dim setting As String = Nothing
        Dim value As String = "Test Value"
        target.setSetting(userID, setting, value)
    End Sub

    '''<summary>
    '''A test for setSetting
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "An over-maximum length setting was inappropriately allowed")> _
    Public Sub setSettingSettingMaxLen()
        Dim target As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim userID As String = "JTurner"
        Dim setting As String = longText
        Dim value As String = "Test Value"
        target.setSetting(userID, setting, value)
    End Sub

    '''<summary>
    '''A test for setSetting
    '''</summary>
    <TestMethod()> _
    <ExpectedException(GetType(ArgumentException), "An over-maximum length value was inappropriately allowed")> _
    Public Sub setSettingValueMaxLen()
        Dim target As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim userID As String = "JTurner"
        Dim setting As String = "Test Setting"
        Dim value As String = longText
        target.setSetting(userID, setting, value)
    End Sub

    '''<summary>
    '''A test for setSetting
    '''</summary>
    <TestMethod()> _
    Public Sub setSettingValueBlank()
        Dim target As BudgeteerBusiness.ApplicationSetting = New BudgeteerBusiness.ApplicationSetting
        Dim userID As String = "JTurner"
        Dim setting As String = "Test Setting 2"
        Dim value As String = String.Empty
        Dim expected As Integer = 1
        Dim actual As Integer
        actual = target.setSetting(userID, setting, value)
        Assert.AreEqual(expected, actual)
    End Sub

End Class
