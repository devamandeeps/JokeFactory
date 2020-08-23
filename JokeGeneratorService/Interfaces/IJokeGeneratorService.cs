using JokeApi.Models;
using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JokeApi.Interfaces
{
    public interface IJokeGeneratorService
    {
        Task<IServiceResult<string>> GetRandomJoke();
        Task<IServiceResult<string>> GetJokeByCategory(string category);
        Task<IServiceResult<ICollection<string>>> GetCategories();
        Task<IServiceResult<RandomPerson>> GetRandomPerson();
    }
}
