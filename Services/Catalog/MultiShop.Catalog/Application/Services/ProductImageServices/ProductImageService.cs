using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Application.Dtos.ProductImageDtos;
using MultiShop.Catalog.Domain.Entities;
using MultiShop.Catalog.Infrastructure.Settings;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Application.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<ProductImage> _productImageCollection;

        public ProductImageService(IMapper mapper, IDatabaseSettings databaseSettings) 
        { 
           var client =new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productImageCollection=database.GetCollection<ProductImage>(databaseSettings.ProductImageCollectionName);
            _mapper = mapper;
        }
        public async Task<Result<string>> CreateProductImageAsync(CreateProductImageDto createProductImageDto)
        {
           var value=_mapper.Map<ProductImage>(createProductImageDto);  
            await _productImageCollection.InsertOneAsync(value);
            return "ProductImage oluşturuldu.";
        }

        public async Task<Result<string>> DeleteProductImageAsync(string id)
        {
            await _productImageCollection.DeleteOneAsync(x=>x.ProductImageId==id);
            return "ProductImage silindi.";

        }

        public async Task<Result<List<ResultProductImageDto>>> GetAllProductImageAsync()
        {
            var values =await _productImageCollection.Find(x=>true).ToListAsync();
            return _mapper.Map<List<ResultProductImageDto>>(values);
        }

        public async Task<Result<GetByIdProductImageDto>> GetByIdProductImageAsync(string id)
        {
            var values = await _productImageCollection.Find(x => x.ProductImageId==id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductImageDto>(values);
        }

        public async Task<Result<GetByIdProductImageDto>> GetByProductIdProductImageAsync(string id)
        {
            var values = await _productImageCollection.Find(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductImageDto>(values);
        }

        public async Task<Result<string>> UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
        {
            var value = _mapper.Map<ProductImage>(updateProductImageDto);
            await _productImageCollection.FindOneAndReplaceAsync(x=>x.ProductImageId==updateProductImageDto.ProductImageId, value);
            return "ProductImage kaydedildi.";

        }
    }
}
