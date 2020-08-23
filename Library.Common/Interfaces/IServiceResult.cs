using System;

namespace Library.Common.Interfaces
{
    public interface IServiceResult<T>
    {
        bool IsSuccess { get; set; }
        string ErrorMessage { get; set; }
        T Result { get; set; }
    }
}
