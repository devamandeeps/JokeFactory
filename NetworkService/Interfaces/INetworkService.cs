using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Library.Common;
using Library.Common.Interfaces;

namespace Library.NetworkService.Interfaces
{
    public interface INetworkService
    {
        Task<IServiceResult<string>> GetStringResult(string url);        
    }
}
