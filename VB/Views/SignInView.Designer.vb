Namespace DXHtmlMessengerSample.Views
    Partial Public Class SignInView
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
            Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(SignInView))
            Me.mvvmContext = New DevExpress.Utils.MVVM.MVVMContext(Me.components)
            Me.signInView = New DevExpress.XtraEditors.HtmlContentControl()
            Me.pwdEdit = New DevExpress.XtraEditors.TextEdit()
            Me.signInBindingSource = New System.Windows.Forms.BindingSource(Me.components)
            DirectCast(Me.mvvmContext, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.signInView, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.pwdEdit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            DirectCast(Me.signInBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' mvvmContext
            ' 
            Me.mvvmContext.ContainerControl = Me
            Me.mvvmContext.ViewModelType = GetType(DXHtmlMessengerSample.ViewModels.SignInViewModel)
            ' 
            ' signInView
            ' 
            Me.signInView.Controls.Add(Me.pwdEdit)
            Me.signInView.DataContext = signInBindingSource
            Me.signInView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.signInView.Location = New System.Drawing.Point(0, 0)
            Me.signInView.Margin = New System.Windows.Forms.Padding(0)
            Me.signInView.Name = "signInView"
            Me.signInView.Size = New System.Drawing.Size(420, 260)
            Me.signInView.TabIndex = 4
            ' 
            ' pwdEdit
            ' 
            Me.pwdEdit.DataBindings.Add(New System.Windows.Forms.Binding("EditValue", Me.signInBindingSource, "Password", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
            Me.pwdEdit.Location = New System.Drawing.Point(34, 96)
            Me.pwdEdit.Name = "pwdEdit"
            Me.pwdEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
            Me.pwdEdit.Properties.UseSystemPasswordChar = True
            Me.pwdEdit.Size = New System.Drawing.Size(362, 20)
            Me.pwdEdit.TabIndex = 5
            ' 
            ' signInBindingSource
            ' 
            Me.signInBindingSource.DataSource = GetType(DXHtmlMessengerSample.ViewModels.SignInViewModel)
            ' 
            ' LoginView
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0F, 96.0F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
            Me.Controls.Add(Me.signInView)
            Me.Name = "LoginView"
            Me.Size = New System.Drawing.Size(480, 320)
            DirectCast(Me.mvvmContext, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.signInView, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.pwdEdit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            DirectCast(Me.signInBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

#End Region

        Private mvvmContext As DevExpress.Utils.MVVM.MVVMContext
        Private signInView As DevExpress.XtraEditors.HtmlContentControl
        Private pwdEdit As DevExpress.XtraEditors.TextEdit
        Private signInBindingSource As System.Windows.Forms.BindingSource
    End Class
End Namespace
