using MongoDB.Bson;


namespace PLM.CmsService.Data
{
    public interface IEntity
    { 
         ObjectId id { get; set; }
    }
}