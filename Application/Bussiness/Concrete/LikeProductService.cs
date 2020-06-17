using Application.Bussiness.Abstract;
using Application.Core.Utilities.Results;
using Application.DataAccess.Abstract;
using Application.Entities.Dtos.LikeProduct;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Entities.Entity;

namespace Application.Bussiness.Concrete
{
    public class LikeProductService:ILikeProductService
    {
        private ILikeProductDal _likeProductDal;
        public LikeProductService(ILikeProductDal likeProductDal)
        {
            _likeProductDal = likeProductDal;
        }

        public async Task<IResult> Add(LikeProductCreateDto likeProduct)
        {
            var lproduct = new LikeProduct
            {
                ProductId = likeProduct.ProductId,
                UserId = likeProduct.UserId,
                LikeStatus = likeProduct.LikeStatus
            };
           await _likeProductDal.Add(lproduct);
            return new SuccessResult(Messages.LikeProductAdded);
        }

        public async Task<IResult> Delete(LikeProductCreateDto likeProduct)
        {
           await _likeProductDal.DeleteById(w => w.ProductId == likeProduct.ProductId && w.UserId == likeProduct.UserId);
            return new SuccessResult();
        }

        public async Task<IResult> DeleteProductLike(string productId)
        {
            await _likeProductDal.DeleteById(w => w.ProductId == productId);
            return new SuccessResult();
        }

        public async Task<IDataResult<LikeProductNumberStatusDto>> GetNumberStatus(string productId)
        {
            return new SuccessDataResult<LikeProductNumberStatusDto>(await _likeProductDal.GetNumberStatus(productId));
        }

        public async Task<string> LikeProductExists(LikeProductCreateDto likeProduct)
        {
            //sonuc==0 ise yeni gelen datayı ekle
            //sonuc==1 ise güncelleme yap.(eski datayı sil yenisini ekle)
            //sonuc==2 ise hiçbirşey yapma
            var sonuc = "";
            //postId ve userId göndererek daha önce işlem yapılıp yaplmadığını döndüm.

            var isThere =await _likeProductDal.GetList(w => w.ProductId == likeProduct.ProductId && w.UserId == likeProduct.UserId);
            //kayıt yoksa yeni gelen değeri ekle
            if (isThere.Count == 0)
            {
                sonuc = "0";
                return sonuc;
            }
            //postId userId ve likestatu durular gönderilir.
            var likestatu =await _likeProductDal.GetList(w => w.ProductId == likeProduct.ProductId && w.UserId == likeProduct.UserId && w.LikeStatus == likeProduct.LikeStatus);
            //eğer gelen data db yoksa güncelleme yap
            if (likestatu.Count == 0)
            {
                sonuc = "1";
                return sonuc;
            }
            //eğer ikisine de girmezse hiç birişlem yapma diyeceğiz
            sonuc = "2";
            return sonuc;
        }
    }
}
