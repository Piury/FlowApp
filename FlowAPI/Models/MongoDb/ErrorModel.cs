using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace FlowApi.Models.MongoDb;
public class ErrorModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
    public Exception Exception { get; set; }
    public string StackTrace { get; set; }

}