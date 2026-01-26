using MultiShop.DtoLayer.CommentDtos;

namespace MultiShop.WebUI.Services.CommentServices
{
    public interface ICommentService
    {
        Task<List<ResultCommentDto>> GetAllCommentAsync();
        Task<string> CreateCommentAsync(CreateCommentDto createCommentDto);
        Task<string> UpdateCommentAsync(UpdateCommentDto updateCommentDto);
        Task<string> DeleteCommentAsync(string id);
        Task<UpdateCommentDto> GetByIdCommentAsync(string id);
        Task<List<ResultCommentDto>> CommentListByProductId(string productId);
    }
}
