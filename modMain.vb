Option Strict Off
Imports System.Net
Imports System.Environment
Imports System.Net.Mail
Module modMain

    Public con1 As New clsConnection1
    Public formPM As New frmMain
    'Public formlogin As New frmLogin
    Public catchEmedusername As String
    Public gvUsername As String
    Public gvempHash As String
    Public gvempHashSelected As String
    Public gvDesignation As String
    Public gvOfficialName As String
    Public gvEmpno As String
    ' Public gvPassword As String
    Public mvErrorName As String
    Public gvEmpName As String
    Public gveMedUsername As String
    Public gveMedPassword As String
    Public gvAccountinUse As String

    Public gvMyIP As String
    Public gvSortingColumn As ColumnHeader
    Public gvforce As Integer
    Public gvForceFree As Boolean = False
    Public gvForceLocked As Boolean = False
    Public gvAddAccount As Boolean = False
    Public gvEditAccount As Boolean = False

    Public MyNetwork As New Microsoft.VisualBasic.Devices.Network
    Public MyComputer As New Microsoft.VisualBasic.Devices.Computer

    Public hostName As String = System.Net.Dns.GetHostName()
    Public myIPs As IPHostEntry = Dns.GetHostEntry(hostName)

    'Dim lvCreatorID As String, lvPrefCat As String, lvLastName As String, lvFirstName As String, lvMiddleName As String, lvGender As String, lvCivStatus As String, lvmaiden_name As String, lvDOB As String, lvBirthCity As String, lvBirthCountry As String, lvcountry_nationality As String, lvHomeAddress As String
    'Dim lvAddr_Region As String, lvHomeTelNo As String, lvEmailAdd As String, lvPresCountry As String, lvPriorCountry As String, lvPassportNo As String, lvIssueCountry As String, lvIssueDate As String, lvValidDate As String, lvPeti_Name As String, lvPeti_Street As String, lvPeti_City As String
    'Dim lvPeti_State As String, lvPeti_Relation As String, lvUSContactName As String, lvUSContactNo As String, lvNear_Name As String, lvNear_Relation As String, lvNear_Address As String, lvNear_ContactNo As String, lvPortEntry As String, lvDepartDate As String, lvCaseNo As String, lvInterviewDate As String
    'Dim US_NIV As String, NIV_IssueDate As String, NIV_ExpireDate As String, Education As String, Occupation As String, Prev_CXRDate As String, PreferredMedicalExamDate As String, Country As String, RcvDate As String, slec_done As Integer, InterviewSource As String, printhash As String


    'Dim lvSQL As String
    'Dim lvSQL_Command As String = ""
    'Dim lvMySQL_Command As String = ""
    'Dim lvIntvwDate As String
    'Dim lvPrevMedDate As String
    'Dim lvPrevCXRDate As String
    'Dim lvNIV_IssueDate As String
    'Dim lvNIV_ExpireDate As String
    'Dim lvMdlName As String
    'Dim lvIssueDate As String
    'Dim lvValidUntil As String
    'Dim lvSource As String
    'Dim lvDOB As String

    'Dim lvPeti_Zip As String
    'Dim lvHomeAddStreet As String
    'Dim lvHomeAddBrgy As String         'Choy @2023-03-27
    'Dim lvHomeAddCity As String
    'Dim lvHomeAddProvince As String
    'Dim lvHomeAddZip As String
    'Dim lvFAddStreet As String
    'Dim lvFAddCity As String
    'Dim lvFAddProv As String
    'Dim lvFAddCountry As String
    'Dim lvFAddZip As String
    'Dim lvPayCode As String                     'Choy @03-06-2017
    'Dim lvPeti_Email As String                  'Choy @2018-09-19
    'Dim lvPeti_Number As String                 'Choy @2018-09-19
    'Dim lvPrefCat As String                     'Choy @2019-04-15
    'Dim lvEducation As String                   'Choy @2019-04-15
    'Dim lvDepartDate As String                  'Choy @2019-04-16
    'Dim lvUS_NIV As String                      'Choy @2019-04-16
    'Dim lvAddr_Region As String                 'Choy @2019-04-21 Remote
    'Dim lvHomeAddress As String                 'Choy @2019-04-21
    'Dim lvpreferredmedicalexamdate As String    'Choy @2020-04-07
    'Dim lvPriorityTime As String                'Choy @2021-04-07

    'Dim lvMaiden_Name As String                 'Choy @2021-09-27
    'Dim lv1st_Dose As String                    'Choy @2021-09-26 Remote
    'Dim lv2nd_Dose As String                    'Choy @2021-09-26 Remote
    'Dim lvCompanion_DOB As String               'Choy @2021-09-26 Remote

    'Dim lvcovid_vaccine_priority As String      'Choy @2021-09-27
    'Dim lvreceived_vaccine As String            'Choy @2021-09-27
    'Dim lvvaccine_brand As String               'Choy @2021-09-27
    'Dim lvrt_pcr_test As String                 'Choy @2021-09-27
    'Dim lvcompanion As String                   'Choy @2021-09-27
    'Dim lvCompanion_rt_pcr_test As String       'Choy @2021-10-25
    'Dim lvcompanion_visa_applicant As String    'Choy @2021-09-27
    'Dim lvcompanion_last_name As String         'Choy @2021-09-27
    'Dim lvcompanion_first_name As String        'Choy @2021-09-27
    'Dim lvcompanion_middle_name As String       'Choy @2021-09-27
    'Dim lvcompanion_gender As String            'Choy @2021-09-27
    'Dim lvcompanion_civil_status As String      'Choy @2021-09-27
    'Dim lvcompanion_add_street As String        'Choy @2021-09-27
    'Dim lvcompanion_add_city As String          'Choy @2021-09-27
    'Dim lvcompanion_add_province As String      'Choy @2021-09-27
    'Dim lvcompanion_contact_number As String    'Choy @2021-09-27
    'Dim lvcompanion_email_address As String     'Choy @2021-09-27
    'Dim lvrt_pcr_schedule As String             'Choy @2021-09-27
    'Dim lvrt_pcr_paycode As String              'Choy @2021-09-27
    'Dim lvcompanion_paycode As String           'Choy @2021-09-27
    'Dim lvcompanion_maiden_name As String       'Choy @2021-09-27

    'Dim lvSeniorID As String                    'Choy @2021-11-10
    'Dim lvCompanion_SeniorID As String          'Choy @2021-11-10
    'Dim lvCountry_Nationality As String         'Choy @2022-02-10
    'Dim lvVIP As String                         'Choy @2022-03-07
    'Dim lvCareOf As String                      'Choy @2022-03-07
    'Dim lvExpired As String                     'Choy @2022-03-09

    'Dim lv1st_Booster As String                 'Choy @2022-05-13
    'Dim lv2nd_Booster As String                 'Choy @2022-05-13
    'Dim lv1st_Booster_Vax As String             'Choy @2022-05-13
    'Dim lv2nd_Booster_Vax As String             'Choy @2022-05-13


    'Private Sub Add()

    '    Dim lvSQLOnline As String
    '    Dim lvSQL_Command As String


    '    lvSQL_Command = "Select CaseNo, InterviewDate, LastName,FirstName, MiddleName, DOB, " & _
    '                                                                    " Gender, CivStatus, BirthCity, BirthCountry, HomeAdd, HomeTelno, " & _
    '                                                                    " EmailAdd, PresCountry, PriorCountry, PassNo, IssueCountry, IssuedDate,  " & _
    '                                                                    " ValidUntil, Peti_Name, Peti_Stat, Peti_StAdd, Peti_City,Peti_State,  " & _
    '                                                                    " Peti_Relation, USContactName, USContactNo, Near_Name,Near_Relation, Near_Address,  " & _
    '                                                                    " Near_ContactNo, PortEntry, DepartDate, Addr_Region, RcvDate, Reg_Status, EncBy,PrefCat," & _
    '                                                                    " Education,Occupation,PrevMed_Date,PrevCXR_Date,US_NIV,NIV_IssueDate,NIV_ExpireDate, InterviewSource, " & _
    '                                                                    " Peti_Zip,HomeAddStreet,HomeAddBrgy,HomeAddCity,HomeAddProvince,HomeAddZip, " & _
    '                                                                    " ForeignAddStreet,ForeignAddCity,ForeignAddState,ForeignAddCountry,ForeignAddZip, PayCode, Peti_Email, Peti_Number, PreferredMedExamDate, PriorityTime, " & _
    '                                                                    " Maiden_Name, " & _
    '                                                                    " covid_vaccine_priority, received_vaccine, vaccine_brand, " & _
    '                                                                    " first_dose, second_dose, rt_pcr_test, " & _
    '                                                                    " companion, Companion_rt_pcr_test, companion_visa_applicant, companion_last_name, " & _
    '                                                                    " companion_first_name, companion_middle_name, companion_birthdate, " & _
    '                                                                    " companion_gender, companion_civil_status, companion_add_street, " & _
    '                                                                    " companion_add_city, companion_add_province, companion_contact_number, " & _
    '                                                                    " companion_email_address, rt_pcr_schedule, rt_pcr_paycode, companion_paycode, companion_maiden_name, SeniorID, Companion_SeniorID, Country_Nationality, vip, care_of, Expired, " & _
    '                                                                    " Booster1_Brand, Booster1_Date, Booster2_Brand, Booster2_Date) " & _
    '                                                            " from tblPreRegistration_WebDB"

    '    Dim cmdSQL As New OleDb.OleDbCommand(lvSQL_Command, myConn.gvDBConn_SLEC)
    '    cmdSQL.ExecuteNonQuery()

    '    Dim rsOnline As MySqlDataReader = cmdOnline.ExecuteReader
    '    With rsOnline
    '        Do While .Read

    '            lvSQLOnline = " Insert into tblPreReg (CreatorID, PrefCat, LastName, FirstName, MiddleName, " & _
    '                        " Gender, CivStatus, maiden_name, DOB, BirthCity, " & _
    '                        " BirthCountry, country_nationality, HomeAddress, Addr_Region, HomeTelNo, " & _
    '                        " EmailAdd, PresCountry, PriorCountry, PassportNo, IssueCountry, " & _
    '                        " IssueDate, ValidDate, Peti_Name, Peti_Stat, Peti_Street, " & _
    '                        " Peti_City, Peti_State, Peti_Relation, USContactName, USContactNo, " & _
    '                        " Near_Name, Near_Relation, Near_Address, Near_ContactNo, PortEntry, " & _
    '                        " DepartDate, CaseNo, InterviewDate, US_NIV, NIV_IssueDate " & _
    '                        " NIV_ExpireDate, Education, Occupation, Prev_CXRDate, PreferredMedicalExamDate, " & _
    '                        " Country, RcvDate, slec_done, InterviewSource, printhash, " & _
    '                        " PayCode, Peti_Email, Peti_Number, Peti_Zip, HomeAddStreet, " & _
    '                        " HomeAddCity, HomeAddProvince, HomeAddZip, ForeignAddCountry, " & _
    '                        " ForeignAddStreet, ForeignAddState, ForeignAddZip, covid_vaccine_priority, received_vaccine, " & _
    '                        " vaccine_brand, first_dose, second_dose, booster1_brand, booster1, " & _
    '                        " booster2_brand, booster2, senior_citizen_id, priorityTime, ConsentAcknowledged, Expired) " & _
    '                        " Values ( 'NULL', '" & Repl_All(Trim(.Item("PrefCat") & "',  '" & Repl_All(Trim(.Item("LastName") & "',  '" & Repl_All(Trim(.Item("FirstName") & "', '" & Repl_All(Trim(.Item("MiddleName") & "', " & _
    '                        " '" & Repl_All(Trim(.Item("Gender") & "' , '" & Repl_All(Trim(.Item("CivStatus") & "',  '', '" & Repl_All(Trim(.Item("DOB") & "',  '" & Repl_All(Trim(.Item("BirthCity") & "', " & _ 
    '                        " '" & Repl_All(Trim(.Item("BirthCountry") & "', 'NULL', '" & Repl_All(Trim(.Item("HomeAdd") & "', '" & Repl_All(Trim(.Item("Addr_Region") & "', '" & Repl_All(Trim(.Item("HomeTelno") & "', " & _
    '                        " '" & Repl_All(Trim(.Item("EmailAdd") & "', '" & Repl_All(Trim(.Item("PresCountry") & "' , '" & Repl_All(Trim(.Item("PriorCountry") & "', '" & Repl_All(Trim(.Item("PassNo") & "', '" & Repl_All(Trim(.Item("IssueCountry") & "', " & _
    '                        " '" & Repl_All(Trim(.Item("IssuedDate") & "', '" & Repl_All(Trim(.Item("ValidUntil") & "', '" & Repl_All(Trim(.Item("Peti_Name") & "', '" & Repl_All(Trim(.Item("Peti_Stat") & "','" & Repl_All(Trim(.Item("Peti_StAdd") & "', " & _
    '                        " '" & Repl_All(Trim(.Item("Peti_City") & "', '" & Repl_All(Trim(.Item("Peti_State") & "','" & Repl_All(Trim(.Item("Peti_Relation") & "','" & Repl_All(Trim(.Item("USContactName") & "', '" & Repl_All(Trim(.Item("USContactNo") & "', " & _
    '                        " '" & Repl_All(Trim(.Item("Near_Name") & "', '" & Repl_All(Trim(.Item("Near_Relation") & "','" & Repl_All(Trim(.Item("Near_Address") & "', '" & Repl_All(Trim(.Item("Near_ContactNo") & "', '" & Repl_All(Trim(.Item("PortEntry") & "', " & _
    '                        " '" & Repl_All(Trim(.Item("DepartDate") & "', '" & Repl_All(Trim(.Item("CaseNo") & "', '" & Repl_All(Trim(.Item("InterviewDate") & "', '" & Repl_All(Trim(.Item("US_NIV") & "', '" & Repl_All(Trim(.Item("NIV_IssueDate") & "', " & _
    '                        " '" & Repl_All(Trim(.Item("NIV_ExpireDate") & "', '" & Repl_All(Trim(.Item("Education") & "', '" & Repl_All(Trim(.Item("Occupation") & "', '" & Repl_All(Trim(.Item("PrevCXR_Date") & "','" & Repl_All(Trim(.Item("PreferredMedExamDate") & "', " & _
    '                        " '" & Repl_All(Trim(.Item("Addr_Region") & "', '" & Repl_All(Trim(.Item("RcvDate") & "', 0 ,'" & Repl_All(Trim(.Item("InterviewSource") & "', 'NULL' , " & _
    '                        " '" & Repl_All(Trim(.Item("PayCode") & "','" & Repl_All(Trim(.Item("Peti_Email") & "', '" & Repl_All(Trim(.Item("Peti_Number") & "', '" & Repl_All(Trim(.Item("Peti_Zip") & "','" & Repl_All(Trim(.Item("HomeAddStreet") & "', " & _
    '                        "  '" & Repl_All(Trim(.Item("HomeAddCity") & "', '" & Repl_All(Trim(.Item("HomeAddProvince") & "', '" & Repl_All(Trim(.Item("HomeAddZip") & "', '" & Repl_All(Trim(.Item("ForeignAddCountry") & "', " & _
    '                        " '" & Repl_All(Trim(.Item("ForeignAddStreet") & "', '" & Repl_All(Trim(.Item("ForeignAddState") & "', '" & Repl_All(Trim(.Item("ForeignAddZip") & "', '" & Repl_All(Trim(.Item("covid_vaccine_priority") & "', '" & Repl_All(Trim(.Item("received_vaccine") & "', " & _
    '                        " '" & Repl_All(Trim(.Item("vaccine_brand") & "', '" & Repl_All(Trim(.Item("first_dose") & "', '" & Repl_All(Trim(.Item("second_dose") & "', '" & Repl_All(Trim(.Item("Booster1_Brand") & "', '" & Repl_All(Trim(.Item("Booster1_Date") & "', " & _
    '                        " '" & Repl_All(Trim(.Item("Booster2_Brand") & "',  '" & Repl_All(Trim(.Item("Booster2_Date") & "',  '" & Repl_All(Trim(.Item("Companion_SeniorID") & "', '" & Repl_All(Trim(.Item("PriorityTime") & "',  '" & Repl_All(Trim(.Item("care_of") & "', '" & Repl_All(Trim(.Item("Expired") & "'"


    '            Dim cmdOnline As New MySqlCommand(lvSQLOnline, myConn.gvDBConn_Web)
    '            Dim rsOnline As MySqlDataReader = cmdOnline.ExecuteReader
    '            With rsOnline
    '                Do While .Read
    '                    Dim cmdSQL As New OleDb.OleDbCommand(lvSQL_Command, myConn.gvDBConn_SLEC)
    '                    cmdSQL.ExecuteNonQuery()
    '                Loop
    '                .Close()
    '            End With
    '            rsOnline = Nothing



    'End Sub



    Sub main()
        ' normal winforms startup
        Application.EnableVisualStyles()
        Application.Run(New frmMSS_Login)
        ' LaunchNuGetSite()
    End Sub
    Sub LaunchNuGetSite()
        Diagnostics.Process.Start("http://www.nuget.org/List/Packages")
    End Sub
    Public Function Encrypt(ByVal Password As String) As String
        Dim lvRetVal As String
        Dim lvCtr As Integer
        lvRetVal = ""
        For lvCtr = 1 To Microsoft.VisualBasic.Len(Password)
            lvRetVal = lvRetVal & Chr(Asc(Mid(Password, lvCtr, 1)) + (lvCtr * 2))
        Next
        Encrypt = lvRetVal

    End Function

    Public Function Decrypt(ByVal Password As String) As String
        Dim lvRetVal As String
        Dim lvCtr As Integer
        lvRetVal = ""
        For lvCtr = 1 To Microsoft.VisualBasic.Len(Password)
            lvRetVal = lvRetVal & Chr(Asc(Mid(Password, lvCtr, 1)) - (lvCtr * 2))
        Next
        Decrypt = lvRetVal
    End Function

    Public Function Repl(ByVal Str As String) As String
        Repl = Replace(Str, "'", "''")
    End Function
    Public Sub DisplayError(ByVal CurrentAction As String, ByVal ErrorNo As String, ByVal ErrorMsg As String)
        Dim lvMsg As String
        lvMsg = "The following error occurred while " & CurrentAction & ":" & Chr(13) & Chr(13) & _
                "Error No. " & ErrorNo & Chr(13) & _
                ErrorMsg & Chr(13) & Chr(13) & _
                "Date and Time: " & Date.Today & " - " & TimeOfDay & Chr(13) & _
                "Please WRITE THIS MESSAGE DOWN and inform your I.T. personnel. Thank you!"
        MsgBox(lvMsg, MsgBoxStyle.OkOnly, "Error")
    End Sub

    Public Function TakeScreenShot() As Bitmap
        Dim TestFilePath As String = Application.StartupPath & "\test.jpg"

        ' Dim desktopPath As String = GetFolderPath(SpecialFolder.Desktop)
        Dim screenSize As Size = New Size(MyComputer.Screen.Bounds.Width, MyComputer.Screen.Bounds.Height)

        Dim screenGrab As New Bitmap(MyComputer.Screen.Bounds.Width, MyComputer.Screen.Bounds.Height)

        Dim g As Graphics = Graphics.FromImage(screenGrab)

        g.CopyFromScreen(New Point(0, 0), New Point(0, 0), screenSize)

        screenGrab.Save(TestFilePath, Imaging.ImageFormat.Jpeg)

        Return screenGrab

    End Function

    'Public Sub SentEmail()

    '    ' Sender's email address and password
    '    Dim senderEmail As String = "ely.inalvez@gmail.com"
    '    Dim senderPassword As String = "miub ipiy ckcb iycn"

    '    ' Recipient's email address
    '    Dim recipientEmail As String = "recipient@example.com"

    '    ' Create the MailMessage object
    '    Dim mail As New MailMessage()
    '    mail.From = New MailAddress(senderEmail)
    '    mail.To.Add(recipientEmail)
    '    mail.Subject = "Subject of the email"
    '    mail.Body = "Body of the email"

    '    ' Set up the SMTP client
    '    Dim smtpClient As New SmtpClient("smtp.gmail.com")
    '    smtpClient.Port = 587
    '    smtpClient.Credentials = New NetworkCredential(senderEmail, senderPassword)
    '    smtpClient.EnableSsl = True ' Use SSL for Gmail

    '    Try
    '        ' Send the email
    '        smtpClient.Send(mail)
    '        Console.WriteLine("Email sent successfully")
    '    Catch ex As Exception
    '        Console.WriteLine("Error sending email: " & ex.Message)
    '    End Try

    'End Sub



    'Public Sub GetScreenShotError()
    '    Create a new bitmap
    '    Using Bmp As New Bitmap(150, 150, Imaging.PixelFormat.Format32bppPArgb)
    '        Set the resolution to 300 DPI
    '        Bmp.SetResolution(100, 100)
    '        Create a graphics object from the bitmap
    '        Using G = Graphics.FromImage(Bmp)
    '            Paint the canvas white
    '            G.Clear(Color.White)
    '            Set various modes to higher quality
    '            G.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
    '            G.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
    '            G.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

    '            Create a font
    '            Using F As New Font("Arial", 12)
    '                Create a brush
    '                Using B As New SolidBrush(Color.Black)
    '                    Draw some text
    '                    G.DrawString("Hello world", F, B, 20, 20)
    '                End Using
    '            End Using
    '        End Using

    '        Save the file as a TIFF
    '        Bmp.Save("C:\Users\slec-276\Desktop\test.Jpeg", Imaging.ImageFormat.Jpeg)
    '    End Using
    'End Sub

    'Public Sub ErrorDatainDetailLog()
    '    Try
    '        '*************************************************
    '        'LOG DETAIL Ely 7/19/2023
    '        Dim Host_name As String = Dns.GetHostName()
    '        Dim ip_address As String = Dns.GetHostEntry(Host_name).AddressList(1).ToString()

    '        Dim FILE_NAME As String = "C:\Users\slec-276\Desktop\testlv\PasswordManager\PasswordManager\Logs\Log_Details-Error.ini"
    '        ' Append to TOP
    '        Dim _FStream As New System.IO.FileStream(FILE_NAME, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite, 32, IO.FileOptions.RandomAccess)
    '        Dim _Reader As New System.IO.StreamReader(_FStream)
    '        Dim AppendVal As String = ""
    '        Dim CurVal As String = ""
    '        AppendVal = "Log Details for Error on " & mvErrorName & "  and by:" & gvEmpName & " - " & gvDesignation & " - " & gvOfficialName & " - " & Now & " - IP Address:" & ip_address
    '        ' Read old file in, then Place the Header at the beginning of the stream
    '        _FStream.Seek(0, IO.SeekOrigin.Begin) ' Set the stream to the beginning of the file
    '        _Reader = New IO.StreamReader(_FStream)
    '        While Not _Reader.EndOfStream
    '            CurVal &= Convert.ToChar(_Reader.Read)
    '        End While
    '        AppendVal &= vbNewLine & CurVal
    '        _Reader.Close()
    '        ' Write file with header
    '        _FStream = New System.IO.FileStream(FILE_NAME, IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.None, 32, IO.FileOptions.None)
    '        Dim _Writer As New System.IO.StreamWriter(_FStream)
    '        _FStream.Seek(0, IO.SeekOrigin.Begin) ' Set the stream to the beginning of the file
    '        _Writer = New IO.StreamWriter(_FStream)
    '        _Writer.WriteLine(AppendVal)
    '        _Writer.Close()
    '        '*************************************************************
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Public Sub UpdateDatainDetailLog()
    '    Try
    '        '***********************
    '        'LOG DETAIL Ely 7/19/2023
    '        Dim Host_name As String = Dns.GetHostName()
    '        Dim ip_address As String = Dns.GetHostEntry(Host_name).AddressList(1).ToString()

    '        Dim FILE_PATH As String = "C:\Users\slec-276\Desktop\testlv\PasswordManager\PasswordManager\Logs\Log_Details.ini"
    '        ' Append to TOP
    '        Dim _FStream As New System.IO.FileStream(FILE_PATH, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite, 32, IO.FileOptions.RandomAccess)
    '        Dim _Reader As New System.IO.StreamReader(_FStream)
    '        Dim AppendVal As String = ""
    '        Dim CurVal As String = ""
    '        AppendVal = "Update Details:  " & gvEmpName & " - " & gvDesignation & " - " & gvOfficialName & " - " & Now & " - IP Address:" & ip_address

    '        ' Read old file in, then Place the Header at the beginning of the stream
    '        _FStream.Seek(0, IO.SeekOrigin.Begin) ' Set the stream to the beginning of the file
    '        _Reader = New IO.StreamReader(_FStream)
    '        While Not _Reader.EndOfStream
    '            CurVal &= Convert.ToChar(_Reader.Read)
    '        End While
    '        AppendVal &= vbNewLine & CurVal
    '        _Reader.Close()
    '        ' Write file with header
    '        _FStream = New System.IO.FileStream(FILE_PATH, IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.None, 32, IO.FileOptions.None)
    '        Dim _Writer As New System.IO.StreamWriter(_FStream)
    '        _FStream.Seek(0, IO.SeekOrigin.Begin) ' Set the stream to the beginning of the file
    '        _Writer = New IO.StreamWriter(_FStream)
    '        _Writer.WriteLine(AppendVal)
    '        _Writer.Close()
    '        '**********************
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Public Sub InsertDatainDetailLog()
    '    Try
    '        '***********************
    '        'LOG DETAIL Ely 7/19/2023
    '        Dim Host_name As String = Dns.GetHostName()
    '        Dim ip_address As String = Dns.GetHostEntry(Host_name).AddressList(1).ToString()

    '        Dim FILE_PATH As String = "C:\Users\slec-276\Desktop\testlv\PasswordManager\PasswordManager\Logs\Log_Details.ini"
    '        ' Append to TOP
    '        Dim _FStream As New System.IO.FileStream(FILE_PATH, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite, 32, IO.FileOptions.RandomAccess)
    '        Dim _Reader As New System.IO.StreamReader(_FStream)
    '        Dim AppendVal As String = ""
    '        Dim CurVal As String = ""
    '        AppendVal = "Insert Details:  " & gvEmpName & " - " & gvDesignation & " - " & gvOfficialName & " - " & Now & " - IP Address:" & ip_address

    '        ' Read old file in, then Place the Header at the beginning of the stream
    '        _FStream.Seek(0, IO.SeekOrigin.Begin) ' Set the stream to the beginning of the file
    '        _Reader = New IO.StreamReader(_FStream)
    '        While Not _Reader.EndOfStream
    '            CurVal &= Convert.ToChar(_Reader.Read)
    '        End While
    '        AppendVal &= vbNewLine & CurVal
    '        _Reader.Close()
    '        ' Write file with header
    '        _FStream = New System.IO.FileStream(FILE_PATH, IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.None, 32, IO.FileOptions.None)
    '        Dim _Writer As New System.IO.StreamWriter(_FStream)
    '        _FStream.Seek(0, IO.SeekOrigin.Begin) ' Set the stream to the beginning of the file
    '        _Writer = New IO.StreamWriter(_FStream)
    '        _Writer.WriteLine(AppendVal)
    '        _Writer.Close()
    '        '**********************
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Public Sub LoginDatainDetailLog()
    '    Try
    '        '***********************
    '        'LOG DETAIL Ely 7/19/2023
    '        Dim Host_name As String = Dns.GetHostName()
    '        Dim ip_address As String = Dns.GetHostEntry(Host_name).AddressList(1).ToString()

    '        Dim FILE_PATH As String = "C:\Users\slec-276\Desktop\testlv\PasswordManager\PasswordManager\Logs\Log_Details.ini"
    '        ' Append to TOP
    '        Dim _FStream As New System.IO.FileStream(FILE_PATH, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite, 32, IO.FileOptions.RandomAccess)
    '        Dim _Reader As New System.IO.StreamReader(_FStream)
    '        Dim AppendVal As String = ""
    '        Dim CurVal As String = ""
    '        AppendVal = "Update Details:  " & gvEmpName & " - " & gvDesignation & " - " & gvOfficialName & " - " & Now & " - IP Address:" & ip_address

    '        ' Read old file in, then Place the Header at the beginning of the stream
    '        _FStream.Seek(0, IO.SeekOrigin.Begin) ' Set the stream to the beginning of the file
    '        _Reader = New IO.StreamReader(_FStream)
    '        While Not _Reader.EndOfStream
    '            CurVal &= Convert.ToChar(_Reader.Read)
    '        End While
    '        AppendVal &= vbNewLine & CurVal
    '        _Reader.Close()
    '        ' Write file with header
    '        _FStream = New System.IO.FileStream(FILE_PATH, IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.None, 32, IO.FileOptions.None)
    '        Dim _Writer As New System.IO.StreamWriter(_FStream)
    '        _FStream.Seek(0, IO.SeekOrigin.Begin) ' Set the stream to the beginning of the file
    '        _Writer = New IO.StreamWriter(_FStream)
    '        _Writer.WriteLine(AppendVal)
    '        _Writer.Close()
    '        '**********************
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Public Sub LogoutLogs()
    '    Try


    '        '***********************
    '        'LOG DETAIL Ely 7/19/2023
    '        Dim Host_name As String = Dns.GetHostName()
    '        Dim ip_address As String = Dns.GetHostEntry(Host_name).AddressList(1).ToString()

    '        Dim FILE_PATH As String = "C:\Users\slec-276\Desktop\testlv\PasswordManager\PasswordManager\Logs\Log_Details.ini"
    '        ' Append to TOP
    '        Dim _FStream As New System.IO.FileStream(FILE_PATH, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite, 32, IO.FileOptions.RandomAccess)
    '        Dim _Reader As New System.IO.StreamReader(_FStream)
    '        Dim AppendVal As String = ""
    '        Dim CurVal As String = ""
    '        AppendVal = "Logout eMedical:  " & catchEmedusername & " " & gvEmpName & " - " & gvDesignation & " - " & gvOfficialName & " - " & Now & " - IP Address:" & ip_address

    '        ' Read old file in, then Place the Header at the beginning of the stream
    '        _FStream.Seek(0, IO.SeekOrigin.Begin) ' Set the stream to the beginning of the file
    '        _Reader = New IO.StreamReader(_FStream)
    '        While Not _Reader.EndOfStream
    '            CurVal &= Convert.ToChar(_Reader.Read)
    '        End While
    '        AppendVal &= vbNewLine & CurVal
    '        _Reader.Close()
    '        ' Write file with header
    '        _FStream = New System.IO.FileStream(FILE_PATH, IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.None, 32, IO.FileOptions.None)
    '        Dim _Writer As New System.IO.StreamWriter(_FStream)
    '        _FStream.Seek(0, IO.SeekOrigin.Begin) ' Set the stream to the beginning of the file
    '        _Writer = New IO.StreamWriter(_FStream)
    '        _Writer.WriteLine(AppendVal)
    '        _Writer.Close()
    '        '**********************
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    'Public Sub LoginLogs()
    '    Try
    '        '***********************
    '        'LOG DETAIL Ely 7/19/2023
    '        Dim Host_name As String = Dns.GetHostName()
    '        Dim ip_address As String = Dns.GetHostEntry(Host_name).AddressList(1).ToString()

    '        Dim FILE_PATH As String = "C:\Users\slec-276\Desktop\testlv\PasswordManager\PasswordManager\Logs\Log_Details.ini"
    '        ' Append to TOP
    '        Dim _FStream As New System.IO.FileStream(FILE_PATH, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite, 32, IO.FileOptions.RandomAccess)
    '        Dim _Reader As New System.IO.StreamReader(_FStream)
    '        Dim AppendVal As String = ""
    '        Dim CurVal As String = ""
    '        AppendVal = "Login eMedical:  " & catchEmedusername & " " & gvEmpName & " - " & gvDesignation & " - " & gvOfficialName & " - " & Now & " - IP Address:" & ip_address

    '        ' Read old file in, then Place the Header at the beginning of the stream
    '        _FStream.Seek(0, IO.SeekOrigin.Begin) ' Set the stream to the beginning of the file
    '        _Reader = New IO.StreamReader(_FStream)
    '        While Not _Reader.EndOfStream
    '            CurVal &= Convert.ToChar(_Reader.Read)
    '        End While
    '        AppendVal &= vbNewLine & CurVal
    '        _Reader.Close()
    '        ' Write file with header
    '        _FStream = New System.IO.FileStream(FILE_PATH, IO.FileMode.Open, IO.FileAccess.Write, IO.FileShare.None, 32, IO.FileOptions.None)
    '        Dim _Writer As New System.IO.StreamWriter(_FStream)
    '        _FStream.Seek(0, IO.SeekOrigin.Begin) ' Set the stream to the beginning of the file
    '        _Writer = New IO.StreamWriter(_FStream)
    '        _Writer.WriteLine(AppendVal)
    '        _Writer.Close()
    '        '**********************
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

End Module
