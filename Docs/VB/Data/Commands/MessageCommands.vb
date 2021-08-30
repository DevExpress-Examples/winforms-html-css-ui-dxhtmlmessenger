Namespace DevExpress.DevAV.Chat.Commands
    Public MustInherit Class MessageCommand
        Public ReadOnly Property MessageId() As Long
    End Class
    '
    Public Class DeleteMessage
        Inherits MessageCommand

        Public Sub New(ByVal messageID As Long)
            MyBase.New(messageID)
        End Sub
    End Class
    Public Class LikeMessage
        Inherits MessageCommand

        Public Sub New(ByVal messageID As Long)
            MyBase.New(messageID)
        End Sub
    End Class
End Namespace