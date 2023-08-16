using cliph.Models.User;
using MongoDB.Bson;

namespace cliph.Services.StatsService;

public interface IStatsService
{
    public Task<List<ManagedUser>> GetManagedUsers(ObjectId adminUserId);
}