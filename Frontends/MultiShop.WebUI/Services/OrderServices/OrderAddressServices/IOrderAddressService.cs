using MultiShop.DtoLayer.OrderDtos.OrderAddressDtos;

namespace MultiShop.WebUI.Services.OrderServices.OrderAddressServices
{
    public interface IOrderAddressService
    {
       // public Task<List<ResultAboutDto>> AboutListAsync();
        public Task<string> CreateOrderAddressesAsync(CreateOrderAddressDto createOrderAddressDto);
        //public Task UpdateAboutAsync(UpdateAboutDto updateAboutDto);
      //  public Task DeleteAboutAsync(string id);
      //  public Task<UpdateAboutDto> GetByIdAboutAsync(string id);
    }
}
