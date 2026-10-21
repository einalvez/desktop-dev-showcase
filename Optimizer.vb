Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.parser
Imports iTextSharp.text
Imports System.IO


Module Optimizer
    Public Class PDFOptimizeOptions
        Public Property CompressionLevel() As Integer
            Get

            End Get
            Set(ByVal value As Integer)

            End Set
        End Property
        Public Property ImageQuality() As Integer
            Get

            End Get
            Set(ByVal value As Integer)

            End Set
        End Property
        Public Property DPI() As Integer
            Get

            End Get
            Set(ByVal value As Integer)

            End Set
        End Property
    End Class
    Sub OptimizePdf(ByVal inputFile As String, ByVal outputFile As String, ByVal options As PDFOptimizeOptions)
        Dim reader As New PdfReader(inputFile)
        Dim document As New Document()
        Dim writer As PdfCopy = New PdfCopy(document, New FileStream(outputFile, FileMode.Create))
        document.Open()

        For pageNum = 1 To reader.NumberOfPages
            Dim page As PdfImportedPage = writer.GetImportedPage(reader, pageNum)

            ' Add the page to the new document
            writer.AddPage(page)
        Next

        ' Close the document to save the changes
        document.Close()
    End Sub




End Module
