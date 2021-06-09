using MongoDB.Driver;

namespace SnackisAPI.Dal
{
    public interface IDAL
    {
        MongoClient GetClient(string mongoSettingsName);
    }
}