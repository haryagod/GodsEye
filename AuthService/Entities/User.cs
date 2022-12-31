namespace AuthService.Entities
{
    using System.Text.Json.Serialization;

    public class User
    {
        public String Id { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
    public class UserDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string UsersCollectionName { get; set; } = null!;
       
    }
}