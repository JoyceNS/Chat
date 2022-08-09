namespace Chat.Models
{
    public record SendMessageRequest(string sender, string recipient, string txt);
}
