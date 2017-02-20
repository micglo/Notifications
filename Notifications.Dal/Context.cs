using System.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Notifications.Domain;

namespace Notifications.Dal
{
    public class Context : IContext
    {
        private static MongoClient _client;

        public IMongoCollection<Notification> GetCollection()
            => GetMongoDb().GetCollection<Notification>("notifications");

        public IMongoQueryable<Notification> GetCollectionAsQuerable()
            => GetMongoDb().GetCollection<Notification>("notifications").AsQueryable();

        private static IMongoDatabase GetMongoDb()
            => GetMongoClient().GetDatabase("notificationsDb");

        private static MongoClient GetMongoClient()
            => _client ?? (_client = new MongoClient(ConfigurationManager.AppSettings["MongoConnectionString"]));
    }
}