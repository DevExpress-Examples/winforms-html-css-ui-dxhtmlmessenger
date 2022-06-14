
namespace DXHtmlMessengerSample.Views {
    partial class MessagesView {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessagesView));
            this.tablePanel = new DevExpress.Utils.Layout.TablePanel();
            this.typingBox = new DevExpress.XtraEditors.HtmlContentControl();
            this.messageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.toolbarPanel = new DevExpress.XtraEditors.HtmlContentControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.messagesItemsView = new DevExpress.XtraGrid.Views.Items.ItemsView();
            this.colUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAvatar = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatusText = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.messageEdit = new DevExpress.XtraEditors.MemoEdit();
            this.messageMenuPopup = new DevExpress.XtraEditors.HtmlContentPopup(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel)).BeginInit();
            this.tablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.typingBox)).BeginInit();
            this.typingBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.messageBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarPanel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesItemsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageMenuPopup)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel
            // 
            this.tablePanel.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F)});
            this.tablePanel.Controls.Add(this.typingBox);
            this.tablePanel.Controls.Add(this.toolbarPanel);
            this.tablePanel.Controls.Add(this.gridControl);
            this.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel.Location = new System.Drawing.Point(0, 0);
            this.tablePanel.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 49F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 1F)});
            this.tablePanel.Size = new System.Drawing.Size(430, 600);
            this.tablePanel.TabIndex = 1;
            // 
            // typingBox
            // 
            this.tablePanel.SetColumn(this.typingBox, 0);
            this.typingBox.Controls.Add(this.messageEdit);
            this.typingBox.AutoScroll = false;
            this.typingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.typingBox.Location = new System.Drawing.Point(0, 540);
            this.typingBox.Margin = new System.Windows.Forms.Padding(0);
            this.typingBox.Name = "typingBox";
            this.tablePanel.SetRow(this.typingBox, 2);
            this.typingBox.Size = new System.Drawing.Size(430, 60);
            this.typingBox.TabIndex = 4;
            // 
            // messageBindingSource
            // 
            this.messageBindingSource.DataSource = typeof(DXHtmlMessengerSample.ViewModels.MessagesViewModel);
            // 
            // messageMenuPopup
            // 
            this.messageMenuPopup.ContainerControl = this;
            this.messageMenuPopup.HideOnElementClick = DevExpress.Utils.DefaultBoolean.True;
            this.messageMenuPopup.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            // 
            // toolbarPanel
            // 
            this.tablePanel.SetColumn(this.toolbarPanel, 0);
            this.toolbarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolbarPanel.Location = new System.Drawing.Point(0, 0);
            this.toolbarPanel.Margin = new System.Windows.Forms.Padding(0);
            this.toolbarPanel.Name = "toolbarPanel";
            this.tablePanel.SetRow(this.toolbarPanel, 0);
            this.toolbarPanel.Size = new System.Drawing.Size(430, 49);
            this.toolbarPanel.TabIndex = 3;
            // 
            // gridControl
            // 
            this.tablePanel.SetColumn(this.gridControl, 0);
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 48);
            this.gridControl.MainView = this.messagesItemsView;
            this.gridControl.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl.Name = "gridControl";
            this.tablePanel.SetRow(this.gridControl, 1);
            this.gridControl.Size = new System.Drawing.Size(430, 493);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.messagesItemsView});
            // 
            // messagesItemsView
            // 
            this.messagesItemsView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.messagesItemsView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colUserName,
            this.colAvatar,
            this.colText,
            this.colStatusText});
            this.messagesItemsView.GridControl = this.gridControl;
            this.messagesItemsView.Name = "messagesItemsView";
            this.messagesItemsView.QueryItemTemplate += new DevExpress.XtraGrid.Views.Items.QueryItemTemplateEventHandler(this.OnQueryItemTemplate);
            this.messagesItemsView.CustomizeItem += new DevExpress.XtraGrid.Views.Items.CustomizeItemEventHandler(this.OnCustomizeItem);
            // 
            // colUserName
            // 
            this.colUserName.FieldName = "Owner.UserName";
            this.colUserName.Name = "colUserName";
            this.colUserName.Visible = true;
            this.colUserName.VisibleIndex = 0;
            // 
            // colAvatar
            // 
            this.colAvatar.FieldName = "Owner.Avatar";
            this.colAvatar.Name = "colAvatar";
            this.colAvatar.Visible = true;
            this.colAvatar.VisibleIndex = 1;
            // 
            // colText
            // 
            this.colText.FieldName = "Text";
            this.colText.Name = "colText";
            this.colText.Visible = true;
            this.colText.VisibleIndex = 2;
            // 
            // colStatusText
            // 
            this.colStatusText.FieldName = "StatusText";
            this.colStatusText.Name = "colStatusText";
            this.colStatusText.Visible = true;
            this.colStatusText.VisibleIndex = 2;
            // 
            // mvvmContext
            // 
            this.mvvmContext.ContainerControl = this;
            this.mvvmContext.ViewModelType = typeof(DXHtmlMessengerSample.ViewModels.MessagesViewModel);
            // 
            // memoEdit1
            // 
            this.messageEdit.DataBindings.Add(new System.Windows.Forms.Binding("EditValue", this.messageBindingSource, "MessageText", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.messageEdit.Location = new System.Drawing.Point(149, 3);
            this.messageEdit.Name = "messageEdit";
            this.messageEdit.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.25F);
            this.messageEdit.Properties.NullValuePrompt = "Type your message here...";
            this.messageEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.messageEdit.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True;
            this.messageEdit.Size = new System.Drawing.Size(100, 96);
            this.messageEdit.TabIndex = 0;
            // 
            // MessagesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tablePanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MessagesView";
            this.Size = new System.Drawing.Size(430, 600);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel)).EndInit();
            this.tablePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.typingBox)).EndInit();
            this.typingBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.messageBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.toolbarPanel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messagesItemsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.messageMenuPopup)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel;
        private DevExpress.XtraEditors.HtmlContentControl toolbarPanel;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Items.ItemsView messagesItemsView;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraGrid.Columns.GridColumn colUserName;
        private DevExpress.XtraGrid.Columns.GridColumn colAvatar;
        private DevExpress.XtraGrid.Columns.GridColumn colText;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusText;
        private DevExpress.XtraEditors.HtmlContentControl typingBox;
        private System.Windows.Forms.BindingSource messageBindingSource;
        private DevExpress.XtraEditors.MemoEdit messageEdit;
        private DevExpress.XtraEditors.HtmlContentPopup messageMenuPopup;
    }
}
