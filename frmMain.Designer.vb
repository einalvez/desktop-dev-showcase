<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.TimerTransferFile = New System.Windows.Forms.Timer(Me.components)
        Me.lvSearch = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.lblProgress = New System.Windows.Forms.Label
        Me.btnRefresh = New System.Windows.Forms.Button
        Me.txtTo = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtSubject = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtBody = New System.Windows.Forms.RichTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.ComboBox1 = New System.Windows.Forms.ComboBox
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.lvInbox = New System.Windows.Forms.ListView
        Me.ColumnHeader17 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader("(none)")
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader16 = New System.Windows.Forms.ColumnHeader
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.lvSentItems = New System.Windows.Forms.ListView
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader("(none)")
        Me.ColumnHeader9 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader10 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader11 = New System.Windows.Forms.ColumnHeader
        Me.cmdCompress = New System.Windows.Forms.Button
        Me.txtTarget = New System.Windows.Forms.TextBox
        Me.txtOutput = New System.Windows.Forms.TextBox
        Me.Button4 = New System.Windows.Forms.Button
        Me.txtdistiller = New System.Windows.Forms.TextBox
        Me.txtJoboption = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TabControl2 = New System.Windows.Forms.TabControl
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.chkCompressed = New System.Windows.Forms.CheckBox
        Me.btnshow = New System.Windows.Forms.Button
        Me.PicProgress = New System.Windows.Forms.PictureBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.lvAttched = New System.Windows.Forms.ListView
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.RemoveContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RemoveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.TabPage7 = New System.Windows.Forms.TabPage
        Me.btnView = New System.Windows.Forms.Button
        Me.Label19 = New System.Windows.Forms.Label
        Me.txtExcelProtect = New System.Windows.Forms.TextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.lvExcelProtect = New System.Windows.Forms.ListView
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.cmdRemoveMerge = New System.Windows.Forms.Button
        Me.btnMerge = New System.Windows.Forms.Button
        Me.Label9 = New System.Windows.Forms.Label
        Me.lvMerge = New System.Windows.Forms.ListView
        Me.ColumnHeader12 = New System.Windows.Forms.ColumnHeader(0)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.TabPage5 = New System.Windows.Forms.TabPage
        Me.cmdRemoveDismantle = New System.Windows.Forms.Button
        Me.cmdDismantle = New System.Windows.Forms.Button
        Me.Label11 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lvDismantle = New System.Windows.Forms.ListView
        Me.ColumnHeader14 = New System.Windows.Forms.ColumnHeader
        Me.RemoveDismantleContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Remove = New System.Windows.Forms.ToolStripMenuItem
        Me.TabPage6 = New System.Windows.Forms.TabPage
        Me.numFontSize = New System.Windows.Forms.NumericUpDown
        Me.btnFontDecrease = New System.Windows.Forms.Button
        Me.btnFontIncrease = New System.Windows.Forms.Button
        Me.btnAddCheckBox = New System.Windows.Forms.Button
        Me.btnAddTextBox = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.numRight = New System.Windows.Forms.NumericUpDown
        Me.numLeft = New System.Windows.Forms.NumericUpDown
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label17 = New System.Windows.Forms.Label
        Me.numBot = New System.Windows.Forms.NumericUpDown
        Me.numTop = New System.Windows.Forms.NumericUpDown
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.cboOrientation = New System.Windows.Forms.ComboBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.cboPaperSize = New System.Windows.Forms.ComboBox
        Me.Label13 = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnZoomReset = New System.Windows.Forms.Button
        Me.btnZoomOut = New System.Windows.Forms.Button
        Me.btnZoomIn = New System.Windows.Forms.Button
        Me.btnCrop = New System.Windows.Forms.Button
        Me.btnConvert = New System.Windows.Forms.Button
        Me.btnRot180 = New System.Windows.Forms.Button
        Me.btnRotRight = New System.Windows.Forms.Button
        Me.btnRotLeft = New System.Windows.Forms.Button
        Me.picJPGtoPDF = New System.Windows.Forms.PictureBox
        Me.OPFMerge = New System.Windows.Forms.OpenFileDialog
        Me.TimerDelay = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnDeleteField = New System.Windows.Forms.Button
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabControl2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.PicProgress, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RemoveContextMenuStrip.SuspendLayout()
        Me.TabPage7.SuspendLayout()
        Me.TabPage4.SuspendLayout()
        Me.TabPage5.SuspendLayout()
        Me.RemoveDismantleContextMenuStrip.SuspendLayout()
        Me.TabPage6.SuspendLayout()
        CType(Me.numFontSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.numRight, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numLeft, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numBot, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numTop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.picJPGtoPDF, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TimerTransferFile
        '
        Me.TimerTransferFile.Interval = 3000
        '
        'lvSearch
        '
        Me.lvSearch.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.lvSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvSearch.FullRowSelect = True
        Me.lvSearch.GridLines = True
        Me.lvSearch.Location = New System.Drawing.Point(1576, 155)
        Me.lvSearch.Name = "lvSearch"
        Me.lvSearch.Size = New System.Drawing.Size(690, 41)
        Me.lvSearch.TabIndex = 20
        Me.lvSearch.UseCompatibleStateImageBehavior = False
        Me.lvSearch.View = System.Windows.Forms.View.Details
        Me.lvSearch.Visible = False
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Filename"
        Me.ColumnHeader1.Width = 250
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(1311, 173)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(652, 23)
        Me.ProgressBar1.TabIndex = 21
        Me.ProgressBar1.Visible = False
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProgress.Location = New System.Drawing.Point(1322, 199)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(138, 24)
        Me.lblProgress.TabIndex = 22
        Me.lblProgress.Text = "Processing...."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Location = New System.Drawing.Point(1322, 236)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(559, 27)
        Me.btnRefresh.TabIndex = 24
        Me.btnRefresh.Text = "INBOX"
        Me.btnRefresh.UseVisualStyleBackColor = True
        Me.btnRefresh.Visible = False
        '
        'txtTo
        '
        Me.txtTo.Location = New System.Drawing.Point(1548, 155)
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(625, 20)
        Me.txtTo.TabIndex = 28
        Me.txtTo.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1496, 131)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "PDSNo:"
        Me.Label2.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1517, 162)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 13)
        Me.Label1.TabIndex = 26
        Me.Label1.Text = "TO:"
        Me.Label1.Visible = False
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(1548, 181)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(625, 20)
        Me.txtSubject.TabIndex = 31
        Me.txtSubject.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(1496, 184)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Subject:"
        Me.Label3.Visible = False
        '
        'txtBody
        '
        Me.txtBody.Location = New System.Drawing.Point(1548, 218)
        Me.txtBody.Name = "txtBody"
        Me.txtBody.Size = New System.Drawing.Size(625, 276)
        Me.txtBody.TabIndex = 32
        Me.txtBody.Text = ""
        Me.txtBody.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(1508, 218)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(34, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Body:"
        Me.Label4.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1474, 386)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 35
        Me.Button1.Text = "Attached file"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(1548, 128)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(308, 21)
        Me.ComboBox1.TabIndex = 36
        Me.ComboBox1.Visible = False
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.Location = New System.Drawing.Point(1526, 631)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(137, 49)
        Me.Button2.TabIndex = 37
        Me.Button2.Text = "Send"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.Location = New System.Drawing.Point(-33, 41)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(129, 27)
        Me.Button3.TabIndex = 38
        Me.Button3.Text = "SENT ITEMS"
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(1370, 145)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(827, 432)
        Me.TabControl1.TabIndex = 39
        Me.TabControl1.Visible = False
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.lvInbox)
        Me.TabPage1.Controls.Add(Me.Button3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(819, 406)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "INBOX"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'lvInbox
        '
        Me.lvInbox.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader17, Me.ColumnHeader2, Me.ColumnHeader6, Me.ColumnHeader3, Me.ColumnHeader16})
        Me.lvInbox.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvInbox.FullRowSelect = True
        Me.lvInbox.GridLines = True
        Me.lvInbox.Location = New System.Drawing.Point(6, 6)
        Me.lvInbox.MultiSelect = False
        Me.lvInbox.Name = "lvInbox"
        Me.lvInbox.Size = New System.Drawing.Size(807, 393)
        Me.lvInbox.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvInbox.TabIndex = 26
        Me.lvInbox.Tag = ""
        Me.lvInbox.UseCompatibleStateImageBehavior = False
        Me.lvInbox.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader17
        '
        Me.ColumnHeader17.Text = "ID"
        Me.ColumnHeader17.Width = 45
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Sender"
        Me.ColumnHeader2.Width = 255
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Subject"
        Me.ColumnHeader6.Width = 388
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Date & Time"
        Me.ColumnHeader3.Width = 134
        '
        'ColumnHeader16
        '
        Me.ColumnHeader16.Text = "BODY"
        Me.ColumnHeader16.Width = 1000
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.lvSentItems)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(819, 406)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "SENT ITEMS"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'lvSentItems
        '
        Me.lvSentItems.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10, Me.ColumnHeader11})
        Me.lvSentItems.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvSentItems.FullRowSelect = True
        Me.lvSentItems.GridLines = True
        Me.lvSentItems.Location = New System.Drawing.Point(6, 6)
        Me.lvSentItems.MultiSelect = False
        Me.lvSentItems.Name = "lvSentItems"
        Me.lvSentItems.Size = New System.Drawing.Size(807, 567)
        Me.lvSentItems.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvSentItems.TabIndex = 27
        Me.lvSentItems.Tag = ""
        Me.lvSentItems.UseCompatibleStateImageBehavior = False
        Me.lvSentItems.View = System.Windows.Forms.View.Details
        Me.lvSentItems.Visible = False
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "ID"
        Me.ColumnHeader7.Width = 45
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Sender"
        Me.ColumnHeader8.Width = 255
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "Subject"
        Me.ColumnHeader9.Width = 388
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "Date & Time"
        Me.ColumnHeader10.Width = 134
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "BODY"
        Me.ColumnHeader11.Width = 1000
        '
        'cmdCompress
        '
        Me.cmdCompress.Location = New System.Drawing.Point(1341, 134)
        Me.cmdCompress.Name = "cmdCompress"
        Me.cmdCompress.Size = New System.Drawing.Size(93, 27)
        Me.cmdCompress.TabIndex = 40
        Me.cmdCompress.Text = "Compress"
        Me.cmdCompress.UseVisualStyleBackColor = True
        Me.cmdCompress.Visible = False
        '
        'txtTarget
        '
        Me.txtTarget.Location = New System.Drawing.Point(1440, 140)
        Me.txtTarget.Name = "txtTarget"
        Me.txtTarget.Size = New System.Drawing.Size(343, 20)
        Me.txtTarget.TabIndex = 41
        Me.txtTarget.Visible = False
        '
        'txtOutput
        '
        Me.txtOutput.Location = New System.Drawing.Point(1440, 166)
        Me.txtOutput.Name = "txtOutput"
        Me.txtOutput.Size = New System.Drawing.Size(343, 20)
        Me.txtOutput.TabIndex = 42
        Me.txtOutput.Visible = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(1862, 124)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(93, 27)
        Me.Button4.TabIndex = 43
        Me.Button4.Text = "Add"
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'txtdistiller
        '
        Me.txtdistiller.Location = New System.Drawing.Point(1245, 628)
        Me.txtdistiller.Name = "txtdistiller"
        Me.txtdistiller.Size = New System.Drawing.Size(343, 20)
        Me.txtdistiller.TabIndex = 44
        Me.txtdistiller.Visible = False
        '
        'txtJoboption
        '
        Me.txtJoboption.Location = New System.Drawing.Point(1245, 665)
        Me.txtJoboption.Name = "txtJoboption"
        Me.txtJoboption.Size = New System.Drawing.Size(343, 20)
        Me.txtJoboption.TabIndex = 45
        Me.txtJoboption.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(1266, 551)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(64, 13)
        Me.Label5.TabIndex = 46
        Me.Label5.Text = "Distiller path"
        Me.Label5.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(1242, 568)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 13)
        Me.Label6.TabIndex = 47
        Me.Label6.Text = "Joboption path"
        Me.Label6.Visible = False
        '
        'TabControl2
        '
        Me.TabControl2.AllowDrop = True
        Me.TabControl2.Controls.Add(Me.TabPage3)
        Me.TabControl2.Controls.Add(Me.TabPage7)
        Me.TabControl2.Controls.Add(Me.TabPage4)
        Me.TabControl2.Controls.Add(Me.TabPage5)
        Me.TabControl2.Controls.Add(Me.TabPage6)
        Me.TabControl2.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl2.Location = New System.Drawing.Point(0, 0)
        Me.TabControl2.Name = "TabControl2"
        Me.TabControl2.SelectedIndex = 0
        Me.TabControl2.ShowToolTips = True
        Me.TabControl2.Size = New System.Drawing.Size(1240, 984)
        Me.TabControl2.TabIndex = 50
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.chkCompressed)
        Me.TabPage3.Controls.Add(Me.btnshow)
        Me.TabPage3.Controls.Add(Me.PicProgress)
        Me.TabPage3.Controls.Add(Me.Label12)
        Me.TabPage3.Controls.Add(Me.txtPassword)
        Me.TabPage3.Controls.Add(Me.Label8)
        Me.TabPage3.Controls.Add(Me.lvAttched)
        Me.TabPage3.Location = New System.Drawing.Point(4, 33)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(1232, 947)
        Me.TabPage3.TabIndex = 0
        Me.TabPage3.Text = "     Compress File     "
        Me.TabPage3.ToolTipText = "Compress File"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'chkCompressed
        '
        Me.chkCompressed.AutoSize = True
        Me.chkCompressed.Checked = True
        Me.chkCompressed.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCompressed.Location = New System.Drawing.Point(16, 573)
        Me.chkCompressed.Name = "chkCompressed"
        Me.chkCompressed.Size = New System.Drawing.Size(147, 28)
        Me.chkCompressed.TabIndex = 117
        Me.chkCompressed.Text = "Compressed"
        Me.chkCompressed.UseVisualStyleBackColor = True
        '
        'btnshow
        '
        Me.btnshow.Image = Global.PDFeditor.My.Resources.Resources.see_eye_visible_icon_187826
        Me.btnshow.Location = New System.Drawing.Point(396, 605)
        Me.btnshow.Name = "btnshow"
        Me.btnshow.Size = New System.Drawing.Size(65, 38)
        Me.btnshow.TabIndex = 116
        Me.btnshow.UseVisualStyleBackColor = True
        '
        'PicProgress
        '
        Me.PicProgress.Image = Global.PDFeditor.My.Resources.Resources.ezgif_com_crop
        Me.PicProgress.Location = New System.Drawing.Point(552, 184)
        Me.PicProgress.Name = "PicProgress"
        Me.PicProgress.Size = New System.Drawing.Size(144, 143)
        Me.PicProgress.TabIndex = 63
        Me.PicProgress.TabStop = False
        Me.PicProgress.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.Navy
        Me.Label12.Location = New System.Drawing.Point(18, 639)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(192, 18)
        Me.Label12.TabIndex = 54
        Me.Label12.Text = "Set your password here."
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(16, 607)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(10026)
        Me.txtPassword.Size = New System.Drawing.Size(374, 29)
        Me.txtPassword.TabIndex = 53
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Blue
        Me.Label8.Location = New System.Drawing.Point(12, 660)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(435, 24)
        Me.Label8.TabIndex = 52
        Me.Label8.Text = "Please Drag and Drop your file inside the List."
        '
        'lvAttched
        '
        Me.lvAttched.AllowDrop = True
        Me.lvAttched.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader4})
        Me.lvAttched.ContextMenuStrip = Me.RemoveContextMenuStrip
        Me.lvAttched.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvAttched.FullRowSelect = True
        Me.lvAttched.Location = New System.Drawing.Point(6, 6)
        Me.lvAttched.Name = "lvAttched"
        Me.lvAttched.ShowItemToolTips = True
        Me.lvAttched.Size = New System.Drawing.Size(1218, 561)
        Me.lvAttched.SmallImageList = Me.ImageList1
        Me.lvAttched.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvAttched.StateImageList = Me.ImageList1
        Me.lvAttched.TabIndex = 50
        Me.lvAttched.Tag = ""
        Me.lvAttched.UseCompatibleStateImageBehavior = False
        Me.lvAttched.View = System.Windows.Forms.View.List
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "No."
        Me.ColumnHeader4.Width = 200
        '
        'RemoveContextMenuStrip
        '
        Me.RemoveContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RemoveToolStripMenuItem})
        Me.RemoveContextMenuStrip.Name = "RemoveContextMenuStrip"
        Me.RemoveContextMenuStrip.Size = New System.Drawing.Size(142, 26)
        '
        'RemoveToolStripMenuItem
        '
        Me.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem"
        Me.RemoveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.RemoveToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.RemoveToolStripMenuItem.Text = "Remove"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.White
        Me.ImageList1.Images.SetKeyName(0, "pdf24x.png")
        '
        'TabPage7
        '
        Me.TabPage7.Controls.Add(Me.btnView)
        Me.TabPage7.Controls.Add(Me.Label19)
        Me.TabPage7.Controls.Add(Me.txtExcelProtect)
        Me.TabPage7.Controls.Add(Me.Label20)
        Me.TabPage7.Controls.Add(Me.lvExcelProtect)
        Me.TabPage7.Location = New System.Drawing.Point(4, 33)
        Me.TabPage7.Name = "TabPage7"
        Me.TabPage7.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage7.Size = New System.Drawing.Size(1232, 947)
        Me.TabPage7.TabIndex = 4
        Me.TabPage7.Text = "     Excel Protect     "
        Me.TabPage7.UseVisualStyleBackColor = True
        '
        'btnView
        '
        Me.btnView.Image = Global.PDFeditor.My.Resources.Resources.see_eye_visible_icon_187826
        Me.btnView.Location = New System.Drawing.Point(398, 603)
        Me.btnView.Name = "btnView"
        Me.btnView.Size = New System.Drawing.Size(65, 38)
        Me.btnView.TabIndex = 120
        Me.btnView.UseVisualStyleBackColor = True
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.Navy
        Me.Label19.Location = New System.Drawing.Point(20, 637)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(192, 18)
        Me.Label19.TabIndex = 119
        Me.Label19.Text = "Set your password here."
        '
        'txtExcelProtect
        '
        Me.txtExcelProtect.Location = New System.Drawing.Point(18, 605)
        Me.txtExcelProtect.Name = "txtExcelProtect"
        Me.txtExcelProtect.PasswordChar = Global.Microsoft.VisualBasic.ChrW(10026)
        Me.txtExcelProtect.Size = New System.Drawing.Size(374, 29)
        Me.txtExcelProtect.TabIndex = 118
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.Blue
        Me.Label20.Location = New System.Drawing.Point(14, 658)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(435, 24)
        Me.Label20.TabIndex = 117
        Me.Label20.Text = "Please Drag and Drop your file inside the List."
        '
        'lvExcelProtect
        '
        Me.lvExcelProtect.AllowDrop = True
        Me.lvExcelProtect.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader5})
        Me.lvExcelProtect.ContextMenuStrip = Me.RemoveContextMenuStrip
        Me.lvExcelProtect.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvExcelProtect.FullRowSelect = True
        Me.lvExcelProtect.Location = New System.Drawing.Point(6, 6)
        Me.lvExcelProtect.Name = "lvExcelProtect"
        Me.lvExcelProtect.ShowItemToolTips = True
        Me.lvExcelProtect.Size = New System.Drawing.Size(1218, 593)
        Me.lvExcelProtect.SmallImageList = Me.ImageList1
        Me.lvExcelProtect.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.lvExcelProtect.StateImageList = Me.ImageList1
        Me.lvExcelProtect.TabIndex = 51
        Me.lvExcelProtect.Tag = ""
        Me.lvExcelProtect.UseCompatibleStateImageBehavior = False
        Me.lvExcelProtect.View = System.Windows.Forms.View.List
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "No."
        Me.ColumnHeader5.Width = 200
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.cmdRemoveMerge)
        Me.TabPage4.Controls.Add(Me.btnMerge)
        Me.TabPage4.Controls.Add(Me.Label9)
        Me.TabPage4.Controls.Add(Me.lvMerge)
        Me.TabPage4.Controls.Add(Me.MenuStrip1)
        Me.TabPage4.Location = New System.Drawing.Point(4, 33)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(1232, 947)
        Me.TabPage4.TabIndex = 1
        Me.TabPage4.Text = "     Merge PDF File     "
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'cmdRemoveMerge
        '
        Me.cmdRemoveMerge.BackColor = System.Drawing.Color.White
        Me.cmdRemoveMerge.Location = New System.Drawing.Point(403, 630)
        Me.cmdRemoveMerge.Name = "cmdRemoveMerge"
        Me.cmdRemoveMerge.Size = New System.Drawing.Size(365, 56)
        Me.cmdRemoveMerge.TabIndex = 56
        Me.cmdRemoveMerge.Text = "REMOVE FILE"
        Me.cmdRemoveMerge.UseVisualStyleBackColor = False
        '
        'btnMerge
        '
        Me.btnMerge.BackColor = System.Drawing.Color.White
        Me.btnMerge.Location = New System.Drawing.Point(32, 630)
        Me.btnMerge.Name = "btnMerge"
        Me.btnMerge.Size = New System.Drawing.Size(365, 56)
        Me.btnMerge.TabIndex = 54
        Me.btnMerge.Text = "MERGE FILE"
        Me.btnMerge.UseVisualStyleBackColor = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Blue
        Me.Label9.Location = New System.Drawing.Point(3, 569)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(435, 24)
        Me.Label9.TabIndex = 53
        Me.Label9.Text = "Please Drag and Drop your file inside the List."
        '
        'lvMerge
        '
        Me.lvMerge.AllowDrop = True
        Me.lvMerge.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader12})
        Me.lvMerge.ContextMenuStrip = Me.RemoveContextMenuStrip
        Me.lvMerge.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvMerge.FullRowSelect = True
        Me.lvMerge.GridLines = True
        Me.lvMerge.LargeImageList = Me.ImageList1
        Me.lvMerge.Location = New System.Drawing.Point(10, 6)
        Me.lvMerge.Name = "lvMerge"
        Me.lvMerge.Size = New System.Drawing.Size(1214, 560)
        Me.lvMerge.SmallImageList = Me.ImageList1
        Me.lvMerge.StateImageList = Me.ImageList1
        Me.lvMerge.TabIndex = 51
        Me.lvMerge.Tag = ""
        Me.lvMerge.TileSize = New System.Drawing.Size(16, 16)
        Me.lvMerge.UseCompatibleStateImageBehavior = False
        Me.lvMerge.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = ""
        Me.ColumnHeader12.Width = 900
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Location = New System.Drawing.Point(3, 3)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1226, 24)
        Me.MenuStrip1.TabIndex = 57
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'TabPage5
        '
        Me.TabPage5.Controls.Add(Me.cmdRemoveDismantle)
        Me.TabPage5.Controls.Add(Me.cmdDismantle)
        Me.TabPage5.Controls.Add(Me.Label11)
        Me.TabPage5.Controls.Add(Me.Label7)
        Me.TabPage5.Controls.Add(Me.lvDismantle)
        Me.TabPage5.Location = New System.Drawing.Point(4, 33)
        Me.TabPage5.Name = "TabPage5"
        Me.TabPage5.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage5.Size = New System.Drawing.Size(1232, 947)
        Me.TabPage5.TabIndex = 2
        Me.TabPage5.Text = "     SPLIT PDF File     "
        Me.TabPage5.UseVisualStyleBackColor = True
        '
        'cmdRemoveDismantle
        '
        Me.cmdRemoveDismantle.BackColor = System.Drawing.Color.White
        Me.cmdRemoveDismantle.Location = New System.Drawing.Point(417, 635)
        Me.cmdRemoveDismantle.Name = "cmdRemoveDismantle"
        Me.cmdRemoveDismantle.Size = New System.Drawing.Size(365, 56)
        Me.cmdRemoveDismantle.TabIndex = 59
        Me.cmdRemoveDismantle.Text = "REMOVE FILE"
        Me.cmdRemoveDismantle.UseVisualStyleBackColor = False
        '
        'cmdDismantle
        '
        Me.cmdDismantle.BackColor = System.Drawing.Color.White
        Me.cmdDismantle.ImageAlign = System.Drawing.ContentAlignment.BottomLeft
        Me.cmdDismantle.Location = New System.Drawing.Point(47, 635)
        Me.cmdDismantle.Name = "cmdDismantle"
        Me.cmdDismantle.Size = New System.Drawing.Size(364, 56)
        Me.cmdDismantle.TabIndex = 57
        Me.cmdDismantle.Text = "SPLIT PDF"
        Me.cmdDismantle.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Red
        Me.Label11.Location = New System.Drawing.Point(6, 596)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(736, 24)
        Me.Label11.TabIndex = 58
        Me.Label11.Text = "Caution: Please Copy your previous Splited files before Spliting another Filles."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Blue
        Me.Label7.Location = New System.Drawing.Point(6, 572)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(435, 24)
        Me.Label7.TabIndex = 56
        Me.Label7.Text = "Please Drag and Drop your file inside the List."
        '
        'lvDismantle
        '
        Me.lvDismantle.AllowDrop = True
        Me.lvDismantle.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader14})
        Me.lvDismantle.ContextMenuStrip = Me.RemoveDismantleContextMenuStrip
        Me.lvDismantle.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lvDismantle.FullRowSelect = True
        Me.lvDismantle.GridLines = True
        Me.lvDismantle.Location = New System.Drawing.Point(8, 6)
        Me.lvDismantle.Name = "lvDismantle"
        Me.lvDismantle.Size = New System.Drawing.Size(1216, 563)
        Me.lvDismantle.SmallImageList = Me.ImageList1
        Me.lvDismantle.StateImageList = Me.ImageList1
        Me.lvDismantle.TabIndex = 55
        Me.lvDismantle.Tag = ""
        Me.lvDismantle.UseCompatibleStateImageBehavior = False
        Me.lvDismantle.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader14
        '
        Me.ColumnHeader14.Text = ""
        Me.ColumnHeader14.Width = 900
        '
        'RemoveDismantleContextMenuStrip
        '
        Me.RemoveDismantleContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Remove})
        Me.RemoveDismantleContextMenuStrip.Name = "RemoveContextMenuStrip"
        Me.RemoveDismantleContextMenuStrip.Size = New System.Drawing.Size(142, 26)
        '
        'Remove
        '
        Me.Remove.Name = "Remove"
        Me.Remove.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.Remove.Size = New System.Drawing.Size(141, 22)
        Me.Remove.Text = "Remove"
        '
        'TabPage6
        '
        Me.TabPage6.AllowDrop = True
        Me.TabPage6.Controls.Add(Me.btnDeleteField)
        Me.TabPage6.Controls.Add(Me.numFontSize)
        Me.TabPage6.Controls.Add(Me.btnFontDecrease)
        Me.TabPage6.Controls.Add(Me.btnFontIncrease)
        Me.TabPage6.Controls.Add(Me.btnAddCheckBox)
        Me.TabPage6.Controls.Add(Me.btnAddTextBox)
        Me.TabPage6.Controls.Add(Me.GroupBox3)
        Me.TabPage6.Controls.Add(Me.cboOrientation)
        Me.TabPage6.Controls.Add(Me.Label14)
        Me.TabPage6.Controls.Add(Me.Label10)
        Me.TabPage6.Controls.Add(Me.cboPaperSize)
        Me.TabPage6.Controls.Add(Me.Label13)
        Me.TabPage6.Controls.Add(Me.GroupBox2)
        Me.TabPage6.Controls.Add(Me.picJPGtoPDF)
        Me.TabPage6.Location = New System.Drawing.Point(4, 33)
        Me.TabPage6.Name = "TabPage6"
        Me.TabPage6.Size = New System.Drawing.Size(1232, 947)
        Me.TabPage6.TabIndex = 3
        Me.TabPage6.Text = "     IMAGE TO PDF       "
        Me.TabPage6.UseVisualStyleBackColor = True
        '
        'numFontSize
        '
        Me.numFontSize.Location = New System.Drawing.Point(1088, 864)
        Me.numFontSize.Name = "numFontSize"
        Me.numFontSize.Size = New System.Drawing.Size(120, 29)
        Me.numFontSize.TabIndex = 82
        '
        'btnFontDecrease
        '
        Me.btnFontDecrease.Location = New System.Drawing.Point(1072, 816)
        Me.btnFontDecrease.Name = "btnFontDecrease"
        Me.btnFontDecrease.Size = New System.Drawing.Size(152, 40)
        Me.btnFontDecrease.TabIndex = 81
        Me.btnFontDecrease.Text = "Decrese Font"
        Me.btnFontDecrease.UseVisualStyleBackColor = True
        '
        'btnFontIncrease
        '
        Me.btnFontIncrease.Location = New System.Drawing.Point(1072, 768)
        Me.btnFontIncrease.Name = "btnFontIncrease"
        Me.btnFontIncrease.Size = New System.Drawing.Size(152, 40)
        Me.btnFontIncrease.TabIndex = 80
        Me.btnFontIncrease.Text = "Increase Font"
        Me.btnFontIncrease.UseVisualStyleBackColor = True
        '
        'btnAddCheckBox
        '
        Me.btnAddCheckBox.Location = New System.Drawing.Point(1072, 704)
        Me.btnAddCheckBox.Name = "btnAddCheckBox"
        Me.btnAddCheckBox.Size = New System.Drawing.Size(152, 56)
        Me.btnAddCheckBox.TabIndex = 79
        Me.btnAddCheckBox.Text = "ADD Checkbox"
        Me.btnAddCheckBox.UseVisualStyleBackColor = True
        '
        'btnAddTextBox
        '
        Me.btnAddTextBox.Location = New System.Drawing.Point(1072, 656)
        Me.btnAddTextBox.Name = "btnAddTextBox"
        Me.btnAddTextBox.Size = New System.Drawing.Size(152, 40)
        Me.btnAddTextBox.TabIndex = 78
        Me.btnAddTextBox.Text = "Add TextBox"
        Me.btnAddTextBox.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.numRight)
        Me.GroupBox3.Controls.Add(Me.numLeft)
        Me.GroupBox3.Controls.Add(Me.Label18)
        Me.GroupBox3.Controls.Add(Me.Label17)
        Me.GroupBox3.Controls.Add(Me.numBot)
        Me.GroupBox3.Controls.Add(Me.numTop)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Location = New System.Drawing.Point(1056, 304)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(166, 176)
        Me.GroupBox3.TabIndex = 77
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "MARGIN"
        '
        'numRight
        '
        Me.numRight.Location = New System.Drawing.Point(102, 127)
        Me.numRight.Name = "numRight"
        Me.numRight.Size = New System.Drawing.Size(56, 29)
        Me.numRight.TabIndex = 81
        Me.numRight.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'numLeft
        '
        Me.numLeft.Location = New System.Drawing.Point(103, 94)
        Me.numLeft.Name = "numLeft"
        Me.numLeft.Size = New System.Drawing.Size(56, 29)
        Me.numLeft.TabIndex = 80
        Me.numLeft.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(32, 129)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(64, 18)
        Me.Label18.TabIndex = 79
        Me.Label18.Text = "RIGHT:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(32, 96)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(53, 18)
        Me.Label17.TabIndex = 78
        Me.Label17.Text = "LEFT:"
        '
        'numBot
        '
        Me.numBot.Location = New System.Drawing.Point(100, 60)
        Me.numBot.Name = "numBot"
        Me.numBot.Size = New System.Drawing.Size(56, 29)
        Me.numBot.TabIndex = 77
        Me.numBot.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'numTop
        '
        Me.numTop.Location = New System.Drawing.Point(100, 25)
        Me.numTop.Name = "numTop"
        Me.numTop.Size = New System.Drawing.Size(56, 29)
        Me.numTop.TabIndex = 76
        Me.numTop.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(10, 64)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(84, 18)
        Me.Label16.TabIndex = 75
        Me.Label16.Text = "BOTTOM:"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(10, 31)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(47, 18)
        Me.Label15.TabIndex = 74
        Me.Label15.Text = "TOP:"
        '
        'cboOrientation
        '
        Me.cboOrientation.FormattingEnabled = True
        Me.cboOrientation.Items.AddRange(New Object() {"PORTRAIT", "LANDSCAPE"})
        Me.cboOrientation.Location = New System.Drawing.Point(1072, 616)
        Me.cboOrientation.Name = "cboOrientation"
        Me.cboOrientation.Size = New System.Drawing.Size(140, 32)
        Me.cboOrientation.TabIndex = 75
        Me.cboOrientation.Text = "PORTRAIT"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(1064, 584)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(156, 24)
        Me.Label14.TabIndex = 74
        Me.Label14.Text = "ORIENTATION:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(1064, 504)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(135, 24)
        Me.Label10.TabIndex = 73
        Me.Label10.Text = "PAPER SIZE:"
        '
        'cboPaperSize
        '
        Me.cboPaperSize.FormattingEnabled = True
        Me.cboPaperSize.Items.AddRange(New Object() {"A4", "A3", "A5", "US LETTER", "US LEGAL"})
        Me.cboPaperSize.Location = New System.Drawing.Point(1072, 544)
        Me.cboPaperSize.Name = "cboPaperSize"
        Me.cboPaperSize.Size = New System.Drawing.Size(140, 32)
        Me.cboPaperSize.TabIndex = 72
        Me.cboPaperSize.Text = "A4"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(8, 912)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(363, 24)
        Me.Label13.TabIndex = 70
        Me.Label13.Text = "Please Drag and Drop your file inside."
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnZoomReset)
        Me.GroupBox2.Controls.Add(Me.btnZoomOut)
        Me.GroupBox2.Controls.Add(Me.btnZoomIn)
        Me.GroupBox2.Controls.Add(Me.btnCrop)
        Me.GroupBox2.Controls.Add(Me.btnConvert)
        Me.GroupBox2.Controls.Add(Me.btnRot180)
        Me.GroupBox2.Controls.Add(Me.btnRotRight)
        Me.GroupBox2.Controls.Add(Me.btnRotLeft)
        Me.GroupBox2.Location = New System.Drawing.Point(1072, 8)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(150, 288)
        Me.GroupBox2.TabIndex = 69
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "ACTION"
        '
        'btnZoomReset
        '
        Me.btnZoomReset.Enabled = False
        Me.btnZoomReset.Image = CType(resources.GetObject("btnZoomReset.Image"), System.Drawing.Image)
        Me.btnZoomReset.Location = New System.Drawing.Point(80, 128)
        Me.btnZoomReset.Name = "btnZoomReset"
        Me.btnZoomReset.Size = New System.Drawing.Size(48, 40)
        Me.btnZoomReset.TabIndex = 74
        Me.btnZoomReset.Text = "*"
        Me.btnZoomReset.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnZoomReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnZoomReset, "ROTATE COUNTERCLOCKWISE")
        Me.btnZoomReset.UseVisualStyleBackColor = True
        Me.btnZoomReset.Visible = False
        '
        'btnZoomOut
        '
        Me.btnZoomOut.Location = New System.Drawing.Point(80, 80)
        Me.btnZoomOut.Name = "btnZoomOut"
        Me.btnZoomOut.Size = New System.Drawing.Size(48, 40)
        Me.btnZoomOut.TabIndex = 73
        Me.btnZoomOut.Text = "-"
        Me.btnZoomOut.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnZoomOut.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage
        Me.ToolTip1.SetToolTip(Me.btnZoomOut, "ZOOM OUT")
        Me.btnZoomOut.UseVisualStyleBackColor = True
        '
        'btnZoomIn
        '
        Me.btnZoomIn.Location = New System.Drawing.Point(80, 24)
        Me.btnZoomIn.Name = "btnZoomIn"
        Me.btnZoomIn.Size = New System.Drawing.Size(48, 48)
        Me.btnZoomIn.TabIndex = 72
        Me.btnZoomIn.Text = "+"
        Me.btnZoomIn.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnZoomIn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnZoomIn, "ROTATE CLOCKWISE")
        Me.btnZoomIn.UseVisualStyleBackColor = True
        '
        'btnCrop
        '
        Me.btnCrop.Image = CType(resources.GetObject("btnCrop.Image"), System.Drawing.Image)
        Me.btnCrop.Location = New System.Drawing.Point(48, 232)
        Me.btnCrop.Name = "btnCrop"
        Me.btnCrop.Size = New System.Drawing.Size(55, 48)
        Me.btnCrop.TabIndex = 71
        Me.btnCrop.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCrop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnCrop, "CONVERT")
        Me.btnCrop.UseVisualStyleBackColor = True
        '
        'btnConvert
        '
        Me.btnConvert.Image = CType(resources.GetObject("btnConvert.Image"), System.Drawing.Image)
        Me.btnConvert.Location = New System.Drawing.Point(48, 176)
        Me.btnConvert.Name = "btnConvert"
        Me.btnConvert.Size = New System.Drawing.Size(55, 48)
        Me.btnConvert.TabIndex = 70
        Me.btnConvert.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnConvert.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnConvert, "CONVERT")
        Me.btnConvert.UseVisualStyleBackColor = True
        '
        'btnRot180
        '
        Me.btnRot180.Image = CType(resources.GetObject("btnRot180.Image"), System.Drawing.Image)
        Me.btnRot180.Location = New System.Drawing.Point(24, 128)
        Me.btnRot180.Name = "btnRot180"
        Me.btnRot180.Size = New System.Drawing.Size(48, 40)
        Me.btnRot180.TabIndex = 68
        Me.btnRot180.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRot180.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnRot180, "FLIP")
        Me.btnRot180.UseVisualStyleBackColor = True
        '
        'btnRotRight
        '
        Me.btnRotRight.Image = CType(resources.GetObject("btnRotRight.Image"), System.Drawing.Image)
        Me.btnRotRight.Location = New System.Drawing.Point(24, 80)
        Me.btnRotRight.Name = "btnRotRight"
        Me.btnRotRight.Size = New System.Drawing.Size(48, 40)
        Me.btnRotRight.TabIndex = 69
        Me.btnRotRight.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRotRight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnRotRight, "ROTATE COUNTERCLOCKWISE")
        Me.btnRotRight.UseVisualStyleBackColor = True
        '
        'btnRotLeft
        '
        Me.btnRotLeft.Image = CType(resources.GetObject("btnRotLeft.Image"), System.Drawing.Image)
        Me.btnRotLeft.Location = New System.Drawing.Point(21, 24)
        Me.btnRotLeft.Name = "btnRotLeft"
        Me.btnRotLeft.Size = New System.Drawing.Size(55, 48)
        Me.btnRotLeft.TabIndex = 67
        Me.btnRotLeft.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRotLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.ToolTip1.SetToolTip(Me.btnRotLeft, "ROTATE CLOCKWISE")
        Me.btnRotLeft.UseVisualStyleBackColor = True
        '
        'picJPGtoPDF
        '
        Me.picJPGtoPDF.Image = Global.PDFeditor.My.Resources.Resources.drop_files_here_extra1
        Me.picJPGtoPDF.Location = New System.Drawing.Point(5, 7)
        Me.picJPGtoPDF.Name = "picJPGtoPDF"
        Me.picJPGtoPDF.Size = New System.Drawing.Size(1051, 897)
        Me.picJPGtoPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picJPGtoPDF.TabIndex = 71
        Me.picJPGtoPDF.TabStop = False
        '
        'OPFMerge
        '
        Me.OPFMerge.FileName = "OpenFileDialog1"
        '
        'TimerDelay
        '
        Me.TimerDelay.Interval = 1000
        '
        'btnDeleteField
        '
        Me.btnDeleteField.Location = New System.Drawing.Point(1064, 904)
        Me.btnDeleteField.Name = "btnDeleteField"
        Me.btnDeleteField.Size = New System.Drawing.Size(160, 32)
        Me.btnDeleteField.TabIndex = 83
        Me.btnDeleteField.Text = "DELETE OBJ"
        Me.btnDeleteField.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AllowDrop = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1242, 991)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.TabControl2)
        Me.Controls.Add(Me.txtOutput)
        Me.Controls.Add(Me.txtJoboption)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtTarget)
        Me.Controls.Add(Me.cmdCompress)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.txtdistiller)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtBody)
        Me.Controls.Add(Me.txtSubject)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtTo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.lvSearch)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SLEC PDF EDITOR "
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabControl2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.PicProgress, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RemoveContextMenuStrip.ResumeLayout(False)
        Me.TabPage7.ResumeLayout(False)
        Me.TabPage7.PerformLayout()
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.TabPage5.ResumeLayout(False)
        Me.TabPage5.PerformLayout()
        Me.RemoveDismantleContextMenuStrip.ResumeLayout(False)
        Me.TabPage6.ResumeLayout(False)
        Me.TabPage6.PerformLayout()
        CType(Me.numFontSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.numRight, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numLeft, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numBot, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numTop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.picJPGtoPDF, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TimerTransferFile As System.Windows.Forms.Timer
    Friend WithEvents lvSearch As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents txtTo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtBody As System.Windows.Forms.RichTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents lvInbox As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader17 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader16 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lvSentItems As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader9 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader10 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader11 As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdCompress As System.Windows.Forms.Button
    Friend WithEvents txtTarget As System.Windows.Forms.TextBox
    Friend WithEvents txtOutput As System.Windows.Forms.TextBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents txtdistiller As System.Windows.Forms.TextBox
    Friend WithEvents txtJoboption As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents TabControl2 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lvAttched As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents btnMerge As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lvMerge As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader12 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents RemoveContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RemoveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TabPage5 As System.Windows.Forms.TabPage
    Friend WithEvents cmdDismantle As System.Windows.Forms.Button
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lvDismantle As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader14 As System.Windows.Forms.ColumnHeader
    Friend WithEvents RemoveDismantleContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents Remove As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmdRemoveMerge As System.Windows.Forms.Button
    Friend WithEvents cmdRemoveDismantle As System.Windows.Forms.Button
    Friend WithEvents btnshow As System.Windows.Forms.Button
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents OPFMerge As System.Windows.Forms.OpenFileDialog
    Friend WithEvents TimerDelay As System.Windows.Forms.Timer
    Friend WithEvents TabPage6 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRot180 As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnRotRight As System.Windows.Forms.Button
    Friend WithEvents btnRotLeft As System.Windows.Forms.Button
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnConvert As System.Windows.Forms.Button
    Friend WithEvents picJPGtoPDF As System.Windows.Forms.PictureBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboPaperSize As System.Windows.Forms.ComboBox
    Friend WithEvents cboOrientation As System.Windows.Forms.ComboBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents numTop As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents numBot As System.Windows.Forms.NumericUpDown
    Friend WithEvents numRight As System.Windows.Forms.NumericUpDown
    Friend WithEvents numLeft As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents PicProgress As System.Windows.Forms.PictureBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents TabPage7 As System.Windows.Forms.TabPage
    Friend WithEvents btnView As System.Windows.Forms.Button
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtExcelProtect As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lvExcelProtect As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents chkCompressed As System.Windows.Forms.CheckBox
    Friend WithEvents btnCrop As System.Windows.Forms.Button
    Friend WithEvents btnZoomOut As System.Windows.Forms.Button
    Friend WithEvents btnZoomIn As System.Windows.Forms.Button
    Friend WithEvents btnZoomReset As System.Windows.Forms.Button
    Friend WithEvents btnAddCheckBox As System.Windows.Forms.Button
    Friend WithEvents btnAddTextBox As System.Windows.Forms.Button
    Friend WithEvents btnFontDecrease As System.Windows.Forms.Button
    Friend WithEvents btnFontIncrease As System.Windows.Forms.Button
    Friend WithEvents numFontSize As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnDeleteField As System.Windows.Forms.Button

End Class
