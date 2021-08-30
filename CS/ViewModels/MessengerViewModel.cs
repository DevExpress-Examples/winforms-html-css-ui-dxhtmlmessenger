namespace DXHtmlMessengerSample.ViewModels {
    using System.Threading.Tasks;
    using DevExpress.DevAV.Chat;
    using DevExpress.DevAV.Chat.Commands;
    using DevExpress.DevAV.Chat.Events;
    using DevExpress.DevAV.Chat.Model;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using DXHtmlMessengerSample.Services;

    public class MessengerViewModel {
        public virtual string Title {
            get;
            protected set;
        }
        IDispatcherService dispatcher;
        IChannel channel;
        public async Task OnLoad() {
            dispatcher = this.GetRequiredService<IDispatcherService>();
            // Load settings and initialize
            var settingsService = this.GetRequiredService<ISettingsService>();
            var theme = settingsService.Theme ?? "Light";
            var currentUser = settingsService.CurrentUser ?? "John Heart";
            // Open messenger channel
            var messageServer = this.GetRequiredService<IMessageServer>();
            channel = await messageServer.Create(currentUser);
            channel.Subscribe(OnChannelEvent);
            // Pass the channel into dependent ViewModels
            Messenger.Default.Send(channel);
            await dispatcher.BeginInvoke(() => Title = $"DX HTML MESSENGER (CS) - [{currentUser.ToUpper()}]");
        }
        int authCounter = 0;
        void OnChannelEvent(ChannelEvent @event) {
            var credentialsRequired = @event as CredentialsRequiredEvent;
            if(credentialsRequired != null) {
                if(0 == authCounter++) {
                    // provide the access token from local cache without interaction
                    var cacheQuery = QueryAccessTokenFromLocalAuthCache(@event.UserName, credentialsRequired.Salt);
                    credentialsRequired.SetAccessTokenQuery(cacheQuery);
                }
                else {
                    // or query access-token asynchronously for the specific user
                    var userQuery = QueryAccessTokenFromUser(@event.UserName, credentialsRequired.Salt);
                    credentialsRequired.SetAccessTokenQuery(userQuery);
                }
            }
        }
        Task<string> QueryAccessTokenFromLocalAuthCache(string userName, string salt) {
            // simple emulation of local auth cache
            return Task.FromResult(DevAVEmpployeesInMemoryServer.GetPasswordHash(string.Empty, salt));
        }
        Task<string> QueryAccessTokenFromUser(string userName, string salt) {
            var accessTokenQueryCompletionSource = new TaskCompletionSource<string>();
            dispatcher.BeginInvoke(() => {
                var signInViewModel = SignInViewModel.Create(userName, salt);
                signInViewModel.ShowDialog();
                if(!string.IsNullOrEmpty(signInViewModel.AccessToken))
                    accessTokenQueryCompletionSource.SetResult(signInViewModel.AccessToken);
                else
                    accessTokenQueryCompletionSource.SetCanceled();
            });
            return accessTokenQueryCompletionSource.Task;
        }
        public void OnClosed() {
            if(channel != null)
                channel.Dispose();
            channel = null;
        }
        public void LogOff() {
            if(channel != null)
                channel.Send(new LogOff(channel));
        }
        UserViewModel userViewModel;
        [Command(isCommand: false)]
        public void ShowUserInfo(UserInfo userInfo) {
            ShowPopup(userViewModel ?? (userViewModel = UserViewModel.Create(userInfo)), userInfo);
        }
        ContactViewModel contactViewModel;
        [Command(isCommand: false)]
        public void ShowContactInfo(UserInfo contactInfo) {
            ShowPopup(contactViewModel ?? (contactViewModel = ContactViewModel.Create(contactInfo)), contactInfo);
        }
        void ShowPopup(UserInfoViewModel viewModel, UserInfo info) {
            viewModel.SetParentViewModel(this);
            var popup = this.GetService<IWindowService>(viewModel.ServiceKey);
            popup.Show(null, viewModel, info, this);
        }
        public static void ShowUserInfo(object viewModel, UserInfo userInfo) {
            var messenger = viewModel.GetParentViewModel<MessengerViewModel>();
            if(messenger != null)
                messenger.ShowUserInfo(userInfo);
        }
        public static void ShowContactInfo(object viewModel, UserInfo contactInfo) {
            var messenger = viewModel.GetParentViewModel<MessengerViewModel>();
            if(messenger != null)
                messenger.ShowContactInfo(contactInfo);
        }
    }
}