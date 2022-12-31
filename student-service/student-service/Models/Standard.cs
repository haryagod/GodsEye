using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace student_service.Models
{

    public class Standard
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string? name { get; set; }

        [BsonElement("Subjects")]
        [JsonPropertyName("Subjects")]
        public IEnumerable<Subject> subjects { get; set; } = null!;
    }
    public class Subject
    {
        public Subject()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonRequired]
        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string subjectName { get; set; } = null!;

        [BsonElement("Description")]
        [JsonPropertyName("Description")]
        public string? subjectDescription { get; set; }

        [BsonElement("Fee")]
        [JsonPropertyName("Fee")]
        public int Fee { get; set; }
        public IEnumerable<Chapter> Chapters { get; set;} = null!;

    }

    public class Chapter
    {
        public Chapter()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("ChapterNo")]
        [JsonPropertyName("ChapterNo")]
        public int chapterSerialNo { get; set; }
        
        [BsonRequired]
        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string subjectName { get; set; } = null!;

        [BsonElement("Description")]
        [JsonPropertyName("Description")]
        public string? chapterDescription { get; set; }
    }

}
