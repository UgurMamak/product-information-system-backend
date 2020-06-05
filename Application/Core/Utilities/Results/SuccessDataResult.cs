using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Utilities.Results
{
    public class SuccessDataResult<T> : DataResult<T>
    {
        

        // success değerinin başarılı olduğunu bildiğimiz için burada bir daha vermeye gerek yok o yüzden silip true değerini gönderiyoruz.
        // public SuccessDataResult(T data, bool success string message) : base(data, success, message)
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {
        }
        //public SuccessDataResult(T data,bool success) : base(data, success)
        public SuccessDataResult(T data) : base(data, true)
        {
        }
        public SuccessDataResult(string message) : base(default, true, message)
        {
        }
        public SuccessDataResult() : base(default, true)
        {
        }
    }
}
