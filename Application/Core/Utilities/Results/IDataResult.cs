using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Core.Utilities.Results
{
   public interface IDataResult<out T> : IResult
    {  
        //IResulttaki proplar var ama interface olduğu için implement etmedik.
        T Data { get; }
    }
}
