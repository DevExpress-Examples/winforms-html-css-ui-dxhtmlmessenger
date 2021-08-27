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
            DirectCast(Me.mvvmContext, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.userInfoPopup, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.contactInfoPopup, System.ComponentModel.ISupportInitialize).BeginInit()
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
            Me.sidePanelContacts.Location = New System.Drawing.Point(0, 0)
            Me.sidePanelContacts.Margin = New System.Windows.Forms.Padding(0)
            Me.sidePanelContacts.MinimumSize = New System.Drawing.Size(180, 0)
            Me.sidePanelContacts.Name = "sidePanelContacts"
            Me.sidePanelContacts.OverlayResizeZoneThickness = 4
            Me.sidePanelContacts.Size = New System.Drawing.Size(271, 600)
            Me.sidePanelContacts.TabIndex = 0
            ' 
            ' contactsView
            ' 
            Me.contactsView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.contactsView.Location = New System.Drawing.Point(0, 0)
            Me.contactsView.Margin = New System.Windows.Forms.Padding(0)
            Me.contactsView.Name = "contactsView"
            Me.contactsView.Size = New System.Drawing.Size(270, 600)
            Me.contactsView.TabIndex = 0
            ' 
            ' messagesView
            ' 
            Me.messagesView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.messagesView.Location = New System.Drawing.Point(271, 0)
            Me.messagesView.Margin = New System.Windows.Forms.Padding(0)
            Me.messagesView.Name = "messagesView"
            Me.messagesView.Size = New System.Drawing.Size(529, 600)
            Me.messagesView.TabIndex = 1
            ' 
            ' MessengerForm
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96F, 96F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.ClientSize = New System.Drawing.Size(800, 600)
            Me.Controls.Add(Me.messagesView)
            Me.Controls.Add(Me.sidePanelContacts)
            Me.MinimumSize = New System.Drawing.Size(800, 600)
            Me.Name = "MessengerForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            Me.Text = "{Title}"
            DirectCast(Me.mvvmContext, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.userInfoPopup, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.contactInfoPopup, System.ComponentModel.ISupportInitialize).EndInit()
            Me.sidePanelContacts.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub

        #End Region
        Private mvvmContext As DevExpress.Utils.MVVM.MVVMContext
        Private WithEvents userInfoPopup As DevExpress.XtraEditors.HtmlContentPopup
        Private WithEvents contactInfoPopup As DevExpress.XtraEditors.HtmlContentPopup
        Private sidePanelContacts As DevExpress.XtraEditors.SidePanel
        Private contactsView As Views.ContactsView
        Private messagesView As Views.MessagesView
    End Class
End Namespace
