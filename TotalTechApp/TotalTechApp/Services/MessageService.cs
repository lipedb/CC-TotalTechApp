using System;
using System.Threading.Tasks;


namespace TotalTechApp.Services
{
    public class MessageService
    {
        public static void ShowMensaje(string titulo, string mensaje)
        {
            App.Current.MainPage.DisplayAlert(titulo, mensaje, "OK");
        }

        public static async Task ShowMensajeAsync(string titulo, string mensaje)
        {
            await App.Current.MainPage.DisplayAlert(titulo, mensaje, "OK");
        }

        public static async Task ShowMessage(string title, string message, string buttonText, Action afterHideCallback)
        {
            await App.Current.MainPage.DisplayAlert(
                title,
                message,
                buttonText);

            afterHideCallback?.Invoke();
        }
    }
}
