using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Application.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Domain.Entities;
using MultiShop.Catalog.Infrastructure.Settings;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Application.Services.ProductDetailServices
{
    public class ProductDetailService : IProductDetailService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<ProductDetail> _productDetailCollection;
        public ProductDetailService(IMapper _mapper,IDatabaseSettings databaseSettings) {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database=client.GetDatabase(databaseSettings.DatabaseName);
            _productDetailCollection=database.GetCollection<ProductDetail>(databaseSettings.ProductDetailCollectionName);
            this._mapper = _mapper; 
        }
        public async Task<Result<string>> CreateProductDetailAsync(CreateProductDetailDto createProductDetailDto)
        {
           var value = _mapper.Map<ProductDetail>(createProductDetailDto);
            await _productDetailCollection.InsertOneAsync(value);
            return "ProductDetail oluşturuldu.";
        }

        public async Task<Result<string>> DeleteProductDetailAsync(string id)
        {
           await _productDetailCollection.DeleteOneAsync(x=>x.ProductDetailId==id);
            return "ProductDetail silindi.";

        }

        public async Task<Result<List<ResultProductDetailDto>>> GetAllProductDetailAsync()
        {
            var values =await  _productDetailCollection.Find(x => true).ToListAsync();
           
            return _mapper.Map<List<ResultProductDetailDto>>(values);


        }

        public async Task<Result<GetByIdProductDetailDto>> GetByIdProductDetailAsync(string id)
        {
            var value = await _productDetailCollection.Find(x => x.ProductDetailId==id).FirstOrDefaultAsync();

            return _mapper.Map<GetByIdProductDetailDto>(value);
        }

        public async Task<Result<GetByIdProductDetailDto>> GetByProductIdProductDetailAsync(string id)
        {
            var value = await _productDetailCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();

            return _mapper.Map<GetByIdProductDetailDto>(value);
        }

        public async Task<Result<string>> UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
        {
            var value=_mapper.Map<ProductDetail>(updateProductDetailDto);
            await _productDetailCollection.FindOneAndReplaceAsync(x => x.ProductDetailId == updateProductDetailDto.ProductDetailID, value);
            return "ProductDetail kaydedildi.";

        }
    }
}
