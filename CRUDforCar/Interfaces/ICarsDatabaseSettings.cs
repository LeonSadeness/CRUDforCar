namespace CRUDforCar.Interfaces
{
    public interface ICarsDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string ItemCollectionName { get; set; }
    }
}
