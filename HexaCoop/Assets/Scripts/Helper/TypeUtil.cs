using System;
using System.Collections.Generic;
using System.Linq;

public static class TypeUtil
{
    public static IEnumerable<Type> GetTypes(System.Func<System.Type, bool> predicate)
    {
        foreach (var assemb in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var tp in assemb.GetTypes())
            {
                if (predicate == null || predicate(tp)) yield return tp;
            }
        }
    }

    public static IEnumerable<Type> GetTypesAssignableFrom(System.Type rootType)
    {
        foreach (var assemb in System.AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (var tp in assemb.GetTypes())
            {
                if (rootType.IsAssignableFrom(tp)) yield return tp;
            }
        }
    }
}