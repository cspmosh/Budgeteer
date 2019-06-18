Imports System.Drawing
Imports System.ComponentModel

Partial Public Class ProgressBar
    Inherits System.Web.UI.UserControl

    Private _width As Unit = 0
    Private _height As Unit = 0
    Private _minValue As Integer = 0
    Private _maxValue As Decimal = 100
    Private _value As Integer = 0
    Private _backColor As Color
    Private _backgroundImage As String
    Private _progressColor As Color
    Private _progressCompleteColor As Color
    Private _borderColor As Color
    Private _borderStyle As String
    Private _borderWidth As Integer
    Private _progressText As String
    Private _fontBold As Boolean
    Private _fontColor As Color

    Public Property Height() As Unit
        Get
            Return _height
        End Get
        Set(ByVal value As Unit)
            _height = value
            _progressBarBackground.Style("height") = value.ToString
            _progressBarHighlight.Style("height") = value.ToString
            lblProgress.Height = value
        End Set
    End Property

    Public Property Width() As Unit
        Get
            Return _width
        End Get
        Set(ByVal value As Unit)
            _width = value
            _progressBarBackground.Style("width") = value.ToString
            _progressBarHighlight.Style("width") = value.ToString
        End Set
    End Property

    Public Property Maximum() As Decimal
        Get
            Return _maxValue
        End Get
        Set(ByVal value As Decimal)
            _maxValue = value
            If value = 0 Then
                _maxValue = 1
            End If
        End Set
    End Property

    Public Property Minimum() As Integer
        Get
            Return _minValue
        End Get
        Set(ByVal value As Integer)
            _minValue = value
        End Set
    End Property

    Public Property Value() As Decimal
        Get
            Return _value
        End Get
        Set(ByVal value As Decimal)
            If value > _maxValue Then
                value = _maxValue
                If _progressCompleteColor <> Nothing Then
                    lblProgress.BackColor = _progressCompleteColor
                End If
            Else
                If _progressColor <> Nothing Then
                    lblProgress.BackColor = _progressColor
                End If
            End If
            If value < _minValue Then
                value = _minValue
            End If
            lblProgress.Width = ((value * CType(_width.Value, Decimal)) / _maxValue)
            _value = value
        End Set
    End Property

    Public Property BackColor() As Color
        Get
            Return _backColor
        End Get
        Set(ByVal value As Color)
            _backColor = value
            _progressBarBackground.Style("Background-Color") = System.Drawing.ColorTranslator.ToHtml(value)
        End Set
    End Property

    Public Property BackgroundImage() As String
        Get
            Return _backgroundImage
        End Get
        Set(ByVal value As String)
            _backgroundImage = value
            _progressBarBackground.Style("Background-Image") = "url('" & value & "')"
        End Set
    End Property

    Public Property BorderColor() As Color
        Get
            Return _borderColor
        End Get
        Set(ByVal value As Color)
            _borderColor = value
            _progressBarBackground.Style("Border-Color") = System.Drawing.ColorTranslator.ToHtml(value)
        End Set
    End Property

    Public Property BorderStyle() As BorderStyle
        Get
            Return _borderStyle
        End Get
        Set(ByVal value As BorderStyle)
            _borderStyle = value
            _progressBarBackground.Style("Border-Style") = value.ToString
        End Set
    End Property

    Public Property BorderWidth() As Integer
        Get
            Return _borderWidth
        End Get
        Set(ByVal value As Integer)
            _borderWidth = value
            _progressBarBackground.Style("Border-Width") = value.ToString & "px"
        End Set
    End Property

    Public Property ProgressColor() As Color
        Get
            Return _progressColor
        End Get
        Set(ByVal value As Color)
            _progressColor = value
            lblProgress.BackColor = value
        End Set
    End Property

    Public Property ProgressCompleteColor() As Color
        Get
            Return _progressCompleteColor
        End Get
        Set(ByVal value As Color)
            _progressCompleteColor = value
        End Set
    End Property

    Public Property ProgressText() As String
        Get
            Return _progressText
        End Get
        Set(ByVal value As String)
            _progressText = value
            lblProgress.Text = value & "&nbsp;"
        End Set
    End Property

    Public Property FontBold() As Boolean
        Get
            Return _fontBold
        End Get
        Set(ByVal value As Boolean)
            _fontBold = value
            If _fontBold Then
                lblProgress.Style("font-weight") = "bold"
            Else
                lblProgress.Style("font-weight") = "normal"
            End If
        End Set
    End Property

    Public Property FontColor() As Color
        Get
            Return _fontColor
        End Get
        Set(ByVal value As Color)
            _fontColor = value
            lblProgress.ForeColor = value
        End Set
    End Property
End Class