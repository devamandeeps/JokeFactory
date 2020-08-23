using Library.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common
{
    public class ServiceResult<T> : IServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }
    }
}
