namespace DXHtmlMessengerSample.ViewModels {
    using System.ComponentModel;
    using System.Drawing;
    using DevExpress.DevAV.Chat.Model;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.POCO;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public abstract class UserInfoViewModel : IDocumentContent, ISupportParameter {
        UserInfo userInfo;
        protected UserInfoViewModel(UserInfo userInfo) {
            this.userInfo = userInfo;
        }
        object ISupportParameter.Parameter {
            get { return userInfo; }
            set {
                var info = value as UserInfo;
                if(userInfo == info) return;
                userInfo = info;
                this.RaisePropertiesChanged();
            }
        }
        public long Id {
            get { return userInfo.Id; }
        }
        public string Name {
            get { return userInfo.Name; }
        }
        public Image Photo {
            get { return userInfo.Photo; }
        }
        public string MobilePhone {
            get { return userInfo.MobilePhone; }
        }
        public string Email {
            get { return userInfo.Email; }
        }
        #region IDocumentContent
        protected void CloseDocument() {
            var owner = ((IDocumentContent)this).DocumentOwner;
            if(owner != null) owner.Close(this);
        }
        object IDocumentContent.Title { get { return string.Empty; } }
        IDocumentOwner IDocumentContent.DocumentOwner { get; set; }
        void IDocumentContent.OnClose(CancelEventArgs e) { }
        void IDocumentContent.OnDestroy() { }
        #endregion IDocumentContent
        protected internal abstract string ServiceKey { get; }
    }
}