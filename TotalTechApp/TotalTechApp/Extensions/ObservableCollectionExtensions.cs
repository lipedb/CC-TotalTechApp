using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TotalTechApp.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static ObservableCollection<T> AddRange<T>(ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
            return collection;
        }

        public static bool IsNullOrEmpty<T>(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                return true;

            /* If this is a list, use the Count property for efficiency. 
             * The Count property is O(1) while IEnumerable.Count() is O(N). */
            if (enumerable is ICollection<T> collection)
            {
                return collection.Count < 1;
            }
            return !enumerable.Any();
        }
    }
}

