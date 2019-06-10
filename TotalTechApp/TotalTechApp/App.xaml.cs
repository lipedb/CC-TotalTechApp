using Prism;
using Prism.Ioc;
using TotalTechApp.ViewModels;
using TotalTechApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TotalTechApp.Common;
using TotalTechApp.Services;
using System.Resources;
using TotalTechApp.Extensions;

namespace TotalTechApp
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            if(Settings.LoginCompleted)
            {
                await NavigationService.NavigateAsync(Routes.Root);
            }
            else
            {
                await NavigationService.NavigateAsync(Routes.Login);
            }
            
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            RegisterForNavigation(containerRegistry);
            RegisterServices(containerRegistry);
        }

        private void RegisterForNavigation(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<UserDetailPage, UserDetailViewModel>(Routes.Detail);
        }

        private void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ITranslationService, TranslationService>();
            containerRegistry.RegisterInstance<ResourceManager>(TranslateExtension.ResourceManager);
        }
    }
}
