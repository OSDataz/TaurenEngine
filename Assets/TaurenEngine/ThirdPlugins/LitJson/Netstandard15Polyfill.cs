#if NETSTANDARD1_5
using System;
using System.Reflection;
namespace LitJson
{
    internal class Netstandard15Polyfill
    {
        internal Type GetInterface(this Type type, string name)
        {
            return type.GetTypeInfo().GetInterface(name); 
        }

        internal bool IsClass(this Type type)
        {
            return type.GetTypeInfo().IsClass;
        }

        internal bool IsEnum(this Type type)
        {
            return type.GetTypeInfo().IsEnum;
        }
    }
}
#endif