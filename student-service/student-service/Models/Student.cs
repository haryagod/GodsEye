using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace student_service.Models

{
    public enum Medium
    { 
    SEMI,
    ENGLISH,
    MARATHI
    }
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string studentName { get; set; } = null!;

        [BsonRequired]
        [BsonElement("Contact")]
        [JsonPropertyName("Contact")]
        public string mobNo { get; set; } = null!;

        [BsonRequired]
        [BsonElement("Medium")]
        [JsonPropertyName("Medium")]
        public Medium medium { get; set; } 

        [BsonRequired]
        [BsonElement("Batch")]
        [JsonPropertyName("Batch")]
        public string batch { get; set; } = null!;


        [BsonRequired]
        [BsonElement("Standards")]
        [JsonPropertyName("Standards")]
        public IEnumerable<StudentStandard> studentStandards { get; set; } = null!;

    }

    public class StudentStandard
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRequired]
        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string standardName { get; set; } = null!;

     
        [BsonElement("IsCurrent")]
        [JsonPropertyName("IsCurrent")]
        public bool isCurrentStandard { get; set; }

        [BsonRequired]
        [BsonElement("SubjectIds")]
        [JsonPropertyName("SubjectsIds")]
        public IEnumerable<string> subjectIds { get; set; } = null!;

        [BsonRequired]
        [BsonElement("FeesPayed")]
        [JsonPropertyName("FeesPayed")]
        public string FeesPayed { get; set; }

        [BsonRequired]
        [BsonElement("FeesTotal")]
        [JsonPropertyName("FeesTotal")]
        public string FeesTotal { get; set; }

    }

    public class StudentDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string StudentsCollectionName { get; set; } = null!;
        public string StandardsCollectionName { get; set; } = null!;
    }
}
