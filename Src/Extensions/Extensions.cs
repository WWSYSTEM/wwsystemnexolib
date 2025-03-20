using System.Collections;
using System.Collections.Generic;

namespace WWsystemLib
{
    public static class Extensions
    {
  
        public static bool IsEmpty<T>(this ICollection<T> collection)
        {
            return collection.Count == 0;
        }

        public static bool IsEmpty(this string str)
        {
            return str.Length == 0;
        }
        
     
    }
}