using MongoDB.Driver;

namespace PLM.CmsService.Data
{ 
    public interface IDbConnection
    {
        IMongoDatabase Connect { get; }
    }
}
