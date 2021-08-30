using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using DevExpress.Internal;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace DXHtmlMessengerSample {
    static class DXHtmlMessenger {
        public readonly static string AssetsPath;
        public readonly static string DllPath;
        //
        public static SvgImageCollection SvgImages {
            get;
            private set;
        }
        static DXHtmlMessenger() {
            if(!SystemInformation.TerminalServerSession && Screen.AllScreens.Length > 1)
                WindowsFormsSettings.SetPerMonitorDpiAware();
            else
                WindowsFormsSettings.SetDPIAware();
            WindowsFormsSettings.EnableFormSkins();
            WindowsFormsSettings.ForceDirectXPaint();
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Fluent;
            WindowsFormsSettings.FontBehavior = WindowsFormsFontBehavior.ForceSegoeUI;
            WindowsFormsSettings.DefaultLookAndFeel.SetSkinStyle(
                DevExpress.LookAndFeel.SkinStyle.Bezier, DevExpress.LookAndFeel.SkinSvgPalette.Bezier.Default);
            Services.AppSettigns.Register();
            // Setup DevAV Database
            var dataBaseFilePath = DevAVDataDirectoryHelper.GetFile("devav.sqlite3", "DB");
            DevAVDataDirectoryHelper.DataPath = Path.GetDirectoryName(dataBaseFilePath);
            Func<DevExpress.DevAV.DevAVDb> createDB =
#if DXCORE3
                () => new DevExpress.DevAV.DevAVDb(string.Format("Data Source={0}", dataBaseFilePath));
#else
                () => new DevExpress.DevAV.DevAVDb();
#endif
            DevExpress.Mvvm.ServiceContainer.Default.RegisterService(new DevExpress.DevAV.Chat.DevAVEmpployeesInMemoryServer(createDB));
            // Setup UI
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Fluent;
            AssetsPath = Path.Combine(Path.GetDirectoryName(dataBaseFilePath), "..", "Assets");
            SvgImages = SvgImageCollection.FromResources("DXHtmlMessengerSample.Resources", typeof(DXHtmlMessenger).Assembly);
            // Setup Dlls
            DllPath = Path.Combine(Path.GetDirectoryName(dataBaseFilePath), "..", "DLL");
            AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
            DevAVDataDirectoryHelper.LocalPrefix = "WinDXHtmlMessengerApp";
        }
        static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args) {
            string partialName = AssemblyHelper.GetPartialName(args.Name).ToLowerInvariant();
            if(partialName == "entityframework" || partialName == "entityframework.sqlserver" ||
                partialName == "system.data.sqlite" || partialName == "system.data.sqlite.ef6")
                return Assembly.LoadFrom(Path.Combine(DllPath, partialName + ".dll"));
            return null;
        }
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var messenger = new MessengerForm();
            messenger.IconOptions.SvgImage = SvgImages["AppIcon"];
            Application.Run(messenger);
        }
    }
}