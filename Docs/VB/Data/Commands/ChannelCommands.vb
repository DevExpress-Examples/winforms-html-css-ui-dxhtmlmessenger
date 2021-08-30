Namespace DevExpress.DevAV.Chat.Commands
    Public MustInherit Class ChannelCommand
        Public ReadOnly Property Channel() As IChannel
    End Class
    '
    Public Class LogOff
        Inherits ChannelCommand

        Public Sub New(ByVal channel As IChannel)
            MyBase.New(channel)
        End Sub
    End Class
End Namespace