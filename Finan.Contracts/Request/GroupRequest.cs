using Finan.Contracts.Enums;

namespace Finan.Contracts.Request
{
    public class GroupRequest
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public NatureGroup NatureId { get; set; }
    }
}
