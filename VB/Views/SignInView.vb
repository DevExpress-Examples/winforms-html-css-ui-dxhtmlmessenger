Imports DevExpress.Utils.MVVM.UI
Imports DevExpress.XtraEditors
Imports DXHtmlMessengerSample.ViewModels

Namespace DXHtmlMessengerSample.Views
    <ViewType(GetType(SignInViewModel))>
    Partial Public Class SignInView
        Inherits XtraUserControl
        Public Sub New()
            InitializeComponent()
            If Not mvvmContext.IsDesignMode Then
                InitializeStyles()
                InitializeBindings()
            End If
        End Sub
        Sub InitializeStyles()
            Styles.SignIn.Apply(signInView)
        End Sub
        Sub InitializeBindings()
            Dim fluent = mvvmContext.OfType(Of SignInViewModel)()
            fluent.SetObjectDataSourceBinding(signInBindingSource)
            ' Bind sign-in buttons
            fluent.BindCommandToElement(signInView, "btnFacebook", Sub(x) x.SignInViaSocialNetwork())
            fluent.BindCommandToElement(signInView, "btnGoogle", Sub(x) x.SignInViaSocialNetwork())
            fluent.BindCommandToElement(signInView, "btnLinkedIn", Sub(x) x.SignInViaSocialNetwork())
            fluent.BindCommandToElement(signInView, "btnSignIn", Sub(x) x.SignIn())
        End Sub
        Private NotInheritable Class Styles
            Public Shared SignIn As Style = New SignInStyle()
            '
            Private NotInheritable Class SignInStyle
                Inherits Style
            End Class
        End Class
    End Class
End Namespace