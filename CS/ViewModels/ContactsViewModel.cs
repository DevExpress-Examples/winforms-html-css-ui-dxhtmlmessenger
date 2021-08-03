namespace DXHtmlMessengerSample.ViewModels {
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.DevAV.Chat;
    using DevExpress.DevAV.Chat.Events;
    using DevExpress.DevAV.Chat.Model;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.POCO;

    public class ContactsViewModel : ChannelViewModel {
        public ContactsViewModel()
            : base() {
            Contacts = new Contact[0];
        }
        protected override void OnConnected(IChannel channel) {
            base.OnConnected(channel);
            channel.Subscribe(OnContactEvents);
        }
        protected override async void OnChannelReady() {
            var contacts = await Channel.GetContacts();
            await DispatcherService?.BeginInvoke(() => Contacts = contacts);
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
        public virtual Contact SelectedContact {
            get;
            set;
        }
        protected void OnContactsChanged() {
            if(SelectedContact == null)
                SelectedContact = Contacts.FirstOrDefault();
            else {
                long id = SelectedContact.ID;
                SelectedContact = Contacts.Where(x => x.ID == id).FirstOrDefault() ?? Contacts.FirstOrDefault();
            }
        }
        protected void OnSelectedContactChanged() {
            if(SelectedContact != null)
                Messenger.Default.Send(SelectedContact);
        }
        public async void ShowContact(Contact contact) {
            var contactInfo = await Channel.GetUserInfo(contact.ID);
            MessengerViewModel.ShowContactInfo(this, contactInfo);
        }
    }
}