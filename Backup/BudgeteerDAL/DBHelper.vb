Imports System
Imports System.Data
Imports System.Data.Common

#Region " Providers "

Public Enum Providers As Integer
    Odbc = 1
    OleDb = 2
    SqlClient = 3
    OracleClient = 4
    MySql = 5
    SqlAnywhere = 6
End Enum

#End Region

Public Class DbHelper

    Private Shared _factory As Providers = Providers.Odbc
    Private Shared _connectionString As String = "Initial Catalog=RFMLiveDB;Data Source=DAWOOD;User ID=sa;Password=sa;"

    ''' <summary>
    ''' Get or Sets the Connection String
    ''' </summary>
    ''' <value></value>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Public Shared Property ConnectionString() As String
        Get
            Return _connectionString
        End Get
        Set(ByVal value As String)
            _connectionString = value
        End Set
    End Property

    ''' <summary>
    ''' Get Factory By Provider
    ''' </summary>
    ''' <param name="oGetFactory"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetFactoryByProvider(ByVal oGetFactory As Providers) As String
        Select Case CType(oGetFactory, Providers)
            Case Providers.Odbc
                Return "System.Data.Odbc"

            Case Providers.OleDb
                Return "System.Data.OleDb"

            Case Providers.SqlClient
                Return "System.Data.SqlClient"

            Case Providers.OracleClient
                Return "System.Data.OracleClient"

            Case Providers.MySql
                Return "CorLab.MySql.MySqlClient"

            Case Providers.SqlAnywhere
                Return "iAnywhere.Data.SQLAnywhere"

        End Select
        Return ""
    End Function

    ''' <summary>
    ''' Returns a DbProviderFactory based on the private _factory member
    ''' </summary>
    ''' <returns>A DbProviderFactory object specific to the data provider specified internally in this class
    ''' </returns>
    Public Shared Function GetProviderFactory() As DbProviderFactory
        Dim oProviderFactory As DbProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory))
        Return oProviderFactory
    End Function

    ''' <summary>
    ''' Creates a new instance of a System.Data.Commom.dbParameter object.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="type"></param>
    ''' <param name="value"></param>
    ''' <returns>A System.Data.Commom.dbParameter object</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateParameter(ByVal name As String, ByVal type As DbType, ByVal value As Object) As IDataParameter
        Return CreateParameter(name, type, value, ParameterDirection.Input)
    End Function

    ''' <summary>
    ''' Creates a new instance of a System.Data.Commom.dbParameter object.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="type"></param>
    ''' <param name="value"></param>
    ''' <param name="direction"></param>
    ''' <returns>A System.Data.Commom.dbParameter object</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateParameter(ByVal name As String, ByVal type As DbType, ByVal value As Object, ByVal direction As ParameterDirection) As IDataParameter

        Dim param As DbParameter = Nothing

        Dim oProviderFactory As DbProviderFactory = GetProviderFactory()
        Dim Con As DbConnection = oProviderFactory.CreateConnection
        Dim cmd As DbCommand = Con.CreateCommand

        param = cmd.CreateParameter()

        If Not param Is Nothing Then
            param.ParameterName = name
            param.DbType = type
            param.Direction = direction
            param.Value = value
        End If

        Return param
    End Function

    ''' <summary>
    ''' Creates a new instance of a System.Data.Commom.dbParameter object.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="type"></param>
    ''' <param name="value"></param>
    ''' <returns>A System.Data.Commom.dbParameter object</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateParameter(ByVal name As String, ByVal type As DbType, ByVal value As Object, ByRef transaction As DistributedTransaction) As IDataParameter
        Return CreateParameter(name, type, value, ParameterDirection.Input, transaction)
    End Function

    ''' <summary>
    ''' Creates a new instance of a System.Data.Commom.dbParameter object.
    ''' </summary>
    ''' <param name="name"></param>
    ''' <param name="type"></param>
    ''' <param name="value"></param>
    ''' <param name="direction"></param>
    ''' <returns>A System.Data.Commom.dbParameter object</returns>
    ''' <remarks></remarks>
    Public Shared Function CreateParameter(ByVal name As String, ByVal type As DbType, ByVal value As Object, ByVal direction As ParameterDirection, ByRef transaction As DistributedTransaction) As IDataParameter

        If transaction Is Nothing Then
            Throw New ArgumentNullException("Transaction cannot be null")
        End If

        Dim param As DbParameter = Nothing
        Dim Con As DbConnection = transaction.ProviderTransaction.Connection
        Dim cmd As DbCommand = Con.CreateCommand

        param = cmd.CreateParameter()

        If Not param Is Nothing Then
            param.ParameterName = name
            param.DbType = type
            param.Direction = direction
            param.Value = value
        End If

        Return param
    End Function


    ''' <summary>
    ''' Executes a Transact-SQL statement against the connection and returns the number of rows affected.
    ''' </summary>
    ''' <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
    ''' <param name="cmdText">The text of the query.</param>
    ''' <returns></returns>
    ''' <remarks>The number of rows affected.</remarks>
    Public Shared Function ExecuteNonQuery(ByVal cmdType As CommandType, ByVal cmdText As String) As Integer
        Dim cmdParms As DbParameter() = Nothing
        Return ExecuteNonQuery(cmdType, cmdText, cmdParms)
    End Function

    ''' <summary>
    ''' Executes a Transact-SQL statement against the connection and returns the number of rows affected.
    ''' </summary>
    ''' <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
    ''' <param name="cmdText">The text of the query.</param>
    ''' <param name="cmdParms">Set Array of Parameter</param>
    ''' <returns>The number of rows affected.</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteNonQuery(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms As DbParameter()) As Integer

        Dim oProviderFactory As DbProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory))
        Dim Con As DbConnection = oProviderFactory.CreateConnection
        Dim cmd As DbCommand = Con.CreateCommand
        Dim trans As DbTransaction = Nothing

        Try
            Con.ConnectionString = ConnectionString
            cmd.Connection = Con
            cmd.CommandText = cmdText
            cmd.Parameters.Clear()
            cmd.CommandType = cmdType

            If Not (IsNothing(cmdParms)) Then
                Dim param As DbParameter

                For Each param In cmdParms
                    cmd.Parameters.Add(param)
                Next
            End If

            Con.Open()
            trans = Con.BeginTransaction
            cmd.Transaction = trans

            Dim val As Integer = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            trans.Commit()
            Return val

        Catch ex As DbException
            trans.Rollback()
            Throw New Exception("DB Exception " & ex.Message)

        Catch exx As Exception
            trans.Rollback()
            Throw New Exception("ExecuteNonQuery Function", exx)
        Finally
            Con.Close()
            cmd = Nothing
            cmdParms = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Executes a Transact-SQL statement against the connection and returns the number of rows affected.
    ''' </summary>
    ''' <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
    ''' <param name="cmdText">The text of the query.</param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <returns></returns>
    ''' <remarks>The number of rows affected.</remarks>
    Public Shared Function ExecuteNonQuery(ByVal cmdType As CommandType, ByVal cmdText As String, ByRef transaction As DistributedTransaction) As Integer
        Return ExecuteNonQuery(cmdType, cmdText, Nothing, transaction)
    End Function

    ''' <summary>
    ''' Executes a Transact-SQL statement against the connection and returns the number of rows affected.
    ''' </summary>
    ''' <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
    ''' <param name="cmdText">The text of the query.</param>
    ''' <param name="cmdParms">Set Array of Parameter</param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <returns>The number of rows affected.</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteNonQuery(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms As DbParameter(), ByRef transaction As DistributedTransaction) As Integer

        If transaction Is Nothing Then
            Throw New ArgumentNullException("Transaction cannot be null")
        End If

        Dim Con As DbConnection = transaction.ProviderTransaction.Connection
        Dim cmd As DbCommand = Con.CreateCommand
        Dim trans As DbTransaction = transaction.ProviderTransaction

        Try

            PrepareCommand(cmd, Con, cmdType, cmdText, cmdParms)
            cmd.Transaction = trans

            Dim val As Integer = cmd.ExecuteNonQuery()
            cmd.Parameters.Clear()
            Return val

        Catch ex As DbException
            transaction.DisableCommit()
            Throw New Exception("DB Exception " & ex.Message)
        Catch exx As Exception
            transaction.DisableCommit()
            Throw New Exception("ExecuteNonQuery Function", exx)
        Finally
            cmd = Nothing
            cmdParms = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Executes the query, and returns the first column of the first row in the result set returned by the query.
    ''' </summary>
    ''' <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
    ''' <param name="cmdText">The text of the query.</param>
    ''' <returns></returns>
    ''' <remarks>The first column of the first row in the result set, or a null reference if the result set is empty.</remarks>
    Public Shared Function ExecuteScalar(ByVal cmdType As CommandType, ByVal cmdText As String) As Object
        Dim cmdParms As DbParameter() = Nothing
        Return ExecuteScalar(cmdType, cmdText, cmdParms)
    End Function

    ''' <summary>
    ''' Executes the query, and returns the first column of the first row in the result set returned by the query.
    ''' </summary>
    ''' <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
    ''' <param name="cmdText">The text of the query.</param>
    ''' <param name="cmdParms">Set Array of Parameter</param>
    ''' <returns></returns>
    ''' <remarks>The first column of the first row in the result set, or a null reference if the result set is empty.</remarks>
    Public Shared Function ExecuteScalar(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms As DbParameter()) As Object

        Dim oProviderFactory As DbProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory))
        Dim Con As DbConnection = oProviderFactory.CreateConnection
        Dim cmd As DbCommand = Con.CreateCommand
        Dim trans As DbTransaction = Nothing

        Try
            Con.ConnectionString = ConnectionString
            cmd.Connection = Con
            cmd.CommandText = cmdText
            cmd.Parameters.Clear()
            cmd.CommandType = cmdType

            If Not (IsNothing(cmdParms)) Then
                Dim param As DbParameter

                For Each param In cmdParms
                    cmd.Parameters.Add(param)
                Next
            End If

            Con.Open()
            trans = Con.BeginTransaction
            cmd.Transaction = trans

            Dim val As Object = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            trans.Commit()
            Return val

        Catch ex As DbException
            trans.Rollback()
            Throw New Exception("DB Exception " & ex.Message)

        Catch exx As Exception
            trans.Rollback()
            Throw New Exception("ExecuteNonQuery Function", exx)
        Finally
            Con.Close()
            cmd = Nothing
            cmdParms = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Executes the query, and returns the first column of the first row in the result set returned by the query.
    ''' </summary>
    ''' <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
    ''' <param name="cmdText">The text of the query.</param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <returns></returns>
    ''' <remarks>The first column of the first row in the result set, or a null reference if the result set is empty.</remarks>
    Public Shared Function ExecuteScalar(ByVal cmdType As CommandType, ByVal cmdText As String, ByRef transaction As DistributedTransaction) As Object

        If transaction Is Nothing Then
            Throw New ArgumentNullException("Transaction cannot be null")
        End If

        Return ExecuteScalar(cmdType, cmdText, Nothing, transaction)
    End Function

    ''' <summary>
    ''' Executes the query, and returns the first column of the first row in the result set returned by the query.
    ''' </summary>
    ''' <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
    ''' <param name="cmdText">The text of the query.</param>
    ''' <param name="cmdParms">Set Array of Parameter</param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <returns></returns>
    ''' <remarks>The first column of the first row in the result set, or a null reference if the result set is empty.</remarks>
    Public Shared Function ExecuteScalar(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms As DbParameter(), ByRef transaction As DistributedTransaction) As Object

        If transaction Is Nothing Then
            Throw New ArgumentNullException("Transaction cannot be null")
        End If

        Dim Con As DbConnection = transaction.ProviderTransaction.Connection
        Dim cmd As DbCommand = Con.CreateCommand
        Dim trans As DbTransaction = transaction.ProviderTransaction

        Try
            PrepareCommand(cmd, Con, cmdType, cmdText, cmdParms)
            cmd.Transaction = trans
            Dim val As Object = cmd.ExecuteScalar()
            cmd.Parameters.Clear()
            Return val

        Catch ex As DbException
            transaction.DisableCommit()
            Throw New Exception("DB Exception " & ex.Message)
        Catch exx As Exception
            transaction.DisableCommit()
            Throw New Exception("ExecuteNonQuery Function", exx)
        Finally
            cmd = Nothing
            cmdParms = Nothing
        End Try
    End Function

    ''' <summary>
    ''' ExecuteTable Return DataTable
    ''' </summary>
    ''' <param name="cmdType">The command Type</param>
    ''' <param name="cmdText">The command text to execute</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteTable(ByVal cmdType As CommandType, ByVal cmdText As String) As DataTable
        Dim cmdParms As DbParameter() = Nothing
        Return ExecuteTable(cmdType, cmdText, cmdParms)
    End Function

    ''' <summary>
    ''' ExecuteTable Return DataTable
    ''' </summary>
    ''' <param name="cmdType">The command Type</param>
    ''' <param name="cmdText">The command text to execute</param>
    ''' <param name="cmdParms">Array of Parameters</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteTable(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms As DbParameter()) As DataTable
        Dim oProviderFactory As DbProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory))
        Dim oDataAdapter As DbDataAdapter
        Dim Con As DbConnection = oProviderFactory.CreateConnection
        Dim cmd As DbCommand
        Try
            Con.ConnectionString = ConnectionString
            cmd = Con.CreateCommand
            PrepareCommand(cmd, Con, cmdType, cmdText, cmdParms)
            Con.Open()
            oDataAdapter = oProviderFactory.CreateDataAdapter
            Dim oDataTable As New DataTable
            oDataAdapter.SelectCommand = cmd
            oDataAdapter.Fill(oDataTable)
            cmd.Parameters.Clear()
            Return oDataTable
        Catch ex As DbException
            Throw New Exception("DB Exception ", ex)
        Catch exx As Exception
            Throw New Exception("ExecuteTable Exception :", exx)
        Finally
            Con.Close()
            cmd = Nothing
            oDataAdapter = Nothing
        End Try
    End Function

    ''' <summary>
    ''' ExecuteTable Return DataTable
    ''' </summary>
    ''' <param name="cmdType">The command Type</param>
    ''' <param name="cmdText">The command text to execute</param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteTable(ByVal cmdType As CommandType, ByVal cmdText As String, ByRef transaction As DistributedTransaction) As DataTable
        Return ExecuteTable(cmdType, cmdText, Nothing, transaction)
    End Function

    ''' <summary>
    ''' ExecuteTable Return DataTable
    ''' </summary>
    ''' <param name="cmdType">The command Type</param>
    ''' <param name="cmdText">The command text to execute</param>
    ''' <param name="cmdParms">Array of Parameters</param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteTable(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms As DbParameter(), ByRef transaction As DistributedTransaction) As DataTable

        If transaction Is Nothing Then
            Throw New ArgumentNullException("Transaction cannot be null")
        End If

        Dim oProviderFactory As DbProviderFactory = GetProviderFactory()
        Dim oDataAdapter As DbDataAdapter
        Dim Con As DbConnection = transaction.ProviderTransaction.Connection
        Dim cmd As DbCommand = Con.CreateCommand

        Try
            PrepareCommand(cmd, Con, cmdType, cmdText, cmdParms)
            cmd.Transaction = transaction.ProviderTransaction
            oDataAdapter = oProviderFactory.CreateDataAdapter
            Dim oDataTable As New DataTable
            oDataAdapter.SelectCommand = cmd
            oDataAdapter.Fill(oDataTable)
            cmd.Parameters.Clear()
            Return oDataTable
        Catch ex As DbException
            transaction.DisableCommit()
            Throw New Exception("DB Exception ", ex)
        Catch exx As Exception
            transaction.DisableCommit()
            Throw New Exception("ExecuteTable Exception :", exx)
        Finally
            cmd = Nothing
            oDataAdapter = Nothing
        End Try
    End Function


    ''' <summary>
    ''' <para>Executes the <paramref name="commandText"/> as part of the given <paramref name="transaction" /> and returns the results in a new <see cref="DataSet"/>.</para>
    ''' </summary>
    ''' <param name="cmdType"></param>
    ''' <param name="cmdText">The command text to execute.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteDataSet(ByVal cmdType As CommandType, ByVal cmdText As String) As DataSet
        Dim cmdParms As DbParameter() = Nothing
        Return ExecuteDataSet(cmdType, cmdText, cmdParms)
    End Function

    ''' <summary>
    ''' <para>Executes the <paramref name="commandText"/> as part of the given <paramref name="transaction" /> and returns the results in a new <see cref="DataSet"/>.</para>
    ''' </summary>
    ''' <param name="cmdType">One of the <see cref="CommandType"/> values.</param>
    ''' <param name="cmdText">The command text to execute.</param>
    ''' <param name="cmdParms"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteDataSet(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms As DbParameter()) As DataSet
        Dim oProviderFactory As DbProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory))
        Dim con As DbConnection = oProviderFactory.CreateConnection
        Dim oDataAdapter As DbDataAdapter = oProviderFactory.CreateDataAdapter
        Dim cmd As DbCommand = con.CreateCommand

        Try
            con.ConnectionString = ConnectionString
            cmd = con.CreateCommand
            PrepareCommand(cmd, con, cmdType, cmdText, cmdParms)
            con.Open()
            oDataAdapter = oProviderFactory.CreateDataAdapter
            Dim oDataSet As New DataSet
            oDataAdapter.SelectCommand = cmd
            oDataAdapter.Fill(oDataSet)
            cmd.Parameters.Clear()

            Return oDataSet

        Catch ex As DbException

            Throw New Exception("SQL Exception ", ex)
        Catch exx As Exception
            Throw New Exception("Execute DataSet", exx)
        Finally
            con.Close()
            cmd = Nothing
            oDataAdapter = Nothing
        End Try
    End Function

    ''' <summary>
    ''' <para>Executes the <paramref name="commandText"/> as part of the given <paramref name="transaction" /> and returns the results in a new <see cref="DataSet"/>.</para>
    ''' </summary>
    ''' <param name="cmdType"></param>
    ''' <param name="cmdText">The command text to execute.</param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteDataSet(ByVal cmdType As CommandType, ByVal cmdText As String, ByRef transaction As DistributedTransaction) As DataSet
        Return ExecuteDataSet(cmdType, cmdText, Nothing, transaction)
    End Function

    ''' <summary>
    ''' <para>Executes the <paramref name="commandText"/> as part of the given <paramref name="transaction" /> and returns the results in a new <see cref="DataSet"/>.</para>
    ''' </summary>
    ''' <param name="cmdType">One of the <see cref="CommandType"/> values.</param>
    ''' <param name="cmdText">The command text to execute.</param>
    ''' <param name="cmdParms"></param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteDataSet(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms As DbParameter(), ByRef transaction As DistributedTransaction) As DataSet

        If transaction Is Nothing Then
            Throw New ArgumentNullException("Transaction cannot be null")
        End If

        Dim oProviderFactory As DbProviderFactory = GetProviderFactory()
        Dim oDataAdapter As DbDataAdapter
        Dim Con As DbConnection = transaction.ProviderTransaction.Connection
        Dim cmd As DbCommand = Con.CreateCommand

        Try
            PrepareCommand(cmd, Con, cmdType, cmdText, cmdParms)
            cmd.Transaction = transaction.ProviderTransaction
            oDataAdapter = oProviderFactory.CreateDataAdapter
            Dim oDataSet As New DataSet
            oDataAdapter.SelectCommand = cmd
            oDataAdapter.Fill(oDataSet)
            cmd.Parameters.Clear()

            Return oDataSet

        Catch ex As DbException
            transaction.DisableCommit()
            Throw New Exception("SQL Exception ", ex)
        Catch exx As Exception
            transaction.DisableCommit()
            Throw New Exception("Execute DataSet", exx)
        Finally
            cmd = Nothing
            oDataAdapter = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Sends the System.Data.Common.DbCommand.CommandText to the System.Data.Common.DbCommand.Connection and builds a System.Data.Common.DbDataReader.
    ''' </summary>
    ''' <param name="conn">A System.Data.Common.DbConnection that represents the connection to an instance of DataSource.</param>
    ''' <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
    ''' <param name="cmdText">The text of the query.</param>
    ''' <returns>A System.Data.Common.DbDataReader object.</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByRef conn As DbConnection, ByVal cmdType As CommandType, ByVal cmdText As String) As DbDataReader
        Dim cmdParms As DbParameter() = Nothing
        Return ExecuteReader(conn, cmdType, cmdText, cmdParms)
    End Function

    ''' <summary>
    ''' Sends the System.Data.Common.DbCommand.CommandText to the System.Data.Common.DbCommand.Connection and builds a System.Data.Common.DbDataReader.
    ''' </summary>
    ''' <param name="conn">A System.Data.Common.DbConnection that represents the connection to an instance of DataSource.</param>
    ''' <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
    ''' <param name="cmdText">The text of the query.</param>
    ''' <param name="cmdParms">Set Array of Parameter</param>
    ''' <returns>A System.Data.Common.DbDataReader object.</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByRef conn As DbConnection, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms As DbParameter()) As DbDataReader

        Dim oProviderFactory As DbProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory))
        conn = oProviderFactory.CreateConnection

        Dim oDataAdapter As DbDataAdapter = oProviderFactory.CreateDataAdapter
        Dim cmd As DbCommand = conn.CreateCommand

        Dim rdr As DbDataReader

        Try
            PrepareCommand(cmd, conn, cmdType, cmdText, cmdParms)
            conn.Open()
            rdr = cmd.ExecuteReader()
            cmd.Parameters.Clear()

            If Not (IsNothing(cmdParms)) Then
                Dim param As DbParameter

                For Each param In cmdParms
                    cmd.Parameters.Add(param)
                Next
            End If

            Return rdr

        Catch ex As DbException

            Throw New Exception("SQL Exception ", ex)
        Catch exx As Exception
            Throw New Exception("ExecuteReader", exx)
        Finally
            cmd = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Sends the System.Data.Common.DbCommand.CommandText to the System.Data.Common.DbCommand.Connection and builds a System.Data.Common.DbDataReader.
    ''' </summary>
    ''' <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
    ''' <param name="cmdText">The text of the query.</param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <returns>A System.Data.Common.DbDataReader object.</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByVal cmdType As CommandType, ByVal cmdText As String, ByRef transaction As DistributedTransaction) As DbDataReader
        Return ExecuteReader(cmdType, cmdText, Nothing, transaction)
    End Function

    ''' <summary>
    ''' Sends the System.Data.Common.DbCommand.CommandText to the System.Data.Common.DbCommand.Connection and builds a System.Data.Common.DbDataReader.
    ''' </summary>
    ''' <param name="cmdType">Set the Transact-SQL statement or stored procedure to execute at the data source.</param>
    ''' <param name="cmdText">The text of the query.</param>
    ''' <param name="cmdParms">Set Array of Parameter</param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <returns>A System.Data.Common.DbDataReader object.</returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteReader(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms As DbParameter(), ByRef transaction As DistributedTransaction) As DbDataReader

        Dim oProviderFactory As DbProviderFactory = GetProviderFactory()
        Dim oDataAdapter As DbDataAdapter = oProviderFactory.CreateDataAdapter
        Dim conn As DbConnection = transaction.ProviderTransaction.Connection
        Dim cmd As DbCommand = conn.CreateCommand

        Dim rdr As DbDataReader

        Try
            PrepareCommand(cmd, conn, cmdType, cmdText, cmdParms)
            cmd.Transaction = transaction.ProviderTransaction
            rdr = cmd.ExecuteReader()
            cmd.Parameters.Clear()

            If Not (IsNothing(cmdParms)) Then
                Dim param As DbParameter

                For Each param In cmdParms
                    cmd.Parameters.Add(param)
                Next
            End If

            Return rdr

        Catch ex As DbException
            transaction.DisableCommit()
            Throw New Exception("SQL Exception ", ex)
        Catch exx As Exception
            transaction.DisableCommit()
            Throw New Exception("ExecuteReader", exx)
        Finally
            cmd = Nothing
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="cmdType"></param>
    ''' <param name="cmdText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteRow(ByVal cmdType As CommandType, ByVal cmdText As String) As DataRow
        Dim cmdParms As DbParameter() = Nothing
        Return ExecuteRow(cmdType, cmdText, cmdParms)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="cmdType"></param>
    ''' <param name="cmdText"></param>
    ''' <param name="cmdParms"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteRow(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms As DbParameter()) As DataRow
        Dim oProviderFactory As DbProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory))
        Dim Con As DbConnection = oProviderFactory.CreateConnection
        Con.ConnectionString = ConnectionString
        Dim cmd As DbCommand = Con.CreateCommand
        Dim oDataAdapter As DbDataAdapter = oProviderFactory.CreateDataAdapter
        Dim oDataRow As DataRow = Nothing
        Dim oDataTable As New DataTable
        Try
            PrepareCommand(cmd, Con, cmdType, cmdText, cmdParms)
            Con.Open()
            oDataAdapter.SelectCommand = cmd
            oDataAdapter.Fill(oDataTable)
            cmd.Parameters.Clear()
            If oDataTable.Rows.Count = 0 Then
                Return Nothing
            Else
                Dim oRow As DataRow = oDataTable.Rows(0)
                Return oRow
            End If
        Catch ex As DbException
            Throw New Exception("DB Exception ", ex)
        Catch exx As Exception
            Throw New Exception("ExecuteRow", exx)
        Finally
            Con.Close()
            oDataTable = Nothing
            cmd = Nothing
            oDataAdapter = Nothing
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="cmdType"></param>
    ''' <param name="cmdText"></param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteRow(ByVal cmdType As CommandType, ByVal cmdText As String, ByRef transaction As DistributedTransaction) As DataRow
        Return ExecuteRow(cmdType, cmdText, Nothing, transaction)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="cmdType"></param>
    ''' <param name="cmdText"></param>
    ''' <param name="cmdParms"></param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExecuteRow(ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms As DbParameter(), ByRef transaction As DistributedTransaction) As DataRow

        Dim oProviderFactory As DbProviderFactory = GetProviderFactory()
        Dim oDataAdapter As DbDataAdapter = oProviderFactory.CreateDataAdapter
        Dim Con As DbConnection = transaction.ProviderTransaction.Connection
        Dim cmd As DbCommand = Con.CreateCommand
        Dim oDataRow As DataRow = Nothing
        Dim oDataTable As New DataTable

        Try
            PrepareCommand(cmd, Con, cmdType, cmdText, cmdParms)
            cmd.Transaction = transaction.ProviderTransaction
            oDataAdapter.SelectCommand = cmd
            oDataAdapter.Fill(oDataTable)
            cmd.Parameters.Clear()
            If oDataTable.Rows.Count = 0 Then
                Return Nothing
            Else
                Dim oRow As DataRow = oDataTable.Rows(0)
                Return oRow
            End If
        Catch ex As DbException
            transaction.DisableCommit()
            Throw New Exception("DB Exception ", ex)
        Catch exx As Exception
            transaction.DisableCommit()
            Throw New Exception("ExecuteRow", exx)
        Finally
            oDataTable = Nothing
            cmd = Nothing
            oDataAdapter = Nothing
        End Try
    End Function

    ''' <summary>
    ''' Excute Adapter
    ''' </summary>
    ''' <param name="oTable"></param>
    ''' <param name="cmdText"></param>
    ''' <param name="lngMaxID"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExcuteAdapter(ByVal oTable As DataTable, ByVal cmdText As String, Optional ByRef lngMaxID As Long = 0) As Boolean

        Dim oProviderFactory As DbProviderFactory = DbProviderFactories.GetFactory(GetFactoryByProvider(_factory))
        Dim conn As DbConnection = oProviderFactory.CreateConnection
        conn.ConnectionString = ConnectionString
        Dim oSqlCmd As DbCommand = conn.CreateCommand
        Dim oDataAdapter As DbDataAdapter = oProviderFactory.CreateDataAdapter
        Dim oCmdBuilder As DbCommandBuilder = oProviderFactory.CreateCommandBuilder
        Dim trans As DbTransaction = Nothing
        Try

            If Not conn.State = ConnectionState.Open Then
                conn.Open()
            End If

            trans = conn.BeginTransaction
            oSqlCmd.Transaction = trans

            oSqlCmd.Connection = conn
            oSqlCmd.CommandText = cmdText
            oSqlCmd.CommandType = CommandType.Text

            oDataAdapter.SelectCommand = oSqlCmd
            oCmdBuilder.DataAdapter = oDataAdapter
            oCmdBuilder.GetUpdateCommand()
            oCmdBuilder.GetInsertCommand()
            oCmdBuilder.GetDeleteCommand()
            oDataAdapter.Update(oTable)
            oDataAdapter.SelectCommand.CommandText = "SELECT @@IDENTITY"
            trans.Commit()

            '  lngMaxID = CType(oDataAdapter.SelectCommand.ExecuteScalar(), Long)
            Return True

        Catch ex As DbException
            trans.Rollback()
            Throw New Exception("DB Exception ", ex)

        Catch exx As Exception
            trans.Rollback()
            Throw New Exception("ExeculateAdapter", exx)

        Finally
            If conn.State = ConnectionState.Open Then conn.Close()
            oSqlCmd = Nothing
            oDataAdapter = Nothing
            oCmdBuilder = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Excute Adapter
    ''' </summary>
    ''' <param name="oTable"></param>
    ''' <param name="cmdText"></param>
    ''' <param name="transaction">An instantiated distributed transaction</param>
    ''' <remarks></remarks>
    Public Shared Function ExcuteAdapter(ByVal oTable As DataTable, ByVal cmdText As String, ByRef transaction As DistributedTransaction) As Boolean

        Dim oProviderFactory As DbProviderFactory = GetProviderFactory()
        Dim oDataAdapter As DbDataAdapter = oProviderFactory.CreateDataAdapter
        Dim Con As DbConnection = transaction.ProviderTransaction.Connection
        Dim oSqlCmd As DbCommand = Con.CreateCommand
        Dim trans As DbTransaction = transaction.ProviderTransaction
        Dim oCmdBuilder As DbCommandBuilder = oProviderFactory.CreateCommandBuilder

        Try
            oSqlCmd.Transaction = trans
            oSqlCmd.Connection = Con
            oSqlCmd.CommandText = cmdText
            oSqlCmd.CommandType = CommandType.Text

            oDataAdapter.SelectCommand = oSqlCmd
            oCmdBuilder.DataAdapter = oDataAdapter
            oCmdBuilder.GetUpdateCommand()
            oCmdBuilder.GetInsertCommand()
            oCmdBuilder.GetDeleteCommand()
            oDataAdapter.Update(oTable)
            oDataAdapter.SelectCommand.CommandText = "SELECT @@IDENTITY"

            Return True

        Catch ex As DbException
            transaction.DisableCommit()
            Throw New Exception("DB Exception ", ex)

        Catch exx As Exception
            transaction.DisableCommit()
            Throw New Exception("ExeculateAdapter", exx)

        Finally
            oSqlCmd = Nothing
            oDataAdapter = Nothing
            oCmdBuilder = Nothing
        End Try

    End Function

    ''' <summary>
    ''' Prepare Command
    ''' </summary>
    ''' <param name="cmd">Assigns a <paramref name="connection"/> to the <paramref name="command"/> and discovers parameters if needed.</param>
    ''' <param name="conn">The connection to assign to the command.</param>
    ''' <param name="cmdType">The command that contains the query to prepare.</param>
    ''' <param name="cmdText"></param>
    ''' <param name="cmdParms"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function PrepareCommand(ByRef cmd As DbCommand, ByRef conn As DbConnection, ByRef cmdType As CommandType, ByRef cmdText As String, ByRef cmdParms As DbParameter()) As Boolean
        Try
            cmd.Connection = conn
            cmd.CommandText = cmdText
            cmd.Parameters.Clear()
            cmd.CommandType = cmdType

            If Not (IsNothing(cmdParms)) Then
                Dim param As DbParameter

                For Each param In cmdParms
                    cmd.Parameters.Add(param)
                Next
            End If
            Return True
        Catch ex As DbException
            Throw New Exception("DB Exception ", ex)
        Catch exx As Exception
            Throw New Exception("PrepareCommand : ", exx)
        End Try
    End Function

End Class

Public Class DistributedTransaction
    Implements IDisposable

    Private _transaction As DbTransaction = Nothing
    Private _connection As DbConnection
    Private _disposedValue As Boolean = False        ' To detect redundant calls
    Private _done As Boolean = False
    Private _enabled As Boolean = True

    Public Sub New(ByVal connString As String)
        Try
            Dim providerFactory As DbProviderFactory
            providerFactory = DbHelper.GetProviderFactory()
            _connection = providerFactory.CreateConnection()
            _connection.ConnectionString = connString

            ' Opens the connection
            Try
                _connection.Open()
            Catch ex As Exception
                ' Throw exception with incorrect connString
                Throw New ApplicationException("Unable to open connection." & vbCrLf & connString)
            End Try

            ' Starts the transaction
            _transaction = _connection.BeginTransaction

        Catch ex As Exception

            ' Try to close the connection if it is open
            Try
                _connection.Close()
            Catch ex2 As Exception
            End Try

            Throw
        End Try
    End Sub

    Public ReadOnly Property ProviderTransaction() As DbTransaction
        Get
            ' Check that the object has not been disposed
            If _disposedValue Then
                Throw New ObjectDisposedException("Transaction")
            End If

            ' If object's lifetime has expired, caller cannot access the transaction
            If Me._done Then
                Throw New InvalidOperationException("Transaction has been closed and can no longer be used")
            End If

            ' Returns the transaction object internally referenced
            Return _transaction
        End Get
    End Property

    Public Sub DisableCommit()
        ' Checks that the object has not been disposed
        If _disposedValue Then
            Throw New ObjectDisposedException("Transaction")
        End If
        Me._enabled = False
    End Sub

    Public Sub Commit()


        ' Checks that the object has not been disposed
        If _disposedValue Then
            Throw New ObjectDisposedException("Transaction")
        End If

        ' Checks that a commit or rollback has not been executed
        If _done Then
            Throw New InvalidOperationException("Transaction has already been commited/rolled back")
        End If

        ' Checks that we are enabled and therefore can commit the transaction
        If Not _enabled Then
            Throw New InvalidOperationException("Transaction has commit disabled and cannot be commited.")
        End If

        Try
            ' Commits the transaction
            _transaction.Commit()
        Catch ex As Exception
            Throw
        Finally
            ' Update status because object's lifetime has expired
            _done = True

            ' Always close underlying database connection 
            If Not _transaction.Connection Is Nothing Then
                If _transaction.Connection.State = ConnectionState.Open Then
                    _transaction.Connection.Close()
                End If
                _transaction.Connection.Dispose()
            End If

            ' Disposed transaction is no longer useful
            _transaction.Dispose()
        End Try
    End Sub

    Public Sub Rollback()
        ' Checks that the object has not been disposed
        If _disposedValue Then
            Throw New ObjectDisposedException("Transaction")
        End If

        ' Checks that a commit or rollback has not been executed
        If _done Then
            Throw New InvalidOperationException("Transaction has already been commited/rolled back")
        End If

        Try
            ' Rolls back the transaction
            _transaction.Rollback()
            ' Updates status to disabled
            _enabled = False

        Catch ex As Exception
            Throw
        Finally
            ' Update status because object's lifetime has expired
            _done = True

            ' Always close underlying database connection 
            If Not _transaction.Connection Is Nothing Then
                If _transaction.Connection.State = ConnectionState.Open Then
                    _transaction.Connection.Close()
                End If
                _transaction.Connection.Dispose()
            End If

            ' Disposed transaction is no longer useful
            _transaction.Dispose()
        End Try

    End Sub

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If _disposedValue Then Exit Sub

        If disposing Then

            ' Avoid runtime error when disposing (connction broken, etc.)
            Try
                ' If transaction has not been committed or rolled back we need to close it
                If Not _done Then
                    ' Commits if the object is enabled, otherwise rolls back
                    If Me._enabled Then
                        _transaction.Commit()
                    Else
                        _transaction.Rollback()
                    End If
                End If
            Catch ex As Exception
            End Try

            Try
                ' Important: closes and releases reference to transaction 
                _connection.Close()
                _connection.Dispose()
                _transaction.Dispose()
            Catch ex As Exception
                Debug.WriteLine("Error closing transaction's resources: " & ex.Message)
            End Try
        End If

        _disposedValue = True

    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class