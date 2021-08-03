namespace DXHtmlMessengerSample.Services {
    sealed class AppSettigns : ISettingsService {
        public static void Register() {
            DevExpress.Mvvm.ServiceContainer.Default.RegisterService(new AppSettigns());
        }
        public string CurrentUser {
            get { return Properties.Settings.Default.CurrentUser; }
        }
        public string Theme {
            get { return Properties.Settings.Default.Theme; }
        }
    }
}