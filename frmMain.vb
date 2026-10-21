'Date Created: 11/21/2023
'Deployment Date: 11/21/2023
'Programmer: Eleuterio T. Inalvez, Jr

'Date Modified: 09/22/2026
'Deployment Date: 
'Additional feature croping image
'Programmer: Eleuterio T. Inalvez, Jr

Imports System.IO
Imports System.Data
Imports System.Diagnostics
Imports System.Text.RegularExpressions
Imports System.Collections.Generic

Imports iTextSharp.text.io
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Runtime.InteropServices

Imports Excel = Microsoft.Office.Interop.Excel

Imports System.Threading

Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

'Imports iText.Kernel.Pdf
'Imports iText.Kernel.Pdf.Reader
'Imports iText.Kernel.Pdf.Writer

Imports Syncfusion.Pdf

Public Class frmMain

    Private isCropMode As Boolean = False
    Private cropRect As System.Drawing.Rectangle
    Private startCropRect As System.Drawing.Rectangle

    Dim delayTimer As New Timer(AddressOf TimerCallback, Nothing, Timeout.Infinite, Timeout.Infinite)
    Private progressValue As Integer = 0
    Private progressIncrement As Integer = 20
    Private maxProgressValue As Integer = 500
    Dim xfileName As String
    Dim DestinationDirectory As String
    Dim xPassword As String = "m@n1la"
    Dim targetFileSizeBytes As Long = 5 * 1024 * 1024
    Dim jobOptionsFile As String  ' Replace with the path to your custom job options file
    Dim outputpath As String = "C:\Compressed\"
    Dim outputFile As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "MergeFile\MergedOutput.pdf")
    Dim folderFolder As String = "C:\Folder"
    Dim Mergepath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\DismantledPDF"
    Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
    Dim folderName As String = "CompressedFile"
    Dim folderNamedismantle As String = "SplitPDF"
    Dim forlderNameMerge As String = "MergeFile"
    Dim outputPdfFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) & "\SplitPDF"
    Dim mvcount As Integer = 0
    Dim mvReady As Boolean = False

    Private fields As New List(Of DesignField)()
    Private selectedField As DesignField = Nothing
    Private isFieldDragging As Boolean = False
    Private isFieldResizing As Boolean = False
    Private fieldDragOffset As System.Drawing.Point
    Private fieldResizeStartBounds As System.Drawing.Rectangle

    Private WithEvents txtOverlay As New TextBox() With {.Visible = False, .BorderStyle = BorderStyle.FixedSingle}
    Private WithEvents chkOverlay As New CheckBox() With {.Visible = False, .Text = ""}


    Private WithEvents cmsField As New ContextMenuStrip()
    Private WithEvents tsmiDeleteField As New ToolStripMenuItem("Delete Field")

    Private Enum DragHandle
        None
        Body
        TopLeft
        TopMiddle
        TopRight
        RightMiddle
        BottomRight
        BottomMiddle
        BottomLeft
        LeftMiddle
        TextBox
        CheckBox
    End Enum

   

    Private activeHandle As DragHandle = DragHandle.None
    Private startMousePoint As System.Drawing.Point
    'Private startCropRect As System.Drawing.Rectangle

    ' Handle box sizing
    Private Const HANDLE_SIZE As Integer = 8

    Private zoomFactor As Single = 1.0F
    Private Const ZOOM_STEP As Single = 0.1F
    Private Const MIN_ZOOM As Single = 0.1F
    Private Const MAX_ZOOM As Single = 5.0F

    Private Function ImageToScreenRect(ByVal imgRect As System.Drawing.Rectangle) As System.Drawing.Rectangle
        Return New System.Drawing.Rectangle( _
            CInt(imgRect.X * zoomFactor), _
            CInt(imgRect.Y * zoomFactor), _
            CInt(imgRect.Width * zoomFactor), _
            CInt(imgRect.Height * zoomFactor))
    End Function


    Private Function ScreenToImagePoint(ByVal pt As System.Drawing.Point) As System.Drawing.Point
        Return New System.Drawing.Point( _
            CInt(pt.X / zoomFactor), _
            CInt(pt.Y / zoomFactor))
    End Function


    Private Sub btnZoomIn_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZoomIn.Click
        If picJPGtoPDF.Image Is Nothing Then Exit Sub
        If zoomFactor < MAX_ZOOM Then
            zoomFactor += ZOOM_STEP
            picJPGtoPDF.Invalidate() ' Trigger redraw with new zoom level
        End If
    End Sub

    Private Sub btnZoomOut_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZoomOut.Click
        If picJPGtoPDF.Image Is Nothing Then Exit Sub
        If zoomFactor > MIN_ZOOM Then
            zoomFactor -= ZOOM_STEP
            picJPGtoPDF.Invalidate() ' Trigger redraw with new zoom level
        End If
    End Sub

    Private Sub btnZoomReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnZoomReset.Click
        zoomFactor = 1.0F
        picJPGtoPDF.Invalidate()
    End Sub
    Private Sub picJPGtoPDF_MouseDoubleClick(ByVal sender As Object, ByVal e As MouseEventArgs) Handles picJPGtoPDF.MouseDoubleClick
        If isCropMode Then Exit Sub

        ' 1. Convert click coordinates to match zoomed image coordinates
        Dim imgPt As System.Drawing.Point = ScreenToImagePoint(e.Location)

        ' 2. Find which field was double-clicked
        For Each f In fields.AsEnumerable().Reverse()
            If f.Bounds.Contains(imgPt) Then
                selectedField = f

                ' Get the screen-scaled rectangle for placement
                Dim screenRect As System.Drawing.Rectangle = ImageToScreenRect(f.Bounds)

                If f.Type = FieldType.TextBox Then
                    chkOverlay.Visible = False

                    ' Set font size matching current selected field
                    Dim actualSize As Single = Math.Max(6.0F, f.FontSize * zoomFactor)
                    txtOverlay.Bounds = screenRect
                    txtOverlay.Font = New System.Drawing.Font("Arial", actualSize, FontStyle.Regular)
                    txtOverlay.Text = f.Content
                    txtOverlay.Visible = True
                    txtOverlay.Focus()
                    txtOverlay.SelectAll()

                ElseIf f.Type = FieldType.CheckBox Then
                    ' Toggle check state directly on double click
                    f.IsChecked = Not f.IsChecked
                    picJPGtoPDF.Invalidate()
                End If

                Exit For
            End If
        Next
    End Sub

    Private Sub txtOverlay_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles txtOverlay.KeyDown
        If e.KeyCode = Keys.Enter Then
            SaveOverlayText()
            e.SuppressKeyPress = True ' Prevent ding sound
        ElseIf e.KeyCode = Keys.Escape Then
            txtOverlay.Visible = False
        End If
    End Sub

    ' Save text when user clicks outside the overlay box
    Private Sub txtOverlay_Leave(ByVal sender As Object, ByVal e As EventArgs) Handles txtOverlay.Leave
        SaveOverlayText()
    End Sub

    Private Sub SaveOverlayText()
        If selectedField IsNot Nothing AndAlso selectedField.Type = FieldType.TextBox Then
            selectedField.Content = txtOverlay.Text
            txtOverlay.Visible = False
            picJPGtoPDF.Invalidate() ' Redraw PictureBox with new text
        End If
    End Sub

    Private Sub picJPGtoPDF_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles picJPGtoPDF.MouseWheel
        If picJPGtoPDF.Image Is Nothing Then Exit Sub

        ' Check if Ctrl key is pressed
        If (Control.ModifierKeys And Keys.Control) = Keys.Control Then
            If e.Delta > 0 AndAlso zoomFactor < MAX_ZOOM Then
                zoomFactor += ZOOM_STEP
            ElseIf e.Delta < 0 AndAlso zoomFactor > MIN_ZOOM Then
                zoomFactor -= ZOOM_STEP
            End If
            picJPGtoPDF.Invalidate()
        End If
    End Sub

    Private Sub frmMain_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragDrop

    End Sub

    Private Sub frmMain_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles Me.DragEnter

    End Sub

    Private Sub btnCrop_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCrop.Click
        If picJPGtoPDF.Image Is Nothing Then
            MessageBox.Show("Please load an image first.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' TOGGLE 1: Start Cropping Mode
        If Not isCropMode Then
            isCropMode = True
            btnCrop.Text = "Finish Crop"

            ' Initialize crop rectangle in the center of the image (50% size)
            Dim imgW As Integer = picJPGtoPDF.Image.Width
            Dim imgH As Integer = picJPGtoPDF.Image.Height
            cropRect = New System.Drawing.Rectangle(imgW \ 4, imgH \ 4, imgW \ 2, imgH \ 2)

            picJPGtoPDF.Invalidate()
            Exit Sub
        End If

        ' TOGGLE 2: Finish Cropping Mode
        If isCropMode Then
            Dim result As DialogResult = MessageBox.Show( _
                "Are you sure you want to crop this image?", _
                "Confirm Crop", _
                MessageBoxButtons.YesNo, _
                MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                ' Perform crop
                Dim croppedImg As Bitmap = PerformCrop()

                If croppedImg IsNot Nothing Then
                    ' Prompt user to save as Image or PDF
                    SaveCroppedFile(croppedImg)
                End If

                ExitCropMode()
            End If
        End If
    End Sub

    Private Sub ExitCropMode()
        isCropMode = False
        btnCrop.Text = "Crop"
        activeHandle = DragHandle.None
        picJPGtoPDF.Invalidate()
    End Sub

    Private Sub picJPGtoPDF_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles picJPGtoPDF.MouseDown


        Dim clickPoint As Point = e.Location
        If isCropMode Then
            If e.Button <> MouseButtons.Left Then Exit Sub
            activeHandle = HitTest(e.Location)
            If activeHandle <> DragHandle.None Then
                startMousePoint = e.Location
                startCropRect = cropRect
            End If
            Exit Sub
        End If

        ' e.Location returns screen coordinates on the PictureBox control
        Dim screenPoint As Point = e.Location

        ' Convert to original image pixel coordinates (accounting for zoom)
        Dim imagePoint As Point = ScreenToImagePoint(e.Location)

        ' Check if click hit any placed field
        For Each f In fields.AsEnumerable().Reverse()
            If f.Bounds.Contains(imagePoint) Then
                selectedField = f
                isFieldDragging = True
                Exit For
            End If
        Next

        If e.Button = MouseButtons.Left Then
            ' Check if clicking resize handle of currently selected item
            If selectedField IsNot Nothing Then
                Dim selScreenRect As System.Drawing.Rectangle = ImageToScreenRect(selectedField.Bounds)
                Dim resizeHandle As New System.Drawing.Rectangle(selScreenRect.Right - 8, selScreenRect.Bottom - 8, 12, 12)
                If resizeHandle.Contains(e.Location) Then
                    isFieldResizing = True
                    startMousePoint = e.Location
                    fieldResizeStartBounds = selectedField.Bounds
                    Exit Sub
                End If
            End If

            ' Hit-test fields (top to bottom)
            Dim imgPt As System.Drawing.Point = ScreenToImagePoint(e.Location)
            Dim hitField As DesignField = Nothing
            For Each f In fields.AsEnumerable().Reverse()
                If f.Bounds.Contains(imgPt) Then
                    hitField = f
                    Exit For
                End If
            Next

            selectedField = hitField
            If selectedField IsNot Nothing Then
                isFieldDragging = True
                fieldDragOffset = New System.Drawing.Point(imgPt.X - selectedField.Bounds.X, imgPt.Y - selectedField.Bounds.Y)
            End If

            picJPGtoPDF.Invalidate()
        End If

        If txtOverlay.Visible Then
            SaveOverlayText()
        End If

        If isCropMode Then Exit Sub

        ' --- RIGHT CLICK TO SHOW CONTEXT MENU ---
        If e.Button = MouseButtons.Right Then
            Dim imgPt As System.Drawing.Point = ScreenToImagePoint(e.Location)

            ' Find if user right-clicked an existing field
            For Each f In fields.AsEnumerable().Reverse()
                If f.Bounds.Contains(imgPt) Then
                    selectedField = f
                    picJPGtoPDF.Invalidate()
                    ' Show context menu at mouse position
                    cmsField.Show(picJPGtoPDF, e.Location)
                    Exit For
                End If
            Next
            Exit Sub
        End If

    End Sub

    Private Sub tsmiDeleteField_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsmiDeleteField.Click
        DeleteSelectedField()
    End Sub

    Private Sub picJPGtoPDF_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles picJPGtoPDF.MouseMove
        If isCropMode Then
            If e.Button = MouseButtons.None Then
                Dim handle As DragHandle = HitTest(e.Location)
                UpdateCursor(handle)
                Exit Sub
            End If
            ' (Existing crop drag logic remains intact...)
            Exit Sub
        End If

        ' Cursor updates for interactive fields
        If e.Button = MouseButtons.None AndAlso selectedField IsNot Nothing Then
            Dim selScreenRect As System.Drawing.Rectangle = ImageToScreenRect(selectedField.Bounds)
            Dim resizeHandle As New System.Drawing.Rectangle(selScreenRect.Right - 8, selScreenRect.Bottom - 8, 12, 12)
            If resizeHandle.Contains(e.Location) Then
                picJPGtoPDF.Cursor = Cursors.SizeNWSE
            Else
                picJPGtoPDF.Cursor = Cursors.Default
            End If
        End If

        ' Handle Field Dragging
        If isFieldDragging AndAlso selectedField IsNot Nothing Then
            Dim imgPt As System.Drawing.Point = ScreenToImagePoint(e.Location)
            Dim newX As Integer = imgPt.X - fieldDragOffset.X
            Dim newY As Integer = imgPt.Y - fieldDragOffset.Y

            ' Clamp to image bounds
            newX = Math.Max(0, Math.Min(newX, picJPGtoPDF.Image.Width - selectedField.Bounds.Width))
            newY = Math.Max(0, Math.Min(newY, picJPGtoPDF.Image.Height - selectedField.Bounds.Height))

            selectedField.Bounds = New System.Drawing.Rectangle(newX, newY, selectedField.Bounds.Width, selectedField.Bounds.Height)
            picJPGtoPDF.Invalidate()

            ' Handle Field Resizing
        ElseIf isFieldResizing AndAlso selectedField IsNot Nothing Then
            Dim dx As Integer = CInt((e.X - startMousePoint.X) / zoomFactor)
            Dim dy As Integer = CInt((e.Y - startMousePoint.Y) / zoomFactor)

            Dim newW As Integer = Math.Max(15, fieldResizeStartBounds.Width + dx)
            Dim newH As Integer = Math.Max(15, fieldResizeStartBounds.Height + dy)

            selectedField.Bounds = New System.Drawing.Rectangle(fieldResizeStartBounds.X, fieldResizeStartBounds.Y, newW, newH)
            picJPGtoPDF.Invalidate()
        End If
    End Sub

    Private Sub picJPGtoPDF_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles picJPGtoPDF.Paint
        If picJPGtoPDF.Image Is Nothing Then Exit Sub

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.Clear(Color.DarkGray)

        ' 1. Draw image scaled to zoom factor
        Dim zoomedW As Integer = CInt(picJPGtoPDF.Image.Width * zoomFactor)
        Dim zoomedH As Integer = CInt(picJPGtoPDF.Image.Height * zoomFactor)
        g.DrawImage(picJPGtoPDF.Image, New System.Drawing.Rectangle(0, 0, zoomedW, zoomedH))

        ' 2. Draw Interactive Form Fields Overlay
        For Each f In fields
            Dim screenRect As System.Drawing.Rectangle = ImageToScreenRect(f.Bounds)

            If f.Type = FieldType.TextBox Then
                ' Draw Text Box frame
                Using fillBrush As New SolidBrush(Color.FromArgb(180, 255, 255, 255))
                    g.FillRectangle(fillBrush, screenRect)
                End Using
                g.DrawRectangle(Pens.Blue, screenRect)

                ' Use the field's custom FontSize scaled to current zoom factor
                Dim actualSize As Single = Math.Max(6.0F, f.FontSize * zoomFactor)
                Dim textToDraw As String = If(String.IsNullOrEmpty(f.Content), "[Text Field]", f.Content)

                Using customFont As New System.Drawing.Font("Arial", actualSize, FontStyle.Regular)
                    g.DrawString(textToDraw, customFont, Brushes.DarkBlue, screenRect.X + 2, screenRect.Y + 2)
                End Using

            ElseIf f.Type = FieldType.CheckBox Then
                ' Draw Check Box frame
                Using fillBrush As New SolidBrush(Color.FromArgb(180, 255, 255, 255))
                    g.FillRectangle(fillBrush, screenRect)
                End Using
                g.DrawRectangle(Pens.DarkGreen, screenRect)

                If f.IsChecked Then
                    Using font As New System.Drawing.Font("Arial", Math.Max(8, 10 * zoomFactor), FontStyle.Bold)
                        g.DrawString("✓", font, Brushes.Green, screenRect.X + 2, screenRect.Y + 1)
                    End Using
                End If
            End If

            ' Draw Selection Border and Resize Handle for selected field
            If f Is selectedField Then
                Using selPen As New Pen(Color.Red, 2)
                    selPen.DashStyle = DashStyle.Dot
                    g.DrawRectangle(selPen, screenRect)
                End Using

                ' Draw Bottom-Right Resize Handle
                Dim handleRect As New System.Drawing.Rectangle(screenRect.Right - 6, screenRect.Bottom - 6, 8, 8)
                g.FillRectangle(Brushes.Red, handleRect)
            End If
        Next

        ' 3. Draw Crop Overlay if active
        If isCropMode Then
            Dim displayCropRect As System.Drawing.Rectangle = ImageToScreenRect(cropRect)
            Using dimBrush As New SolidBrush(Color.FromArgb(120, 0, 0, 0))
                g.FillRectangle(dimBrush, 0, 0, picJPGtoPDF.Width, displayCropRect.Top)
                g.FillRectangle(dimBrush, 0, displayCropRect.Bottom, picJPGtoPDF.Width, picJPGtoPDF.Height - displayCropRect.Bottom)
                g.FillRectangle(dimBrush, 0, displayCropRect.Top, displayCropRect.Left, displayCropRect.Height)
                g.FillRectangle(dimBrush, displayCropRect.Right, displayCropRect.Top, picJPGtoPDF.Width - displayCropRect.Right, displayCropRect.Height)
            End Using

            Using pen As New Pen(Color.White, 2)
                pen.DashStyle = DashStyle.Dash
                g.DrawRectangle(pen, displayCropRect)
            End Using

            For Each handleType As DragHandle In [Enum].GetValues(GetType(DragHandle))
                If handleType = DragHandle.None OrElse handleType = DragHandle.Body Then Continue For
                Dim handleRect As System.Drawing.Rectangle = GetHandleRectangle(handleType)
                g.FillRectangle(Brushes.White, handleRect)
                g.DrawRectangle(Pens.Black, handleRect)
            Next
        End If
    End Sub

    Private Sub btnCrop_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles btnCrop.Paint
        Dim btn As Button = DirectCast(sender, Button)

        ' Only execute if an image is assigned to btnCrop
        If btn.Image Is Nothing Then Exit Sub

        Dim g As Graphics = e.Graphics
        g.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic

        ' Set padding (margins inside the button frame)
        Dim padding As Integer = 6
        Dim maxWidth As Integer = btn.Width - (padding * 2)
        Dim maxHeight As Integer = btn.Height - (padding * 2)

        If maxWidth <= 0 OrElse maxHeight <= 0 Then Exit Sub

        ' Calculate scale factor to maintain image aspect ratio
        Dim imgW As Single = btn.Image.Width
        Dim imgH As Single = btn.Image.Height
        Dim scale As Single = Math.Min(maxWidth / imgW, maxHeight / imgH)

        Dim newW As Integer = CInt(imgW * scale)
        Dim newH As Integer = CInt(imgH * scale)

        ' Calculate center positions
        Dim x As Integer = (btn.Width - newW) \ 2
        Dim y As Integer = (btn.Height - newH) \ 2

        ' Draw the centered and scaled image over the button
        g.DrawImage(btn.Image, New System.Drawing.Rectangle(x, y, newW, newH))
    End Sub

    Private Sub picJPGtoPDF_MouseUp(ByVal sender As Object, ByVal e As MouseEventArgs) Handles picJPGtoPDF.MouseUp
        If isCropMode Then
            activeHandle = DragHandle.None
        End If
        isFieldDragging = False
        isFieldResizing = False
        picJPGtoPDF.Cursor = Cursors.Default
    End Sub

    Private Function GetHandleRectangle(ByVal handle As DragHandle) As System.Drawing.Rectangle
        Dim hSize As Integer = HANDLE_SIZE
        Dim halfH As Integer = hSize \ 2

        ' Work with screen-scaled crop rectangle
        Dim displayRect As System.Drawing.Rectangle = ImageToScreenRect(cropRect)

        Select Case handle
            Case DragHandle.TopLeft : Return New System.Drawing.Rectangle(displayRect.Left - halfH, displayRect.Top - halfH, hSize, hSize)
            Case DragHandle.TopMiddle : Return New System.Drawing.Rectangle(displayRect.Left + (displayRect.Width \ 2) - halfH, displayRect.Top - halfH, hSize, hSize)
            Case DragHandle.TopRight : Return New System.Drawing.Rectangle(displayRect.Right - halfH, displayRect.Top - halfH, hSize, hSize)
            Case DragHandle.RightMiddle : Return New System.Drawing.Rectangle(displayRect.Right - halfH, displayRect.Top + (displayRect.Height \ 2) - halfH, hSize, hSize)
            Case DragHandle.BottomRight : Return New System.Drawing.Rectangle(displayRect.Right - halfH, displayRect.Bottom - halfH, hSize, hSize)
            Case DragHandle.BottomMiddle : Return New System.Drawing.Rectangle(displayRect.Left + (displayRect.Width \ 2) - halfH, displayRect.Bottom - halfH, hSize, hSize)
            Case DragHandle.BottomLeft : Return New System.Drawing.Rectangle(displayRect.Left - halfH, displayRect.Bottom - halfH, hSize, hSize)
            Case DragHandle.LeftMiddle : Return New System.Drawing.Rectangle(displayRect.Left - halfH, displayRect.Top + (displayRect.Height \ 2) - halfH, hSize, hSize)
            Case Else : Return System.Drawing.Rectangle.Empty
        End Select
    End Function

    Private Function HitTest(ByVal pt As System.Drawing.Point) As DragHandle
        For Each handleType As DragHandle In [Enum].GetValues(GetType(DragHandle))
            If handleType = DragHandle.None OrElse handleType = DragHandle.Body Then Continue For
            If GetHandleRectangle(handleType).Contains(pt) Then
                Return handleType
            End If
        Next

        If cropRect.Contains(pt) Then
            Return DragHandle.Body
        End If

        Return DragHandle.None
    End Function

    Private Sub UpdateCursor(ByVal handle As DragHandle)
        Select Case handle
            Case DragHandle.Body : picJPGtoPDF.Cursor = Cursors.SizeAll
            Case DragHandle.TopLeft, DragHandle.BottomRight : picJPGtoPDF.Cursor = Cursors.SizeNWSE
            Case DragHandle.TopRight, DragHandle.BottomLeft : picJPGtoPDF.Cursor = Cursors.SizeNESW
            Case DragHandle.TopMiddle, DragHandle.BottomMiddle : picJPGtoPDF.Cursor = Cursors.SizeNS
            Case DragHandle.LeftMiddle, DragHandle.RightMiddle : picJPGtoPDF.Cursor = Cursors.SizeWE
            Case Else : picJPGtoPDF.Cursor = Cursors.Default
        End Select
    End Sub

    Private Function PerformCrop() As Bitmap
        Dim srcBitmap As New Bitmap(picJPGtoPDF.Image)
        Dim imgBounds As New System.Drawing.Rectangle(0, 0, srcBitmap.Width, srcBitmap.Height)
        Dim validCropArea As System.Drawing.Rectangle = System.Drawing.Rectangle.Intersect(imgBounds, cropRect)

        If validCropArea.Width > 0 AndAlso validCropArea.Height > 0 Then
            Dim cropped As Bitmap = srcBitmap.Clone(validCropArea, srcBitmap.PixelFormat)
            picJPGtoPDF.Image = cropped
            Return cropped
        End If

        Return Nothing
    End Function

    Private Sub SaveCroppedFile(ByVal croppedBitmap As Bitmap)
        Using sfd As New SaveFileDialog()
            sfd.Title = "Save Cropped File"
            sfd.Filter = "JPEG Image (*.jpg)|*.jpg|PNG Image (*.png)|*.png|PDF Document (*.pdf)|*.pdf"
            sfd.FilterIndex = 1

            If sfd.ShowDialog() = DialogResult.OK Then
                Dim filePath As String = sfd.FileName
                Dim ext As String = Path.GetExtension(filePath).ToLower()

                If ext = ".pdf" Then
                    ' Convert image to PDF using iTextSharp (.NET 3.5 compatible)
                    ConvertImageToPdfiTextSharp(croppedBitmap, filePath)
                Else
                    Dim format As ImageFormat = ImageFormat.Jpeg
                    If ext = ".png" Then format = ImageFormat.Png
                    croppedBitmap.Save(filePath, format)
                End If

                MessageBox.Show("File saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using
    End Sub

    Private Sub ConvertImageToPdfiTextSharp(ByVal bmp As Bitmap, ByVal pdfPath As String)
        ' Create PDF document matching the image pixel dimensions (in points)
        Dim pdfPageSize As New iTextSharp.text.Rectangle(bmp.Width, bmp.Height)
        Dim doc As New iTextSharp.text.Document(pdfPageSize, 0, 0, 0, 0)

        Using fs As New FileStream(pdfPath, FileMode.Create, FileAccess.Write)
            PdfWriter.GetInstance(doc, fs)
            doc.Open()

            ' Convert System.Drawing.Bitmap to iTextSharp Image
            Using ms As New MemoryStream()
                bmp.Save(ms, ImageFormat.Png)
                Dim pdfImage As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(ms.ToArray())
                pdfImage.SetAbsolutePosition(0, 0)
                doc.Add(pdfImage)
            End Using

            doc.Close()
        End Using
    End Sub

    Private Sub frmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        End
    End Sub
    Sub TimerCallback(ByVal state As Object)
        ' Code to be executed after the delay
        Console.WriteLine("Now continuing with the process.")
    End Sub
    Private Sub ListViewAlternateColor(ByVal oCtrl As ListView, ByVal oColor As Color)
        oCtrl.BackColor = oColor
        If TypeOf oCtrl Is ListView Then
            oCtrl.Sort()
            For i As Integer = 0 To oCtrl.Items.Count - 1
                If i Mod 2 = 0 Then
                    oCtrl.Items(i).BackColor = oColor
                Else
                    oCtrl.Items(i).BackColor = Color.White
                End If
            Next
        End If
    End Sub

    Private Sub frmMain_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        ' Only trigger if NOT currently typing inside the overlay text box
        If e.KeyCode = Keys.Delete AndAlso Not txtOverlay.Focused Then
            If selectedField IsNot Nothing Then
                DeleteSelectedField()
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        picJPGtoPDF.Controls.Add(txtOverlay)
        picJPGtoPDF.Controls.Add(chkOverlay)

        picJPGtoPDF.SizeMode = PictureBoxSizeMode.Normal
        txtPassword.Text = xPassword
        txtExcelProtect.Text = xPassword
        lvMerge.SmallImageList = ImageList1
        Me.Text = "SLEC PDF EDITOR Version: " + Me.GetType.Assembly.GetName.Version.ToString()
        Dim item As New ListViewItem
        'item.ImageKey = "icon1" ' Make sure this matches the key in the ImageList
        ' OR
        item.ImageIndex = 0 ' Use the index of the image in the ImageList
        ListViewAlternateColor(lvMerge, Color.LightBlue)
        ListViewAlternateColor(lvDismantle, Color.LightBlue)
        'TimerTransferFile.Enabled = True
        'TimerTransferFile.Start()
        TimerTransferFile.Stop()
        'txtTarget.Text = "C:\Folder\6680GR8M7TVAAUOL0304247.pdf"
        'txtOutput.Text = "C:\Folder\6680GR8M7TVAAUOL0304247Pass.pdf"
        'txtdistiller.Text = "C:\Program Files (x86)\Adobe\Acrobat Distiller\AcroDist.exe"
        'txtJoboption.Text = "C:\Users\slec-276\AppData\Roaming\Adobe\Adobe PDF\Settings\Smallest File Size(2).joboptions"

        '  ProgressBar1.Minimum = 0
        'ProgressBar1.Maximum = maxProgressValue
        'ProgressBar1.Style = ProgressBarStyle.Continuous
        ' ProgressBar1.Value = 0


        If Not Directory.Exists(outputpath) Then
            ' Create the folder
            Directory.CreateDirectory(outputpath)
            'Console.WriteLine("Folder created successfully.")
        Else
            'Console.WriteLine("Folder already exists.")
        End If

        If Not Directory.Exists(desktopPath & "\" & folderName) Then
            ' Create the folder
            Directory.CreateDirectory(desktopPath & "\" & folderName)
            'Console.WriteLine("Folder created successfully.")
        Else
            'Console.WriteLine("Folder already exists.")
        End If

        If Not Directory.Exists(desktopPath & "\" & folderNamedismantle) Then
            ' Create the folder
            Directory.CreateDirectory(desktopPath & "\" & folderNamedismantle)
            'Console.WriteLine("Folder created successfully.")
        Else
            'Console.WriteLine("Folder already exists.")
        End If

        If Not Directory.Exists(folderFolder) Then
            ' Create the folder
            Directory.CreateDirectory(folderFolder)
            'Console.WriteLine("Folder created successfully.")
        Else
            'Console.WriteLine("Folder already exists.")
        End If

        Me.DoubleBuffered = True

        cmsField.Items.Add(tsmiDeleteField)
    End Sub

    Private Sub TimerTransferFile_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerTransferFile.Tick
        ProgressBar1.Value = 0
        Dim xFilename As String
        Dim TempFile As String
        Dim rootDirectory As String
        Dim folderPath As String
        Dim fileCount As Integer = 0
        Dim i As Integer = 0
        DestinationDirectory = "C:\Users\slec-276\Desktop\Folder_Server2"
        ' Set the root directory and patterns
        rootDirectory = "C:\Folder\MoveDocuments\"
        lvSearch.Clear()
        If Directory.Exists(rootDirectory) Then
            ' Get a list of filenames in the folder.
            Dim fileNames As String() = Directory.GetFiles(rootDirectory)
            Dim xcount As Integer

            ' Loop through and display each filena    me.
            For Each fileName As String In fileNames

                Dim item As New ListViewItem(fileName)
                ' xcount = item.SubItems.Count
                '  maxProgressValue = xcount


                TempFile = fileName
                xFilename = System.IO.Path.GetFileName(fileName)
                FindMatchingFolderNameAndFileName(xFilename)
                File.Delete(TempFile)
                lvSearch.Items.Add(xFilename)
                fileCount += 1

            Next
        Else
            ' xfileName = fileName
        End If



        'TimerTransferFile.Stop()
        ' TimerTransferFile.Enabled = False

    End Sub

    Private Sub FindMatchingFolderNameAndFileName(ByVal cFilename As String)

        Dim rootDirectory As String
        Dim folderPattern As String
        Dim filePattern As String
        Dim folderPath As String
        Dim xfileName As String
        Dim Doctype As String
        Dim PDSAlone As String
        Dim inputString As String
        inputString = cFilename
        If Microsoft.VisualBasic.Left(inputString, 8) = "MedCert_" Then
            PDSAlone = Mid(inputString, 9)
        ElseIf Microsoft.VisualBasic.Left(inputString, 10) = "BirthCert_" Then
            PDSAlone = Mid(inputString, 11)
        ElseIf Microsoft.VisualBasic.Left(inputString, 9) = "Passport_" Then
            PDSAlone = Mid(inputString, 10)
        ElseIf Microsoft.VisualBasic.Left(inputString, 5) = "UMID_" Then
            PDSAlone = Mid(inputString, 6)
            ' Handle the case where inputString does not start with "MedCert_"
        End If

        DestinationDirectory = "C:\Users\slec-276\Desktop\Folder_Server2"
        ' Set the root directory and patterns
        rootDirectory = "C:\Folder\MoveDocuments\"
        'If Directory.Exists(rootDirectory) Then
        '    ' Get a list of filenames in the folder.
        '    Dim fileNames As String() = Directory.GetFiles(rootDirectory)

        '    ' Loop through and display each filename.
        '    For Each fileName As String In fileNames
        '        xfileName = fileName
        '        xfileName = Path.GetFileName(xfileName)
        '    Next
        'Else
        '    ' xfileName = fileName
        'End If


        'xfileName = Path.GetFileName(PDSAlone)
        Dim dotPosition As Integer
        dotPosition = InStrRev(PDSAlone, ".")

        If dotPosition > 0 Then
            ' Extract the part of the string before the last dot
            xfileName = Microsoft.VisualBasic.Left(PDSAlone, dotPosition - 1)
        Else
            ' No file extension found, use the original filename
            xfileName = PDSAlone
        End If

        folderPattern = xfileName

        filePattern = xfileName

        ' Start the search
        Call SearchFolders(DestinationDirectory, folderPattern, filePattern, rootDirectory & cFilename, cFilename)


    End Sub

    Private Sub SearchFolders(ByVal folderPath As String, ByVal folderPattern As String, ByVal filePattern As String, ByVal SourceFullpath As String, ByVal cFile As String)
        Try
            Dim subFolder As Object
            Dim File As Object
            Dim fs As Object
            Dim Folder As Object
            Dim folderName As String
            fs = CreateObject("Scripting.FileSystemObject")
            Folder = fs.GetFolder(folderPath)

            For Each subFolder In Folder.Subfolders
                folderName = subFolder.Name

                ' Check if the folder name matches the folderPattern
                If InStr(1, folderName, folderPattern, vbTextCompare = 1) > 0 Then
                    'For Each file In subFolder.files
                    'fileName = file.Name

                    ' Check if the file name matches the filePattern
                    'If InStr(1, fileName, filePattern, vbTextCompare) > 0 Then
                    'Debug.Print "Matching folder: " & folderName
                    'Debug.Print "Matching file: " & fileName
                    'MsgBox (subFolder)
                    progressValue += progressIncrement
                    ProgressBar1.Value = progressValue
                    FileCopy(SourceFullpath, folderPath & "\" & folderName & "\" & cFile)
                    'End If
                    'Next file
                End If

                ' Recursively search subfolders
                Call SearchFolders(subFolder.Path, folderPattern, filePattern, SourceFullpath, cFile)
            Next subFolder
        Catch ex As Exception
            System.Threading.Thread.Sleep(1000)
        End Try

    End Sub
    Private Sub lvAttched_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles lvAttched.DragEnter
        ' Check if the dragged data contains file(s)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())

            ' Check if the dropped file(s) is/are PDF(s)
            Dim isPdf = files.All(Function(file) Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase))

            If isPdf Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.None
            End If
        Else
            e.Effect = DragDropEffects.None
        End If

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub


    'Public Sub CompressPDFWithAdobeAcrobat(ByVal inputFilePath As String, ByVal outputFilePath As String)
    '    ' Set the path to the Acrobat Distiller executable
    '    Dim acrobatDistillerPath As String = "C:\Program Files (x86)\Adobe\Acrobat Distiller\AcroDist.exe" ' Replace with the correct path

    '    ' Specify the path to the job options file with recommended settings
    '    Dim jobOptionsFile As String = "C:\path\to\your\recommended-settings.joboptions" ' Replace with the correct path

    '    ' Define the command to execute
    '    Dim command As String = String.Format("""{0}"" /N /Q /S /O ""{1}"" /job ""{2}"" ""{3}""", acrobatDistillerPath, outputFilePath, jobOptionsFile, inputFilePath)

    '    ' Start the Acrobat Distiller process
    '    Dim processInfo As New ProcessStartInfo("cmd.exe", "/c " & command)
    '    processInfo.CreateNoWindow = True
    '    processInfo.UseShellExecute = False
    '    processInfo.RedirectStandardOutput = True

    '    Dim process As New Process()
    '    process.StartInfo = processInfo
    '    process.Start()

    '    ' Wait for the Acrobat Distiller process to finish
    '    process.WaitForExit()
    '    process.Close()
    'End Sub

    'Public Function CompressPdf(ByVal inputFile As String, ByVal outputFile As String, Optional ByVal targetDpi As Integer = 96, Optional ByVal password As String = "") As Boolean
    '    Dim gsPath As String = "C:\Program Files\gs\gs10.02.1\bin\gswin64c.exe"
    '    Dim cpdfPath As String = "C:\Tools\cpdf\cpdf.exe" ' Adjust to your cpdf.exe path

    '    ' Temporary workspace paths
    '    Dim tempFolder As String = "C:\Compressed\"
    '    If Not Directory.Exists(tempFolder) Then Directory.CreateDirectory(tempFolder)

    '    Dim tempGsPath As String = Path.Combine(tempFolder, "gs_" & Guid.NewGuid().ToString() & ".pdf")
    '    Dim tempCpdfPath As String = Path.Combine(tempFolder, "cpdf_" & Guid.NewGuid().ToString() & ".pdf")

    '    Try
    '        ' Check if Ghostscript exists
    '        If Not File.Exists(gsPath) Then
    '            MsgBox("Ghostscript executable not found at: " & gsPath, MsgBoxStyle.Exclamation, "PDF Editor")
    '            Return False
    '        End If

    '        ' -------------------------------------------------------------
    '        ' PASS 1: Ghostscript (Re-encode and downsample images to targetDpi)
    '        ' -------------------------------------------------------------
    '    Dim gsPsi As New ProcessStartInfo(gsPath) With {
    '        .UseShellExecute = False,
    '        .CreateNoWindow = True,
    '        .RedirectStandardError = True,
    '        .Arguments = String.Format(
    '            "-sDEVICE=pdfwrite -dCompatibilityLevel=1.5 " &
    '            "-dPDFSETTINGS=/screen " &
    '            "-dDownsampleColorImages=true -dColorImageDownsampleType=/Bicubic -dColorImageResolution={0} " &
    '            "-dDownsampleGrayImages=true -dGrayImageDownsampleType=/Bicubic -dGrayImageResolution={0} " &
    '            "-dDownsampleMonoImages=true -dMonoImageDownsampleType=/Subsample -dMonoImageResolution={0} " &
    '            "-dAutoFilterColorImages=false -dColorImageFilter=/DCTEncode " &
    '            "-dAutoFilterGrayImages=false -dGrayImageFilter=/DCTEncode " &
    '            "-dDetectDuplicateImages=true -dCompressFonts=true -dSubsetFonts=true " &
    '            "-dNOPAUSE -dQUIET -dBATCH " &
    '            "-sOutputFile=""{1}"" ""{2}""",
    '            targetDpi, tempGsPath, inputFile)
    '    }

    '        Using gsProcess As Process = Process.Start(gsPsi)
    '            gsProcess.WaitForExit()
    '            If gsProcess.ExitCode <> 0 OrElse Not File.Exists(tempGsPath) Then
    '                Return False
    '            End If
    '        End Using

    '        ' -------------------------------------------------------------
    '        ' PASS 2: Coherent PDF (cpdf -squeeze & metadata removal)
    '        ' -------------------------------------------------------------
    '        Dim sourceForPassword As String = tempGsPath

    '        If File.Exists(cpdfPath) Then
    '        Dim cpdfPsi As New ProcessStartInfo(cpdfPath) With {
    '            .UseShellExecute = False,
    '            .CreateNoWindow = True,
    '            .Arguments = String.Format("-squeeze -metadata false ""{0}"" -o ""{1}""", tempGsPath, tempCpdfPath)
    '        }

    '            Using cpdfProcess As Process = Process.Start(cpdfPsi)
    '                cpdfProcess.WaitForExit()
    '                If cpdfProcess.ExitCode = 0 AndAlso File.Exists(tempCpdfPath) Then
    '                    sourceForPassword = tempCpdfPath
    '                End If
    '            End Using
    '        End If

    '        ' -------------------------------------------------------------
    '        ' PASS 3: Password Encryption & Final Move
    '        ' -------------------------------------------------------------
    '        If Not String.IsNullOrEmpty(password) Then
    '            AddPasswordToPDF(sourceForPassword, outputFile, password)
    '        Else
    '            If File.Exists(outputFile) Then File.Delete(outputFile)
    '            File.Move(sourceForPassword, outputFile)
    '        End If

    '        Return True

    '    Catch ex As Exception
    '        Console.WriteLine("CompressPdf Error: " & ex.Message)
    '        Return False
    '    Finally
    '        ' Cleanup temporary files
    '        If File.Exists(tempGsPath) Then Try : File.Delete(tempGsPath) : Catch : End Try
    '        If File.Exists(tempCpdfPath) Then Try : File.Delete(tempCpdfPath) : Catch : End Try
    '    End Try
    'End Function

    'Private Sub lvAttched_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles lvAttched.DragDrop
    '    Try
    '        PicProgress.Visible = True
    '        'Dim acrobatApp As New Acrobat.AcroAppClass()
    '        'If Trim(Microsoft.VisualBasic.Len(txtPassword.Text)) = 0 Then
    '        '    MsgBox("Password Cannot be empty.")
    '        '    txtPassword.BackColor = Color.Yellow
    '        '    txtPassword.Focus()
    '        '    Exit Sub
    '        'End If
    '        Dim targetDPI As Integer = 72
    '        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
    '            Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
    '            PicProgress.Visible = True
    '            ' Loop through the dropped files and add them to the ListView.
    '            For Each filePath As String In files
    '                ' Add code to parse the file name and add it to the ListView.
    '                Dim fileName As String = System.IO.Path.GetFileName(filePath)
    '                If Path.GetExtension(filePath).Equals(".pdf", StringComparison.OrdinalIgnoreCase) Then

    '                    ' Set the scale factor for image resizing (0.5 in this example)
    '                    Dim scaleFactor As Single = 0.5F
    '                    ' Create PDF with resized image
    '                    'Console.WriteLine("PDF created successfully.")
    '                    xfileName = System.IO.Path.GetFileName(filePath)

    '                    Dim fileInfo As New FileInfo(filePath)
    '                    Dim fileSizeBytes As Long = fileInfo.Length

    '                    Dim item As New ListViewItem(xfileName)
    '                    item.SubItems.Add(outputpath & xfileName) ' Store the full file path in a subitem.
    '                    'CompressPdf(filePath, outputpath & xfileName, 9)
    '                    'ReduceResolution(filePath, outputpath & xfileName, 1, targetDPI)

    '                    Dim dotPosition As Integer
    '                    dotPosition = InStrRev(xfileName, ".")
    '                    If dotPosition > 0 Then
    '                        ' Extract the part of the string before the last dot
    '                        xfileName = Microsoft.VisualBasic.Left(xfileName, dotPosition - 1)
    '                    Else
    '                        ' No file extension found, use the original filename
    '                        xfileName = xfileName
    '                    End If
    '                    ' ReducePdfToTargetSize(filePath, outputpath & xfileName, targetFileSizeBytes)
    '                    xfileName = xfileName & ".pdf"
    '                    'If chkCompressed.Checked = True Then 'Ely @ 05-22-2025 c/o Mam Mari
    '                    '    CompressPdf(filePath, outputpath & xfileName, 9)
    '                    'End If
    '                    'ReducePDFSize(filePath, outputpath & xfileName)
    '                    'ResizeImagesInPdf(filePath, outputpath & xfileName, scaleFactor)
    '                    'ReducePDFOptimizer(filePath, outputpath & xfileName, targetFileSizeBytes, fileSizeBytes)
    '                    'ReducePdfSizeAcrobat(filePath, outputpath & xfileName)
    '                    'OptimizePdfSize(filePath, outputpath & xfileName)


    '                    Dim inputpath As String = filePath
    '                    Dim pdffinal As String = desktopPath & "\" & folderName & "\" & xfileName
    '                    Dim Temppath As String = "C:\Compressed\" & xfileName

    '                    'Ghostcript COMPRESSED eBook Ely 12/28/2023

    '                    If chkCompressed.Checked = True Then 'Ely @ 05-22-2025 c/o Mam Mari
    '                        Dim ghostscriptPath As String = "C:\Program Files\gs\gs10.02.1\bin\gswin64c.exe"
    '                        If Not String.IsNullOrEmpty(ghostscriptPath) Then
    '                            Dim psi As New ProcessStartInfo(ghostscriptPath)
    '                            psi.UseShellExecute = False
    '                            psi.RedirectStandardInput = True
    '                            psi.RedirectStandardOutput = True
    '                            psi.CreateNoWindow = True
    '                            ' Ghostscript command for compressing PDF
    '                            'psi.Arguments = "-sDEVICE=pdfwrite -dCompatibilityLevel=1.4 -dPDFSETTINGS=/ebook -dNOPAUSE -dQUIET -dBATCH -sOutputFile=" & Temppath & " " & inputpath
    '                            'psi.Arguments = "-sDEVICE=pdfwrite -dCompatibilityLevel=1.4 " & _
    '                            '                "-dPDFSETTINGS=/screen " & _
    '                            '                "-dColorImageDownsampleType=/Bicubic -dColorImageResolution=72 -dDownsampleColorImages=true " & _
    '                            '                "-dGrayImageDownsampleType=/Bicubic -dGrayImageResolution=72 -dDownsampleGrayImages=true " & _
    '                            '                "-dMonoImageDownsampleType=/Subsample -dMonoImageResolution=72 -dDownsampleMonoImages=true " & _
    '                            '                "-dDetectDuplicateImages=true -dCompressFonts=true " & _
    '                            '                "-dNOPAUSE -dQUIET -dBATCH " & _
    '                            '                "-sOutputFile=""" & Temppath & """ """ & inputpath & """"
    '                            psi.Arguments = _
    '                                    "-sDEVICE=pdfwrite -dCompatibilityLevel=1.4 " & _
    '                                    "-dPDFSETTINGS=/printer " & _
    '                                    "-dColorImageDownsampleType=/Bicubic -dColorImageResolution=250 -dDownsampleColorImages=true " & _
    '                                    "-dGrayImageDownsampleType=/Bicubic -dGrayImageResolution=250 -dDownsampleGrayImages=true " & _
    '                                    "-dMonoImageDownsampleType=/Subsample -dMonoImageResolution=300 -dDownsampleMonoImages=true " & _
    '                                    "-dDetectDuplicateImages=true -dCompressFonts=true " & _
    '                                    "-dNOPAUSE -dQUIET -dBATCH " & _
    '                                        "-sOutputFile=""" & Temppath & """ """ & inputpath & """"

    '                            Using process As New Process()
    '                                psi.FileName = "C:\Program Files\gs\gs10.02.1\bin\gswin64c.exe" ' Full path to Ghostscript
    '                                process.StartInfo = psi
    '                                process.Start()
    '                                ' Wait for the Ghostscript process to exit
    '                                process.WaitForExit()

    '                                AddPasswordToPDF(Temppath, pdffinal, txtPassword.Text)
    '                                lvAttched.Items.Add(item)
    '                            End Using

    '                        Else
    '                            MsgBox("Error on lvAttched_DragDrop(): Ghostcript not installed ask Administrator for assistance. Path not found.", MsgBoxStyle.Exclamation, "PDF Editor")
    '                            Exit Sub
    '                        End If
    '                    Else
    '                        ProtectPdfWithGhostscript(filePath, pdffinal, txtPassword.Text)
    '                        'AddPasswordToPDF(Temppath, pdffinal, txtPassword.Text)
    '                    End If
    '                Else
    '                    MsgBox("PDF File Only")
    '                    Exit Sub
    '                End If
    '            Next
    '            PicProgress.Visible = False
    '            MsgBox("Process Complete!, Your File is saved to your Desktop and search for the folder named CompressedFile.")
    '            'txtPassword.Text = ""
    '            lvAttched.Clear()
    '        End If
    '        PicProgress.Visible = False
    '    Catch ex As Exception
    '        Dim lvMsg As String
    '        lvMsg = "PDF File must not have password or Corrupted PDF File." & Chr(13) & _
    '                       "Please try Again."
    '        MsgBox(lvMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "PDFeditor")
    '        PicProgress.Visible = False
    '    End Try

    'End Sub

    Private Sub lvAttched_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles lvAttched.DragDrop
        Try
            PicProgress.Visible = True

            ' Ensure desktop target folder exists
            Dim targetFolder As String = Path.Combine(desktopPath, folderName)
            If Not Directory.Exists(targetFolder) Then Directory.CreateDirectory(targetFolder)

            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                Dim files() As String = DirectCast(e.Data.GetData(DataFormats.FileDrop), String())

                For Each filePath As String In files
                    If Path.GetExtension(filePath).Equals(".pdf", StringComparison.OrdinalIgnoreCase) Then

                        Dim cleanFileName As String = Path.GetFileNameWithoutExtension(filePath) & ".pdf"
                        Dim pdffinal As String = Path.Combine(targetFolder, cleanFileName)

                        If chkCompressed.Checked = True Then
                            ' -------------------------------------------------------------------
                            ' Apply Compression Pipeline with targetDpi = 96
                            ' -------------------------------------------------------------------
                            Dim success As Boolean = CompressPdfPipeline(filePath, pdffinal, targetDpi:=96, password:=txtPassword.Text)

                            If success Then
                                Dim item As New ListViewItem(cleanFileName)
                                item.SubItems.Add(pdffinal)
                                lvAttched.Items.Add(item)
                            Else
                                MsgBox("Failed to compress: " & cleanFileName, MsgBoxStyle.Exclamation, "PDF Editor")
                            End If
                        Else
                            ' Uncompressed Path - Apply password directly
                            ProtectPdfWithGhostscript(filePath, pdffinal, txtPassword.Text)

                            Dim item As New ListViewItem(cleanFileName)
                            item.SubItems.Add(pdffinal)
                            lvAttched.Items.Add(item)
                        End If

                    Else
                        MsgBox("Only PDF files are supported.", MsgBoxStyle.Information, "PDF Editor")
                        Exit Sub
                    End If

                    Application.DoEvents()
                Next

                PicProgress.Visible = False
                MsgBox("Process Complete! Files saved to your Desktop in '" & folderName & "'.", MsgBoxStyle.Information, "PDF Editor")
            End If

        Catch ex As Exception
            MsgBox("Error: " & ex.Message & vbCrLf & "Please make sure the PDF is not password protected or corrupted.", MsgBoxStyle.Critical, "PDF Editor")
        Finally
            PicProgress.Visible = False
        End Try
    End Sub

    Private Sub ProtectPdfWithGhostscript(ByVal inputPdf As String, ByVal outputPdf As String, ByVal userPass As String)
        Dim gsPath As String = "C:\Program Files\gs\gs10.02.1\bin\gswin64c.exe" ' Adjust your Ghostscript path here

        ' Note: owner password left empty here by not including -sOwnerPassword or passing empty string
        'Dim args As String = "-sDEVICE=pdfwrite -dCompatibilityLevel=1.7 -dPDFSETTINGS=/default -dNOPAUSE -dQUIET -dBATCH " & _
        '                     "-dCompressPages=false -dCompressFonts=false -dCompressImages=false " & _
        '                     "-sUserPassword=" & userPass & " " & _
        '                     "-dEncryptionR=4 -dKeyLength=128 -dPermissions=4 " & _
        '                     "-sOutputFile=""" & outputPdf & """ """ & inputPdf & """"

        Dim args As String = _
                    "-sDEVICE=pdfwrite -dCompatibilityLevel=1.7 -dPDFSETTINGS=/default -dNOPAUSE -dQUIET -dBATCH " & _
                    "-dCompressPages=false -dCompressFonts=false -dCompressImages=false " & _
                    "-sUserPassword=""" & userPass & """ " & _
                    "-sOwnerPassword=""" & userPass & """ " & _
                    "-dEncryptionR=3 -dKeyLength=128 " & _
                    "-sOutputFile=""" & outputPdf & """ """ & inputPdf & """"

        If Not File.Exists(inputPdf) Then
            Throw New Exception("Input PDF does not exist: " & inputPdf)
        End If

        Dim outputFolder As String = Path.GetDirectoryName(outputPdf)
        If Not Directory.Exists(outputFolder) Then
            Directory.CreateDirectory(outputFolder)
        End If

        Dim psi As New ProcessStartInfo(gsPath, args)
        psi.UseShellExecute = False
        psi.CreateNoWindow = True
        psi.RedirectStandardOutput = True
        psi.RedirectStandardError = True

        Using process As Process = process.Start(psi)
            Dim output As String = process.StandardOutput.ReadToEnd()
            Dim err As String = process.StandardError.ReadToEnd()
            process.WaitForExit()

            If process.ExitCode = 0 Then
                If File.Exists(outputPdf) Then
                    MsgBox("PDF processed successfully: " & outputPdf)
                Else
                    MsgBox("Ghostscript ran but output file was not created!", MsgBoxStyle.Critical)
                End If
            Else
                MsgBox("Ghostscript error: " & vbCrLf & err, MsgBoxStyle.Critical)
            End If
        End Using
    End Sub


    Public Function CompressPdfPipeline(ByVal inputFile As String, ByVal outputFile As String, Optional ByVal targetDpi As Integer = 96, Optional ByVal password As String = "") As Boolean
        Dim gsPath As String = "C:\Program Files\gs\gs10.02.1\bin\gswin64c.exe"
        Dim cpdfPath As String = "C:\Tools\cpdf\cpdf.exe" ' Adjust path to your cpdf.exe location

        ' Define temporary working directory
        Dim tempFolder As String = "C:\Compressed\"
        If Not Directory.Exists(tempFolder) Then Directory.CreateDirectory(tempFolder)

        Dim tempGsPath As String = Path.Combine(tempFolder, "gs_" & Guid.NewGuid().ToString() & ".pdf")
        Dim tempCpdfPath As String = Path.Combine(tempFolder, "cpdf_" & Guid.NewGuid().ToString() & ".pdf")

        Try
            ' Validate Ghostscript existence
            If Not File.Exists(gsPath) Then
                MsgBox("Ghostscript executable not found at: " & gsPath, MsgBoxStyle.Exclamation, "PDF Editor")
                Return False
            End If

            ' -------------------------------------------------------------
            ' PASS 1: Ghostscript (Downsample Images to targetDpi)
            ' -------------------------------------------------------------
            Dim gsPsi As New ProcessStartInfo(gsPath) With { _
                .UseShellExecute = False, _
                .CreateNoWindow = True, _
                .RedirectStandardError = True, _
                .Arguments = String.Format( _
                    "-sDEVICE=pdfwrite -dCompatibilityLevel=1.5 " & _
                    "-dPDFSETTINGS=/screen " & _
                    "-dDownsampleColorImages=true -dColorImageDownsampleType=/Bicubic -dColorImageResolution={0} " & _
                    "-dDownsampleGrayImages=true -dGrayImageDownsampleType=/Bicubic -dGrayImageResolution={0} " & _
                    "-dDownsampleMonoImages=true -dMonoImageDownsampleType=/Subsample -dMonoImageResolution={0} " & _
                    "-dAutoFilterColorImages=false -dColorImageFilter=/DCTEncode " & _
                    "-dAutoFilterGrayImages=false -dGrayImageFilter=/DCTEncode " & _
                    "-dDetectDuplicateImages=true -dCompressFonts=true -dSubsetFonts=true " & _
                    "-dNOPAUSE -dQUIET -dBATCH " & _
                    "-sOutputFile=""{1}"" ""{2}""", _
                    targetDpi, tempGsPath, inputFile) _
            }

            Using gsProcess As Process = Process.Start(gsPsi)
                gsProcess.WaitForExit()
                If gsProcess.ExitCode <> 0 OrElse Not File.Exists(tempGsPath) Then
                    Return False
                End If
            End Using

            ' -------------------------------------------------------------
            ' PASS 2: Coherent PDF (cpdf -squeeze & metadata removal)
            ' -------------------------------------------------------------
            Dim sourceForPassword As String = tempGsPath

            If File.Exists(cpdfPath) Then
                Dim cpdfPsi As New ProcessStartInfo(cpdfPath) With { _
                    .UseShellExecute = False, _
                    .CreateNoWindow = True, _
                    .Arguments = String.Format("-squeeze -metadata false ""{0}"" -o ""{1}""", tempGsPath, tempCpdfPath) _
                }

                Using cpdfProcess As Process = Process.Start(cpdfPsi)
                    cpdfProcess.WaitForExit()
                    If cpdfProcess.ExitCode = 0 AndAlso File.Exists(tempCpdfPath) Then
                        sourceForPassword = tempCpdfPath
                    End If
                End Using
            End If

            ' -------------------------------------------------------------
            ' PASS 3: Apply Password Protection or Move File
            ' -------------------------------------------------------------
            If Not String.IsNullOrEmpty(password) Then
                AddPasswordToPDF(sourceForPassword, outputFile, password)
            Else
                If File.Exists(outputFile) Then File.Delete(outputFile)
                File.Move(sourceForPassword, outputFile)
            End If

            Return True

        Catch ex As Exception
            Console.WriteLine("CompressPdfPipeline Error: " & ex.Message)
            Return False
        Finally
            ' Cleanup temporary files safely
            If File.Exists(tempGsPath) Then Try : File.Delete(tempGsPath) : Catch : End Try
            If File.Exists(tempCpdfPath) Then Try : File.Delete(tempCpdfPath) : Catch : End Try
        End Try
    End Function

    Sub ReducePDFOptimizer(ByVal inputFile As String, ByVal outputFile As String, ByVal targetFileSizeBytes As Long, ByVal fileSizeBytes As Long)
        Try

            Dim compressionFactor As Single = 1.0
            Dim originalFileSize As Long

            Dim reader As New PdfReader(inputFile)
            Using output As New FileStream(outputFile, FileMode.Create)
                Dim stamper As New PdfStamper(reader, output)
                Dim memoryStream As New MemoryStream()
                ' Reduce image quality and compression level to achieve target size
                While memoryStream.Length < targetFileSizeBytes AndAlso compressionFactor > 0.1
                    stamper.Writer.CompressionLevel = 9
                    stamper.SetFullCompression()

                    'For pageNum = 1 To reader.NumberOfPages
                    '    Dim importedPage As PdfImportedPage = stamper.GetImportedPage(reader, pageNum)
                    '    Dim pdfContentByte As PdfContentByte = stamper.GetOverContent(pageNum)

                    '    '    'Scale the page content to adjust image quality
                    '    pdfContentByte.AddTemplate(importedPage, 1, 0, 0, 1, 0, 0)
                    'Next

                    memoryStream.SetLength(0)
                    stamper.Close()

                    ' Calculate the new file size
                    memoryStream.WriteTo(memoryStream)
                    originalFileSize = memoryStream.Length

                    ' Adjust the compression factor
                    compressionFactor -= 0.1
                End While


                ' Adjust the file size to match the target size
                'If fileSizeBytes > targetFileSizeBytes Then
                '    Dim scale As Single = targetFileSizeBytes / fileSizeBytes
                '    Dim pdfReader2 As New PdfReader(New RandomAccessFileOrArray(inputFile), Nothing)
                '    Using outputStream As New FileStream(outputFile, FileMode.Create)
                '        Dim pdfStamper As New PdfStamper(pdfReader2, outputStream)
                '        For pageNum = 1 To pdfReader2.NumberOfPages
                '            Dim importedPage As PdfImportedPage = pdfStamper.GetImportedPage(pdfReader2, pageNum)
                '            Dim pdfContentByte As PdfContentByte = pdfStamper.GetOverContent(pageNum)

                '            ' Scale the page content
                '            ' pdfContentByte.AddTemplate(importedPage, scale, 0, 0, scale, 0, 0)
                '        Next

                '        pdfStamper.Close()
                '    End Using
                'Else
                '    ' If the file size is already smaller than the target, simply copy the input file
                '    File.Copy(inputFile, outputFile)
                'End If
                reader.Close()
                stamper.Close()
            End Using ' PdfReader, PdfStamper, and PdfWriter will be automatically closed
        Catch ex As Exception

            MsgBox(ex.Message, ex.Source, MsgBoxStyle.Critical)

        End Try


    End Sub

    'Public Sub CompressPDFWithAdobeAcrobatBestCompression(ByVal inputFilePath As String, ByVal outputFilePath As String, ByVal jobOptionsFile As String)
    '    ' Specify the path to the Acrobat Distiller executable
    '    Dim acrobatDistillerPath As String
    '    acrobatDistillerPath = txtdistiller.Text

    '    ' Define the command to execute with custom job options file
    '    Dim command As String = """" & acrobatDistillerPath & """ /N /Q /S /O """ & outputFilePath & """ /job """ & jobOptionsFile & """ """ & inputFilePath & """"
    '    'Dim command As String = $"""{acrobatDistillerPath}"" /N /Q /S /O ""{outputFilePath}"" /job ""{jobOptionsFile}"" ""{inputFilePath}"""

    '    ' Start the Acrobat Distiller process
    '    'Dim processInfo As New ProcessStartInfo("cmd.exe", $"/c {command}")
    '    Dim processInfo As New ProcessStartInfo("cmd.exe", "/c " & command)
    '    processInfo.CreateNoWindow = True
    '    processInfo.UseShellExecute = False
    '    processInfo.RedirectStandardOutput = True

    '    Dim process As New Process()
    '    process.StartInfo = processInfo
    '    process.Start()

    '    ' Wait for the Acrobat Distiller process to finish
    '    process.WaitForExit()
    '    process.Close()
    'End Sub

    Sub OptimizePdfSize(ByVal inputFilePath As String, ByVal outputFilePath As String)
        Try
            ' Create an instance of the Acrobat application
            Dim acroApp As Object = CreateObject("AcroExch.App")

            ' Create an AVDoc (PDF view) and open the input PDF
            Dim avDoc As Object = CreateObject("AcroExch.AVDoc")
            avDoc.Open(inputFilePath, "")

            ' Get the PDDoc (PDF document) from the AVDoc
            Dim pdfDoc As Object = avDoc.GetPDDoc()

            ' Create optimization parameters
            Dim jsObjOptParams As Object = CreateObject("AcroExch.OCG.OCG")
            jsObjOptParams = pdfDoc.CreateOptParams()

            'fonts
            jsObjOptParams.AddKey("SubsetFonts", True) ' subset fonts
            jsObjOptParams.AddKey("CompressFonts", True) ' Compress font objects
            jsObjOptParams.AddKey("EmbedAllFonts", True) ' Embed all fonts
            jsObjOptParams.AddKey("SubsetFontsBelow", -1) ' Embed all fonts, regardless of subset threshold
            jsObjOptParams.AddKey("PreserveOPIComments", True) ' Remove OPI (Open Prepress Interface) comments
            'image
            jsObjOptParams.AddKey("DownsampleColorImages", True) ' Downsample color images
            jsObjOptParams.AddKey("DownsampleGrayImages", True) ' Downsample grayscale images
            jsObjOptParams.AddKey("DownsampleMonoImages", True) ' Downsample monochrome images
            jsObjOptParams.AddKey("ColorImageDownsampleThreshold", 2) ' Color image downsample threshold (adjust as needed)
            jsObjOptParams.AddKey("GrayImageDownsampleThreshold", 2) ' Grayscale image downsample threshold (adjust as needed)
            jsObjOptParams.AddKey("MonoImageDownsampleThreshold", 2) ' Monochrome image downsample threshold (adjust as needed)
            jsObjOptParams.AddKey("ColorImageResolution", 150) ' Color image resolution after downsampling (adjust as needed)
            jsObjOptParams.AddKey("GrayImageResolution", 150) ' Grayscale image resolution after downsampling (adjust as needed)
            jsObjOptParams.AddKey("MonoImageResolution", 150) ' Monochrome image resolution after downsampling (adjust as needed)
            jsObjOptParams.AddKey("ColorImageCompression", 2) ' Color image compression: 0 (auto), 1 (JPEG), 2 (ZIP), 3 (JPEG2000)
            jsObjOptParams.AddKey("GrayImageCompression", 2) ' Grayscale image compression: 0 (auto), 1 (JPEG), 2 (ZIP), 3 (JPEG2000)
            jsObjOptParams.AddKey("MonoImageCompression", 2) ' Monochrome image compression: 0 (auto), 1 (CCITT Group 4), 2 (ZIP), 3 (RunLength)

            jsObjOptParams.AddKey("CompressStreams", True) ' Compress content streams
            jsObjOptParams.AddKey("FlattenTransparency", True) ' Flatten transparency
            jsObjOptParams.AddKey("DiscardEmbeddedFonts", True) ' Discard embedded fonts
            jsObjOptParams.AddKey("DiscardBookmarks", True) ' Discard bookmarks
            jsObjOptParams.AddKey("DiscardAnnotations", True) ' Discard annotations
            jsObjOptParams.AddKey("FlateEncodePages", True) ' Use Flate compression for pages
            jsObjOptParams.AddKey("FlateEncode", True) ' Use Flate compression for objects
            jsObjOptParams.AddKey("ImageResolution", 72) ' Image resolution (DPI)
            jsObjOptParams.AddKey("AllOptimizations", True)

            ' Perform optimization to reduce file size
            ' Save the PDF with optimization options
            pdfDoc.Save(1, outputFilePath, 1, "ReduceFileSize") ' 1 indicates saving changes.



            ' Close the AVDoc and release resources
            avDoc.Close(1) ' 1 indicates closing without saving changes

            ' Quit the Acrobat application
            acroApp.Exit()
        Catch ex As Exception
            ' Handle exceptions (e.g., log the error)
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error on: OptimizePdfSize()")
        End Try
    End Sub



    Sub ReducePdfSizeAcrobat(ByVal inputFilePath As String, ByVal outputFilePath As String)
        Try
            ' Create an instance of the Acrobat application
            Dim acroApp As New Acrobat.AcroApp()

            ' Open the input PDF file
            Dim avDoc As Acrobat.AcroAVDoc = acroApp.GetAVDoc(inputFilePath)

            ' Get the PDDoc (PDF document) from the AVDoc
            Dim pdfDoc As Acrobat.AcroPDDoc = avDoc.GetPDDoc()

            ' Optimize the PDF to reduce file size
            pdfDoc.Save(1, outputFilePath) ' 1 indicates saving the changes

            ' Close the AVDoc and release resources
            avDoc.Close(1) ' 1 indicates closing without saving changes

            ' Quit the Acrobat application
            acroApp.Exit()
        Catch ex As Exception
            ' Handle exceptions (e.g., log the error)
            'Console.WriteLine($"Error: {ex.Message}")
        End Try
    End Sub
    Public Sub ReducePDFSize(ByVal inputFilePath As String, ByVal outputFilePath As String)
        Dim reader As New PdfReader(inputFilePath)
        Dim document As New Document()
        Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(outputFilePath, FileMode.Create))
        writer.SetPageSize(PageSize.LETTER)
        writer.SetPdfVersion(PdfWriter.PDF_VERSION_1_4)
        writer.CompressionLevel = 9
        writer.SetMargins(0.5, 0.5, 0.5, 0.5)
        writer.SetPageSize(PageSize.LETTER)
        writer.FullCompression.ToString()
        writer.SetFullCompression()

        document.Open()

        Dim cb As PdfContentByte = writer.DirectContent
        Dim page As PdfImportedPage

        For pageNum As Integer = 1 To reader.NumberOfPages
            page = writer.GetImportedPage(reader, pageNum)
            document.NewPage()
            cb.AddTemplate(page, 0, 0)
        Next

        document.Close()
        writer.Close()
        reader.Close()
    End Sub

    Sub ReduceResolution(ByVal inputFile As String, ByVal outputFile As String, ByVal pageNumber As Integer, ByVal targetDPI As Integer)


        ' Set optimization options (adjust these as needed)
        Dim optimizationOptions As PDFOptimizeOptions = New PDFOptimizeOptions()
        optimizationOptions.CompressionLevel = PdfStream.BEST_COMPRESSION ' Adjust compression level
        optimizationOptions.ImageQuality = 0 ' Adjust image quality (0-100)
        optimizationOptions.DPI = 72 ' Adjust DPI


        ' Optimize the PDF file
        OptimizePdf(inputFile, outputFile, optimizationOptions)

        'MsgBox("PDF optimized and saved to " & outputFile)



        'Dim reader As New PdfReader(inputFile)
        'Dim stamper As New PdfStamper(reader, New FileStream(outputFile, FileMode.Create))

        '' Get the page dictionary
        'Dim pageDict As PdfDictionary = reader.GetPageN(pageNumber)

        '' Get the resources dictionary for the page
        'Dim resources As PdfDictionary = DirectCast(PdfReader.GetPdfObject(pageDict.Get(PdfName.RESOURCES)), PdfDictionary)

        '' Get the XObject dictionary
        'Dim xObjects As PdfDictionary = DirectCast(resources.Get(PdfName.XOBJECT), PdfDictionary)

        'If xObjects IsNot Nothing Then
        '    For Each key As PdfName In xObjects.Keys
        '        Dim obj As PdfObject = xObjects.Get(key)
        '        Dim xObjectDict As PdfDictionary = DirectCast(PdfReader.GetPdfObject(obj), PdfDictionary)

        '        If xObjectDict IsNot Nothing AndAlso xObjectDict.Get(PdfName.SUBTYPE) IsNot Nothing AndAlso xObjectDict.Get(PdfName.SUBTYPE).ToString() = PdfName.IMAGE.ToString() Then
        '            ' Reduce the DPI by modifying the /Filter property
        '            Dim filter As PdfObject = xObjectDict.Get(PdfName.FILTER)
        '            If filter IsNot Nothing Then
        '                ' If /Filter is present, add /DCTDecode to apply JPEG compression
        '                xObjectDict.Put(PdfName.FILTER, New PdfName("/DCTDecode"))
        '            End If

        '            ' Add /DecodeParms to specify the target DPI
        '            Dim decodeParms As PdfDictionary = New PdfDictionary()
        '            decodeParms.Put(PdfName.DP, New PdfNumber(targetDPI))
        '            xObjectDict.Put(PdfName.DECODEPARMS, decodeParms)
        '        End If
        '    Next
        'End If

        '' Close the stamper to save the changes
        'stamper.Close()
    End Sub

    Private Sub lvAttched_ItemDrag(ByVal sender As Object, ByVal e As ItemDragEventArgs) Handles lvAttched.ItemDrag
        Try

            Dim selectedItems As ListView.SelectedListViewItemCollection = lvAttched.SelectedItems
            If selectedItems.Count > 0 Then
                Dim filePaths As New List(Of String)
                For Each selectedItem As ListViewItem In selectedItems
                    ' Retrieve the file path from the subitem.
                    Dim filePath As String = selectedItem.SubItems(1).Text
                    filePaths.Add(filePath)
                Next

                ' Start the drag-and-drop operation.
                Dim data As New DataObject(DataFormats.FileDrop, filePaths.ToArray())
                DoDragDrop(data, DragDropEffects.Copy)
            End If

        Catch ex As Exception
            MsgBox("PDF File must not have password or Corrupted PDF File.Please try Again.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "PDFeditor")
        End Try
    End Sub
    Sub AddPasswordToPDF(ByVal pdfPath As String, ByVal outputPath As String, ByVal password As String)
        Try
            Dim reader As New PdfReader(pdfPath)

            'Using output As New FileStream(outputPath, FileMode.Create)
            '    Dim stamper As New PdfStamper(reader, output)

            '    ' Set encryption (optional)
            '    stamper.SetEncryption(PdfWriter.STRENGTH128BITS, password, "", PdfWriter.AllowPrinting)

            '    ' Disable compression for new objects
            '    stamper.Writer.CompressionLevel = PdfStream.NO_COMPRESSION

            '    ' Remove unused objects
            '    reader.RemoveUnusedObjects()

            '    ' Re-write each page content stream with no compression
            '    For i As Integer = 1 To reader.NumberOfPages
            '        Dim contentBytes As Byte() = reader.GetPageContent(i)
            '        Dim newStream As New PdfStream(contentBytes)
            '        newStream.Remove(PdfName.FILTER) ' Remove existing compression filter

            '        ' Add the new stream without compression
            '        Dim indirectRef As PdfIndirectObject = stamper.Writer.AddToBody(newStream)
            '        Dim pageDict As PdfDictionary = reader.GetPageN(i)
            '        pageDict.Put(PdfName.CONTENTS, indirectRef.IndirectReference)
            '    Next

            '    stamper.Close()
            '    reader.Close()
            'End Using

            'Dim reader As New PdfReader(pdfPath)

            Using output As New FileStream(outputPath, FileMode.Create)
                Dim stamper As New PdfStamper(reader, output)

                ' Set encryption (optional)
                stamper.SetEncryption(PdfWriter.STRENGTH128BITS, password, "", PdfWriter.AllowPrinting)

                ' Disable compression for new objects
                stamper.Writer.CompressionLevel = PdfStream.NO_COMPRESSION

                ' Remove unused compressed objects
                reader.RemoveUnusedObjects()

                ' Re-write each page content stream with no compression
                For i As Integer = 1 To reader.NumberOfPages
                    Dim contentBytes As Byte() = reader.GetPageContent(i)
                    Dim newStream As New PdfStream(contentBytes)
                    newStream.Remove(PdfName.FILTER) ' Remove existing compression filter

                    ' Add the new uncompressed stream to the writer
                    Dim indirectRef As PdfIndirectObject = stamper.Writer.AddToBody(newStream)
                    Dim pageDict As PdfDictionary = reader.GetPageN(i)
                    pageDict.Put(PdfName.CONTENTS, indirectRef.IndirectReference)
                Next

                stamper.Close()
            End Using

            reader.Close()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

    End Sub
    'AddPasswordToPDF("input.pdf", "output.pdf", "StLukes120@Ermita")

    Sub ProtectExcelWorkbook(ByVal workbookPath As String, ByVal password As String)
        Dim app As New Microsoft.Office.Interop.Excel.Application()
        'Dim wb As Workbook = app.Workbooks.Open(workbookPath)
        'wb.Password = password
        'wb.Save()
        'wb.Close()
        'app.Quit()
    End Sub
    'ProtectExcelWorkbook("workbook.xlsx", "StLukes120@Ermita")


    Private Sub cmdCompress_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCompress.Click
        ' Specify the path of the input PDF file
        Dim inputFile As String = Trim(txtTarget.Text)

        ' Specify the path of the output reduced PDF file
        Dim outputFile As String = Trim(txtOutput.Text)
        ' Specify the target file size in bytes (5MB = 5 * 1024 * 1024 bytes)

        ' Attempt to reduce the PDF file size
        ReducePdfToTargetSize(inputFile, outputFile, targetFileSizeBytes)
        AddPasswordToPDF(inputFile, outputFile, txtPassword.Text)
        File.Copy(inputFile, outputFile)
        'ReducePdfToTargetSize(inputFile, outputFile, targetFileSizeBytes)

        MsgBox("PDF file size reduced.")
    End Sub

    Sub CompressPdf(ByVal inputFile As String, ByVal outputFile As String, ByVal compressionLevel As Integer)
        Dim reader As New PdfReader(inputFile)
        Dim document As New Document()
        Dim writer As PdfCopy = New PdfCopy(document, New FileStream(outputFile, FileMode.Create))
        ' Set compression level for the writer
        writer.SetPageSize(PageSize.A4)
        writer.CompressionLevel = compressionLevel
        writer.SetFullCompression()
        document.Open()

        ' Loop through the pages of the PDF
        For pageNum = 1 To reader.NumberOfPages
            Dim importedPage As PdfImportedPage = writer.GetImportedPage(reader, pageNum)

            writer.AddPage(importedPage)
        Next

        ' Close the writer to save the changes
        writer.Close()
        document.Close()
    End Sub




    Sub ReducePdfToTargetSize(ByVal inputFile As String, ByVal outputFile As String, ByVal targetFileSizeBytes As Long)
        ' Create a reader for the input PDF
        Dim reader As New PdfReader(inputFile)

        ' Create a stamper for the output PDF
        Dim stamper As New PdfStamper(reader, New FileStream(outputFile, FileMode.Create))

        ' Set compression levels for images
        Dim compressionLevel As Integer = 9 ' Highest compression level (0-9)

        ' Loop through the pages of the PDF
        For pageNum = 1 To reader.NumberOfPages
            ' Get the page content
            Dim pdfContentByte As PdfContentByte = stamper.GetOverContent(pageNum)

            ' Set image compression for the page
            'pdfContentByte.SetCompressionLevel(compressionLevel)

            ' Add content to the page as needed
            ' You can use pdfContentByte to add content to the page

            ' Mark the page for garbage collection to free up resources
            pdfContentByte = Nothing
        Next

        ' Close the stamper to save the changes
        stamper.Close()

        ' Ensure the created PDF meets the target file size
        Dim createdFileSize As Long = New FileInfo(outputFile).Length

        If createdFileSize > targetFileSizeBytes Then
            ' The created PDF is larger than the target size
            ' You may need to further optimize the content or consider other techniques
            Console.WriteLine("The created PDF is larger than the target size. Further optimization may be needed.")
        Else
            Console.WriteLine("PDF file size reduced to approximately 5MB.")
        End If
    End Sub





    'Sub ReducePdfToTargetSize(ByVal inputFile As String, ByVal outputFile As String, ByVal targetFileSizeBytes As Long)
    '    Dim compressionFactor As Single = 1.0
    '    Dim originalFileSize As Long

    '    ' Initialize iTextSharp
    '    Dim pdfReader As New PdfReader(inputFile)
    '    Using memoryStream As New MemoryStream()
    '        Dim pdfStamper As New PdfStamper(pdfReader, memoryStream)

    '        ' Reduce image quality and compression level to achieve target size
    '        While memoryStream.Length < targetFileSizeBytes AndAlso compressionFactor > 0.1
    '            'pdfStamper.Writer.SetCompressionLevel(PdfStream.BEST_COMPRESSION)
    '            pdfStamper.Writer.SetFullCompression()

    '            For pageNum = 1 To pdfReader.NumberOfPages
    '                Dim importedPage As PdfImportedPage = pdfStamper.GetImportedPage(pdfReader, pageNum)
    '                Dim pdfContentByte As PdfContentByte = pdfStamper.GetOverContent(pageNum)

    '                ' Scale the page content to adjust image quality
    '                pdfContentByte.AddTemplate(importedPage, 1, 0, 0, 1, 0, 0)
    '            Next

    '            memoryStream.SetLength(0)
    '            pdfStamper.Close()

    '            ' Calculate the new file size
    '            memoryStream.WriteTo(memoryStream)
    '            originalFileSize = memoryStream.Length

    '            ' Adjust the compression factor
    '            compressionFactor -= 0.1
    '        End While
    '    End Using

    '    ' Adjust the file size to match the target size
    '    If originalFileSize > targetFileSizeBytes Then
    '        Dim scale As Single = targetFileSizeBytes / originalFileSize
    '        Dim pdfReader2 As New PdfReader(New RandomAccessFileOrArray(inputFile), Nothing)
    '        Using outputStream As New FileStream(outputFile, FileMode.Create)
    '            Dim pdfStamper As New PdfStamper(pdfReader2, outputStream)

    '            For pageNum = 1 To pdfReader2.NumberOfPages
    '                Dim importedPage As PdfImportedPage = pdfStamper.GetImportedPage(pdfReader2, pageNum)
    '                Dim pdfContentByte As PdfContentByte = pdfStamper.GetOverContent(pageNum)

    '                ' Scale the page content
    '                pdfContentByte.AddTemplate(importedPage, scale, 0, 0, scale, 0, 0)
    '            Next

    '            pdfStamper.Close()
    '        End Using
    '    Else
    '        ' If the file size is already smaller than the target, simply copy the input file
    '        File.Copy(inputFile, outputFile)
    '    End If
    'End Sub


    'Sub ReducePdfToTargetSize(ByVal inputFile As String, ByVal outputFile As String, ByVal targetFileSizeBytes As Long)
    '    Dim document As New Document()
    '    Dim writer As PdfWriter = PdfWriter.GetInstance(document, New FileStream(outputFile, FileMode.Create))

    '    ' Set compression level for the new PDF
    '    writer.SetFullCompression()

    '    'document.Open()

    '    ' Add content to the new PDF (e.g., text, images, etc.)
    '    ' You can use PdfContentByte to add content to the document

    '    'document.Close()

    '    ' Ensure the created PDF meets the target file size
    '    Dim createdFileSize As Long = New FileInfo(outputFile).Length

    '    If createdFileSize > targetFileSizeBytes Then
    '        ' The created PDF is larger than the target size
    '        ' You may need to further optimize the content, reduce image quality, or make other adjustments
    '        Console.WriteLine("The created PDF is larger than the target size. Further optimization may be needed.")
    '    Else
    '        Console.WriteLine("PDF file size reduced to approximately 5MB.")
    '    End If
    'End Sub

    Private Sub lvMerge_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs) Handles lvMerge.DragDrop
        Try
            PicProgress.Visible = True

            ' Retrieve the dragged item
            Dim draggedItem As ListViewItem = DirectCast(e.Data.GetData(GetType(ListViewItem)), ListViewItem)

            ' Get the drop location
            Dim dropPoint As Point = Point.Empty
            dropPoint.X = e.X
            dropPoint.Y = e.Y
            dropPoint = lvMerge.PointToClient(dropPoint)

            ' Get the item at the drop location
            Dim dropItem As ListViewItem = lvMerge.GetItemAt(dropPoint.X, dropPoint.Y)

            ' Perform the move operation
            If dropItem IsNot Nothing Then
                Dim indexDragged As Integer = draggedItem.Index
                Dim indexDrop As Integer = dropItem.Index

                ' Remove the dragged item
                lvMerge.Items.RemoveAt(indexDragged)

                ' Insert the dragged item at the drop location
                lvMerge.Items.Insert(indexDrop, draggedItem)
            End If

            If e.Data.GetDataPresent(DataFormats.FileDrop) Then

                Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the dropped files and add them to the ListView.

                For Each filePath As String In files
                    ' Add code to parse the file name and add it to the ListView.
                    Dim fileName As String = System.IO.Path.GetFileName(filePath)
                    xfileName = System.IO.Path.GetFileName(filePath)
                    If Path.GetExtension(filePath).Equals(".pdf", StringComparison.OrdinalIgnoreCase) Then

                        Dim fileInfo As New FileInfo(filePath)
                        Dim fileSizeBytes As Long = fileInfo.Length

                        Dim item As New ListViewItem(filePath)
                        item.SubItems.Add(filePath & xfileName) ' Store the full file path in a subitem.
                        lvMerge.Items.Add(item)


                    Else
                        MsgBox("PDF Only!")
                        Exit Sub
                    End If
                Next

            End If
            PicProgress.Visible = False
            '  ListViewAlternateColor(lvMerge, Color.LightBlue)
        Catch ex As Exception
            Dim lvMsg As String
            lvMsg = "PDF File must not have password or Corrupted PDF File." & Chr(13) & _
                           "Please try Again."
            MsgBox(lvMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "PDFeditor")
        End Try
        'CompressPDFWithAdobeAcrobatBestCompression(filePath, outputpath & xfileName, txtJoboption.Text)
        'CompressPDFWithAdobeAcrobat(filePath, outputpath)
    End Sub

    Private Sub lvMerge_ItemDrag(ByVal sender As Object, ByVal e As ItemDragEventArgs) Handles lvMerge.ItemDrag
        'Dim selectedItems As ListView.SelectedListViewItemCollection = lvMerge.SelectedItems
        'If selectedItems.Count > 0 Then
        '    Dim filePaths As New List(Of String)
        '    For Each selectedItem As ListViewItem In selectedItems
        '         Retrieve the file path from the subitem.
        '        Dim filePath As String = selectedItem.SubItems(0).Text
        '        filePaths.Add(filePath)
        '    Next
        '     Start the drag-and-drop operation.
        '    Dim data As New DataObject(DataFormats.FileDrop, filePaths.ToArray())
        '    DoDragDrop(data, DragDropEffects.Copy)
        'End If
        lvMerge.DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub
    Private Sub lvMerge_DragEnter(ByVal sender As Object, ByVal e As DragEventArgs) Handles lvMerge.DragEnter
        'If e.Data.GetDataPresent(DataFormats.FileDrop) Then
        '    Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())

        '    ' Check if the dropped file(s) is/are PDF(s)
        '    Dim isPdf = files.All(Function(file) Path.GetExtension(file).Equals(".pdf", StringComparison.OrdinalIgnoreCase))

        '    If isPdf Then
        '        e.Effect = DragDropEffects.Copy
        '    Else
        '        e.Effect = DragDropEffects.None
        '    End If
        'Else
        '    e.Effect = DragDropEffects.None
        'End If
        ' Allow dropping of items


        If e.Data.GetDataPresent(GetType(ListViewItem)) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If

    End Sub

    Private Sub btnMerge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMerge.Click
        PicProgress.Visible = True
        ' Create a list to store paths of PDF files
        Dim inputFiles As New List(Of String)
        ' Iterate through each item in the ListView
        For Each item As ListViewItem In lvMerge.Items
            ' Assuming the file path is in the first subitem
            Dim filePath As String = item.SubItems(0).Text

            ' Check if the file has a PDF extension
            If filePath.ToLower().EndsWith(".pdf") Then
                inputFiles.Add(filePath)
            End If
        Next
        ' Call the function to merge PDF files

        MergePDFs(inputFiles, outputFile)
        '  MsgBox("Merge Complete! Please see your documents on your Desktop\MergeFile")
        lvMerge.Items.Clear()
        PicProgress.Visible = False

    End Sub


    Public Function MergePdfFiles(ByVal pdfFiles() As String, ByVal outputPath As String) As Boolean
        Dim result As Boolean = False
        Dim pdfCount As Integer = 0                                                 'total input pdf file count
        Dim f As Integer = 0                                                        'pointer to current input pdf file
        Dim fileName As String = String.Empty                                       'current input pdf filename
        Dim reader As iTextSharp.text.pdf.PdfReader = Nothing
        Dim pageCount As Integer = 0                                                'cureent input pdf page count
        Dim pdfDoc As iTextSharp.text.Document = Nothing                            'the output pdf document
        Dim writer As PdfWriter = Nothing
        Dim cb As PdfContentByte = Nothing
        'Declare a variable to hold the imported pages
        Dim page As PdfImportedPage = Nothing
        Dim rotation As Integer = 0
        'Declare a font to used for the bookmarks
        Dim bookmarkFont As iTextSharp.text.Font = iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA, _
                                                                  12, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLUE)

        Try
            pdfCount = pdfFiles.Length
            If pdfCount > 1 Then
                'Open the 1st pad using PdfReader object
                fileName = pdfFiles(f)
                reader = New iTextSharp.text.pdf.PdfReader(fileName)
                'Get page count
                pageCount = reader.NumberOfPages
                'Instantiate an new instance of pdf document and set its margins. This will be the output pdf.
                'NOTE: bookmarks will be added at the 1st page of very original pdf file using its filename. The location
                'of this bookmark will be placed at the upper left hand corner of the document. So you'll need to adjust
                'the margin left and margin top values such that the bookmark won't overlay on the merged pdf page. The 
                'unit used is "points" (72 points = 1 inch), thus in this example, the bookmarks' location is at 1/4 inch from
                'left and 1/4 inch from top of the page.
                pdfDoc = New iTextSharp.text.Document(reader.GetPageSizeWithRotation(1), 18, 18, 18, 18)
                'Instantiate a PdfWriter that listens to the pdf document
                writer = PdfWriter.GetInstance(pdfDoc, New FileStream(outputPath, FileMode.Create))
                writer.CompressionLevel = 9
                writer.SetFullCompression()

                'Set metadata and open the document
                With pdfDoc
                    .AddAuthor(".")
                    .AddCreationDate()
                    .AddCreator(".")
                    .AddSubject(".")
                    'Use the filename as the title... You can give it any title of course.
                    .AddTitle(Path.GetFileNameWithoutExtension(outputPath))
                    'Add keywords, whatever keywords you want to attach to it
                    '.AddKeywords("Report, Merged PDF, " & Path.GetFileName(outputPath))
                    .Open()
                End With
                'Instantiate a PdfContentByte object

                cb = writer.DirectContent
                'Now loop thru the input pdfs
                While f < pdfCount
                    'Declare a page counter variable
                    Dim i As Integer = 0
                    'Loop thru the current input pdf's pages starting at page 1
                    While i < pageCount
                        i += 1
                        'Get the input page size
                        pdfDoc.SetPageSize(reader.GetPageSizeWithRotation(i))

                        'Create a new page on the output document
                        pdfDoc.NewPage()
                        'If it is the 1st page, we add bookmarks to the page
                        'If i = 1 Then
                        '    First create a paragraph using the filename as the heading
                        '    Dim para As New iTextSharp.text.Paragraph(IO.Path.GetFileName(fileName).ToUpper(), bookmarkFont)
                        '    Then create a chapter from the above paragraph
                        '    Dim chpter As New iTextSharp.text.Chapter(para, f + 1)
                        '    Finally add the chapter to the document
                        '    pdfDoc.Add(chpter)
                        'End If
                        'Now we get the imported page
                        page = writer.GetImportedPage(reader, i)

                        'Read the imported page's rotation
                        rotation = reader.GetPageRotation(i)
                        'Then add the imported page to the PdfContentByte object as a template based on the page's rotation
                        If rotation = 90 Then
                            cb.AddTemplate(page, 0, -1.0F, 1.0F, 0, 0, reader.GetPageSizeWithRotation(i).Height)
                        ElseIf rotation = 270 Then
                            cb.AddTemplate(page, 0, 1.0F, -1.0F, 0, reader.GetPageSizeWithRotation(i).Width + 60, -30)
                        Else
                            cb.AddTemplate(page, 1.0F, 0, 0, 1.0F, 0, 0)
                        End If



                    End While
                    'Increment f and read the next input pdf file
                    f += 1
                    If f < pdfCount Then
                        fileName = pdfFiles(f)
                        reader = New iTextSharp.text.pdf.PdfReader(fileName)
                        pageCount = reader.NumberOfPages
                    End If
                End While
                'When all done, we close the documwent so that the pdfwriter object can write it to the output file

                pdfDoc.Close()
                result = True
            End If
        Catch ex As System.Exception
            Throw New System.Exception(ex.Message)
        End Try
        Return result
    End Function
    Private Sub MergePDFs(ByVal inputFiles As List(Of String), ByVal outputFile As String)
        ' Create a FileStream for the output PDF
        ' Prompt the user for a file name using InputBox.
        Try

            Dim fileName As String = InputBox("Enter the file name:", "SLEC PDFeditor Save File", "Text.pdf")
            If Not String.IsNullOrEmpty(fileName) Then
                ' Create a SaveFileDialog instance
                Dim saveFileDialog As New SaveFileDialog()

                ' Set the default file name and extension
                saveFileDialog.FileName = fileName
                saveFileDialog.DefaultExt = "pdf"

                ' Set the initial directory (optional)
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

                ' Set the title of the dialog
                saveFileDialog.Title = "SLEC PDD Editor - Save File"

                ' Set the file filters (optional)
                saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*"

                ' Show the SaveFileDialog and check if the user clicked OK

                If saveFileDialog.ShowDialog() = DialogResult.OK Then
                    ' Get the selected file name and save the file
                    Dim filePath As String = saveFileDialog.FileName

                    ' Perform your file-saving logic here
                    ' For example, you can use StreamWriter to write content to the file
                    PicProgress.Visible = True

                    Using outputStream As New FileStream(filePath, FileMode.Create)
                        ' Create a Document
                        Using document As New Document()
                            ' Create a PdfCopy instance
                            Using pdfCopy As New PdfCopy(document, outputStream)
                                ' Open the document for writing
                                document.Open()

                                ' Iterate through each input file and merge its content
                                For Each inputFile As String In inputFiles
                                    ' Open the source PDF
                                    Using pdfReader As New PdfReader(inputFile)
                                        ' Iterate through each page and add it to the destination PDF
                                        For pageNum = 1 To pdfReader.NumberOfPages
                                            pdfCopy.AddPage(pdfCopy.GetImportedPage(pdfReader, pageNum))
                                        Next
                                    End Using
                                Next
                            End Using
                        End Using
                    End Using

                    MessageBox.Show("File saved successfully.")
                    PicProgress.Visible = False
                End If

            Else
                PicProgress.Visible = False
                MessageBox.Show("Invalid file name. Please enter a valid file name.")
                PicProgress.Visible = False
            End If

        Catch ex As Exception
            Dim lvMsg As String
            PicProgress.Visible = False
            lvMsg = "PDF File must not have password or Corrupted PDF File." & Chr(13) & _
                           "Please try Again."
            MsgBox(lvMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "PDFeditor")

        End Try

    End Sub
    'modified split by Ely 04-29-2024
    Sub SplitPdfByOrientation(ByVal inputPdfPath As String, ByVal outputDirectory As String)
        ' Create output directory if it doesn't exist
        If Not Directory.Exists(outputDirectory) Then
            Directory.CreateDirectory(outputDirectory)
        End If

        ' Open the input PDF document
        Using reader As New PdfReader(inputPdfPath)
            ' Iterate through each page
            For pageIndex As Integer = 1 To reader.NumberOfPages
                Dim pageRotation As Integer = reader.GetPageRotation(pageIndex)

                ' Determine the orientation based on page rotation
                Dim orientation As PageOrientation
                If pageRotation = 0 OrElse pageRotation = 180 Then
                    orientation = PageOrientation.Portrait
                Else
                    orientation = PageOrientation.Landscape
                End If

                ' Determine the output subdirectory based on orientation
                Dim outputSubdirectory As String = Path.Combine(outputDirectory, orientation.ToString().ToLower())
                If Not Directory.Exists(outputSubdirectory) Then
                    Directory.CreateDirectory(outputSubdirectory)
                End If

                ' Extract the page and save it as a separate PDF file
                Dim outputFilePath As String = Path.Combine(outputSubdirectory, "page_" & pageIndex & ".pdf")
                Using document As New Document()
                    Using writer As New PdfCopy(document, New FileStream(outputFilePath, FileMode.Create))
                        document.Open()
                        writer.AddPage(writer.GetImportedPage(reader, pageIndex))
                    End Using
                End Using
            Next
        End Using
    End Sub
    Enum PageOrientation
        Portrait
        Landscape
    End Enum
    'Sub DismantlePdf(ByVal inputPdfFilePath As String, ByVal outputFolder As String)
    '    ' Open the input PDF file
    '    Try
    '        Using pdfReader As New PdfReader(inputPdfFilePath)
    '            ' Iterate through each page and create a separate PDF file for each page
    '            For page As Integer = 1 To pdfReader.NumberOfPages
    '                ' Create a new document for each page
    '                Dim outputPdfFilePath As String = Path.Combine(outputFolder, "Page_" & page & ".pdf")
    '                Using pdfDocument As New Document()
    '                    ' Determine the page size (width and height)
    '                    Dim pageSize As Rectangle = pdfReader.GetPageSizeWithRotation(page)
    '                    Dim width As Single = pageSize.Width
    '                    Dim height As Single = pageSize.Height

    '                    ' Determine the orientation
    '                    Dim orientation As Rectangle
    '                    If width > height Then
    '                        ' Landscape
    '                        orientation = iTextSharp.text.PageSize.A4.Rotate
    '                    Else
    '                        ' Portrait
    '                        orientation = pageSize
    '                    End If

    '                    Using pdfWriter As PdfWriter = pdfWriter.GetInstance(pdfDocument, New FileStream(outputPdfFilePath, FileMode.Create))
    '                        ' Open the new document
    '                        pdfDocument.Open()
    '                        ' Set the page size and orientation for the new document
    '                        pdfDocument.SetPageSize(orientation)
    '                        pdfWriter.SetPageSize(orientation)
    '                        ' Copy the current page to the new document
    '                        Dim pdfContentByte As PdfContentByte = pdfWriter.DirectContent
    '                        Dim pdfImportedPage As PdfImportedPage = pdfWriter.GetImportedPage(pdfReader, page)

    '                        pdfContentByte.AddTemplate(pdfImportedPage, 1, 0, 0, 1, 0, 0)

    '                        ' Close the new document
    '                        pdfDocument.Close()
    '                    End Using
    '                End Using
    '            Next
    '        End Using

    '        '====================================================
    '        '==================================================
    '        'Using pdfReader As New PdfReader(inputPdfFilePath)
    '        '    ' Iterate through each page and create a separate PDF file for each page
    '        '    For page As Integer = 1 To pdfReader.NumberOfPages
    '        '        ' Create a new document for each page
    '        '        Dim outputPdfFilePath As String = Path.Combine(outputFolder, "Page_" & page & ".pdf")
    '        '        Using pdfDocument As New Document()
    '        '            Using pdfWriter As PdfWriter = pdfWriter.GetInstance(pdfDocument, New FileStream(outputPdfFilePath, FileMode.Create))
    '        '                ' Open the new document
    '        '                pdfDocument.Open()

    '        '                ' Copy the current page to the new document
    '        '                Dim pdfContentByte As PdfContentByte = pdfWriter.DirectContent

    '        '                Dim pdfImportedPage As PdfImportedPage = pdfWriter.GetImportedPage(pdfReader, page)

    '        '                pdfContentByte.AddTemplate(pdfImportedPage, 1, 0, 0, 1, 0, 0)

    '        '                ' Close the new document
    '        '                pdfDocument.Close()
    '        '            End Using
    '        '        End Using
    '        '    Next
    '        'End Using
    '    Catch ex As Exception
    '        Dim lvMsg As String
    '        PicProgress.Visible = False
    '        lvMsg = "PDF File must not have password or Corrupted PDF File." & Chr(13) & _
    '                       "Please try Again."
    '        MsgBox(lvMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "PDFeditor")
    '    End Try
    'End Sub

    Private Sub cmdDismantle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdDismantle.Click
        PicProgress.Visible = True
        'Delete all Files on a folder
        If Directory.Exists(outputPdfFilePath) Then
            ' Get all files in the folder
            Dim files As String() = Directory.GetFiles(outputPdfFilePath)

            ' Delete each file
            For Each filePath In files
                File.Delete(filePath)
                'Console.WriteLine($"Deleted file: {filePath}")
            Next

            'Console.WriteLine("All files deleted successfully.")
        Else
            ' Console.WriteLine("Folder does not exist.")
        End If

        ' Create a list to store paths of PDF files
        Dim inputFiles As New List(Of String)
        ' Iterate through each item in the ListView
        For Each item As ListViewItem In lvDismantle.Items
            ' Assuming the file path is in the first subitem
            Dim filePath As String = item.SubItems(0).Text

            ' Check if the file has a PDF extension
            If filePath.ToLower().EndsWith(".pdf") Then
                inputFiles.Add(filePath)
                SplitPdfByOrientation(filePath, outputPdfFilePath)
                'DismantlePdf(filePath, outputPdfFilePath)
            End If
        Next
        lvDismantle.Items.Clear()
        MsgBox("Split PDF Successful!, Your PDF files is save to Desktop and navigate the folder named SplitPDF")
        PicProgress.Visible = False
    End Sub

    Private Sub lvDismantle_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvDismantle.DragDrop
        Try
            PicProgress.Visible = True
            ' Retrieve the dragged item
            Dim draggedItem As ListViewItem = DirectCast(e.Data.GetData(GetType(ListViewItem)), ListViewItem)

            ' Get the drop location
            Dim dropPoint As Point = Point.Empty
            dropPoint.X = e.X
            dropPoint.Y = e.Y
            dropPoint = lvMerge.PointToClient(dropPoint)

            ' Get the item at the drop location
            Dim dropItem As ListViewItem = lvDismantle.GetItemAt(dropPoint.X, dropPoint.Y)

            ' Perform the move operation
            If dropItem IsNot Nothing Then
                Dim indexDragged As Integer = draggedItem.Index
                Dim indexDrop As Integer = dropItem.Index

                ' Remove the dragged item
                lvDismantle.Items.RemoveAt(indexDragged)

                ' Insert the dragged item at the drop location
                lvDismantle.Items.Insert(indexDrop, draggedItem)
            End If

            If e.Data.GetDataPresent(DataFormats.FileDrop) Then

                Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
                ' Loop through the dropped files and add them to the ListView.

                For Each filePath As String In files
                    ' Add code to parse the file name and add it to the ListView.
                    Dim fileName As String = System.IO.Path.GetFileName(filePath)
                    xfileName = System.IO.Path.GetFileName(filePath)
                    If Path.GetExtension(filePath).Equals(".pdf", StringComparison.OrdinalIgnoreCase) Then

                        Dim fileInfo As New FileInfo(filePath)
                        Dim fileSizeBytes As Long = fileInfo.Length

                        Dim item As New ListViewItem(filePath)
                        item.SubItems.Add(filePath & xfileName) ' Store the full file path in a subitem.
                        lvDismantle.Items.Add(item)

                    Else
                        MsgBox("PDF Only!")
                        Exit Sub
                    End If

                Next

            End If
            PicProgress.Visible = False
        Catch ex As Exception
            Dim lvMsg As String
            lvMsg = "PDF File must not have password or Corrupted PDF File." & Chr(13) & _
                           "Please try Again."
            MsgBox(lvMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "PDFeditor")
        End Try

    End Sub

    Private Sub lvDismantle_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvDismantle.DragEnter
        If e.Data.GetDataPresent(GetType(ListViewItem)) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If

    End Sub


    Private Sub RemoveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveToolStripMenuItem.Click
        ' Remove the selected item from the ListView
        If lvMerge.SelectedItems.Count > 0 Then
            lvMerge.Items.Remove(lvMerge.SelectedItems(0))
        End If
    End Sub

    Private Sub lvDismantle_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles lvDismantle.ItemDrag
        lvDismantle.DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub


    Private Sub Remove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Remove.Click
        ' Remove the selected item from the ListView
        If lvDismantle.SelectedItems.Count > 0 Then
            lvDismantle.Items.Remove(lvDismantle.SelectedItems(0))
        End If
    End Sub

    Private Sub cmdRemoveDismantle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveDismantle.Click
        ' Remove the selected item from the ListView
        If lvDismantle.SelectedItems.Count > 0 Then
            lvDismantle.Items.Remove(lvDismantle.SelectedItems(0))
        End If
    End Sub

    Private Sub cmdRemoveMerge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdRemoveMerge.Click
        ' Remove the selected item from the ListView
        If lvMerge.SelectedItems.Count > 0 Then
            lvMerge.Items.Remove(lvMerge.SelectedItems(0))
        End If
    End Sub

    Private Sub lvDismantle_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvDismantle.MouseDoubleClick
        Try
            If IsNothing(lvDismantle) Then
            Else
                Process.Start(lvDismantle.SelectedItems(0).Text)
            End If
        Catch ex As Exception
            ' MsgBox("Error in lvDismantle_MouseDoubleClick()", ex.Message)
        End Try
    End Sub

    Private Sub lvMerge_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvMerge.SelectedIndexChanged

    End Sub

    Private Sub btnshow_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnshow.MouseDown
        txtPassword.PasswordChar = ""
    End Sub

    Private Sub btnshow_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnshow.MouseUp
        txtPassword.PasswordChar = "✪"
    End Sub

    Private Sub lvAttched_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvAttched.SelectedIndexChanged

    End Sub

    Private Sub txtPassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPassword.TextChanged
        If Trim(Microsoft.VisualBasic.Len(txtPassword.Text)) = 0 Then
            txtPassword.BackColor = Color.Yellow
        Else
            txtPassword.BackColor = Color.White
        End If
    End Sub

    Private Sub lvMerge_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvMerge.MouseDoubleClick
        Try
            If IsNothing(lvMerge) Then
            Else
                Process.Start(lvMerge.SelectedItems(0).Text)
            End If
        Catch ex As Exception
            ' MsgBox("Error in lvDismantle_MouseDoubleClick()", ex.Message)
        End Try
    End Sub

    Private Sub lvDismantle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvDismantle.SelectedIndexChanged

    End Sub

    Private Sub lvAttched_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvAttched.MouseDoubleClick
        Try
            If IsNothing(lvAttched) Then
            Else
                Process.Start(lvAttched.SelectedItems(0).Text)
            End If
        Catch ex As Exception
            ' MsgBox("Error in lvDismantle_MouseDoubleClick()", ex.Message)
        End Try
    End Sub

    Private Sub TimerDelay_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerDelay.Tick
        mvcount += 1
        If mvcount = 3 Then
            mvReady = True
        Else

        End If
    End Sub

    Public Function GetFilename(ByVal path As String)
        Return System.IO.Path.GetFileNameWithoutExtension(path)
    End Function


    Private Sub btnConvert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConvert.Click
        If picJPGtoPDF.Image Is Nothing Then
            MessageBox.Show("PictureBox does not contain an image.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Using sfd As New SaveFileDialog()
            sfd.Title = "Save Interactive PDF"
            sfd.Filter = "PDF Document (*.pdf)|*.pdf"
            sfd.FileName = "FormOutput.pdf"

            If sfd.ShowDialog() = DialogResult.OK Then
                PicProgress.Visible = True

                ' Match document page size to original image dimensions
                Dim imgW As Single = picJPGtoPDF.Image.Width
                Dim imgH As Single = picJPGtoPDF.Image.Height
                Dim pageSize As New iTextSharp.text.Rectangle(imgW, imgH)

                Using doc As New iTextSharp.text.Document(pageSize, 0, 0, 0, 0)
                    Using fs As New FileStream(sfd.FileName, FileMode.Create, FileAccess.Write)
                        Dim writer As PdfWriter = PdfWriter.GetInstance(doc, fs)
                        doc.Open()

                        ' 1. Draw Background Image
                        Using ms As New MemoryStream()
                            picJPGtoPDF.Image.Save(ms, ImageFormat.Png)
                            Dim pdfImg As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(ms.ToArray())
                            pdfImg.SetAbsolutePosition(0, 0)
                            doc.Add(pdfImg)
                        End Using

                        ' 2. Embed AcroForm TextBoxes and CheckBoxes
                        For Each f In fields
                            ' Invert Windows Y coordinate (Top-Left) to PDF Y coordinate (Bottom-Left)
                            Dim pdfX As Single = f.Bounds.X
                            Dim pdfY As Single = imgH - f.Bounds.Y - f.Bounds.Height
                            Dim fieldRect As New iTextSharp.text.Rectangle(pdfX, pdfY, pdfX + f.Bounds.Width, pdfY + f.Bounds.Height)

                            If f.Type = FieldType.TextBox Then
                                Dim textField As TextField = New TextField(writer, fieldRect, f.Id)
                                textField.Text = f.Content
                                textField.FontSize = f.FontSize ' Use user-selected font size
                                writer.AddAnnotation(textField.GetTextField())

                            ElseIf f.Type = FieldType.CheckBox Then
                                Dim chkField As RadioCheckField = New RadioCheckField(writer, fieldRect, f.Id, "Yes")
                                chkField.CheckType = RadioCheckField.TYPE_CHECK
                                chkField.Checked = f.IsChecked
                                writer.AddAnnotation(chkField.CheckField)
                            End If
                        Next

                        doc.Close()
                    End Using
                End Using

                PicProgress.Visible = False
                MessageBox.Show("Interactive PDF saved successfully!", "Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End Using

    End Sub

    ' Function to convert Image to byte array
    Private Function ImageToByteArray(ByVal image As System.Drawing.Image) As Byte()
        Using stream As New MemoryStream()
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg)
            Return stream.ToArray()
        End Using
    End Function


    Private Sub TabPage6_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TabPage6.DragDrop
        PicProgress.Visible = False
        Dim droppedFiles As String() = e.Data.GetData(DataFormats.FileDrop)
        For Each File In droppedFiles
            Dim filename As String = GetFilename(File)
            picJPGtoPDF.Image = System.Drawing.Image.FromFile(File)
        Next
    End Sub

    Private Sub TabPage6_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TabPage6.DragEnter
        PicProgress.Visible = False
        If e.Data.GetDataPresent(DataFormats.FileDrop, False) = True Then
            e.Effect = DragDropEffects.All
        End If
    End Sub

    Private Sub btnRotLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRotLeft.Click
        RotateImage(picJPGtoPDF.Image, RotateFlipType.Rotate90FlipXY)
    End Sub
    Private Sub RotateImage(ByRef img As System.Drawing.Image, ByVal rotateFlipType As RotateFlipType)
        If img IsNot Nothing Then
            img.RotateFlip(rotateFlipType)
            picJPGtoPDF.Image = img
        End If
    End Sub

    Private Sub btnRotRight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRotRight.Click
        RotateImage(picJPGtoPDF.Image, RotateFlipType.Rotate90FlipNone)
    End Sub


    Private Sub btnRot180_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRot180.Click
        RotateImage(picJPGtoPDF.Image, RotateFlipType.Rotate180FlipNone)
    End Sub

    Private Sub picJPGtoPDF_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picJPGtoPDF.Click

    End Sub

    Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView.Click

    End Sub

    Private Sub btnView_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnView.MouseDown
        txtExcelProtect.PasswordChar = ""
    End Sub

    Private Sub btnView_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnView.MouseUp
        txtExcelProtect.PasswordChar = "✪"
    End Sub

    Private Sub lvExcelProtect_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvExcelProtect.DragDrop
        Try
            PicProgress.Visible = True
            Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            Dim newFolderPath As String = Path.Combine(desktopPath, "OutputExcelFiles")

            If Not Directory.Exists(newFolderPath) Then
                Directory.CreateDirectory(newFolderPath)
            End If

            If e.Data.GetDataPresent(DataFormats.FileDrop) Then
                Dim files() As String = e.Data.GetData(DataFormats.FileDrop)
                PicProgress.Visible = True
                ' Loop through the dropped files and add them to the ListView.
                For Each filePath As String In files
                    ' Add code to parse the file name and add it to the ListView.
                    Dim fileName As String = System.IO.Path.GetFileName(filePath)
                    If Path.GetExtension(filePath).Equals(".xlsx", StringComparison.OrdinalIgnoreCase) Then

                        ' Set the scale factor for image resizing (0.5 in this example)
                        Dim scaleFactor As Single = 0.5F
                        ' Create PDF with resized image
                        'Console.WriteLine("PDF created successfully.")
                        xfileName = System.IO.Path.GetFileName(filePath)

                        Dim fileInfo As New FileInfo(filePath)
                        Dim fileSizeBytes As Long = fileInfo.Length

                        Dim item As New ListViewItem(xfileName)
                        item.SubItems.Add(filePath & xfileName) ' Store the full file path in a subitem.
                        'CompressPdf(filePath, outputpath & xfileName, 9)
                        'ReduceResolution(filePath, outputpath & xfileName, 1, targetDPI)

                        Dim dotPosition As Integer
                        dotPosition = InStrRev(xfileName, ".")
                        If dotPosition > 0 Then
                            ' Extract the part of the string before the last dot
                            xfileName = Microsoft.VisualBasic.Left(xfileName, dotPosition - 1)
                        Else
                            ' No file extension found, use the original filename
                            xfileName = xfileName
                        End If
                        ' ReducePdfToTargetSize(filePath, outputpath & xfileName, targetFileSizeBytes)
                        xfileName = xfileName & ".xlsx"
                        'execute code
                        Dim xlApp As New Excel.Application()
                        Dim xlWorkbook As Excel.Workbook = Nothing

                        'xlWorkbook = xlApp.Workbooks.Add()
                        Try
                            xlWorkbook = xlApp.Workbooks.Open(filePath)
                            xlWorkbook.Password = txtExcelProtect.Text
                            xlWorkbook.SaveAs(newFolderPath & "\" & xfileName)
                        Catch ex As Exception
                            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Finally
                            ' Close and Release Objects
                            If xlWorkbook IsNot Nothing Then xlWorkbook.Close()
                            xlApp.Quit()
                            ReleaseObject(xlWorkbook)
                            ReleaseObject(xlApp)
                        End Try
                        'Dim ofilePath As String = filePath

                        MessageBox.Show("Excel file saved and password protected! File has been saved to Desktop\OutputExcelFiles\", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    Else
                        MsgBox("Excel Only with file extension(*.xlsx)")
                        Exit Sub
                    End If
                Next
                PicProgress.Visible = False
                ' MsgBox("Process Complete!, Your File is saved to your Desktop and search for the folder named CompressedFile.")
                'txtPassword.Text = ""
                lvExcelProtect.Clear()
            End If
            PicProgress.Visible = False
        Catch ex As Exception
            Dim lvMsg As String
            lvMsg = "Excel File must not have password or Corrupted Excel File." & Chr(13) & _
                           "Please try Again."
            MsgBox(lvMsg, MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "Excel Protect")
            PicProgress.Visible = False
        End Try
    End Sub

    Private Sub lvExcelProtect_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles lvExcelProtect.DragEnter
        ' Check if the dragged data contains file(s)
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())

            ' Check if the dropped file(s) is/are PDF(s)
            Dim isPdf = files.All(Function(file) Path.GetExtension(file).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))

            If isPdf Then
                e.Effect = DragDropEffects.Copy
            Else
                e.Effect = DragDropEffects.None
            End If
        Else
            e.Effect = DragDropEffects.None
        End If

        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        End If
    End Sub

    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub lvExcelProtect_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles lvExcelProtect.ItemDrag
        'lvExcelProtect.DoDragDrop(e.Item, DragDropEffects.Move)
        Try

            Dim selectedItems As ListView.SelectedListViewItemCollection = lvExcelProtect.SelectedItems
            If selectedItems.Count > 0 Then
                Dim filePaths As New List(Of String)
                For Each selectedItem As ListViewItem In selectedItems
                    ' Retrieve the file path from the subitem.
                    Dim filePath As String = selectedItem.SubItems(1).Text
                    filePaths.Add(filePath)
                Next

                ' Start the drag-and-drop operation.
                Dim data As New DataObject(DataFormats.FileDrop, filePaths.ToArray())
                DoDragDrop(data, DragDropEffects.Copy)
            End If

        Catch ex As Exception
            MsgBox("PDF File must not have password or Corrupted PDF File.Please try Again.", MsgBoxStyle.Critical + MsgBoxStyle.OkOnly, "PDFeditor")
        End Try
    End Sub

    Private Sub lvExcelProtect_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles lvExcelProtect.MouseDoubleClick
        Try
            If IsNothing(lvExcelProtect) Then
            Else
                Process.Start(lvExcelProtect.SelectedItems(0).Text)
            End If
        Catch ex As Exception
            ' MsgBox("Error in lvDismantle_MouseDoubleClick()", ex.Message)
        End Try
    End Sub

    Private Sub lvExcelProtect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lvExcelProtect.SelectedIndexChanged

    End Sub

    Private Sub btnAddTextBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        
    End Sub

    Private Sub btnAddCheckBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        
    End Sub


 
    Private Sub btnAddTextBox_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTextBox.Click
        If picJPGtoPDF.Image Is Nothing Then
            MessageBox.Show("Please load an image first.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim newField As New DesignField("txt_" & (fields.Count + 1)) With { _
             .Type = FieldType.TextBox, _
             .Bounds = New System.Drawing.Rectangle(50, 50, 160, 30), _
             .Content = "" _
        }
        fields.Add(newField)
        selectedField = newField
        picJPGtoPDF.Invalidate()
    End Sub

    Private Sub btnAddCheckBox_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCheckBox.Click
        If picJPGtoPDF.Image Is Nothing Then
            MessageBox.Show("Please load an image first.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim newField As New DesignField("chk_" & (fields.Count + 1)) With { _
            .Type = FieldType.CheckBox, _
            .Bounds = New System.Drawing.Rectangle(50, 50, 20, 20), _
        .IsChecked = False _
        }
        fields.Add(newField)
        selectedField = newField
        picJPGtoPDF.Invalidate()
    End Sub

    Private Sub DeleteSelectedField()
        If selectedField Is Nothing Then Exit Sub

        ' Ask for confirmation (optional, remove If you want instant delete)
        Dim result As DialogResult = MessageBox.Show( _
            "Are you sure you want to delete this field?", _
            "Delete Field", _
            MessageBoxButtons.YesNo, _
            MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            ' Hide text overlay if it's currently open on this field
            If txtOverlay.Visible Then
                txtOverlay.Visible = False
            End If

            ' Remove from list and reset selection
            fields.Remove(selectedField)
            selectedField = Nothing

            ' Redraw canvas
            picJPGtoPDF.Invalidate()
        End If
    End Sub

    Private Sub btnFontIncrease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFontIncrease.Click
        If selectedField IsNot Nothing AndAlso selectedField.Type = FieldType.TextBox Then
            selectedField.FontSize += 2.0F
            picJPGtoPDF.Invalidate()
        End If
    End Sub

    Private Sub btnFontDecrease_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFontDecrease.Click
        If selectedField IsNot Nothing AndAlso selectedField.Type = FieldType.TextBox Then
            selectedField.FontSize = Math.Max(6.0F, selectedField.FontSize - 2.0F)
            picJPGtoPDF.Invalidate()
        End If
    End Sub

    Private Sub numFontSize_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles numFontSize.ValueChanged
        If selectedField IsNot Nothing AndAlso selectedField.Type = FieldType.TextBox Then
            selectedField.FontSize = CSng(numFontSize.Value)

            ' If typing overlay is open, update its font size as well
            If txtOverlay.Visible Then
                txtOverlay.Font = New System.Drawing.Font("Arial", selectedField.FontSize * zoomFactor)
            End If

            picJPGtoPDF.Invalidate() ' Redraw screen
        End If
    End Sub

    Private Sub btnDeleteField_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteField.Click
        If selectedField IsNot Nothing Then
            DeleteSelectedField()
        Else
            MessageBox.Show("Please select a field to delete first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub
End Class
