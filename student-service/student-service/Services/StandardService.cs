using System.Threading;
using student_service.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using rabbitMessaging;

namespace student_service.Services;

public class StandardService
{
    private readonly IMongoCollection<Standard> _standardCollection;
    private readonly IMessagePublisher _messagePublisher;
    private readonly IMongoCollection<Student> _studentCollection;
    public StandardService(
            IOptions<StudentDatabaseSettings> studentsDatabaseSettings, IMessagePublisher messagePublisher)
    {

        var mongoClient = new MongoClient(
            studentsDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            studentsDatabaseSettings.Value.DatabaseName);

        _studentCollection = mongoDatabase.GetCollection<Student>(
            studentsDatabaseSettings.Value.StudentsCollectionName);
        _standardCollection = mongoDatabase.GetCollection<Standard>(
                studentsDatabaseSettings.Value.StandardsCollectionName);
        _messagePublisher = messagePublisher;
    }

    private void FilterChapterOnFees(IEnumerable<Subject> subjects ,StudentStandard student)
    {

        decimal feesTotal = Convert.ToDecimal(student.FeesTotal);
        decimal FeesPayed = Convert.ToDecimal(student.FeesPayed);
        if (FeesPayed >= feesTotal)
            return;
        var feesPayedInPercent = (FeesPayed / feesTotal)*100 ;

        foreach (var subject in subjects)
        {
            var totalChapters =subject.Chapters.Count();

            var chapterCount = Math.Ceiling(Convert.ToDecimal(feesPayedInPercent * totalChapters / 100));

            subject.Chapters = subject.Chapters.Take((int)chapterCount);

        }
    
    }

    public async Task<List<Standard>> GetAsync() 
        {
        if (true)
        {
            Student currentStudent = await _studentCollection.Find(x => x.Id == "621217f1ab11c1054898d534").FirstOrDefaultAsync();
            FilterDefinition<Standard> filter = Builders<Standard>.Filter.In(x => x.Id, currentStudent.studentStandards.Select(e => e.Id));
            List<Standard> standards = await _standardCollection.Find(filter).ToListAsync();
            Parallel.ForEach(standards, x =>
            {
                
                    var studentStandard = currentStudent.studentStandards.FirstOrDefault(e => e.Id == x.Id);
                    x.subjects = x.subjects.Where(e => studentStandard.subjectIds.Contains(e.Id));

                FilterChapterOnFees( x.subjects, studentStandard);
            });
            return standards;
        }
        }
    public async Task<Standard?> GetAsync(string id) =>
            await _standardCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Standard newStandard) {
        var isPresent = await _standardCollection.Find(x => x.name.Equals(newStandard.name)).FirstOrDefaultAsync();
        if (isPresent is not null)
            throw new BadHttpRequestException("Duplicate Standard");
        await _standardCollection.InsertOneAsync(newStandard);
        await _messagePublisher.PublishMessageAsync("StandardCreated", newStandard, "");
    }


    public async Task UpdateAsync(string id, Standard updatedStandard)
    {
        await _standardCollection.ReplaceOneAsync(x => x.Id == id, updatedStandard);
        await _messagePublisher.PublishMessageAsync("StandardUpdated", updatedStandard, "");
    }

    public async Task RemoveAsync(string id)
    {
        await _standardCollection.DeleteOneAsync(x => x.Id == id);
        await _messagePublisher.PublishMessageAsync("StandardDeleted", id, "");
    }
    
}
