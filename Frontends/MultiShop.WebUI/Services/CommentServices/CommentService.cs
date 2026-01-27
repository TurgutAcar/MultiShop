using MultiShop.DtoLayer.CommentDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MultiShop.WebUI.Services.CommentServices
{
    public class CommentService:ICommentService
    {
        private readonly IApiClientFactory _factory;
        private readonly IUiNotifierService _uiNotifierService;

        public CommentService(IApiClientFactory factory, IUiNotifierService uiNotifierService)
        {
            _factory = factory;
            _uiNotifierService = uiNotifierService;
        }

        public async Task<Result<List<ResultCommentDto>>> CommentListByProductId(string productId)
        {
            var _httpClient = _factory.Create("Comment");
            var response = await _httpClient.GetAsync("comments/CommentListByProductId/" + productId);
            return await response.ReadSafeResultAsync<List<ResultCommentDto>>();

        }

        public async Task<Result<string>> CreateCommentAsync(CreateCommentDto createCommentDto)
        {
            var _httpClient = _factory.Create("Comment");

            var response = await _httpClient.PostAsJsonAsync("comments", createCommentDto);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<string>> DeleteCommentAsync(string id)
        {
            var _httpClient = _factory.Create("Comment");

            var response = await _httpClient.DeleteAsync("comments?id=" + id);
            return await response.ReadSafeResultAsync<string>();

        }

        public async Task<Result<List<ResultCommentDto>>> GetAllCommentAsync()
        {
            var _httpClient = _factory.Create("Comment");

            var response = await _httpClient.GetAsync("comments");
            return await response.ReadSafeResultAsync<List<ResultCommentDto>>();

        }

        public async Task<Result<UpdateCommentDto>> GetByIdCommentAsync(string id)
        {
            var _httpClient = _factory.Create("Comment");

            var response = await _httpClient.GetAsync("comments/" + id);
            return await response.ReadSafeResultAsync<UpdateCommentDto>();

        }

        public async Task<Result<string>> UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            var _httpClient = _factory.Create("Comment");

            var response =  await _httpClient.PutAsJsonAsync("comments", updateCommentDto);
            return await response.ReadSafeResultAsync<string>();

        }
    }
}
