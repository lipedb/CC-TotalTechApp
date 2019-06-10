using AndroidHUD;

namespace TotalTechApp.Droid.Services
{
    public class LoadingIndicatorService : ILoadingIndicatorService
    {
        public LoadingIndicatorService()
        {
        }

        public void Dismiss()
        {
            AndHUD.Shared.Dismiss();
        }

        public void Show(string text)
        {
            AndHUD.Shared.Show(MainActivity.Instance, text, -1, MaskType.Clear);
        }
    }
}