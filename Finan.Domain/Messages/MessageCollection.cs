using Finan.Domain.Enums;
using System.Collections;

namespace Finan.Domain.Messages
{
    public class MessageCollection : IList<MessageRecord>
    {
        private readonly List<MessageRecord> _items;
        public MessageCollection()
        {
            _items = new List<MessageRecord>();
        }
        public MessageCollection(IEnumerable<MessageRecord>? items)
        {
            _items = items is not null ? new List<MessageRecord>(items) : new List<MessageRecord>();
        }
        public MessageRecord this[int index]
        {
            get => _items[index];
            set => _items[index] = value ?? throw new ArgumentNullException(nameof(value));
        }
        public int Count => _items.Count;
        public bool IsReadOnly => false;
        public void Add(MessageRecord item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _items.Add(item);
        }
        public void Clear() => _items.Clear();
        public bool Contains(MessageRecord item) => item is not null && _items.Contains(item);
        public void CopyTo(MessageRecord[] array, int arrayIndex) => _items.CopyTo(array, arrayIndex);
        public IEnumerator<MessageRecord> GetEnumerator() => _items.GetEnumerator();
        public int IndexOf(MessageRecord item) => item is null ? -1 : _items.IndexOf(item);
        public void Insert(int index, MessageRecord item)
        {
            if (item is null) throw new ArgumentNullException(nameof(item));
            _items.Insert(index, item);
        }
        public bool Remove(MessageRecord item)
        {
            if (item is null) return false;
            return _items.Remove(item);
        }
        public void RemoveAt(int index) => _items.RemoveAt(index);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public bool HasErrors() => _items.Any(m => m.Type == MessageType.Error);
        public void Success(string message) => _items.Add(MessageRecord.Create(MessageType.Success, message));
        public void Error(string message) => _items.Add(MessageRecord.Create(MessageType.Error, message));
        public string[] GetErros() => _items.Where(m => m.Type == MessageType.Error).Select(x => x.Description).ToArray();

    }
}
