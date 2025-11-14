using Finan.Domain.Enums;

namespace Finan.Domain.Messages
{
    public sealed record MessageRecord
    {
        public string Key { get; init; }
        public string? Description { get; init; }
        public MessageType Type { get; init; }

        public MessageRecord(MessageType type, string? description = null, string? key = null)
        {
            Type = type;
            Description = NormalizeDescription(description);
            Key = string.IsNullOrWhiteSpace(key) ? Guid.NewGuid().ToString("N") : key!;
        }

        public static MessageRecord Create(MessageType type, string? description = null) =>
            new MessageRecord(type, description);

        public static MessageRecord FromKey(string key, MessageType type, string? description = null) =>
            new MessageRecord(type, description, key);

        public MessageRecord WithDescription(string? description) =>
            this with { Description = NormalizeDescription(description) };

        public MessageRecord WithType(MessageType type) =>
            this with { Type = type };

        private static string? NormalizeDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description)) return null;
            return description.Trim();
        }

        public override string ToString() =>
            $"{Type}: {(Description ?? Key)}";
    }
}
