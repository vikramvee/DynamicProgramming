using Conference.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Concrete
{
    public class Talk : ITalk
    {
        private int duration;
        private string name;
        private bool scheduled;
        private TimeSpan scheduledTime;
        private string talk;
        private int time;

        public Talk(string name, int time)
        {
            //this.talk = talk;
            this.name = name;
            this.duration = time;
        }

        /// <summary>
        /// This is duaration property of the talk which is analogus to the Value property of the IItem
        /// </summary>
        public int Value
        {
            get
            {
                return duration;
            }

            set
            {
                duration = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public bool IsScheduled
        {
            get
            {
                return scheduled;
            }

            set
            {
                scheduled = value;
            }
        }

        public TimeSpan ScheduledTime
        {
            get
            {
                return scheduledTime;
            }

            set
            {
                scheduledTime = value;
            }
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            Talk talkObj = obj as Talk;
            if(talkObj != null && !string.IsNullOrEmpty(this.Name) && !string.IsNullOrEmpty(talkObj.Name))
            {
                return talkObj.Name.Equals(this.Name);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        public override string ToString()
        {
            return ScheduledTime + " " + Name;
        }
    }
}
