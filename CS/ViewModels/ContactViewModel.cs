namespace DXHtmlMessengerSample.ViewModels {
    using DevExpress.DevAV.Chat.Model;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.POCO;

    public class ContactViewModel : UserInfoViewModel {
        protected internal override string ServiceKey {
            get { return "ContactInfoFlyout"; }
        }
        public static ContactViewModel Create(UserInfo contact) {
            return ViewModelSource.Create(() => new ContactViewModel(contact));
        }
        //
        protected ContactViewModel(UserInfo contact)
            : base(contact) {
        }
        public void MailTo() {
            var msgService = this.GetRequiredService<IMessageBoxService>();
            msgService.ShowMessage("Mail To: " + Email);
        }
        public void VideoCall() {
            var msgService = this.GetRequiredService<IMessageBoxService>();
            int index = Email.IndexOf('@');
            msgService.ShowMessage("Video Call: " + Email.Substring(0, index));
        }
        public void PhoneCall() {
            var msgService = this.GetRequiredService<IMessageBoxService>();
            msgService.ShowMessage("Phone Call: " + MobilePhone);
        }
        public void TextMessage() {
            CloseDocument();
            Messenger.Default.Send(new Contact(Id, Name, Photo));
        }
    }
}