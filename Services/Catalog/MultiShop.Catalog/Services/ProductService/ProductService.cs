using AutoMapper;
using Elastic.Clients.Elasticsearch;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.settings;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        private readonly ElasticsearchClient _es;

        public ProductService(IMapper mapper, IDatabaseSettings databaseSettings, ElasticsearchClient es)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Product>(databaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
            _es = es;
        }
        public async Task<Result<string>> CreateProductAsync(CreateProductDto createProductDto)
        {
           var value= _mapper.Map<Product>(createProductDto); 
            await _productCollection.InsertOneAsync(value);
            var result=await _es.IndexAsync(value, idx => idx
    .Index("products")
    .Id(value.ProductId ?? Guid.NewGuid().ToString())
);
            if (!result.IsValidResponse)
            {
                Console.WriteLine("ES ERROR:");
                Console.WriteLine(result.DebugInformation);
            }
            else
            {
                Console.WriteLine("ES OK — Kaydedildi");
            }


            return "Product olusturuldu.";
        }

        public async Task<Result<string>> DeleteProductAsync(string id)
        {
            await _productCollection.DeleteOneAsync(x=>x.ProductId==id);
            return "Product silindi.";

        }

        public async Task<Result<List<ResultProductDto>>> GetAllProductAsync()
        {
             var values= await _productCollection.Find(x=>true).ToListAsync();
             return _mapper.Map<List<ResultProductDto>>(values);
        }

        public async Task<Result<GetByIdProductDto>> GetByIdProductAsync(string id)
        {
           var value = await _productCollection.Find<Product>(x=>x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDto>(value);
        }

        public async Task<Result<List<ResultProductsWithCategoryDto>>> GetProductsWithCategoryAsync()
        {
            var values =await _productCollection.Find(x=>true).ToListAsync();
            foreach (var item in values) 
            {
                item.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == item.CategoryId).FirstAsync();
            
            }
            return _mapper.Map<List<ResultProductsWithCategoryDto>>(values);
        }

        public async Task<Result<List<ResultProductsWithCategoryDto>>> GetProductsWithCategoryByCategoryIdAsync(string CategoryId)
        {
            var values=await _productCollection.Find(x=>x.CategoryId == CategoryId).ToListAsync();
            foreach (var item in values)
            {
                item.Category = await _categoryCollection.Find<Category>(x => x.CategoryId == item.CategoryId).FirstAsync();

            }
            return _mapper.Map<List<ResultProductsWithCategoryDto>>(values);
        }

        public async Task<Result<string>> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
           var value = _mapper.Map<Product>(updateProductDto);
            await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDto.ProductId, value);
            return "Product kaydedildi.";

        }
    }
}
