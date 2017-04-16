using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Interfaces
{
    public interface IConferenceCreator
    {
        IList<ITalk> CreateTalksFromInput(IList<string> talks);
        ITalkList ScheduleTalksForEachTrack(int sessionDuration, IList<ITalk> talks);              
    }
}
