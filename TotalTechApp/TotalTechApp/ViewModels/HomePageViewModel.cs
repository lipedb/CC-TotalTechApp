using Prism.Navigation;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Resources;
using TotalTechApp.Extensions;
using TotalTechApp.Services;
using Prism.Services;
using TotalTechApp.Common;
using TotalTechApp.Models.Responses;
using TotalTechApp.Services.Remote;
using TotalTechApp.Models;
using System.Linq;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace TotalTechApp.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public HomePageViewModel(INavigationService navigationService,
                                IPageDialogService dialogService,
                                ILoadingIndicatorService loadingIndicatorService)
            : base(navigationService, loadingIndicatorService)
        {
            Title = "Main Page";
            UserList = new ObservableCollection<Result>();
            ResourceManager = TranslateExtension.ResourceManager;
            
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if(IsConnected)
            {
                if(String.IsNullOrEmpty(UserSettings.UserList))
                {
                    GettingItens = true;
                    randomUsers = GetUsers(true);
                    GettingItens = false;
                    GenerateRating();
                    StoreUsersOnDB();
                    DisplayUsers();
                }
                else
                {
                    GettingItens = true;
                    DisplayUsers();
                    GettingItens = false;
                }

            }
            else
            {
                if (String.IsNullOrEmpty(UserSettings.UserList))
                {
                    NoCahcheItens = true;
                }
                else
                {
                    GettingItens = true;
                    DisplayUsers();
                    GettingItens = false;
                }
            }
        }

        public void GenerateRating()
        {
            foreach(Result element in randomUsers.Results)
            {

                double[] randomNumbers = new double[10];
                for (int i = 0; i < 10; i++)
                {
                    randomNumbers[i] = GetRandomNumber(0,5);
                }
                double average = randomNumbers.Average(t => t);
                element.Rating = average.ToString() + " ⭐";
            }
        }

        private static readonly Random getrandom = new Random();

        public static double GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }

        public void StoreUsersOnDB()
        {
            UserSettings.UserList = randomUsers.ToJson();
        }

        public void DisplayUsers()
        {
            if (UserList.Count > 0)
                UserList.Clear();
            RandomUsers StoredUsers = GetUsers(false);
            foreach(Result element in StoredUsers.Results)
            {
                UserList.Add(element);
            }
        }

        public RandomUsers GetUsers(bool remote)
        {
            if(remote)
            {
                Task<RandomUsers> task = Task.Run<RandomUsers>(async () => await GetUsersAsync());
                return task.Result;
            }
            else
            {
                RandomUsers cacheUsers = RandomUsers.FromJson(UserSettings.UserList);
                return cacheUsers;
            }

        }

        public async Task<RandomUsers> GetUsersAsync()
        {
            var result = await RemoteService.GetUsersAsync();
            return result as RandomUsers;
        }

        private async Task LogOut()
        {
            UserSettings.EmailAddress = String.Empty;
            Settings.LoginCompleted = false;
            UserSettings.UserList = String.Empty;
            await NavigationService.NavigateAsync(new Uri("/" + Routes.Login, UriKind.Absolute));
        }

        private async Task SyncAction()
        {
            GettingItens = true;
            randomUsers = GetUsers(true);
            GettingItens = false;
            GenerateRating();
            StoreUsersOnDB();
            DisplayUsers();
        }

        private async Task UserDetailsAsync(Result selectedtem)
        {
            string jsonObject = String.Empty;
            foreach (Result item in UserList)
            {
                if (selectedtem.Id == item.Id)
                    jsonObject = JsonConvert.SerializeObject(item);
            }

            var navParameters = new NavigationParameters
            {
                { AppConstants.ObjectDetail, jsonObject},
            };
            try
            {
                NavigationService.NavigateAsync(Routes.Detail, navParameters);
            }
            catch (Exception ex)
            {
                string exeception = ex.ToString();
                Console.WriteLine(exeception);
            }
            

        }

        public ICommand LogOutCommand => new AsyncCommand(LogOut);

        public ICommand SyncActionCommand => new AsyncCommand(SyncAction); 

        public ICommand MenuItemSelectedCommand => new AsyncCommand<Result>(UserDetailsAsync);

        private RandomUsers randomUsers;

        private Color _stateColor;
        public Color StateColor
        {
            get { return _stateColor; }
            set { SetProperty(ref _stateColor, value); }
        }

        private bool gettingItens = false;
        public bool GettingItens
        {
            get => gettingItens;
            set => SetProperty(ref gettingItens, value);
        }

        private bool noCahcheItens = false;
        public bool NoCahcheItens
        {
            get => noCahcheItens;
            set => SetProperty(ref noCahcheItens, value);
        }

        private ObservableCollection<Result> _userList;
        public ObservableCollection<Result> UserList
        {
            get => _userList;
            set => _userList = value;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
        }

        private ResourceManager ResourceManager { get; set; }

    }
}

