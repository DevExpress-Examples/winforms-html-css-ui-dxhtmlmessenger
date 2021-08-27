Imports DevExpress.XtraEditors
Imports System.IO
Imports System.Collections.Concurrent

Namespace DXHtmlMessengerSample.Views
    Friend MustInherit Class Style
        Private ReadOnly htmlName, cssName As String
        Protected Sub New(Optional ByVal htmlName As String = Nothing, Optional ByVal cssName As String = Nothing)
            If String.IsNullOrEmpty(htmlName) Then
                Dim typeName = Me.GetType().Name
                Me.htmlName = typeName.Substring(0, typeName.Length - NameOf(Style).Length)
            Else
                Me.htmlName = htmlName
            End If
            If String.IsNullOrEmpty(cssName) Then
                Dim typeName = Me.GetType().Name
                Me.cssName = typeName.Substring(0, typeName.Length - NameOf(Style).Length)
            Else
                Me.cssName = cssName
            End If
        End Sub
        Private htmlCore As String
        Public ReadOnly Property Html() As String
            Get
                If htmlCore IsNot Nothing Then
                    Return htmlCore
                Else
                    htmlCore = ReadText(htmlName, NameOf(Html))
                    Return htmlCore
                End If
            End Get
        End Property
        Private cssCore As String
        Public ReadOnly Property Css() As String
            Get
                If cssCore IsNot Nothing Then
                    Return cssCore
                Else
                    cssCore = ReadText(cssName, NameOf(Css))
                    Return cssCore
                End If
            End Get
        End Property
#Region "ReadText"
        Private Shared ReadOnly texts As New ConcurrentDictionary(Of String, String)()
        Shared Function ReadText(ByVal fileName As String, ByVal type As String) As String
            Dim filePath = Path.Combine(DXHtmlMessenger.AssetsPath, $"{type}\{fileName}.{type}")
            Return texts.GetOrAdd(filePath, Function(x) File.ReadAllText(x))
        End Function
#End Region ' ReadText
#Region "Apply"
        Public Sub Apply(ByVal control As HtmlContentControl)
            control.HtmlImages = DXHtmlMessenger.SvgImages
            control.Template = Html
            control.Styles = Css
        End Sub
        Public Sub Apply(ByVal popup As HtmlContentPopup)
            popup.HtmlImages = DXHtmlMessenger.SvgImages
            popup.Template = Html
            popup.Styles = Css
        End Sub
        Public Sub Apply(ByVal template As DevExpress.Utils.Html.HtmlTemplate)
            template.Template = Html
            template.Styles = Css
        End Sub
#End Region ' Apply
    End Class
End Namespace