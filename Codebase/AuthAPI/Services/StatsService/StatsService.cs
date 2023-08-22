using cliph.Library;
using cliph.Models.User;
using MongoDB.Bson;
using MongoDB.Driver;

namespace cliph.Services.StatsService;

class StatsService : IStatsService
{
    private readonly IConfiguration _configuration;

    public StatsService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<List<ManagedUser>> GetManagedUsers(ObjectId adminUserId)
    {
        using var db = new Database(
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:ConnectionString"),
            ConfigurationContext.RetrieveSafeConfigurationValue<string>(_configuration, "Database:Name")
        );

        var adminUserFilter = Builders<AdminUser>.Filter.Eq(doc => doc.Id, adminUserId);
        var adminUser = await db.GetCollection<AdminUser>("users").Find(adminUserFilter).FirstOrDefaultAsync();

        if (adminUser == null)
        {
            throw new Exception("Unable to find an administrator account with the provided user id.");
        }

        var usersFilterBuilder = Builders<ManagedUser>.Filter;
        var usersFilter =
            usersFilterBuilder.Eq(doc => doc.AdminApiKey, adminUser.ApiKey!.Value);

        var users = await db.GetCollection<ManagedUser>("users").Find(usersFilter).ToListAsync();

        return users;
    }
}