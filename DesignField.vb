Imports System.Drawing

' 1. Enum MUST be declared here so DesignField recognizes it
Public Enum FieldType
    TextBox
    CheckBox
End Enum

' 2. DesignField Class
Public Class DesignField
    Private _id As String = ""
    Private _type As FieldType ' No longer throws "not defined" error!
    Private _bounds As System.Drawing.Rectangle
    Private _content As String = ""
    Private _isChecked As Boolean = False
    Private _fontSize As Single = 12.0F

    ' Properties
    Public ReadOnly Property Id() As String
        Get
            Return _id
        End Get
    End Property

    Public Property Type() As FieldType
        Get
            Return _type
        End Get
        Set(ByVal value As FieldType)
            _type = value
        End Set
    End Property

    Public Property Bounds() As System.Drawing.Rectangle
        Get
            Return _bounds
        End Get
        Set(ByVal value As System.Drawing.Rectangle)
            _bounds = value
        End Set
    End Property

    Public Property Content() As String
        Get
            Return _content
        End Get
        Set(ByVal value As String)
            _content = value
        End Set
    End Property

    Public Property IsChecked() As Boolean
        Get
            Return _isChecked
        End Get
        Set(ByVal value As Boolean)
            _isChecked = value
        End Set
    End Property

    Public Property FontSize() As Single
        Get
            Return _fontSize
        End Get
        Set(ByVal value As Single)
            _fontSize = Math.Max(6.0F, value)
        End Set
    End Property

    Public Sub New(ByVal idValue As String)
        _id = idValue
    End Sub
End Class