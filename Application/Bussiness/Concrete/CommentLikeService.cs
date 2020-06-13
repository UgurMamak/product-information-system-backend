using Application.Bussiness.Abstract;
using Application.Core.Utilities.Results;
using Application.DataAccess.Abstract;
using Application.Entities.Dtos.CommetLike;
using Application.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Bussiness.Concrete
{
    public class CommentLikeService : ICommentLikeServive
    {
        private ICommentLikeDal _commentLikeDal;
        public CommentLikeService(ICommentLikeDal commentLikeDal)
        {
            _commentLikeDal = commentLikeDal;
        }
        public async Task<IResult> Add(CommentLikeCreateDto commentLike)
        {
            await _commentLikeDal.Add(new CommentLike { CommentId=commentLike.CommentId,UserId=commentLike.userId,LikeStatus=commentLike.LikeStatus });
            return new SuccessResult (commentLike.LikeStatus == true ? "Yorumu beğendiniz" : "Yorumu beğenmediniz");
        }

        public async Task<IResult> Delete(CommentLikeCreateDto commentLike)
        {
            await _commentLikeDal.DeleteById(x => x.CommentId == commentLike.CommentId && x.UserId == commentLike.userId);
            return new SuccessResult(Messages.CommentLikeDeleted);
        }

        public async Task<string> LikeExists(CommentLikeCreateDto commentLike)
        {
            //daha önce commente like işlemi yapmış mı diye kontrol etmek için
            var isThere = await _commentLikeDal.GetList(x => x.CommentId == commentLike.CommentId && x.UserId == commentLike.userId);
            //kayıt yok demek direk ekleme işlemi yap
            if (isThere.Count == 0)
            {
                return ("0");
            }
            var likeStatus = await _commentLikeDal.GetList(x => x.CommentId == commentLike.CommentId && x.UserId == commentLike.userId && x.LikeStatus == commentLike.LikeStatus);
            //gelen işlem dbde yoksa güncelleme yap demek
            if (likeStatus.Count == 0)
            {
                return ("1");
            }
            //gelen data dbde varsa kaldırma işlemi yapılacak.
            return "2";
        }

        //CommentLike-----------------------
        public async Task<IDataResult<CommentLikeDto>> GetCommentLike(string commentId)
        {
            return new SuccessDataResult<CommentLikeDto>(await _commentLikeDal.GetCommentLike(commentId));
        }

    }
}
