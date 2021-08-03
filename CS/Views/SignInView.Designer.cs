
namespace DXHtmlMessengerSample.Views {
    partial class SignInView {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignInView));
            this.mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.signInView = new DevExpress.XtraEditors.HtmlContentControl();
            this.pwdEdit = new DevExpress.XtraEditors.TextEdit();
            this.signInBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signInView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pwdEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.signInBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // mvvmContext
            // 
            this.mvvmContext.ContainerControl = this;
            this.mvvmContext.ViewModelType = typeof(DXHtmlMessengerSample.ViewModels.SignInViewModel);
            // 
            // signInView
            // 
            this.signInView.Controls.Add(this.pwdEdit);
            this.signInView.DataContext = signInBindingSource;
            this.signInView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signInView.Location = new System.Drawing.Point(0, 0);
            this.signInView.Margin = new System.Windows.Forms.Padding(0);
            this.signInView.Name = "signInView";
            this.signInView.Size = new System.Drawing.Size(420, 260);
            this.signInView.TabIndex = 4;
            // 
            // pwdEdit
            // 
            this.pwdEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.signInBindingSource, "Password", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.pwdEdit.Location = new System.Drawing.Point(34, 96);
            this.pwdEdit.Name = "pwdEdit";
            this.pwdEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pwdEdit.Properties.UseSystemPasswordChar = true;
            this.pwdEdit.Size = new System.Drawing.Size(362, 20);
            this.pwdEdit.TabIndex = 5;
            // 
            // signInBindingSource
            // 
            this.signInBindingSource.DataSource = typeof(DXHtmlMessengerSample.ViewModels.SignInViewModel);
            // 
            // LoginView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.signInView);
            this.Name = "LoginView";
            this.Size = new System.Drawing.Size(480, 320);
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signInView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pwdEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.signInBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraEditors.HtmlContentControl signInView;
        private DevExpress.XtraEditors.TextEdit pwdEdit;
        private System.Windows.Forms.BindingSource signInBindingSource;
    }
}
