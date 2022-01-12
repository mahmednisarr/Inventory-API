using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace INV.Helper.helper
{
    public static class Maper
    {
        public static void Map(ExpandoObject source, object destination)
        {
            source = source ?? throw new ArgumentNullException(nameof(source));
            destination = destination ?? throw new ArgumentNullException(nameof(destination));

            string normalizeName(string name) => name.ToLowerInvariant();

            IDictionary<string, object> dict = source;
            var type = destination.GetType();

            var setters = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanWrite && p.GetSetMethod() != null)
                .ToDictionary(p => normalizeName(p.Name));

            foreach (var item in dict)
            {
                if (setters.TryGetValue(normalizeName(item.Key), out var setter))
                {
                    var value = setter.PropertyType.ChangeType(item.Value);
                    setter.SetValue(destination, value);
                }
            }

        }



        public static int ReplaceNull(int? value)
        {
            if (value == null)
            {
                return 0;
            }
            else
            {
                return (int)value;
            }
        }
        public static string ReplaceNull(string value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value;
            }
        }

        public static DateTime ReplaceNull(DateTime value)
        {
            if (value == null)
            {
                return DateTime.Now;
            }
            else
            {
                return value;
            }
        }

        public static double ReplaceNull(double value)
        {
            if (value == null)
            {
                return 0.0;
            }
            else
            {
                return value;
            }
        }

        public static List<T> MapList<T>(List<dynamic> source)
        {
            string normalizeName(string name) => name.ToLowerInvariant();
            var obj = new List<T>();
            for (int i = 0; i < source.Count; i++)
            {
                IDictionary<string, object> dict = source[i];
                var data = (T)Activator.CreateInstance(typeof(T));
                Type type = typeof(T);
                var setters = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanWrite && p.GetSetMethod() != null)
                    .ToDictionary(p => normalizeName(p.Name));
                foreach (var item in dict)
                {
                    if (setters.TryGetValue(normalizeName(item.Key), out var setter))
                    {
                        if (setter.PropertyType.Name.ToUpper() == "INT32" && typeof(DBNull) == item.Value.GetType()) {

                            var value = setter.PropertyType.ChangeType(0);
                            setter.SetValue(data, value);
                        }
                        else
                        {
                            var value = setter.PropertyType.ChangeType(item.Value);
                            setter.SetValue(data, value);
                        }
                    }
                }
                obj.Add(data);
            }
            return obj;
        }
    }
}
