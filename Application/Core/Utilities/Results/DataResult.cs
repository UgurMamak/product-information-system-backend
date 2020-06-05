using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Utilities.Results
{
   public class DataResult<T> : Result, IDataResult<T>
    {
        //Resulttan farkı bir de data göndermesi
        //base dediği burada Result sınıfı oluyo
        public DataResult(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }
        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }
        public T Data { get; }
    }
}
