namespace DXHtmlMessengerSample.ViewModels {
    using DevExpress.DevAV.Chat.Model;
    using DevExpress.Mvvm.POCO;

    public class UserViewModel : UserInfoViewModel {
        protected internal override string ServiceKey {
            get { return "UserInfoDialog"; }
        }
        public static UserViewModel Create(UserInfo user) {
            return ViewModelSource.Create(() => new UserViewModel(user));
        }
        //
        protected UserViewModel(UserInfo user)
            : base(user) {
        }
        public void LogOff() {
            CloseDocument();
            var messenger = this.GetParentViewModel<MessengerViewModel>();
            if(messenger != null) messenger.LogOff();
        }
        public void Close() {
            CloseDocument();
        }
    }
}