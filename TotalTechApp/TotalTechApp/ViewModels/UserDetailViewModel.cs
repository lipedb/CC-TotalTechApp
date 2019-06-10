using Newtonsoft.Json;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using TotalTechApp.Common;
using TotalTechApp.Extensions;
using TotalTechApp.Models.Responses;

namespace TotalTechApp.ViewModels
{
    public class UserDetailViewModel : ViewModelBase
    {
        public UserDetailViewModel(INavigationService navigationService,
                               IPageDialogService dialogService,
                               ILoadingIndicatorService loadingIndicatorService)
           : base(navigationService, loadingIndicatorService)
        {
            Title = "Detail Page";
            ResourceManager = TranslateExtension.ResourceManager;
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
            base.OnNavigatingTo(parameters);
            string receivedJsonObject = parameters.GetValue<string>(key: AppConstants.ObjectDetail);
            ReceivedItem = JsonConvert.DeserializeObject<Result>(receivedJsonObject);
            UpdateUserInfo();
        }

        private void UpdateUserInfo()
        {
            Name = ReceivedItem.Name.First + " " + ReceivedItem.Name.Last;
            Picture = ReceivedItem.Picture.Thumbnail;
            Rating = ReceivedItem.Rating;
            Email = ReceivedItem.Email;
            Adress = ReceivedItem.Location.Street;
            City = ReceivedItem.Location.City;
            State = ReceivedItem.Location.State;
            Zip = ReceivedItem.Location.Postcode.ToString();
            Phone = ReceivedItem.Phone;
            UrlMaps = GetMapsUrl();
        }

        public string GetMapsUrl()
        {
            string url = "https://www.google.com/maps/search/?api=1&query=" + ReceivedItem.Location.Coordinates.Latitude.ToString() + "," + ReceivedItem.Location.Coordinates.Longitude.ToString();
            return url;
        }

        private ResourceManager ResourceManager { get; set; }

        private Result ReceivedItem { get; set; }

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private string rating;
        public string Rating
        {
            get => rating;
            set => SetProperty(ref rating, value);
        }

        private string urlMaps;
        public string UrlMaps
        {
            get => urlMaps;
            set => SetProperty(ref urlMaps, value);
        }

        private string email;
        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        private string adress;
        public string Adress
        {
            get => adress;
            set => SetProperty(ref adress, value);
        }

        private string city;
        public string City
        {
            get => city;
            set => SetProperty(ref city, value);
        }

        private string state;
        public string State
        {
            get => state;
            set => SetProperty(ref state, value);
        }

        private string zip;
        public string Zip
        {
            get => zip;
            set => SetProperty(ref zip, value);
        }

        private string phone;
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        private System.Uri picture;
        public System.Uri Picture
        {
            get => picture;
            set => SetProperty(ref picture, value);
        }
    }
}
