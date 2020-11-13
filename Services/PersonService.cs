using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using PBAPI.Models;

namespace PBAPI.Services
{
    public class PersonService
    {
        private readonly IMongoCollection<PBEntry> _pbentries;

        public PersonService(IPersonStoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _pbentries = database.GetCollection<PBEntry>(settings.PersonCollectionName);
        }

        public List<PBEntry> Get() =>
            _pbentries.Find(pbentry => true).ToList();

        public PBEntry Get(string id) =>
           _pbentries.Find<PBEntry>(person => person.Id == id).FirstOrDefault();

        public PBEntry Create(PBEntry person)
        {
            _pbentries.InsertOne(person);
            return person;
        }
        public void Update(string id, PBEntry personIn) =>
           _pbentries.ReplaceOne(person => person.Id == id, personIn);

        public void Remove(PBEntry personIn) =>
            _pbentries.DeleteOne(person => person.Id == personIn.Id);

        public void Remove(string id) =>
            _pbentries.DeleteOne(PBEntry => PBEntry.Id == id);

    }
}
