using AutoMapper;
using MultiShop.Cargo.DtoLayer.CargoCompanyDtos;
using MultiShop.Cargo.DtoLayer.CargoCustomerDtos;
using MultiShop.Cargo.DtoLayer.CargoDetailDtos;
using MultiShop.Cargo.DtoLayer.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;


namespace MultiShop.Cargo.WebApi.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCargoCompanyDto, CargoCompany>();
            CreateMap<UpdateCargoCompanyDto, CargoCompany>();

            CreateMap<CreateCargoCustomerDto, CargoCustomer>();
            CreateMap<UpdateCargoCustomerDto, CargoCustomer>();

            CreateMap<CreateCargoDetailDto, CargoDetail>();
            CreateMap<UpdateCargoDetailDto, CargoDetail>();

            CreateMap<CreateCargoOperationDto, CargoOperation>();
            CreateMap<UpdateCargoOperationDto, CargoOperation>();

        }
    }
}
