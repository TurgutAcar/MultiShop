using MultiShop.DtoLayer.CommentDtos;
using MultiShop.Shared.Responses;

namespace MultiShop.WebUI.Services.CommentServices
{
    public interface ICommentService
    {
        Task<Result<List<ResultCommentDto>>> GetAllCommentAsync();
        Task<Result<string>> CreateCommentAsync(CreateCommentDto createCommentDto);
        Task<Result<string>> UpdateCommentAsync(UpdateCommentDto updateCommentDto);
        Task<Result<string>> DeleteCommentAsync(string id);
        Task<Result<UpdateCommentDto>> GetByIdCommentAsync(string id);
        Task<Result<List<ResultCommentDto>>> CommentListByProductId(string productId);
    }
}
