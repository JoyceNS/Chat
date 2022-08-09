namespace Chat.Models
{
    public class MessageDataBaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string MessageCollectionName { get; set; } = null!; 

    }
}
