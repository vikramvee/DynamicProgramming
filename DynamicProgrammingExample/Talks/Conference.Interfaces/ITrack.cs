using Conference.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Interfaces
{
    public interface ITrack:IDictionary<SessionEnum, ITalkList>
    {
    }
}
