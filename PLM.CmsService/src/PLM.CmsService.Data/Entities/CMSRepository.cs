using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

//ToDo: 
// A generic repository can be made and reuse in other services.
// Collection name "contents" can be handled with naming convention or as a configuration value.

namespace PLM.CmsService.Data
{
    public class CMSRepository : IRepository<CMSContent>,IDisposable
    {     
        IMongoDatabase contentDatabase;

        public CMSRepository(IDbConnection connection)
        {         
            contentDatabase = connection.Connect;
        }

        public IEnumerable<CMSContent> List
        {
            get
            {               
                var contents = contentDatabase.GetCollection<CMSContent>("contents");                
                var contentsCollection = contents.Find(new BsonDocument()).ToList();
                return contentsCollection;
            }
        }

        public void Add(CMSContent entity)
        {
            contentDatabase.GetCollection<CMSContent>("contents").InsertOne(entity);
            
        }

        public void Delete(CMSContent entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing.....");
        }

        public CMSContent FindById(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(CMSContent entity)
        {
            throw new NotImplementedException();
        }
    }
}
