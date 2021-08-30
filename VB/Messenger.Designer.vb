Namespace DXHtmlMessengerSample
    Partial Public Class MessengerForm
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

#Region "Windows Form Designer generated code"

        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container()
            Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(MessengerForm))
            Me.mvvmContext = New DevExpress.Utils.MVVM.MVVMContext(Me.components)
            Me.userInfoPopup = New DevExpress.XtraEditors.HtmlContentPopup(Me.components)
            Me.contactInfoPopup = New DevExpress.XtraEditors.HtmlContentPopup(Me.components)
            Me.sidePanelContacts = New DevExpress.XtraEditors.SidePanel()
            Me.contactsView = New DXHtmlMessengerSample.Views.ContactsView()
            Me.messagesView = New DXHtmlMessengerSample.Views.MessagesView()
            Me.toolbarFormControlCore = New DevExpress.XtraBars.ToolbarForm.ToolbarFormControl()
            Me.toolbarFormManager = New DevExpress.XtraBars.ToolbarForm.ToolbarFormManager(Me.components)
            Me.darkThemeBBI = New DevExpress.XtraBars.BarButtonItem()
            DirectCast(Me.mvvmContext, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.userInfoPopup, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.contactInfoPopup, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.toolbarFormControlCore, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.toolbarFormManager, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.sidePanelContacts.SuspendLayout()
            Me.SuspendLayout()
            ' 
            ' mvvmContext
            ' 
            Me.mvvmContext.ContainerControl = Me
            Me.mvvmContext.ViewModelType = GetType(DXHtmlMessengerSample.ViewModels.MessengerViewModel)
            ' 
            ' userInfoPopup
            ' 
            Me.userInfoPopup.ContainerControl = Me
            Me.userInfoPopup.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True
            Me.userInfoPopup.ViewModelType = GetType(DXHtmlMessengerSample.ViewModels.UserViewModel)
            ' 
            ' contactInfoPopup
            ' 
            Me.contactInfoPopup.ContainerControl = Me
            Me.contactInfoPopup.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True
            Me.contactInfoPopup.ViewModelType = GetType(DXHtmlMessengerSample.ViewModels.ContactViewModel)
            ' 
            ' sidePanelContacts
            ' 
            Me.sidePanelContacts.Controls.Add(Me.contactsView)
            Me.sidePanelContacts.Dock = System.Windows.Forms.DockStyle.Left
            Me.sidePanelContacts.Location = New System.Drawing.Point(0, 31)
            Me.sidePanelContacts.Margin = New System.Windows.Forms.Padding(0)
            Me.sidePanelContacts.MinimumSize = New System.Drawing.Size(180, 0)
            Me.sidePanelContacts.Name = "sidePanelContacts"
            Me.sidePanelContacts.OverlayResizeZoneThickness = 4
            Me.sidePanelContacts.Size = New System.Drawing.Size(271, 569)
            Me.sidePanelContacts.TabIndex = 0
            ' 
            ' contactsView
            ' 
            Me.contactsView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.contactsView.Location = New System.Drawing.Point(0, 0)
            Me.contactsView.Margin = New System.Windows.Forms.Padding(0)
            Me.contactsView.Name = "contactsView"
            Me.contactsView.Size = New System.Drawing.Size(270, 569)
            Me.contactsView.TabIndex = 0
            ' 
            ' messagesView
            ' 
            Me.messagesView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.messagesView.Location = New System.Drawing.Point(271, 31)
            Me.messagesView.Margin = New System.Windows.Forms.Padding(0)
            Me.messagesView.Name = "messagesView"
            Me.messagesView.Size = New System.Drawing.Size(529, 569)
            Me.messagesView.TabIndex = 1
            ' 
            ' toolbarFormControlCore
            ' 
            Me.toolbarFormControlCore.Location = New System.Drawing.Point(0, 0)
            Me.toolbarFormControlCore.Manager = Me.toolbarFormManager
            Me.toolbarFormControlCore.Name = "toolbarFormControl"
            Me.toolbarFormControlCore.Size = New System.Drawing.Size(800, 31)
            Me.toolbarFormControlCore.TabIndex = 2
            Me.toolbarFormControlCore.TabStop = False
            Me.toolbarFormControlCore.TitleItemLinks.Add(Me.darkThemeBBI)
            Me.toolbarFormControlCore.ToolbarForm = Me
            ' 
            ' toolbarFormManager
            ' 
            Me.toolbarFormManager.DockingEnabled = False
            Me.toolbarFormManager.Form = Me
            Me.toolbarFormManager.Items.AddRange(New DevExpress.XtraBars.BarItem() {
            Me.darkThemeBBI})
            Me.toolbarFormManager.MaxItemId = 1
            ' 
            ' darkThemeBBI
            ' 
            Me.darkThemeBBI.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right
            Me.darkThemeBBI.Caption = "Dark Theme"
            Me.darkThemeBBI.Id = 0
            Me.darkThemeBBI.Name = "darkThemeBBI"
            ' 
            ' MessengerForm
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0F, 96.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(800, 600)
            Me.Controls.Add(Me.messagesView)
            Me.Controls.Add(Me.sidePanelContacts)
            Me.Controls.Add(Me.toolbarFormControlCore)
            Me.MinimumSize = New System.Drawing.Size(800, 600)
            Me.Name = "MessengerForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "{Title}"
            Me.ToolbarFormControl = Me.toolbarFormControlCore
            DirectCast(Me.mvvmContext, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.userInfoPopup, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.contactInfoPopup, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.toolbarFormControlCore, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.toolbarFormManager, System.ComponentModel.ISupportInitialize).EndInit()
            Me.sidePanelContacts.ResumeLayout(False)
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub

#End Region
        Private mvvmContext As DevExpress.Utils.MVVM.MVVMContext
        Private WithEvents userInfoPopup As DevExpress.XtraEditors.HtmlContentPopup
        Private WithEvents contactInfoPopup As DevExpress.XtraEditors.HtmlContentPopup
        Private sidePanelContacts As DevExpress.XtraEditors.SidePanel
        Private contactsView As Views.ContactsView
        Private messagesView As Views.MessagesView
        Private toolbarFormControlCore As DevExpress.XtraBars.ToolbarForm.ToolbarFormControl
        Private toolbarFormManager As DevExpress.XtraBars.ToolbarForm.ToolbarFormManager
        Private WithEvents darkThemeBBI As DevExpress.XtraBars.BarButtonItem
    End Class
End Namespace
