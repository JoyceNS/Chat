using Chat.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Chat.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<Message> _messagesCollection;

        public MessageService(
            IOptions<MessageDataBaseSettings> messageDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                messageDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(
                messageDatabaseSettings.Value.DatabaseName);
            _messagesCollection = mongoDatabase.GetCollection<Message>(
                messageDatabaseSettings.Value.MessageCollectionName);
        }

        public async Task<List<Message>> GetAsync() =>
            await _messagesCollection.Find(_ => true).ToListAsync();
        public async Task<Message?> GetAsync(string sender) =>
            await _messagesCollection.Find(x => x.Sender == sender).FirstOrDefaultAsync();

        public async Task<List<Message>> GetMessagesAsync(string sender) =>
            await _messagesCollection.Find(x => x.Sender == sender && x.DateSent >= DateTime.UtcNow.AddDays(-30)).Limit(100).ToListAsync();

        public async Task CreateAsync(Message newMessage) =>
            await _messagesCollection.InsertOneAsync(newMessage);

        public async Task RemoveAsync(string id) =>
            await _messagesCollection.DeleteOneAsync(x => x.Id == id);

        public async Task SendMessage(Message newMessage) =>
            await CreateAsync(newMessage);

        public async Task SendMessage(string senderId, string recipientId, string txt)
        {
            Message newSMS = new()
            {
                Sender = senderId,
                Recipient = recipientId,
                DateSent = DateTime.UtcNow,
                MessageTxt = txt
            };
            await CreateAsync(newSMS);
        }
        public async Task<List<Message>> GetAllRecent() =>
            await _messagesCollection.Find<Message>(x => x.DateSent >= DateTime.Today.AddDays(-30)).Limit(100).ToListAsync();

        public async Task<List<Message>> GetChatRecent(string sender, string recipient) =>
            await _messagesCollection.Find(x => x.Sender == sender && x.Recipient == recipient && x.DateSent >= DateTime.UtcNow.AddDays(-30)).Limit(100).ToListAsync();

    }
}
