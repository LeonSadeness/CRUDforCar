using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDforCar.Models
{
    [BsonIgnoreExtraElements]
    public class Car
    {
        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        [Key]
        [Column("Id")]
        [BsonElement("Id")]
        [JsonProperty("Id")]
        public string CarId { get; set; }

        [Column("Name")]
        [BsonElement("Name")]
        [JsonProperty("Name")]
        public string CarName { get; set; }

        public string Description { get; set; }
    }
}