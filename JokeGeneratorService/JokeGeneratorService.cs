using JokeApi.Interfaces;
using JokeApi.Models;
using Library.Common;
using Library.Common.Interfaces;
using Library.NetworkService.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JokeApi
{
    public class JokeGeneratorService : IJokeGeneratorService
    {
        private readonly ILogger _logger;
        private readonly INetworkService _networkService;
        private const string JokesAPIBaseUrl = "https://api.chucknorris.io";
        private const string NamesAPIBaseUrl = "https://www.names.privserv.com/api/";
        
        public JokeGeneratorService(INetworkService networkService, ILogger<JokeGeneratorService> logger = null)
        {
            _logger = logger;
            _networkService = networkService;
        }

        /// <summary>
        /// returns string (random joke) 
        /// </summary>
        /// <returns>IServiceResult - string</returns>
        public async Task<IServiceResult<string>> GetRandomJoke()
        {
            var result = new ServiceResult<string> { IsSuccess = true };            
            var response = await _networkService.GetStringResult($"{JokesAPIBaseUrl}/jokes/random");

            if (!response.IsSuccess)
            {
                result.IsSuccess = false;
                result.ErrorMessage = response.ErrorMessage;
                return result;
            }            
            result.Result = JsonConvert.DeserializeObject<dynamic>(response.Result).value; 
            return result;
        }

        /// <summary>
        /// returns string (joke) by category
        /// </summary>
        /// <returns>IServiceResult - string</returns>
        public async Task<IServiceResult<string>> GetJokeByCategory(string category)
        {
            var result = new ServiceResult<string> { IsSuccess = true };
            var response = await _networkService.GetStringResult($"{JokesAPIBaseUrl}/jokes/random?category={category}");

            if (!response.IsSuccess)
            {
                result.IsSuccess = false;
                result.ErrorMessage = response.ErrorMessage;
                return result;
            }
            result.Result = JsonConvert.DeserializeObject<dynamic>(response.Result).value;
            return result;
        }
        
        /// <summary>
        /// returns RandomPerson that contains name and surname
        /// </summary>
        /// <returns>IServiceResult - RandomPerson</returns>
        public async Task<IServiceResult<RandomPerson>> GetRandomPerson()    
        {
            var result = new ServiceResult<RandomPerson> { IsSuccess = true };
            var response = await _networkService.GetStringResult(NamesAPIBaseUrl);

            if (!response.IsSuccess)
            {
                result.IsSuccess = false;
                result.ErrorMessage = response.ErrorMessage;
                return result;
            }

            result.Result = JsonConvert.DeserializeObject<RandomPerson>(response.Result);             
            return result;
        }

        /// <summary>
        /// returns list of categories 
        /// </summary>
        /// <returns>IServiceResult - ICollection - string</returns>
        public async Task<IServiceResult<ICollection<string>>> GetCategories()
        {
            var result = new ServiceResult<ICollection<string>> { IsSuccess = true };
            var response = await _networkService.GetStringResult($"{JokesAPIBaseUrl}/jokes/categories");

            if (!response.IsSuccess)
            {
                result.IsSuccess = false;
                result.ErrorMessage = response.ErrorMessage;
                return result;
            }
            result.Result = JsonConvert.DeserializeObject<ICollection<string>>(response.Result);            
            return result;
        }
    }
}
