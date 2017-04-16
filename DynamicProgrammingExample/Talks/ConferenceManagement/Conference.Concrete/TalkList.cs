using Conference.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Conference.Concrete
{
    public class TalkList : ITalkList
    {
        private IList<ITalk> talkList = new List<ITalk>();

        public ITalk this[int index]
        {
            get
            {
                return talkList[index];
            }

            set
            {
                talkList[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return talkList.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return talkList.IsReadOnly;
            }
        }

        public void Add(ITalk item)
        {
            talkList.Add(item);
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ITalk item)
        {
            return talkList.Contains(item);
        }

        public void CopyTo(ITalk[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ITalk> GetEnumerator()
        {
            return talkList.GetEnumerator();
        }

        public int IndexOf(ITalk item)
        {
            return talkList.IndexOf(item);
        }

        public void Insert(int index, ITalk item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(ITalk item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
