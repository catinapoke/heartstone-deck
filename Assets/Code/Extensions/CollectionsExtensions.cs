using System.Collections.Generic;

namespace Extensions
{
    public static class CollectionsExtensions
    {
        public static T GetRandom<T>(this ICollection<T> collection)
        {
            int index = UnityEngine.Random.Range(0, collection.Count);
            int current = 0;
            foreach (var item in collection)
            {
                if (current++ == index) return item;
            }

            return default;
        }
        
        public static T GetRandom<T>(this T[] collection)
        {
            int index = UnityEngine.Random.Range(0, collection.Length);
            return collection[index];
        }
    }
}