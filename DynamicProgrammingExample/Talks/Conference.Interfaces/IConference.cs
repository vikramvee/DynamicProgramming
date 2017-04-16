using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Interfaces
{
    public interface IConference
    {
        IList<ITrack> Track { get; }
       
    }
}
