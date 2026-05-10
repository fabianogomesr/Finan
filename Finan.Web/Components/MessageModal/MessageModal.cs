namespace Finan.Web.Components.MessageModal
{
    public class MessageModalModel
    {
        public string Id { get; set; } = "messageModal";
        public string Title { get; set; } = "Mensagem";
        public List<string> Messages { get; set; } = new();
        public string Type { get; set; } = "info"; // success | warning | danger | info
    }
}
