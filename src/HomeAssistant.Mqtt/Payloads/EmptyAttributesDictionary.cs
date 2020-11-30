using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HomeAssistant.Mqtt.Payloads
{
    /// <summary>
    /// Empty <see cref="IDictionary{string, string}"/>, to avoid
    /// allocations for this commonly-used item.
    /// Use <see cref="Instance"/> to get an instance of it.
    /// </summary>
    internal sealed class EmptyAttributesDictionary : IDictionary<string, string>
    {
        private EmptyAttributesDictionary() { }
        public static EmptyAttributesDictionary Instance = new EmptyAttributesDictionary();
#pragma warning disable RCS1079 // Throwing of new NotImplementedException.

        public ICollection<string> Keys => throw new NotImplementedException();
        public ICollection<string> Values => throw new NotImplementedException();
        public int Count => 0;
        public bool IsReadOnly => true;
        public string this[string key] { get => throw new KeyNotFoundException(); set => throw new NotImplementedException(); }

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator()
            => Enumerable.Empty<KeyValuePair<string, string>>().GetEnumerator();

        public void Add(string key, string value) => throw new NotImplementedException();

        public bool ContainsKey(string key) => false;

        public bool Remove(string key) => false;

        public bool TryGetValue(string key, out string value)
        {
            value = null;
            return false;
        }

        public void Add(KeyValuePair<string, string> item) => throw new NotImplementedException();

        public void Clear() => throw new NotImplementedException();

        public bool Contains(KeyValuePair<string, string> item) => false;

        public void CopyTo(KeyValuePair<string, string>[] array, int arrayIndex) => throw new NotImplementedException();

        public bool Remove(KeyValuePair<string, string> item) => false;

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
#pragma warning restore RCS1079 // Throwing of new NotImplementedException.
    }
}
