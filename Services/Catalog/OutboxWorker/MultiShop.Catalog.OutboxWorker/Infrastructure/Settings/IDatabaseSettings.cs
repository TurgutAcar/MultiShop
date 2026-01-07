
namespace MultiShop.Catalog.OutboxWorker.Infrastructure.Settings
{
    public interface IDatabaseSettings
    {
      
        public string OutboxMessageCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }



    }
}
