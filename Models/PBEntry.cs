using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PBAPI.Models
{
    public class PBEntry
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string PreferredName { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string PrimaryEmail { get; set; }

        public string PrimaryMobile { get; set; }
        public string Updated { get; set; }
    }

}
