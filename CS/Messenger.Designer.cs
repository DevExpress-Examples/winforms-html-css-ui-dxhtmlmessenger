
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
            this.mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.userInfoPopup = new DevExpress.XtraEditors.HtmlContentPopup(this.components);
            this.contactInfoPopup = new DevExpress.XtraEditors.HtmlContentPopup(this.components);
            this.sidePanelContacts = new DevExpress.XtraEditors.SidePanel();
            this.contactsView = new DXHtmlMessengerSample.Views.ContactsView();
            this.messagesView = new DXHtmlMessengerSample.Views.MessagesView();
            this.toolbarFormControl = new DevExpress.XtraBars.ToolbarForm.ToolbarFormControl();
            this.toolbarFormManager = new DevExpress.XtraBars.ToolbarForm.ToolbarFormManager(this.components);
            this.darkThemeBBI = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userInfoPopup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactInfoPopup)).BeginInit();
            this.sidePanelContacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormManager)).BeginInit();
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
            this.sidePanelContacts.Location = new System.Drawing.Point(0, 31);
            this.sidePanelContacts.Margin = new System.Windows.Forms.Padding(0);
            this.sidePanelContacts.MinimumSize = new System.Drawing.Size(180, 0);
            this.sidePanelContacts.Name = "sidePanelContacts";
            this.sidePanelContacts.OverlayResizeZoneThickness = 4;
            this.sidePanelContacts.Size = new System.Drawing.Size(271, 569);
            this.sidePanelContacts.TabIndex = 0;
            // 
            // contactsView
            // 
            this.contactsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contactsView.Location = new System.Drawing.Point(0, 0);
            this.contactsView.Margin = new System.Windows.Forms.Padding(0);
            this.contactsView.Name = "contactsView";
            this.contactsView.Size = new System.Drawing.Size(270, 569);
            this.contactsView.TabIndex = 0;
            // 
            // messagesView
            // 
            this.messagesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.messagesView.Location = new System.Drawing.Point(271, 31);
            this.messagesView.Margin = new System.Windows.Forms.Padding(0);
            this.messagesView.Name = "messagesView";
            this.messagesView.Size = new System.Drawing.Size(529, 569);
            this.messagesView.TabIndex = 1;
            // 
            // toolbarFormControl
            // 
            this.toolbarFormControl.Location = new System.Drawing.Point(0, 0);
            this.toolbarFormControl.Manager = this.toolbarFormManager;
            this.toolbarFormControl.Name = "toolbarFormControl";
            this.toolbarFormControl.Size = new System.Drawing.Size(800, 31);
            this.toolbarFormControl.TabIndex = 2;
            this.toolbarFormControl.TabStop = false;
            this.toolbarFormControl.TitleItemLinks.Add(this.darkThemeBBI);
            this.toolbarFormControl.ToolbarForm = this;
            // 
            // toolbarFormManager
            // 
            this.toolbarFormManager.DockingEnabled = false;
            this.toolbarFormManager.Form = this;
            this.toolbarFormManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.darkThemeBBI});
            this.toolbarFormManager.MaxItemId = 1;
            // 
            // darkThemeBBI
            // 
            this.darkThemeBBI.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.darkThemeBBI.Caption = "Dark Theme";
            this.darkThemeBBI.Id = 0;
            this.darkThemeBBI.Name = "darkThemeBBI";
            this.darkThemeBBI.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnDarkThemeClick);
            // 
            // MessengerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.messagesView);
            this.Controls.Add(this.sidePanelContacts);
            this.Controls.Add(this.toolbarFormControl);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MessengerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "{Title}";
            this.ToolbarFormControl = this.toolbarFormControl;
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userInfoPopup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactInfoPopup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarFormManager)).EndInit();
            this.sidePanelContacts.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraEditors.HtmlContentPopup userInfoPopup;
        private DevExpress.XtraEditors.HtmlContentPopup contactInfoPopup;
        private DevExpress.XtraEditors.SidePanel sidePanelContacts;
        private Views.ContactsView contactsView;
        private Views.MessagesView messagesView;
        private DevExpress.XtraBars.ToolbarForm.ToolbarFormControl toolbarFormControl;
        private DevExpress.XtraBars.ToolbarForm.ToolbarFormManager toolbarFormManager;
        private DevExpress.XtraBars.BarButtonItem darkThemeBBI;
    }
}
