using System;
using System.Net;
using System.Text.RegularExpressions;

namespace SQLSVN
{
    public static class dataTypeExtensions
    {
        public static T ToType<T>(this object input, T defaultValue)
        {
            if (input is null || input.ToString() == string.Empty)
                return defaultValue;

            if (input is T)
            {
                return (T)input;
            }
            else
            {
                try
                {
                    return (T)Convert.ChangeType(input, typeof(T));                    
                }
                catch (InvalidCastException)
                {
                    if (defaultValue != null)
                        return defaultValue;

                    return default(T);
                }
            }
        }
    }
}
