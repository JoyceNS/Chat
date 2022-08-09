using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Chat.Models;

namespace Chat.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string MessageTxt { get; set; }
        public DateTime DateSent { get; set; }
    }
}
