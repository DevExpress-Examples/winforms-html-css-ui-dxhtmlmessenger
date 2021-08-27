Imports System.IO
Imports DevExpress.Internal
Imports DevExpress.Utils
Imports DevExpress.XtraEditors

Namespace DXHtmlMessengerSample
    Friend NotInheritable Class DXHtmlMessenger
        Public Shared ReadOnly AssetsPath As String
        Public ReadOnly Shared DllPath As String
        '
        Public Shared Property SvgImages() As SvgImageCollection
        Shared Sub New()
            WindowsFormsSettings.LoadApplicationSettings()
            Services.AppSettigns.Register()
            ' Setup DevAV Database
            Dim dataBaseFilePath = DevAVDataDirectoryHelper.GetFile("devav.sqlite3", "DB")
            DevAVDataDirectoryHelper.DataPath = Path.GetDirectoryName(dataBaseFilePath)
            DevExpress.Mvvm.ServiceContainer.Default.RegisterService(New DevExpress.DevAV.Chat.DevAVEmpployeesInMemoryServer())
            ' Setup UI
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Fluent
            AssetsPath = Path.Combine(Path.GetDirectoryName(dataBaseFilePath), "..", "Assets")
            SvgImages = SvgImageCollection.FromResources(GetType(DXHtmlMessenger).Assembly)
            ' Setup Dlls
            DllPath = Path.Combine(Path.GetDirectoryName(dataBaseFilePath), "..", "DLL")
            AddHandler AppDomain.CurrentDomain.AssemblyResolve, AddressOf OnCurrentDomainAssemblyResolve
            DevAVDataDirectoryHelper.LocalPrefix = "WinDXHtmlMessengerApp"
        End Sub
        Shared Function OnCurrentDomainAssemblyResolve(ByVal sender As Object, ByVal args As ResolveEventArgs) As System.Reflection.Assembly
            Dim partialName As String = AssemblyHelper.GetPartialName(args.Name).ToLowerInvariant()
            If partialName = "entityframework" OrElse partialName = "entityframework.sqlserver" _
                OrElse partialName = "system.data.sqlite" OrElse partialName = "system.data.sqlite.ef6" Then
                Return System.Reflection.Assembly.LoadFrom(Path.Combine(DllPath, partialName & ".dll"))
            End If
            Return Nothing
        End Function
        <STAThread>
        Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Dim messenger = New MessengerForm()
            messenger.IconOptions.SvgImage = SvgImages("AppIcon")
            Application.Run(messenger)
        End Sub
    End Class
End Namespace