using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.settings;
using MultiShop.Shared.Responses;

namespace MultiShop.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        public CategoryService(IMapper mapper,IDatabaseSettings _databaseSettings)
        {
            var client =new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }
        public async Task<Result<string>> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var value = _mapper.Map<Category>(createCategoryDto);
            await _categoryCollection.InsertOneAsync(value);
            return "Kategori başarıyla oluşturuldu.";
        }

        public async Task<Result<string>> DeleteCategoryAsync(string id)
        {
            await _categoryCollection.DeleteOneAsync(x=>x.CategoryId == id);
            return "Kategori başarıyla silindi.";

        }

        public async Task<Result<List<ResultCategoryDto>>> GetAllCategoryAsync()
        {
            var values= await _categoryCollection.Find(x=>true).ToListAsync();
            return _mapper.Map<List<ResultCategoryDto>>(values);
        }

        public async Task<Result<GetByIdCategoryDto>> GetByIdCategoryAsync(string id)
        {
            var values = await _categoryCollection.Find<Category>(x=>x.CategoryId==id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdCategoryDto>(values);
        }

        public async Task<Result<string>> UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
        {
            var values = _mapper.Map<Category>(updateCategoryDto);
            await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == updateCategoryDto.CategoryId, values);
            return "Kategori başarıyla kaydedildi.";

        }
    }
}
