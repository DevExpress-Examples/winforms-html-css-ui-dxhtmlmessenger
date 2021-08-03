namespace DXHtmlMessengerSample.ViewModels {
    using DevExpress.DevAV.Chat;
    using DevExpress.DevAV.Chat.Events;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.POCO;

    public abstract class ChannelViewModel {
        protected ChannelViewModel() {
            Messenger.Default.Register<IChannel>(this, OnConnected);
        }
        protected virtual void OnConnected(IChannel channel) {
            channel.Subscribe(OnChannelEvent);
        }
        public virtual void OnCreate() {
            EnsureDispatcherService();
        }
        public virtual void OnDestroy() {
            Messenger.Default.Unregister<IChannel>(this, OnConnected);
        }
        protected IChannel Channel {
            get;
            private set;
        }
        void OnChannelEvent(ChannelEvent @event) {
            var channelReady = @event as ChannelReadyEvent;
            if(channelReady != null) {
                EnsureDispatcherService();
                Channel = channelReady.Channel;
                OnChannelReady();
            }
        }
        protected virtual void OnChannelReady() { }
        protected IDispatcherService DispatcherService {
            get;
            private set;
        }
        protected IDispatcherService EnsureDispatcherService() {
            return DispatcherService ?? (DispatcherService = this.GetRequiredService<IDispatcherService>());
        }
    }
}