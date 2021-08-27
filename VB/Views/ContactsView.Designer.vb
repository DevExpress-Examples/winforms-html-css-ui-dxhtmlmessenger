Namespace DXHtmlMessengerSample.Views
    Partial Public Class ContactsView
        ''' <summary> 
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary> 
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (components IsNot Nothing) Then
                components.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Component Designer generated code"

        ''' <summary> 
        ''' Required method for Designer support - do not modify 
        ''' the contents of this method with the code editor.
        ''' </summary>
        Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(ContactsView))
            Me.tablePanel = New DevExpress.Utils.Layout.TablePanel()
            Me.searchPanel = New DevExpress.XtraEditors.HtmlContentControl()
            Me.searchControl = New DevExpress.XtraEditors.SearchControl()
            Me.gridControl = New DevExpress.XtraGrid.GridControl()
            Me.contactsTileView = New DevExpress.XtraGrid.Views.Tile.TileView()
            Me.mvvmContext = New DevExpress.Utils.MVVM.MVVMContext(Me.components)
            DirectCast(Me.tablePanel, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.tablePanel.SuspendLayout()
            DirectCast(Me.searchPanel, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.searchPanel.SuspendLayout()
            DirectCast(Me.searchControl.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.gridControl, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.contactsTileView, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.mvvmContext, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' tablePanel
            ' 
            Me.tablePanel.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() { New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F)})
            Me.tablePanel.Controls.Add(Me.searchPanel)
            Me.tablePanel.Controls.Add(Me.gridControl)
            Me.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tablePanel.Location = New System.Drawing.Point(0, 0)
            Me.tablePanel.Margin = New System.Windows.Forms.Padding(0)
            Me.tablePanel.Name = "tablePanel"
            Me.tablePanel.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {
                New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 49F),
                New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1F)
            })
            Me.tablePanel.Size = New System.Drawing.Size(270, 600)
            Me.tablePanel.TabIndex = 1
            ' 
            ' searchPanel
            ' 
            Me.tablePanel.SetColumn(Me.searchPanel, 0)
            Me.searchPanel.Controls.Add(Me.searchControl)
            Me.searchPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.searchPanel.Location = New System.Drawing.Point(0, 0)
            Me.searchPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.searchPanel.Name = "searchPanel"
            Me.tablePanel.SetRow(Me.searchPanel, 0)
            Me.searchPanel.Size = New System.Drawing.Size(270, 49)
            Me.searchPanel.TabIndex = 3
            ' 
            ' searchControl
            ' 
            Me.searchControl.Client = Me.gridControl
            Me.searchControl.Location = New System.Drawing.Point(76, 15)
            Me.searchControl.Margin = New System.Windows.Forms.Padding(0)
            Me.searchControl.Name = "searchControl"
            Me.searchControl.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            Me.searchControl.Properties.Client = Me.gridControl
            Me.searchControl.Properties.ShowClearButton = False
            Me.searchControl.Properties.ShowSearchButton = False
            Me.searchControl.Size = New System.Drawing.Size(179, 18)
            Me.searchControl.TabIndex = 0
            ' 
            ' gridControl
            ' 
            Me.tablePanel.SetColumn(Me.gridControl, 0)
            Me.gridControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.gridControl.Location = New System.Drawing.Point(0, 49)
            Me.gridControl.MainView = Me.contactsTileView
            Me.gridControl.Margin = New System.Windows.Forms.Padding(0)
            Me.gridControl.Name = "gridControl"
            Me.tablePanel.SetRow(Me.gridControl, 1)
            Me.gridControl.Size = New System.Drawing.Size(270, 551)
            Me.gridControl.TabIndex = 0
            Me.gridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.contactsTileView})
            ' 
            ' contactsTileView
            ' 
            Me.contactsTileView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            Me.contactsTileView.GridControl = Me.gridControl
            Me.contactsTileView.Name = "contactsTileView"
            Me.contactsTileView.OptionsList.DrawItemSeparators = DevExpress.XtraGrid.Views.Tile.DrawItemSeparatorsMode.None
            Me.contactsTileView.OptionsTiles.GroupTextPadding = New System.Windows.Forms.Padding(0)
            Me.contactsTileView.OptionsTiles.HighlightFocusedTileStyle = DevExpress.XtraGrid.Views.Tile.HighlightFocusedTileStyle.None
            Me.contactsTileView.OptionsTiles.IndentBetweenGroups = 0
            Me.contactsTileView.OptionsTiles.IndentBetweenItems = 0
            Me.contactsTileView.OptionsTiles.ItemPadding = New System.Windows.Forms.Padding(0)
            Me.contactsTileView.OptionsTiles.ItemSize = New System.Drawing.Size(248, 72)
            Me.contactsTileView.OptionsTiles.LayoutMode = DevExpress.XtraGrid.Views.Tile.TileViewLayoutMode.List
            Me.contactsTileView.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical
            Me.contactsTileView.OptionsTiles.Padding = New System.Windows.Forms.Padding(0)
            Me.contactsTileView.OptionsTiles.RowCount = 0
            ' 
            ' mvvmContext
            ' 
            Me.mvvmContext.ContainerControl = Me
            Me.mvvmContext.ViewModelType = GetType(DXHtmlMessengerSample.ViewModels.ContactsViewModel)
            ' 
            ' ContactsView
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.Controls.Add(Me.tablePanel)
            Me.Margin = New System.Windows.Forms.Padding(0)
            Me.Name = "ContactsView"
            Me.Size = New System.Drawing.Size(270, 600)
            DirectCast(Me.tablePanel, System.ComponentModel.ISupportInitialize).EndInit()
            Me.tablePanel.ResumeLayout(False)
            DirectCast(Me.searchPanel, System.ComponentModel.ISupportInitialize).EndInit()
            Me.searchPanel.ResumeLayout(False)
            DirectCast(Me.searchControl.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.gridControl, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.contactsTileView, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.mvvmContext, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        #End Region

        Private tablePanel As DevExpress.Utils.Layout.TablePanel
        Private searchPanel As DevExpress.XtraEditors.HtmlContentControl
        Private searchControl As DevExpress.XtraEditors.SearchControl
        Private gridControl As DevExpress.XtraGrid.GridControl
        Private WithEvents contactsTileView As DevExpress.XtraGrid.Views.Tile.TileView
        Private mvvmContext As DevExpress.Utils.MVVM.MVVMContext
    End Class
End Namespace