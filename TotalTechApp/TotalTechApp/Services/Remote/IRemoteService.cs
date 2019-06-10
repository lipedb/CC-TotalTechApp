using Refit;
using System.Threading.Tasks;
using TotalTechApp.Models.Responses;

namespace TotalTechApp.Services.Remote
{
    [Headers("User-Agent: :request:")]
    public interface IRemoteService
    {
        [Get("/api/?results=5")]
        Task<RandomUsers> ListaUsers();
    }
}
