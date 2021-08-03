
namespace DXHtmlMessengerSample {
    partial class MessengerForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessengerForm));
            this.mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.userInfoPopup = new DevExpress.XtraEditors.HtmlContentPopup(this.components);
            this.contactInfoPopup = new DevExpress.XtraEditors.HtmlContentPopup(this.components);
            this.sidePanelContacts = new DevExpress.XtraEditors.SidePanel();
            this.contactsView = new DXHtmlMessengerSample.Views.ContactsView();
            this.messagesView = new DXHtmlMessengerSample.Views.MessagesView();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userInfoPopup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactInfoPopup)).BeginInit();
            this.sidePanelContacts.SuspendLayout();
            this.SuspendLayout();
            // 
            // mvvmContext
            // 
            this.mvvmContext.ContainerControl = this;
            this.mvvmContext.ViewModelType = typeof(DXHtmlMessengerSample.ViewModels.MessengerViewModel);
            // 
            // userInfoPopup
            // 
            this.userInfoPopup.ContainerControl = this;
            this.userInfoPopup.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.userInfoPopup.ViewModelType = typeof(DXHtmlMessengerSample.ViewModels.UserViewModel);
            this.userInfoPopup.ViewModelSet += new DevExpress.Utils.MVVM.ViewModelSetEventHandler(this.userInfoPopup_ViewModelSet);
            // 
            // contactInfoPopup
            // 
            this.contactInfoPopup.ContainerControl = this;
            this.contactInfoPopup.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.contactInfoPopup.ViewModelType = typeof(DXHtmlMessengerSample.ViewModels.ContactViewModel);
            this.contactInfoPopup.ViewModelSet += new DevExpress.Utils.MVVM.ViewModelSetEventHandler(this.contactInfoPopup_ViewModelSet);
            // 
            // sidePanelContacts
            // 
            this.sidePanelContacts.Controls.Add(this.contactsView);
            this.sidePanelContacts.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidePanelContacts.Location = new System.Drawing.Point(0, 0);
            this.sidePanelContacts.Margin = new System.Windows.Forms.Padding(0);
            this.sidePanelContacts.MinimumSize = new System.Drawing.Size(180, 0);
            this.sidePanelContacts.Name = "sidePanelContacts";
            this.sidePanelContacts.OverlayResizeZoneThickness = 4;
            this.sidePanelContacts.Size = new System.Drawing.Size(271, 600);
            this.sidePanelContacts.TabIndex = 0;
            // 
            // contactsView
            // 
            this.contactsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactsView.Location = new System.Drawing.Point(0, 0);
            this.contactsView.Margin = new System.Windows.Forms.Padding(0);
            this.contactsView.Name = "contactsView";
            this.contactsView.Size = new System.Drawing.Size(270, 600);
            this.contactsView.TabIndex = 0;
            // 
            // messagesView
            // 
            this.messagesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesView.Location = new System.Drawing.Point(271, 0);
            this.messagesView.Margin = new System.Windows.Forms.Padding(0);
            this.messagesView.Name = "messagesView";
            this.messagesView.Size = new System.Drawing.Size(529, 600);
            this.messagesView.TabIndex = 1;
            // 
            // MessengerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.messagesView);
            this.Controls.Add(this.sidePanelContacts);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MessengerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "{Title}";
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userInfoPopup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactInfoPopup)).EndInit();
            this.sidePanelContacts.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraEditors.HtmlContentPopup userInfoPopup;
        private DevExpress.XtraEditors.HtmlContentPopup contactInfoPopup;
        private DevExpress.XtraEditors.SidePanel sidePanelContacts;
        private Views.ContactsView contactsView;
        private Views.MessagesView messagesView;
    }
}
