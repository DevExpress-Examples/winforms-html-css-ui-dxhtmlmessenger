namespace DXHtmlMessengerSample.ViewModels {
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DevExpress.DevAV.Chat;
    using DevExpress.DevAV.Chat.Commands;
    using DevExpress.DevAV.Chat.Events;
    using DevExpress.DevAV.Chat.Model;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;

    public class MessagesViewModel : ChannelViewModel {
        public MessagesViewModel()
            : base() {
            Messages = new Message[0];
            Messenger.Default.Register<Contact>(this, OnContact);
        }
        public override void OnDestroy() {
            base.OnDestroy();
            Messenger.Default.Unregister<Contact>(this, OnContact);
        }
        protected override void OnConnected(IChannel channel) {
            base.OnConnected(channel);
            channel.Subscribe(OnMessageEvents);
            channel.Subscribe(OnContactEvents);
        }
        protected override async void OnChannelReady() {
            await LoadMessages(Channel, Contact);
            await DispatcherService?.BeginInvoke(UpdateUIOnChannelReady);
        }
        async void OnContactEvents(Dictionary<long, ContactEvent> events) {
            if(Contact != null) {
                ContactEvent @event;
                if(events.TryGetValue(Contact.ID, out @event)) {
                    if(@event is UnreadChanged || @event is NewMessages)
                        await LoadMessages(Channel, Contact);
                }
                if(events.Count > 0)
                    await DispatcherService?.BeginInvoke(RaiseMessagesChanged);
                if(@event is NewMessages)
                    await DispatcherService?.BeginInvoke(RaiseContactChanged);
            }
        }
        HashSet<int> updatedMessagesIdices = new HashSet<int>();
        async void OnMessageEvents(Dictionary<long, MessageEvent> events) {
            updatedMessagesIdices.Clear();
            MessageEvent @event = null; int index = 0;
            foreach(Message message in Messages) {
                if(events.TryGetValue(message.ID, out @event) && updatedMessagesIdices.Add(index)) 
                    @event.Apply(message);
                index++;
            }
            if(events.Count > 0)
                await DispatcherService?.BeginInvoke(RaiseMessagesUpdated);
        }
        void UpdateUIOnChannelReady() {
            this.RaisePropertyChanged(x => x.Messages);
            UpdateActions();
        }
        void RaiseMessagesChanged() {
            this.RaisePropertyChanged(x => x.Messages);
        }
        void RaiseMessagesUpdated() {
            this.RaisePropertyChanged(x => x.UpdatedMessageIndices);
            updatedMessagesIdices.Clear();
        }
        async void OnContact(Contact contact) {
            await LoadMessages(Channel, contact);
            await DispatcherService?.BeginInvoke(() => this.Contact = contact);
        }
        async Task LoadMessages(IChannel channel, Contact contact) {
            if(channel != null && contact != null) {
                var history = await channel.GetHistory(contact);
                await DispatcherService?.BeginInvoke(() => Messages = history);
            }
        }
        public virtual Contact Contact {
            get;
            protected set;
        }
        void RaiseContactChanged() {
            this.RaisePropertyChanged(x => x.Contact);
        }
        protected void OnContactChanged() {
            UpdateActions();
        }
        public bool CanExecuteActions() {
            return (Channel != null) && (Contact != null);
        }
        protected void UpdateActions() {
            this.RaiseCanExecuteChanged(x => x.SendMessage());
            this.RaiseCanExecuteChanged(x => x.PhoneCall());
            this.RaiseCanExecuteChanged(x => x.VideoCall());
            this.RaiseCanExecuteChanged(x => x.ShowContact());
            this.RaiseCanExecuteChanged(x => x.ShowUser());
        }
        public virtual IReadOnlyCollection<Message> Messages {
            get;
            protected set;
        }
        public IReadOnlyCollection<int> UpdatedMessageIndices {
            get { return updatedMessagesIdices.ToArray(); }
        }
        Message lastMessage;
        protected void OnMessagesChanged() {
            lastMessage = Messages.LastOrDefault();
        }
        public virtual string MessageText {
            get;
            set;
        }
        protected void OnMessageTextChanged() {
            this.RaiseCanExecuteChanged(x => x.SendMessage());
        }
        public bool CanSendMessage() {
            return CanExecuteActions() && !string.IsNullOrEmpty(MessageText);
        }
        public void SendMessage() {
            if(Channel != null)
                Channel.Send(new AddMessage(Contact, MessageText));
            MessageText = null;
        }
        public void Update() {
            this.RaiseCanExecuteChanged(x => x.SendMessage());
        }
        [Command(CanExecuteMethodName = nameof(CanExecuteActions))]
        public async void PhoneCall() {
            var contactInfo = await Channel.GetUserInfo(Contact.ID);
            DoCall("Phone Call: " + contactInfo.MobilePhone);
        }
        [Command(CanExecuteMethodName = nameof(CanExecuteActions))]
        public async void VideoCall() {
            var contactInfo = await Channel.GetUserInfo(Contact.ID);
            DoCall("Video Call: " + contactInfo.MobilePhone);
        }
        void DoCall(string call) {
            var msgService = this.GetRequiredService<IMessageBoxService>();
            msgService.ShowMessage(call);
        }
        [Command(CanExecuteMethodName = nameof(CanExecuteActions))]
        public async void ShowContact() {
            var contactInfo = await Channel.GetUserInfo(Contact.ID);
            MessengerViewModel.ShowContactInfo(this, contactInfo);
        }
        [Command(CanExecuteMethodName = nameof(CanExecuteActions))]
        public async void ShowUser() {
            var userInfo = await Channel.GetUserInfo(Channel.UserName);
            MessengerViewModel.ShowUserInfo(this, userInfo);
        }
        public virtual Message SelectedMessage {
            get;
            set;
        }
        protected void OnSelectedMessageChanged() {
            this.RaiseCanExecuteChanged(x => x.DeleteMessage());
            this.RaiseCanExecuteChanged(x => x.CopyMessage());
            this.RaiseCanExecuteChanged(x => x.CopyMessageText());
            this.RaiseCanExecuteChanged(x => x.LikeMessage());
        }
        public bool CanDeleteMessage() {
            return (SelectedMessage != null) && !SelectedMessage.IsDeleted;
        }
        public void DeleteMessage() {
            if(Channel != null)
                Channel.Send(new DeleteMessage(SelectedMessage.ID));
        }
        public bool CanCopyMessage() {
            return (SelectedMessage != null) && !SelectedMessage.IsDeleted;
        }
        public void CopyMessage() {
            try {
                string message = "[" + SelectedMessage.StatusText + "] " +
                    SelectedMessage.Owner.UserName + System.Environment.NewLine +
                    SelectedMessage.Text;
                System.Windows.Forms.Clipboard.SetText(message);
            }
            catch { }
        }
        public bool CanCopyMessageText() {
            return (SelectedMessage != null) && !SelectedMessage.IsDeleted;
        }
        public void CopyMessageText() {
            try {
                System.Windows.Forms.Clipboard.SetText(SelectedMessage.Text);
            }
            catch { }
        }
        public bool CanLikeMessage() {
            return (SelectedMessage != null) && !SelectedMessage.IsLiked;
        }
        public void LikeMessage() {
            if(Channel != null)
                Channel.Send(new LikeMessage(SelectedMessage.ID));
        }
        [Command(false)]
        public void OnMessageRead(Message message) {
            if(lastMessage != null && message == lastMessage) {
                lastMessage = null;
                if(Channel != null)
                    Channel.Send(new ReadMessages(Contact));
            }
        }
    }
}