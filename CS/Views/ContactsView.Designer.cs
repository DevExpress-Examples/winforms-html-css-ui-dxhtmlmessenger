namespace DXHtmlMessengerSample.Views {
    partial class ContactsView {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContactsView));
            this.tablePanel = new DevExpress.Utils.Layout.TablePanel();
            this.searchPanel = new DevExpress.XtraEditors.HtmlContentControl();
            this.searchControl = new DevExpress.XtraEditors.SearchControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.contactsTileView = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.mvvmContext = new DevExpress.Utils.MVVM.MVVMContext(this.components);
            this.contactMenuPopup = new DevExpress.XtraEditors.HtmlContentPopup(this.components);
            this.contactTooltip = new DevExpress.XtraEditors.HtmlContentPopup(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel)).BeginInit();
            this.tablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchPanel)).BeginInit();
            this.searchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactsTileView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactMenuPopup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactTooltip)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel
            // 
            this.tablePanel.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F)});
            this.tablePanel.Controls.Add(this.searchPanel);
            this.tablePanel.Controls.Add(this.gridControl);
            this.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel.Location = new System.Drawing.Point(0, 0);
            this.tablePanel.Margin = new System.Windows.Forms.Padding(0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 49F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F)});
            this.tablePanel.Size = new System.Drawing.Size(270, 600);
            this.tablePanel.TabIndex = 1;
            // 
            // searchPanel
            // 
            this.tablePanel.SetColumn(this.searchPanel, 0);
            this.searchPanel.Controls.Add(this.searchControl);
            this.searchPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchPanel.Location = new System.Drawing.Point(0, 0);
            this.searchPanel.Margin = new System.Windows.Forms.Padding(0);
            this.searchPanel.Name = "searchPanel";
            this.tablePanel.SetRow(this.searchPanel, 0);
            this.searchPanel.Size = new System.Drawing.Size(270, 49);
            this.searchPanel.TabIndex = 3;
            // 
            // searchControl
            // 
            this.searchControl.Client = this.gridControl;
            this.searchControl.Location = new System.Drawing.Point(76, 15);
            this.searchControl.Margin = new System.Windows.Forms.Padding(0);
            this.searchControl.Name = "searchControl";
            this.searchControl.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.searchControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Repository.ClearButton()});
            this.searchControl.Properties.Client = this.gridControl;
            this.searchControl.Properties.ShowSearchButton = false;
            this.searchControl.Size = new System.Drawing.Size(179, 18);
            this.searchControl.TabIndex = 0;
            // 
            // gridControl
            // 
            this.tablePanel.SetColumn(this.gridControl, 0);
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 49);
            this.gridControl.MainView = this.contactsTileView;
            this.gridControl.Margin = new System.Windows.Forms.Padding(0);
            this.gridControl.Name = "gridControl";
            this.tablePanel.SetRow(this.gridControl, 1);
            this.gridControl.Size = new System.Drawing.Size(270, 551);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.contactsTileView});
            // 
            // contactsTileView
            // 
            this.contactsTileView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.contactsTileView.GridControl = this.gridControl;
            this.contactsTileView.Name = "contactsTileView";
            this.contactsTileView.OptionsList.DrawItemSeparators = DevExpress.XtraGrid.Views.Tile.DrawItemSeparatorsMode.None;
            this.contactsTileView.OptionsTiles.AllowPressAnimation = false;
            this.contactsTileView.OptionsTiles.GroupTextPadding = new System.Windows.Forms.Padding(0);
            this.contactsTileView.OptionsTiles.HighlightFocusedTileStyle = DevExpress.XtraGrid.Views.Tile.HighlightFocusedTileStyle.None;
            this.contactsTileView.OptionsTiles.IndentBetweenGroups = 0;
            this.contactsTileView.OptionsTiles.IndentBetweenItems = 0;
            this.contactsTileView.OptionsTiles.ItemPadding = new System.Windows.Forms.Padding(0);
            this.contactsTileView.OptionsTiles.ItemSize = new System.Drawing.Size(248, 72);
            this.contactsTileView.OptionsTiles.LayoutMode = DevExpress.XtraGrid.Views.Tile.TileViewLayoutMode.List;
            this.contactsTileView.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.contactsTileView.OptionsTiles.Padding = new System.Windows.Forms.Padding(0);
            this.contactsTileView.OptionsTiles.RowCount = 0;
            this.contactsTileView.ItemCustomize += new DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventHandler(this.OnContactItemTemplateCustomize);
            this.contactsTileView.CustomItemTemplate += new DevExpress.XtraGrid.Views.Tile.TileViewCustomItemTemplateEventHandler(this.OnContactItemTemplate);
            // 
            // mvvmContext
            // 
            this.mvvmContext.ContainerControl = this;
            this.mvvmContext.ViewModelType = typeof(DXHtmlMessengerSample.ViewModels.ContactsViewModel);
            // 
            // contactMenuPopup
            // 
            this.contactMenuPopup.ContainerControl = this;
            this.contactMenuPopup.HideOnElementClick = DevExpress.Utils.DefaultBoolean.True;
            this.contactMenuPopup.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            // 
            // contactTooltip
            // 
            this.contactTooltip.AutoHidingDelay = 1500;
            this.contactTooltip.ContainerControl = this;
            this.contactTooltip.HideAutomatically = DevExpress.Utils.DefaultBoolean.True;
            this.contactTooltip.HideOnElementClick = DevExpress.Utils.DefaultBoolean.True;
            this.contactTooltip.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.contactTooltip.ViewModelType = typeof(DXHtmlMessengerSample.ViewModels.ContactViewModel);
            this.contactTooltip.ViewModelSet += new DevExpress.Utils.MVVM.ViewModelSetEventHandler(OnContactTooltipViewModelSet);
            // 
            // ContactsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.tablePanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ContactsView";
            this.Size = new System.Drawing.Size(270, 600);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel)).EndInit();
            this.tablePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchPanel)).EndInit();
            this.searchPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.searchControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactsTileView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mvvmContext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactMenuPopup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactTooltip)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private DevExpress.Utils.Layout.TablePanel tablePanel;
        private DevExpress.XtraEditors.HtmlContentControl searchPanel;
        private DevExpress.XtraEditors.SearchControl searchControl;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Tile.TileView contactsTileView;
        private DevExpress.Utils.MVVM.MVVMContext mvvmContext;
        private DevExpress.XtraEditors.HtmlContentPopup contactMenuPopup;
        private DevExpress.XtraEditors.HtmlContentPopup contactTooltip;
    }
}