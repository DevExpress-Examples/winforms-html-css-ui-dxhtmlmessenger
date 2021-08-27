namespace DXHtmlMessengerSample.ViewModels {
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.POCO;

    public class SignInViewModel : IDocumentContent {
        public static SignInViewModel Create(string userName, string salt) {
            return ViewModelSource.Create(() => new SignInViewModel(userName, salt));
        }
        //
        readonly string salt;
        protected SignInViewModel(string userName, string salt) {
            this.UserName = userName;
            this.salt = salt;
        }
        public string UserName {
            get;
            private set;
        }
        [DataType(DataType.Password)]
        public virtual string Password {
            get;
            set;
        }
        public string AccessToken {
            get;
            private set;
        }
        public void ShowDialog() {
            var flyout = this.GetService<IDocumentManagerService>("Flyout");
            var document = flyout.CreateDocument(this);
            document.Show();
        }
        public void SignInViaSocialNetwork() {
            AccessToken = DevExpress.DevAV.Chat.DevAVEmpployeesInMemoryServer.GetPasswordHash(string.Empty, salt);
            CloseDocument();
        }
        public void SignIn() {
            AccessToken = DevExpress.DevAV.Chat.DevAVEmpployeesInMemoryServer.GetPasswordHash(Password, salt);
            CloseDocument();
        }
        #region IDocumentContent
        void CloseDocument() {
            ((IDocumentContent)this).DocumentOwner?.Close(this);
        }
        object IDocumentContent.Title {
            get { return string.Empty; }
        }
        IDocumentOwner IDocumentContent.DocumentOwner { get; set; }
        void IDocumentContent.OnClose(CancelEventArgs e) { }
        void IDocumentContent.OnDestroy() { }
        #endregion IDocumentContent
    }
}