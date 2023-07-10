using MongoDB.Driver;
using static System.GC;

namespace cliph.Library;

public class Database : IDisposable
{
    private MongoClient _client;
    private IMongoDatabase _database;
    private bool _disposed = false;

    public Database(string connectionString, string databaseName)
    {
        _client = new MongoClient(connectionString);
        _database = _client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    public void Dispose()
    {
        Dispose(true);
        SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;
        if (disposing)
        {
            // Dispose managed resources
            _client = null;
            _database = null;
        }

        // Dispose unmanaged resources

        _disposed = true;
    }

    ~Database()
    {
        Dispose(false);
    }
}