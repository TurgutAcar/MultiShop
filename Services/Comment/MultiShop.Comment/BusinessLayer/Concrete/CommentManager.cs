using MultiShop.Comment.BusinessLayer.Abstract;
using MultiShop.Comment.DataAccessLayer.Abstract;
using MultiShop.Comment.Entities;
using MultiShop.Shared.Responses;

namespace MultiShop.Comment.BusinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            this.commentDal = commentDal;
        }

        public Result<List<UserComment>> CommentListByProductId(string id)
        {
           return commentDal.CommentListByProductId(id);
        }

        public Result<int> GetActiveCommentCount()
        {
            return commentDal.GetActiveCommentCount();
        }

        public Result<int> GetPassiveCommentCount()
        {
            return commentDal.GetPassiveCommentCount();
        }

        public Result<int> GetTotalCommentCount()
        {
            return commentDal.GetTotalCommentCount();
         }

        public Result<string> TDelete(int id)
        {
            commentDal.Delete(id);
            return "Yorum Silindi";
        }

        public Result<List<UserComment>> TGetAll()
        {
           return commentDal.GetAll();

        }

        public Result<UserComment> TGetById(int id)
        {
            return commentDal.GetById(id);
        }

        public Result<string> TInsert(UserComment entity)
        {
            commentDal.Insert(entity);
            return "Yorum oluşturuldu";
        }

        public Result<string> TUpdate(UserComment entity)
        {
            commentDal.Update(entity);
            return "Yorum kaydedildi";
        }
    }
}
