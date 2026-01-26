using MultiShop.DtoLayer.CommentDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Enums;
using MultiShop.WebUI.Handlers;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;

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

        public async Task<List<ResultCommentDto>> CommentListByProductId(string productId)
        {
            var _httpClient = _factory.Create("Comment");
            var responseMessage = await _httpClient.GetAsync("comments/CommentListByProductId/" + productId);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultCommentDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultCommentDto>();
            //var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultCommentDto>>();
            //return values;
        }

        public async Task<string> CreateCommentAsync(CreateCommentDto createCommentDto)
        {
            var _httpClient = _factory.Create("Comment");

            var response = await _httpClient.PostAsJsonAsync("comments", createCommentDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<string> DeleteCommentAsync(string id)
        {
            var _httpClient = _factory.Create("Comment");

            var response = await _httpClient.DeleteAsync("comments?id=" + id);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }

        public async Task<List<ResultCommentDto>> GetAllCommentAsync()
        {
            var _httpClient = _factory.Create("Comment");

            var responseMessage = await _httpClient.GetAsync("comments");
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<List<ResultCommentDto>>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new List<ResultCommentDto>();
            //var jsonData = await responseMessage.Content.ReadAsStringAsync();
            // var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData);
            // return values;
        }

        public async Task<UpdateCommentDto> GetByIdCommentAsync(string id)
        {
            var _httpClient = _factory.Create("Comment");

            var responseMessage = await _httpClient.GetAsync("comments/" + id);
            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<UpdateCommentDto>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? new UpdateCommentDto();
            //var values = await responseMessage.Content.ReadFromJsonAsync<UpdateCommentDto>();
            //return values;

        }

        public async Task<string> UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            var _httpClient = _factory.Create("Comment");

            var response =  await _httpClient.PutAsJsonAsync("comments", updateCommentDto);
            var jsonData = await response.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<Result<string>>(jsonData);
            return values.HandleUiResult(_uiNotifierService)
       ?? "";
        }
    }
}
