Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm
Imports DevExpress.DevAV.Chat.Model
Imports System.Drawing
Imports System.ComponentModel

Namespace DXHtmlMessengerSample.ViewModels

    <EditorBrowsable(EditorBrowsableState.Never)>
    Public MustInherit Class UserInfoViewModel
        Implements IDocumentContent, ISupportParameter

        Private userInfo As UserInfo
        Protected Sub New(ByVal userInfo As UserInfo)
            Me.userInfo = userInfo
        End Sub
        Private Property ISupportParameter_Parameter() As Object Implements ISupportParameter.Parameter
            Get
                Return userInfo
            End Get
            Set(ByVal value As Object)
                Dim info = TryCast(value, UserInfo)
                If userInfo Is info Then
                    Return
                End If
                userInfo = info
                Me.RaisePropertiesChanged()
            End Set
        End Property
        Public ReadOnly Property Id() As Long
            Get
                Return userInfo.Id
            End Get
        End Property
        Public ReadOnly Property Name() As String
            Get
                Return userInfo.Name
            End Get
        End Property
        Public ReadOnly Property Photo() As Image
            Get
                Return userInfo.Photo
            End Get
        End Property
        Public ReadOnly Property MobilePhone() As String
            Get
                Return userInfo.MobilePhone
            End Get
        End Property
        Public ReadOnly Property Email() As String
            Get
                Return userInfo.Email
            End Get
        End Property
        #Region "IDocumentContent"
        Protected Sub CloseDocument()
            Dim owner = DirectCast(Me, IDocumentContent).DocumentOwner
            If owner IsNot Nothing Then
                owner.Close(Me)
            End If
        End Sub
        Private ReadOnly Property IDocumentContent_Title() As Object Implements IDocumentContent.Title
            Get
                Return String.Empty
            End Get
        End Property
        Private Property IDocumentContent_DocumentOwner() As IDocumentOwner Implements IDocumentContent.DocumentOwner
        Sub IDocumentContent_OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
        End Sub
        Sub IDocumentContent_OnDestroy() Implements IDocumentContent.OnDestroy
        End Sub
        #End Region ' IDocumentContent
        Protected Friend MustOverride ReadOnly Property ServiceKey() As String
    End Class
End Namespace