namespace DXHtmlMessengerSample.Services {
    public interface ISettingsService {
        string CurrentUser { get; }
        string Theme { get; }
    }
}