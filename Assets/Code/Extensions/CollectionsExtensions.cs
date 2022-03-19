using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
        public static T GetSorted<T>(this List<T> collection, Comparison<T> comparison)
        {
            T sorted = collection[0];
            for (int i = 1; i < collection.Count; i++)
            {
                if (comparison(sorted, collection[i]) == -1)
                {
                    sorted = collection[i];
                }
            }
            
            return sorted;
        }

        public class CircularListEnumerator<T> : IEnumerator<T> where T : class
        {
            private List<T> collection;
            private int _position;
            private T previousItem;
            
            public CircularListEnumerator(List<T> collection)
            {
                this.collection = collection;
                Reset();
            }
            
            public bool MoveNext()
            {
                if (collection.Count == 0) return false;

                _position %= collection.Count;
                
                // Check item removal
                if (collection[_position] == previousItem)
                {
                    _position = (_position + 1) % collection.Count;
                }

                previousItem = collection[_position];
                return true;
            }

            public void Reset()
            {
                previousItem = null;
                _position = 0;
            }

            public T Current => collection[_position];

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                Reset();
                collection = null;
            }
        }
    }
}