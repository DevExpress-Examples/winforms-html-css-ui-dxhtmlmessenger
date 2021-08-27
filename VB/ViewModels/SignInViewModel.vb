Imports System.ComponentModel
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DXHtmlMessengerSample.ViewModels
    Public Class SignInViewModel
        Implements IDocumentContent
        Public Shared Function Create(ByVal userName As String, ByVal salt As String) As SignInViewModel
            Return ViewModelSource.Create(Function() New SignInViewModel(userName, salt))
        End Function
        '
        Private ReadOnly salt As String
        Protected Sub New(ByVal userName As String, ByVal salt As String)
            Me.UserName = userName
            Me.salt = salt
        End Sub
        Public ReadOnly Property UserName() As String
        <DataType(DataType.Password)>
        Public Overridable Property Password() As String
        Private AccessTokenCore As String
        Public ReadOnly Property AccessToken() As String
            Get
                Return AccessTokenCore
            End Get
        End Property
        Public Sub ShowDialog()
            Dim flyout = Me.GetService(Of IDocumentManagerService)("Flyout")
            Dim document = flyout.CreateDocument(Me)
            document.Show()
        End Sub
        Public Sub SignInViaSocialNetwork()
            AccessTokenCore = DevExpress.DevAV.Chat.DevAVEmpployeesInMemoryServer.GetPasswordHash(String.Empty, salt)
            CloseDocument()
        End Sub
        Public Sub SignIn()
            AccessTokenCore = DevExpress.DevAV.Chat.DevAVEmpployeesInMemoryServer.GetPasswordHash(Password, salt)
            CloseDocument()
        End Sub
#Region "IDocumentContent"
        Sub CloseDocument()
            DirectCast(Me, IDocumentContent).DocumentOwner.Close(Me)
        End Sub
        ReadOnly Property IDocumentContent_Title() As Object Implements IDocumentContent.Title
            Get
                Return String.Empty
            End Get
        End Property
        Property IDocumentContent_DocumentOwner() As IDocumentOwner Implements IDocumentContent.DocumentOwner
        Sub IDocumentContent_OnClose(ByVal e As CancelEventArgs) Implements IDocumentContent.OnClose
        End Sub
        Sub IDocumentContent_OnDestroy() Implements IDocumentContent.OnDestroy
        End Sub
#End Region ' IDocumentContent
    End Class
End Namespace