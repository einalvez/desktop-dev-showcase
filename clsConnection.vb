
'Created by: Ely Inalvez 00792
'Date Start: 8-8-2023


Option Explicit On
Imports System.Data.OleDb

Imports ADODB

Public Class clsConnection1

    Dim lvDataSource As String = "SQLSYS"
    'Dim lvDataSource As String = "HV2012SVR"


    Public gvOleDB As OleDbConnection = New OleDbConnection("Provider=SQLOLEDB;Data Source=" & lvDataSource & "; initial catalog=slec;Integrated Security=SSPI")
    Public gvADODB As New ADODB.Connection

    Public Sub New()
        On Error GoTo SubNew_Err

        gvADODB.ConnectionString = "Provider=SQLOLEDB;Data Source=" & lvDataSource & ";Integrated Security=SSPI;Initial Catalog=Slec"
        gvADODB.Open()

SubNew_Err:

        If Err.Number = 0 Then
        Else
            DisplayError("Error on SubNew()", Err.Number, Err.Description)
            TakeScreenShot()
        End If
    End Sub

End Class

