using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Utilities.Results
{
    public class ErrorResult : Result
    {
        //başarısız versiyonu yazıldı. (İşlem sonucunun)
        public ErrorResult(string message) : base(false, message)
        {
        }
        public ErrorResult() : base(false)
        {
        }
    }
}
