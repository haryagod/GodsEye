using student_service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using rabbitMessaging;

namespace student_service.Services;

public class StudentsService
{

    private readonly IMongoCollection<Student> _studentCollection;
    private readonly IMessagePublisher _messagePublisher;
    
    public StudentsService(
        IOptions<StudentDatabaseSettings> studentsDatabaseSettings,IMessagePublisher messagePublisher)
    {

        var mongoClient = new MongoClient(
            studentsDatabaseSettings.Value.ConnectionString);
        var mongoDatabase = mongoClient.GetDatabase(
            studentsDatabaseSettings.Value.DatabaseName);

        _studentCollection = mongoDatabase.GetCollection<Student>(
            studentsDatabaseSettings.Value.StudentsCollectionName);
        _messagePublisher = messagePublisher;
    }

    public async Task<List<Student>> GetAsync() =>
        await _studentCollection.Find(_ => true).ToListAsync();

    public async Task<Student?> GetAsync(string id) =>
        await _studentCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Student newStudent){
        await _studentCollection.InsertOneAsync(newStudent);
        await _messagePublisher.PublishMessageAsync("StudentCreated", newStudent, "");
        }

    public async Task UpdateAsync(string id, Student updatedStudent)
    {
        await _studentCollection.ReplaceOneAsync(x => x.Id == id, updatedStudent);
        await _messagePublisher.PublishMessageAsync("StudentUpdated", updatedStudent, "");
    }

    public async Task RemoveAsync(string id)
    {
        await _studentCollection.DeleteOneAsync(x => x.Id == id);
        await _messagePublisher.PublishMessageAsync("StudentDeleted",id, "");
    }
}