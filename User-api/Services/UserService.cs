using Microsoft.Extensions.Options;
using MongoDB.Driver;
using UserApi.Api.Models;

namespace UserApi.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _users = database.GetCollection<User>("Users");
        }

        public List<User> Get() => _users.Find(u => true).ToList();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }
    }
}
