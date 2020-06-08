using Application.Core.Utilities.Results;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Abstract
{
    public interface IProductImageService
    {
        Task<IResult> Add(List<string>images,string productId);
    }
}
