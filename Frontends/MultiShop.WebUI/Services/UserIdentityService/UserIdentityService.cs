using MultiShop.DtoLayer.IdentityDtos.UserDtos;
using MultiShop.Shared.Responses;
using MultiShop.WebUI.Extensions;
using MultiShop.WebUI.Services.NotifierServices;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MultiShop.WebUI.Services.UserIdentityService
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IUiNotifierService _uiNotifierService;

        public UserIdentityService(HttpClient httpClient, IUiNotifierService uiNotifierService)
        {
            this._httpClient = httpClient;
            _uiNotifierService = uiNotifierService;
        }

        public async Task<Result<List<ResultUserDto>>> GetAllUserListAsync()
        {
            var response = await _httpClient.GetAsync("/api/users/GetAllUserList");
            return await response.ReadSafeResultAsync<List<ResultUserDto>>();

        }
    }
}
