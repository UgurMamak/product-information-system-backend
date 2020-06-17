using Application.Core.Utilities.Results;
using Application.Entities.Dtos.LikeProduct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Abstract
{
    public interface ILikeProductService
    {
        Task<IResult> Add(LikeProductCreateDto likeProduct);
        
        Task<IDataResult<LikeProductNumberStatusDto>> GetNumberStatus(string productId);//like ve dislike sayıları çekmek için yazdım.
        Task<string> LikeProductExists(LikeProductCreateDto likeProduct);//gelen datanın db olup olmadığına bakılır.
        Task<IResult> Delete(LikeProductCreateDto likeProduct);
        Task<IResult> DeleteProductLike(string productId);

    } 
}
