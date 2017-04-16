using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Interfaces
{
    public interface IEngine<T> where T : IItem
    {
        List<T> GetTheBestSuitableItem(int capacity, IList<T> items);
    }
}
