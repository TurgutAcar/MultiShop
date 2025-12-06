using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.DataAccessLayer.Abstract;
using MultiShop.Comment.DataAccessLayer.Context;
using MultiShop.Comment.DataAccessLayer.Repositories;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.DataAccessLayer.EntityFramework
{
    public class EfCommentDal : GenericRepository<UserComment, CommentContext>, ICommentDal
    {
        private readonly CommentContext _commentContext;

        public EfCommentDal(CommentContext context) : base(context)
        {
            _commentContext = context;
        }

        public List<UserComment> CommentListByProductId(string id)
        {
            var list = _commentContext.UserComments.Where(x => x.ProductId == id).ToList();
            return list;
        }

        public int GetActiveCommentCount()
        {
            int value = _commentContext.UserComments.Where(x => x.Status == true).Count();
            return value;   
        }

        public int GetPassiveCommentCount()
        {
            int value = _commentContext.UserComments.Where(x => x.Status == false).Count();
            return value;
        }

        public int GetTotalCommentCount()
        {
            int value = _commentContext.UserComments.Count();
            return value;
        }
    }
}
