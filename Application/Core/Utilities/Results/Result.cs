using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Utilities.Results
{
    public class Result:IResult
    {
        public Result(bool success, string message) : this(success)
        {
            //Success = success;
            Message = message;
        }
        //kullanıcı sadece işlem başarılı değil mi onuda geçmek isteyebilir.
        public Result(bool success)
        {
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }
    }
}
