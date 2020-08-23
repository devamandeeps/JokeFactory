using Library.Common;
using Library.Common.Interfaces;
using Library.NetworkService.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Library.NetworkService
{
    public class NetworkService : INetworkService
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public NetworkService(HttpClient httpClient, ILogger<NetworkService> logger = null)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<IServiceResult<string>> GetStringResult(string url)
        {
            var result = new ServiceResult<string> { IsSuccess = true };

            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var response = await _httpClient.SendAsync(request);
                result.Result = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, ex.Message);
                result.IsSuccess = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
