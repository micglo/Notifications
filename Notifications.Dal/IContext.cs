using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Notifications.Domain;

namespace Notifications.Dal
{
    public interface IContext
    {
        IMongoCollection<Notification> GetCollection();
        IMongoQueryable<Notification> GetCollectionAsQuerable();
    }
}