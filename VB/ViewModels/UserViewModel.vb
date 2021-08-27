Imports DevExpress.Mvvm.POCO
Imports DevExpress.DevAV.Chat.Model

Namespace DXHtmlMessengerSample.ViewModels

    Public Class UserViewModel
        Inherits UserInfoViewModel

        Protected Friend Overrides ReadOnly Property ServiceKey() As String
            Get
                Return "UserInfoDialog"
            End Get
        End Property
        Public Shared Function Create(ByVal user As UserInfo) As UserViewModel
            Return ViewModelSource.Create(Function() New UserViewModel(user))
        End Function
        '
        Protected Sub New(ByVal user As UserInfo)
            MyBase.New(user)
        End Sub
        Public Sub LogOff()
            Dim messenger = Me.GetParentViewModel(Of MessengerViewModel)()
            If messenger IsNot Nothing Then
                messenger.LogOff()
            End If
        End Sub
        Public Sub Close()
            CloseDocument()
        End Sub
    End Class
End Namespace