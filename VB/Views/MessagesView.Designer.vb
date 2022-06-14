Namespace DXHtmlMessengerSample.Views
    Partial Public Class MessagesView
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
            Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(MessagesView))
            Me.tablePanel = New DevExpress.Utils.Layout.TablePanel()
            Me.typingBox = New DevExpress.XtraEditors.HtmlContentControl()
            Me.messageBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            Me.toolbarPanel = New DevExpress.XtraEditors.HtmlContentControl()
            Me.gridControl = New DevExpress.XtraGrid.GridControl()
            Me.messagesItemsView = New DevExpress.XtraGrid.Views.Items.ItemsView()
            Me.colUserName = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colAvatar = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colText = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.colStatusText = New DevExpress.XtraGrid.Columns.GridColumn()
            Me.mvvmContext = New DevExpress.Utils.MVVM.MVVMContext(Me.components)
            Me.messageEdit = New DevExpress.XtraEditors.MemoEdit()
            Me.messageMenuPopup = New DevExpress.XtraEditors.HtmlContentPopup(Me.components)
            DirectCast(Me.tablePanel, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.tablePanel.SuspendLayout()
            DirectCast(Me.typingBox, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.typingBox.SuspendLayout()
            DirectCast(Me.messageBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.toolbarPanel, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.gridControl, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.messagesItemsView, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.mvvmContext, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.messageEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.messageMenuPopup, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' tablePanel
            ' 
            Me.tablePanel.Columns.AddRange(New DevExpress.Utils.Layout.TablePanelColumn() {New DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1.0F)})
            Me.tablePanel.Controls.Add(Me.typingBox)
            Me.tablePanel.Controls.Add(Me.toolbarPanel)
            Me.tablePanel.Controls.Add(Me.gridControl)
            Me.tablePanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.tablePanel.Location = New System.Drawing.Point(0, 0)
            Me.tablePanel.Margin = New System.Windows.Forms.Padding(0)
            Me.tablePanel.Name = "tablePanel"
            Me.tablePanel.Rows.AddRange(New DevExpress.Utils.Layout.TablePanelRow() {
                New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 49.0F),
                New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 1.0F),
                New DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.AutoSize, 1.0F)
            })
            Me.tablePanel.Size = New System.Drawing.Size(430, 600)
            Me.tablePanel.TabIndex = 1
            ' 
            ' typingBox
            ' 
            Me.tablePanel.SetColumn(Me.typingBox, 0)
            Me.typingBox.AutoScroll = False
            Me.typingBox.Controls.Add(Me.messageEdit)
            Me.typingBox.Dock = System.Windows.Forms.DockStyle.Fill
            Me.typingBox.Location = New System.Drawing.Point(0, 540)
            Me.typingBox.Margin = New System.Windows.Forms.Padding(0)
            Me.typingBox.Name = "typingBox"
            Me.tablePanel.SetRow(Me.typingBox, 2)
            Me.typingBox.Size = New System.Drawing.Size(430, 60)
            Me.typingBox.TabIndex = 4
            ' 
            ' messageBindingSource
            ' 
            Me.messageBindingSource.DataSource = GetType(DXHtmlMessengerSample.ViewModels.MessagesViewModel)
            ' 
            ' messageMenuPopup
            ' 
            Me.messageMenuPopup.ContainerControl = Me
            Me.messageMenuPopup.HideOnElementClick = DevExpress.Utils.DefaultBoolean.True
            Me.messageMenuPopup.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True
            ' 
            ' toolbarPanel
            ' 
            Me.tablePanel.SetColumn(Me.toolbarPanel, 0)
            Me.toolbarPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.toolbarPanel.Location = New System.Drawing.Point(0, 0)
            Me.toolbarPanel.Margin = New System.Windows.Forms.Padding(0)
            Me.toolbarPanel.Name = "toolbarPanel"
            Me.tablePanel.SetRow(Me.toolbarPanel, 0)
            Me.toolbarPanel.Size = New System.Drawing.Size(430, 49)
            Me.toolbarPanel.TabIndex = 3
            ' 
            ' gridControl
            ' 
            Me.tablePanel.SetColumn(Me.gridControl, 0)
            Me.gridControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.gridControl.Location = New System.Drawing.Point(0, 48)
            Me.gridControl.MainView = Me.messagesItemsView
            Me.gridControl.Margin = New System.Windows.Forms.Padding(0)
            Me.gridControl.Name = "gridControl"
            Me.tablePanel.SetRow(Me.gridControl, 1)
            Me.gridControl.Size = New System.Drawing.Size(430, 493)
            Me.gridControl.TabIndex = 0
            Me.gridControl.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() {Me.messagesItemsView})
            ' 
            ' messagesItemsView
            ' 
            Me.messagesItemsView.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            Me.messagesItemsView.Columns.AddRange(New DevExpress.XtraGrid.Columns.GridColumn() {Me.colUserName, Me.colAvatar, Me.colText, Me.colStatusText})
            Me.messagesItemsView.GridControl = Me.gridControl
            Me.messagesItemsView.Name = "messagesItemsView"
            ' 
            ' colUserName
            ' 
            Me.colUserName.FieldName = "Owner.UserName"
            Me.colUserName.Name = "colUserName"
            Me.colUserName.Visible = True
            Me.colUserName.VisibleIndex = 0
            ' 
            ' colAvatar
            ' 
            Me.colAvatar.FieldName = "Owner.Avatar"
            Me.colAvatar.Name = "colAvatar"
            Me.colAvatar.Visible = True
            Me.colAvatar.VisibleIndex = 1
            ' 
            ' colText
            ' 
            Me.colText.FieldName = "Text"
            Me.colText.Name = "colText"
            Me.colText.Visible = True
            Me.colText.VisibleIndex = 2
            ' 
            ' colStatusText
            ' 
            Me.colStatusText.FieldName = "StatusText"
            Me.colStatusText.Name = "colStatusText"
            Me.colStatusText.Visible = True
            Me.colStatusText.VisibleIndex = 2
            ' 
            ' mvvmContext
            ' 
            Me.mvvmContext.ContainerControl = Me
            Me.mvvmContext.ViewModelType = GetType(DXHtmlMessengerSample.ViewModels.MessagesViewModel)
            ' 
            ' memoEdit1
            ' 
            Me.messageEdit.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.messageBindingSource, "MessageText", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.messageEdit.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 10.25F)
            Me.messageEdit.Location = New System.Drawing.Point(149, 3)
            Me.messageEdit.Name = "messageEdit"
            Me.messageEdit.Properties.NullValuePrompt = "Type your message here..."
            Me.messageEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            Me.messageEdit.Properties.UseAdvancedMode = DevExpress.Utils.DefaultBoolean.True
            Me.messageEdit.Size = New System.Drawing.Size(100, 96)
            Me.messageEdit.TabIndex = 0
            ' 
            ' MessagesView
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0F, 96.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.Controls.Add(Me.tablePanel)
            Me.Margin = New System.Windows.Forms.Padding(0)
            Me.Name = "MessagesView"
            Me.Size = New System.Drawing.Size(430, 600)
            DirectCast(Me.tablePanel, System.ComponentModel.ISupportInitialize).EndInit()
            Me.tablePanel.ResumeLayout(False)
            DirectCast(Me.typingBox, System.ComponentModel.ISupportInitialize).EndInit()
            Me.typingBox.ResumeLayout(False)
            DirectCast(Me.messageBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.toolbarPanel, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.gridControl, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.messagesItemsView, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.mvvmContext, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.messageEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.messageMenuPopup, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
#End Region

        Private tablePanel As DevExpress.Utils.Layout.TablePanel
        Private toolbarPanel As DevExpress.XtraEditors.HtmlContentControl
        Private gridControl As DevExpress.XtraGrid.GridControl
        Private WithEvents messagesItemsView As DevExpress.XtraGrid.Views.Items.ItemsView
        Private mvvmContext As DevExpress.Utils.MVVM.MVVMContext
        Private colUserName As DevExpress.XtraGrid.Columns.GridColumn
        Private colAvatar As DevExpress.XtraGrid.Columns.GridColumn
        Private colText As DevExpress.XtraGrid.Columns.GridColumn
        Private colStatusText As DevExpress.XtraGrid.Columns.GridColumn
        Private typingBox As DevExpress.XtraEditors.HtmlContentControl
        Private messageBindingSource As System.Windows.Forms.BindingSource
        Private messageEdit As DevExpress.XtraEditors.MemoEdit
        Private messageMenuPopup As DevExpress.XtraEditors.HtmlContentPopup
    End Class
End Namespace
