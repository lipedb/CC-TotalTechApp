using Android.App;
using Android.Content.PM;
using Android.OS;
using Prism;
using Prism.Ioc;
using Android.Views;
using Android.Support.V7.Widget;
using Plugin.CurrentActivity;
using TotalTechApp.Extensions;



namespace TotalTechApp.Droid
{
    [Activity(
        Label = "PROTECT",
        Icon = "@drawable/app_icon",
        RoundIcon = "@drawable/circle_app_icon",
        Theme = "@style/AppTheme.Splash",
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleTask,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        public static Toolbar ToolBar { get; private set; }
        public static MainActivity Instance { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            Instance = this;

            CrossCurrentActivity.Current.Init(this, bundle);
            base.Window.RequestFeature(WindowFeatures.ActionBar);
            base.SetTheme(Resource.Style.MainTheme);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(Resources.GetString(Resource.Color.colorNavegationBarBackground)));
            }
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //FFImageLoading Init
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            var ScreenHeight = (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density);
            var ScreenWidth = (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density);


            LoadApplication(new App(new AndroidInitializer()));

        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILoadingIndicatorService, Services.LoadingIndicatorService>();
        }
    }
}
