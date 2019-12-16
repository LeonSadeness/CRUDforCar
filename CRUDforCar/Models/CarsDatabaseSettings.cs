using CRUDforCar.Interfaces;

namespace CRUDforCar.Models
{
    public class CarsDatabaseSettings : ICarsDatabaseSettings
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// Имя базы данных
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Имя коллекции
        /// </summary>
        public string ItemCollectionName { get; set; }
    }
}