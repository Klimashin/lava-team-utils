using System;
using System.Collections.Generic;
using UnityEngine;

namespace LavaUtils
{
    public static class ExtensionMethods
    {
        public static string GetPath(this Transform current) 
        {
            if (current.parent == null)
            {
                return "/" + current.name;
            }

            return current.parent.GetPath() + "/" + current.name;
        }
        
        private static System.Random rng = new System.Random();
        public static void Shuffle<T>(this IList<T> list)  
        {  
            var n = list.Count;  
            while (n > 1) {  
                n--;  
                var k = rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }
        
        public static IEnumerable<T> GetUniqueFlags<T>(this T flags)
            where T : Enum    // New constraint for C# 7.3
        {
            foreach (Enum value in Enum.GetValues(flags.GetType()))
                if (flags.HasFlag(value))
                    yield return (T)value;
        }
        
        public static Vector3 GetRandomPoint(this Collider2D col, int maxAttempts = 100)
        {
            Vector3 randomPoint;
            var attemptsDone = 0;
            var minBound = col.bounds.min;
            var maxBound = col.bounds.max;

            do {
                randomPoint =
                    new Vector3(
                        UnityEngine.Random.Range(minBound.x, maxBound.x),
                        UnityEngine.Random.Range(minBound.y, maxBound.y),
                        0f
                    );
                attemptsDone ++;

                if (attemptsDone > maxAttempts)
                {
                    throw new InvalidOperationException("Max attempts reached: " + attemptsDone);
                }

            } while(!col.OverlapPoint(randomPoint));

            return randomPoint;
        }
    }
}
