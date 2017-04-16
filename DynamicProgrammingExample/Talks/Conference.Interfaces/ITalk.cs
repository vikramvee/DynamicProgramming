using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Interfaces
{
    public interface ITalk : IItem, IComparable
    {
        string Name { get; set; }       
        bool IsScheduled { get; set; }
        TimeSpan ScheduledTime { get; set; }

    }
}
