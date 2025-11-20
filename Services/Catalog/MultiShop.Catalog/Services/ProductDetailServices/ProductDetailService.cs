using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.settings;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Services.ProductDetailServices
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
            var value = await _productDetailCollection.Find<ProductDetail>(x => x.ProductDetailId==id).FirstOrDefaultAsync();

            return _mapper.Map<GetByIdProductDetailDto>(value);
        }

        public async Task<Result<GetByIdProductDetailDto>> GetByProductIdProductDetailAsync(string id)
        {
            var value = await _productDetailCollection.Find<ProductDetail>(x => x.ProductId == id).FirstOrDefaultAsync();

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
