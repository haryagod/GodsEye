using AuthService.Dto;
using AuthService.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Newtonsoft.Json;
using rabbitMessaging;

namespace AuthService.Handler
{
    public class UserHandler : IHostedService, IMessageHandlerCallback
    {
        IMessageHandler _messageHandler;
        IMongoCollection<User> _userCollection;

        public UserHandler(IMessageHandler messageHandler, IOptions<UserDatabaseSettings> usersDatabaseSettings)
        {
            _messageHandler = messageHandler;
            var mongoClient = new MongoClient(
              usersDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                usersDatabaseSettings.Value.DatabaseName);
            _userCollection = mongoDatabase.GetCollection<User>(
                usersDatabaseSettings.Value.UsersCollectionName);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _messageHandler.Start(this);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _messageHandler.Stop();
            return Task.CompletedTask;
        }

        public async Task<bool> HandleMessageAsync(string messageType, string message)
        {
            switch (messageType)
            {
                case "StudentCreated":
                case "StudentUpdated":
                    StudentDto student =JsonConvert.DeserializeObject<StudentDto>(messageType);
                    User user = new User();
                    user.Id = student.Id;
                    user.Username = student.studentName;
                    user.IsAdmin = false;
                    if (messageType == "StudentUpdated")
                    {
                       await UpdateUser(user);
                        break;
                    }
                   await CreateUser(user);
                        break;
            }

            return false;
        }

        public async Task CreateUser(User user)
        { 
            await _userCollection.InsertOneAsync(user);
        }
        public async Task UpdateUser(User user)
        {
            await _userCollection.ReplaceOneAsync(x => x.Id == user.Id, user);
        }
    }
    
}
