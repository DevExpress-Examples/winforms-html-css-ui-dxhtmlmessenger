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
            WindowsFormsSettings.LoadApplicationSettings();
            Services.AppSettigns.Register();
            // Setup DevAV Database
            var dataBaseFilePath = DevAVDataDirectoryHelper.GetFile("devav.sqlite3", "DB");
            DevAVDataDirectoryHelper.DataPath = Path.GetDirectoryName(dataBaseFilePath);
            DevExpress.Mvvm.ServiceContainer.Default.RegisterService(new DevExpress.DevAV.Chat.DevAVEmpployeesInMemoryServer());
            // Setup UI
            WindowsFormsSettings.ScrollUIMode = ScrollUIMode.Fluent;
            AssetsPath = Path.Combine(Path.GetDirectoryName(dataBaseFilePath), "..", "Assets");
            SvgImages = SvgImageCollection.FromResources("DXHtmlMessengerSample.Resources", typeof(DXHtmlMessenger).Assembly);
            // Setup Dlls
            DllPath = Path.Combine(Path.GetDirectoryName(dataBaseFilePath), "..", "DLL");
            AppDomain.CurrentDomain.AssemblyResolve += OnCurrentDomainAssemblyResolve;
            DevAVDataDirectoryHelper.LocalPrefix = "WinOutlookInspiredApp";
        }
        static Assembly OnCurrentDomainAssemblyResolve(object sender, ResolveEventArgs args) {
            string partialName = AssemblyHelper.GetPartialName(args.Name).ToLowerInvariant();
            if(partialName == "entityframework" || partialName == "system.data.sqlite" || partialName == "system.data.sqlite.ef6") 
                return Assembly.LoadFrom(Path.Combine(DllPath, partialName + ".dll"));
            return null;
        }
        //
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