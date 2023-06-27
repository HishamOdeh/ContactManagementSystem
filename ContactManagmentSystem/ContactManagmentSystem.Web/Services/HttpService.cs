using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ContactManagementSystem.Shared;
using MudBlazor;
using Newtonsoft.Json;

namespace ContactManagementSystem.Web.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ISnackbar _snackbar;

        public HttpService(HttpClient httpClient, ISnackbar snackbar)
        {
            _httpClient = httpClient;
            _snackbar = snackbar;
        }

        public async Task<T> SendGetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
            }
        }

        public async Task<CommandResponse> SendPostAsync<T>(string url, T request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            var commandResponse = await ConvertToCommandResponse(response);

            if (!response.IsSuccessStatusCode)
            {
                DisplayErrorSnackBars(commandResponse);
            }

            return commandResponse;
        }

        public async Task<CommandResponse> SendPostAsync(string url, MultipartFormDataContent content)
        {
            var response = await _httpClient.PostAsync(url, content);

            var commandResponse = await ConvertToCommandResponse(response);

            if (!response.IsSuccessStatusCode)
            {
                DisplayErrorSnackBars(commandResponse);
            }

            return commandResponse;
        }

        public async Task<CommandResponse> SendPostAsync(string url)
        {
            var response = await _httpClient.PostAsync(url, null);

            var commandResponse = await ConvertToCommandResponse(response);

            if (!response.IsSuccessStatusCode)
            {
                DisplayErrorSnackBars(commandResponse);
            }

            return commandResponse;
        }

        public async Task<CommandResponse> SendPutAsync<T>(string url, T request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, content);

            var commandResponse = await ConvertToCommandResponse(response);

            if (!response.IsSuccessStatusCode)
            {
                DisplayErrorSnackBars(commandResponse);
            }

            return commandResponse;
        }

        public async Task<CommandResponse> SendPutAsync(string url, MultipartFormDataContent content)
        {
            var response = await _httpClient.PutAsync(url, content);

            var commandResponse = await ConvertToCommandResponse(response);

            if (!response.IsSuccessStatusCode)
            {
                DisplayErrorSnackBars(commandResponse);
            }

            return commandResponse;
        }

        public async Task<CommandResponse> SendPutAsync(string url)
        {
            var response = await _httpClient.PutAsync(url, null);

            var commandResponse = await ConvertToCommandResponse(response);

            if (!response.IsSuccessStatusCode)
            {
                DisplayErrorSnackBars(commandResponse);
            }

            return commandResponse;
        }

        public async Task SendDeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Invalid status code in the HttpResponseMessage: {response.StatusCode}.");
            }
        }
        public async Task<CommandResponse> SendDeleteWithResponseAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);

            var commandResponse = await ConvertToCommandResponse(response);

            if (!response.IsSuccessStatusCode)
            {
                DisplayErrorSnackBars(commandResponse);
            }

            return commandResponse;
        }

        private static async Task<CommandResponse> ConvertToCommandResponse(HttpResponseMessage response)
        {
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<CommandResponse>(result);
        }

        private void DisplayErrorSnackBars(CommandResponse commandResponse)
        {
            foreach (var error in commandResponse.Errors)
            {
                foreach (var message in error.Value)
                {
                    _snackbar.Add(message, Severity.Error, config =>
                    {
                        config.RequireInteraction = true;
                        config.ShowTransitionDuration = 300;
                        config.HideTransitionDuration = 100;
                    });
                }
            }
        }
    }
}