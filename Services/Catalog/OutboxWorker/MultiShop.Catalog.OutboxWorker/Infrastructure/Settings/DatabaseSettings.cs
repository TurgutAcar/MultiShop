namespace MultiShop.Catalog.OutboxWorker.Infrastructure.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string OutboxMessageCollectionName {  get; set; }    

    }
}
