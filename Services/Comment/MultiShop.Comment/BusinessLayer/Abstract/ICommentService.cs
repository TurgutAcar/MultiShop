using MultiShop.Comment.Entities;
using MultiShop.Shared.Responses;

namespace MultiShop.Comment.BusinessLayer.Abstract
{
    public interface ICommentService: IGenericService<UserComment>
    {
        Result<List<UserComment>> CommentListByProductId(string id);
        Result<int> GetActiveCommentCount();
        Result<int> GetPassiveCommentCount();
        Result<int> GetTotalCommentCount();
    }
}
