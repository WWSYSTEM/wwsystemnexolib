using System;
using System.Collections.Generic;
using System.Linq;

namespace WWsystemLib
{
    public class EnumExtensions
    {
        public static List<T> GetEnumValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }
    }
}