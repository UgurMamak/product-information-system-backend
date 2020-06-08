using Application.Bussiness.Abstract;
using Application.Core.Utilities.Results;
using Application.DataAccess.Abstract;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Concrete
{
  public  class ProductImageService:IProductImageService
    {
        private IProductImageDal _productImageDal;
        public ProductImageService(IProductImageDal productImageDal)
        {
            _productImageDal = productImageDal;
        }

        public async Task<IResult> Add(List<string> images, string productId)
        {
            foreach (var item in images)
            {
              await  _productImageDal.Add(new Image { ProductId=productId,ImageName=item});
            }
            return new SuccessResult(Messages.CategoryAdded);    
        }
    }
}
