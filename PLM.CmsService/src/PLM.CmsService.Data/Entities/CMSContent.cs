using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PLM.CmsService.Data
{
    public class CMSContent: IEntity
    {
        [BsonId]
        public ObjectId id
        {
            get;set;
        }
        public ContentBlock content { get; set; }
    }

    public class ContentBlock
    {
        public int contentId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string text { get; set; }
    }
}
