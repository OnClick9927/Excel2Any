using System;
using System.Collections.Generic;
using System.Data;

namespace Excel2Other
{
    public class FieldTypeUtil
    {
        private static Dictionary<string, Type> fieldTypeDict = new Dictionary<string, Type>
        {
            { "string",typeof(string) },
            { "byte", typeof(byte)},
            { "int", typeof(int) },
            { "long", typeof(long) },
            { "float", typeof(float) },
            { "double", typeof(double) },

            { "bool", typeof(bool)},
            { "boolean", typeof(bool) },

            { "obj", typeof(object) },
            { "object", typeof(object) },

            { "datetime", typeof(DateTime) }
        };

        private static Dictionary<Type, string> sqliteTypeDict = new Dictionary<Type, string>
        {
            { typeof(string), "text" },
            { typeof(byte), "integer" },
            { typeof(int), "integer" },
            { typeof(long), "integer" },
            { typeof(float), "real" },
            { typeof(double), "real" },
            { typeof(DateTime), "integer" },
            { typeof(object), "text" },
            { typeof(bool), "integer" }

        };

        private static Dictionary<Type, string> TypeNameDict = new Dictionary<Type, string>
        {
            { typeof(string), "string" },
            { typeof(byte), "byte" },
            { typeof(int), "int" },
            { typeof(long), "long" },
            { typeof(float), "float" },
            { typeof(double), "double" },
            { typeof(DateTime), "DateTime" },
            { typeof(object), "object" },
            { typeof(bool), "bool" }

        };


        public static string GetTypeName(Type type)
        {
            if (TypeNameDict.ContainsKey(type))
            {
                return TypeNameDict[type];
            }
            else
            {
                return "object";
            }
        }

        public static string GetTypeName(string fieldType)
        {
            return GetTypeName(GetType(fieldType));
        }

        public static string GetSqliteType(Type type)
        {
            if (sqliteTypeDict.ContainsKey(type))
            {
                return sqliteTypeDict[type];
            }
            else
            {
                return "text";
            }
        }

        public static Type GetType(string fieldType)
        {
            if (fieldTypeDict.ContainsKey(fieldType))
            {
                return fieldTypeDict[fieldType];
            }
            else
            {
                return typeof(object);
            }
        }

        public static string GetSqliteType(string fieldType)
        {
            return GetSqliteType(GetType(fieldType));
        }
    }
}
