

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.Entities;
using AuthService.Models;
using AuthService.Helper;
using MongoDB.Driver;

namespace AuthService.Services
{

    public interface IUserService
    {
        Task<AuthenticateResponse> Authenticate(AuthenticateRequest model);
        Task<User> GetById(string id);
        public Task<User> Validate(HttpContext context);
    }

    public class UserService : IUserService
    {
        IMongoCollection<User> _userCollection;
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
      

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, IOptions<UserDatabaseSettings> usersDatabaseSettings)
        {
            _appSettings = appSettings.Value;


            var mongoClient = new MongoClient(
                usersDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                usersDatabaseSettings.Value.DatabaseName);
            _userCollection = mongoDatabase.GetCollection<User>(
                usersDatabaseSettings.Value.UsersCollectionName);
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model)
        {
            var user =await _userCollection.Find(x => x.Username == model.Username && x.Password == model.Password).FirstOrDefaultAsync();

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }


        public async Task<User> Validate(HttpContext context)
        {
            try
            {
                string token = context.Request.Headers["x-auth-token"];
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = jwtToken.Claims.First(x => x.Type == "id").Value;
                return await GetById(accountId);
            }
            catch (Exception e)
            {
                return null;
             }
        }


        public async Task<User> GetById(string id)
        {
            return await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        // helper methods

        private string generateJwtToken(User user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
      


    }
}
