using Conference.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using Conference.Utility;

namespace Conference.Concrete
{
    public class Track : ITrack
    {
        private IDictionary<SessionEnum, ITalkList> local = new Dictionary<SessionEnum, ITalkList>();

        public ITalkList this[SessionEnum key]
        {
            get
            {
                return local[key];
            }

            set
            {
                local[key] = value;
            }
        }

        public int Count
        {
            get
            {
                return local.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return local.IsReadOnly;
            }
        }

        public ICollection<Conference.Utility.SessionEnum> Keys
        {
            get
            {
                return local.Keys;
            }
        }

        public ICollection<ITalkList> Values
        {
            get
            {
                return local.Values;
            }
        }

        public void Add(KeyValuePair<Conference.Utility.SessionEnum, ITalkList> item)
        {
            local.Add(item);
        }

        public void Add(Conference.Utility.SessionEnum key, ITalkList value)
        {
            local.Add(key, value);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<Conference.Utility.SessionEnum, ITalkList> item)
        {
            return local.Contains(item);
        }

        public bool ContainsKey(Conference.Utility.SessionEnum key)
        {
            return local.ContainsKey(key);
        }

        public void CopyTo(KeyValuePair<Conference.Utility.SessionEnum, ITalkList>[] array, int arrayIndex)
        {
            local.CopyTo(array, 0);
        }

        public IEnumerator<KeyValuePair<Conference.Utility.SessionEnum, ITalkList>> GetEnumerator()
        {
            return local.GetEnumerator();
        }

        public bool Remove(KeyValuePair<Conference.Utility.SessionEnum, ITalkList> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(Conference.Utility.SessionEnum key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(Conference.Utility.SessionEnum key, out ITalkList value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
