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

    public class ContactsViewModel : ChannelViewModel {
        public ContactsViewModel()
            : base() {
            Contacts = new Contact[0];
            Messenger.Default.Register<Contact>(this, OnContact);
        }
        protected override void OnConnected(IChannel channel) {
            base.OnConnected(channel);
            channel.Subscribe(OnContactEvents);
        }
        protected override async void OnChannelReady() {
            var channelContacts = await Channel.GetContacts();
            await DispatcherService?.BeginInvoke(() => Contacts = channelContacts);
        }
        void OnContact(Contact contact) {
            UpdateSelectedContact(contact);
        }
        async void OnContactEvents(Dictionary<long, ContactEvent> events) {
            ContactEvent @event;
            foreach(Contact contact in Contacts) {
                if(events.TryGetValue(contact.ID, out @event))
                    @event.Apply(contact);
            }
            if(events.Count > 0)
                await DispatcherService?.BeginInvoke(RaiseContactsChanged);
        }
        void RaiseContactsChanged() {
            this.RaisePropertyChanged(x => x.Contacts);
        }
        public virtual IReadOnlyCollection<Contact> Contacts {
            get;
            protected set;
        }
        protected void OnContactsChanged() {
            if(SelectedContact == null)
                SelectedContact = Contacts.FirstOrDefault();
            else UpdateSelectedContact(SelectedContact);
            }
        public virtual Contact SelectedContact {
            get;
            set;
        }
        protected void OnSelectedContactChanged() {
            NotifyContactSelected(SelectedContact);
            this.RaiseCanExecuteChanged(x => x.ClearConversation());
            this.RaiseCanExecuteChanged(x => x.CopyContact());
        }
        int lockContact = 0;
        void UpdateSelectedContact(Contact contact) {
            if(lockContact > 0 || contact == null)
                return;
            lockContact++;
            try {
                long id = contact.ID;
                SelectedContact = Contacts.Where(x => x.ID == id).FirstOrDefault() ?? Contacts.FirstOrDefault();
            }
            finally { lockContact--; }
        }
        void NotifyContactSelected(Contact contact) {
            if(lockContact > 0 || contact == null)
                return;
            lockContact++;
            try {
                Messenger.Default.Send(contact);
            }
            finally { lockContact--; }
        }
        public async void ShowContact(Contact contact) {
            var contactInfo = await Channel.GetUserInfo(contact.ID);
            MessengerViewModel.ShowContactInfo(this, contactInfo);
        }
        [Command(false)]
        public bool HasContact() {
            return SelectedContact != null;
        }
        [Command(CanExecuteMethodName = nameof(HasContact))]
        public void ClearConversation() {
            if(Channel != null)
                Channel.Send(new ClearConversation(SelectedContact));
        }
        [Command(CanExecuteMethodName = nameof(HasContact))]
        public async void CopyContact() {
            try {
                var info = await Channel.GetUserInfo(SelectedContact.ID);
                string contact =
                    info.Name + System.Environment.NewLine +
                    $"Email: {info.Email}" + System.Environment.NewLine +
                    $"Phone: {info.MobilePhone}";
                System.Windows.Forms.Clipboard.SetText(contact);
            }
            catch { }
        }
        ContactViewModel contactTooltipViewModel;
        public async Task<ContactViewModel> EnsureTooltipViewModel(Contact contact) {
            var contactInfo = await Channel.GetUserInfo(contact.ID);
            if(contactTooltipViewModel == null)
                contactTooltipViewModel = ContactViewModel.Create(contactInfo);
            else
            ((ISupportParameter)contactTooltipViewModel).Parameter = contactInfo;
            return contactTooltipViewModel;
        }
    }
}