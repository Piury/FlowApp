using FlowApi.Models.MongoDb;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace FlowAPI.Services.Errors;
public interface IErrorsServices
{
    Task<List<ErrorModel>> GetAsync();
    Task<ErrorModel?> GetAsync(string id);
    Task CreateAsync(ErrorModel newError);
    Task RemoveAsync(string id);
}
public class ErrorsServices : IErrorsServices
{
    private readonly IMongoCollection<ErrorModel> _ErrorsCollection;

    public ErrorsServices(
        IOptions<ErrorHandlerDbSettings> ErrorstoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            ErrorstoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            ErrorstoreDatabaseSettings.Value.DatabaseName);

        _ErrorsCollection = mongoDatabase.GetCollection<ErrorModel>(
            ErrorstoreDatabaseSettings.Value.CollectionName);
    }

    public async Task<List<ErrorModel>> GetAsync() =>
        await _ErrorsCollection.Find(_ => true).ToListAsync();

    public async Task<ErrorModel?> GetAsync(string id) =>
        await _ErrorsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(ErrorModel newError) =>
        await _ErrorsCollection.InsertOneAsync(newError);

    public async Task RemoveAsync(string id) =>
        await _ErrorsCollection.DeleteOneAsync(x => x.Id == id);
}