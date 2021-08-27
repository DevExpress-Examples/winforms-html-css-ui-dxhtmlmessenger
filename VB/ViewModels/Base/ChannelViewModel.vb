Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm
Imports DevExpress.DevAV.Chat.Events
Imports DevExpress.DevAV.Chat

Namespace DXHtmlMessengerSample.ViewModels

    Public MustInherit Class ChannelViewModel
        Protected Sub New()
            Messenger.Default.Register(Of IChannel)(Me, AddressOf OnConnected)
        End Sub
        Protected Overridable Sub OnConnected(ByVal channel As IChannel)
            channel.Subscribe(AddressOf OnChannelEvent)
        End Sub
        Public Overridable Sub OnCreate()
            EnsureDispatcherService()
        End Sub
        Public Overridable Sub OnDestroy()
            Messenger.Default.Unregister(Of IChannel)(Me, AddressOf OnConnected)
        End Sub
        Private privateChannel As IChannel
        Protected Property Channel() As IChannel
            Get
                Return privateChannel
            End Get
            Private Set(ByVal value As IChannel)
                privateChannel = value
            End Set
        End Property
        Sub OnChannelEvent(ByVal [event] As ChannelEvent)
            Dim channelReady = TryCast([event], ChannelReadyEvent)
            If channelReady IsNot Nothing Then
                EnsureDispatcherService()
                Channel = channelReady.Channel
                OnChannelReady()
            End If
        End Sub
        Protected Overridable Sub OnChannelReady()
        End Sub
        Private privateDispatcherService As IDispatcherService
        Protected Property DispatcherService() As IDispatcherService
            Get
                Return privateDispatcherService
            End Get
            Private Set(ByVal value As IDispatcherService)
                privateDispatcherService = value
            End Set
        End Property
        Protected Function EnsureDispatcherService() As IDispatcherService
            If DispatcherService IsNot Nothing Then
                Return DispatcherService
            Else
                DispatcherService = Me.GetRequiredService(Of IDispatcherService)()
                Return DispatcherService
            End If
        End Function
    End Class
End Namespace