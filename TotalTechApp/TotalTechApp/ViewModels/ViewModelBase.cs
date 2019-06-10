using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Resources;
using TotalTechApp.Extensions;
using TotalTechApp.Resources;
using TotalTechApp.Services;

namespace TotalTechApp.ViewModels
{
    public class ViewModelBase : BindableBase, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }
        protected ILoadingIndicatorService LoadingIndicatorService { get; private set; }

        private IConnectivity ConnectivityIndicatorService = CrossConnectivity.Current;

        private ResourceManager BaseResourceManager { get; set; }

        public bool IsConnected { get; set; }

        private bool _infoOptionVisible;
        public bool InfoOptionVisible
        {
            get => _infoOptionVisible;
            set => SetProperty(ref _infoOptionVisible, value);
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public ViewModelBase(INavigationService navigationService, ILoadingIndicatorService loadingIndicatorService)
        {
            InfoOptionVisible = false;
            NavigationService = navigationService;
            LoadingIndicatorService = loadingIndicatorService;
            BaseResourceManager = TranslateExtension.ResourceManager;
            this.IsConnected = ConnectivityIndicatorService.IsConnected;
            this.ConnectivityIndicatorService.ConnectivityChanged += OnConnectivityChanged;

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

        protected Busy SetIsBusy(string message = "")
        {
            return new Busy(this, message);
        }

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.IsConnected;

            if (!IsConnected)
            {
                var stringResponse = "A problem has occurred with your network connection.";
                MessageService.ShowMensaje("⚠️", stringResponse);
            }
        }



        protected class Busy : IDisposable
        {
            private readonly object sync = new object();
            private readonly ViewModelBase viewModel;

            public Busy(ViewModelBase viewModel, string message)
            {
                this.viewModel = viewModel;
                lock (sync)
                {
                    viewModel.LoadingIndicatorService.Show(message);
                    viewModel.IsBusy = true;
                }
            }

            public void Dispose()
            {
                lock (sync)
                {
                    viewModel.LoadingIndicatorService.Dismiss();
                    viewModel.IsBusy = false;
                }
            }
        }
    }
}

