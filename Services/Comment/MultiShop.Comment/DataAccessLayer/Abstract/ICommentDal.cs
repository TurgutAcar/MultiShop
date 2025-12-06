using MultiShop.Comment.Entities;

namespace MultiShop.Comment.DataAccessLayer.Abstract
{
    public interface ICommentDal:IGenericDal<UserComment>
    {
        List<UserComment> CommentListByProductId(string id);
       int GetActiveCommentCount();
       int GetPassiveCommentCount();
       int GetTotalCommentCount();

    }
}
