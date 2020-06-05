using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Utilities.Results
{
    public class SuccessResult : Result
    {
        // success değerinin başarılı olduğunu bildiğimiz için true değerini gönderidik.
        public SuccessResult(string message) : base(true, message)
        {

        }
        //mesaj almadan sadece success değeri de döndürülebilir.
        public SuccessResult() : base(true)
        {
        }
    }
}
