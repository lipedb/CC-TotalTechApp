using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TotalTechApp.Common;
using Prism.Navigation;
using TotalTechApp.Extensions;
using Xamarin.Forms;
using TotalTechApp.Models;
using TotalTechApp.Services;
using System.Resources;
using TotalTechApp.Resources;

namespace TotalTechApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private bool hasLoginError;
        private bool _isPasswordVisible;
        private string email, password;
        private bool _isValidEmail;
        private bool _isLoginEnabled;


        public LoginPageViewModel(INavigationService navigationService,
                                    ILoadingIndicatorService loadingIndicatorService)
            : base(navigationService, loadingIndicatorService)
        {
            ResourceManager = TranslateExtension.ResourceManager;
            ChangeVisibilityCommand = new Command(ChangePasswordStatus);
            LoginCommand = new AsyncCommand(Login);
            NavigateToForgotPasswordCommand = new AsyncCommand(Login);
            UnfocusedEmailCommand = new Command<FocusEventArgs>(UnfocusedEmailAction);
            FocusedEmailCommand = new Command<FocusEventArgs>(FocusedEmailAction);
            IsValidEmail = true;
            IsPasswordVisible = false;

#if DEBUG
            Email = "user@ttapp.com";
            Password = "123456";
#endif

        }

        public ICommand ChangeVisibilityCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }
        public ICommand NavigateToForgotPasswordCommand { get; private set; }
        public Command<FocusEventArgs> UnfocusedEmailCommand { get; private set; }
        public Command<FocusEventArgs> FocusedEmailCommand { get; private set; }
        private ResourceManager ResourceManager { get; set; }

        public bool HasLoginError
        {
            get => hasLoginError;
            set => SetProperty(ref hasLoginError, value);
        }

        public bool IsPasswordVisible
        {
            get => _isPasswordVisible;
            set => SetProperty(ref _isPasswordVisible, value);
        }

        public bool IsLoginEnabled
        {
            get => _isLoginEnabled;
            set => SetProperty(ref _isLoginEnabled, value);
        }

        public bool IsValidEmail
        {
            get => _isValidEmail;
            set => SetProperty(ref _isValidEmail, value);
        }

        public string Email
        {
            get => email;
            set
            {
                SetProperty(ref email, value);
                if (!String.IsNullOrWhiteSpace(Email))
                    IsLoginEnabled = CheckFields();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
                IsLoginEnabled = CheckFields();
            }
        }

        private async Task Login()
        {
            IsBusy = true;
            // TODO: Mock data only
            if (!IsAcceptablePassword())
            {
                IsValidEmail = false;
                HasLoginError = true;
                IsLoginEnabled = false;
                IsBusy = false;
                await MessageService.ShowMensajeAsync(ResourceManager.GetString(nameof(Strings.WarningSignal)), ResourceManager.GetString(nameof(Strings.IncorrectEmailOrPassword)));
                return;
            }
            //Store information
            StoreCredentials();
            try
            {
                await NavigationService.NavigateAsync(Routes.Root);
                IsBusy = false;
            }
            catch (Exception ex)
            {
                string exeception = ex.ToString();
                Console.WriteLine(exeception);
            }
            
        }

        private bool IsAcceptablePassword()
        {
            //TODO: Change with client's logic validation
            if(Email != "user@ttapp.com")
                return false;
            if (Password != "123456")
                return false;
            return true;
        }

        private bool IsAcceptableEmail()
        {
            return Email.IsValidEmailAddress();
        }

        private void ChangePasswordStatus()
        {
            IsPasswordVisible = !IsPasswordVisible;
        }

        private bool CheckFields()
        {
            HasLoginError = false;
            if (IsAcceptableEmail() && !string.IsNullOrEmpty(Password))
            {
                IsValidEmail = true;
                return true;
            }
            return false;
        }

        private void FocusedEmailAction(FocusEventArgs args)
        {
            IsValidEmail = true;
        }

        private void StoreCredentials()
        {
            //Store login Email on preferences
            UserSettings.EmailAddress = Email;
            Settings.LoginCompleted = true;
        }

        private void UnfocusedEmailAction(FocusEventArgs args)
        {
            //Only display error if the email is not empty
            if (!String.IsNullOrWhiteSpace(Email))
                IsValidEmail = IsAcceptableEmail();
            IsLoginEnabled = CheckFields();
        }
    }
}