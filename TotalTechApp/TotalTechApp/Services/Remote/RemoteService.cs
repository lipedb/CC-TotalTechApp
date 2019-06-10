using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TotalTechApp.Models.Responses;

namespace TotalTechApp.Services.Remote
{
    public static class RemoteService
    {
        public static string ENDPOINT = "https://randomuser.me";

        public static async Task<RandomUsers> GetUsersAsync()
        {
            RandomUsers users = new RandomUsers();
            IRemoteService remoteServices;
            remoteServices = RestService.For<IRemoteService>(ENDPOINT);
            try
            {
                users = await remoteServices.ListaUsers();
                
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine(ex.ToString());
#endif
            }
            return users;
        }
    }
}
