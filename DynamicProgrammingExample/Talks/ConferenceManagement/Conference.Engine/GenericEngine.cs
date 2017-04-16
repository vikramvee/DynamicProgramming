using Conference.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conference.Engine
{
    public class GenericEngine<T> : IEngine<T> where T : IItem
    {
        /// <summary>
        /// This engine will return the best solution by creating the sub solutions which is based on the knapsack algorithum. The items returned will have the total
        /// capacity equal to the capacity passed to the solution. The solution will keep creating cache which will be used for further processing and making the algo 
        /// faster.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="capacity">The capacity to be passed to the algo based on which we are getting the best matching items</param>
        /// <param name="items">The items from which we need to get the best suited items based on the capacity.</param>
        /// <returns></returns>
        public List<T> GetTheBestSuitableItem(int capacity, IList<T> items)
        {
            try
            {
                // Create arrays for keeping track of the maximum value for each sub-solution
                // and which items to include, respectively:
                var maxValues = (int[,])Array.CreateInstance(typeof(int), items.Count + 1, capacity + 1);
                var doInclude = (bool[,])Array.CreateInstance(typeof(bool), items.Count + 1, capacity + 1);

                // Notice, that at this point, both arrays are initialized with 0's and false, respectively.

                // Fill out the cache-arrays, starting with the smallest sub-solution first...
                for (var i = 0; i < items.Count; i++)
                {
                    var currentItem = items[i];
                    var row = i + 1;
                    for (var c = 1; c <= capacity; c++)
                    {
                        // What is the maximum possible value for the two cases where the
                        // item is included and excluded from the final solution, respectively?
                        var valueExcluded = maxValues[row - 1, c];
                        var valueIncluded = 0;
                        if (currentItem.Value <= c)
                            valueIncluded = currentItem.Value + maxValues[row - 1, c - currentItem.Value];

                        // Only use the current item if it results in a higher value:
                        if (valueIncluded > valueExcluded)
                        {
                            maxValues[row, c] = valueIncluded;
                            doInclude[row, c] = true;
                        }
                        else
                        {
                            maxValues[row, c] = valueExcluded;
                        }
                    }
                }


                // Find out which items that actually constitutes the above found solution value...
                var chosenItems = new List<T>();
                // Iterate backwards, starting from the last item...
                for (var row = items.Count; row > 0; row--)
                {
                    // If the current item should not be included, just skip it and look at the next one:
                    if (!doInclude[row, capacity])
                        continue;

                    // Otherwise, add it to the list of chosen items:
                    var item = items[row - 1];
                    chosenItems.Add(item);

                    // Now, with that item chosen, the next sub-problem's capacity is correspondingly smaller:
                    capacity -= item.Value;
                }
                return chosenItems;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }
    }
}
