using System.Net.Http;
using System.Threading.Tasks;
using ContactManagementSystem.Shared;

namespace ContactManagementSystem.Web.Services
{
    public interface IHttpService
    {
        Task SendDeleteAsync(string url);
        Task<CommandResponse> SendDeleteWithResponseAsync(string url);
        Task<T> SendGetAsync<T>(string url);
        Task<CommandResponse> SendPostAsync(string url);
        Task<CommandResponse> SendPostAsync<T>(string url, T request);
        Task<CommandResponse> SendPostAsync(string url, MultipartFormDataContent content);
        Task<CommandResponse> SendPutAsync(string url);
        Task<CommandResponse> SendPutAsync<T>(string url, T request);
        Task<CommandResponse> SendPutAsync(string url, MultipartFormDataContent content);
    }
}