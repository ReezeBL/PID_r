using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace PID_r.Helpers
{
    internal static class CombHelper
    {
        public static IList<IList<T>> GetAllCombinations<T>(IList<T> collection)
        {
            var combinations = new List<IList<T>>();

            void InternalCombinations(IList<T> remaining, IList<T> current = null)
            {
                if (current == null)
                    current = new List<T>();
                if (remaining.Count == 0)
                {
                    combinations.Add(current);
                    return;
                }
                for (var i = 0; i < remaining.Count; i++)
                {
                    var newRemaining = new List<T>(remaining);
                    newRemaining.RemoveAt(i);
                    var newCurrent = new List<T>(current) { remaining[i] };
                    InternalCombinations(newRemaining, newCurrent);
                }
            }

            InternalCombinations(collection);
            return combinations;
        }

        public static bool NextPermuation<T>(List<T> collection) where T : IComparable<T>
        {
            if (collection.Count == 0)
                return false;
            var i = collection.Count;
            if (--i == 0)
                return false;
            while (true)
            {
                var i1 = i;
                if (collection[--i].CompareTo(collection[i1]) == -1)
                {
                    var i2 = collection.Count;
                    while (collection[i].CompareTo(collection[--i2]) == 1) { }
                    collection.Swap(i, i2);
                    collection.Reverse(i1, collection.Count - i1);
                    return true;
                }
                if (i == 0)
                {
                    collection.Reverse();
                    return false;
                }
            }
        }

        public static bool NextMultyPermutation<T>(IEnumerable<List<T>> collections) where T: IComparable<T>
        {
            return collections.Any(NextPermuation);
        }

        public static void Swap<T>(this IList<T> list, int firstIndex, int secondIndex)
        {
            Contract.Requires(list != null);
            Contract.Requires(firstIndex >= 0 && firstIndex < list.Count);
            Contract.Requires(secondIndex >= 0 && secondIndex < list.Count);

            if (firstIndex == secondIndex)
            {
                return;
            }
            T temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }
        
    }
}
