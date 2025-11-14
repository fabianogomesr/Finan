namespace Finan.Domain.Parameters
{
    public class ClassificationCommand
    {
        public int? Id { get; set; }
        public string? Description { get; set; }
        public int GroupId { get; set; }
    }
}
