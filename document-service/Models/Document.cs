using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace document_service.Models
{
    public class Document
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string documentName { get; set; } = null!;

 
        [BsonElement("Key")]
        [JsonPropertyName("Key")]
        public string documentKey { get; set; } = null!;

        [BsonId]
        [BsonElement("Folder")]
        [JsonPropertyName("Folder")]
        public string subjectId { get; set; } = null!;



    }
}
