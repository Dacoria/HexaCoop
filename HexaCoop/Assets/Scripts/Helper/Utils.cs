using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class Utils
{
    public static void ComponentInject(this MonoBehaviour monoBehaviour)
    {
        var injectableFields = monoBehaviour.GetType()
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.GetCustomAttributes(typeof(ComponentInject), true).Length >= 1)
            .Select(x => x).ToList();

        var injectableProperties = monoBehaviour.GetType()
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(x => x.GetCustomAttributes(typeof(ComponentInject), true).Length >= 1)
            .Select(x => x).ToList();

        foreach (var injectableField in injectableFields)
        {
            var fieldType = injectableField.FieldType;

            var componentInject = (ComponentInject)injectableField.GetCustomAttributes
                (typeof(ComponentInject), false).Single();
            var valueToInject = getValueToInject(fieldType, componentInject, monoBehaviour);
            injectableField.SetValue(monoBehaviour, valueToInject);
        }

        foreach (var injectableProperty in injectableProperties)
        {
            var propertyType = injectableProperty.PropertyType;
            var componentInject = (ComponentInject)injectableProperty.GetCustomAttributes
                (typeof(ComponentInject), false).Single();
            var valueToInject = getValueToInject(propertyType, componentInject, monoBehaviour);
            injectableProperty.SetValue(monoBehaviour, valueToInject);
        }
    }

    private static object getValueToInject(Type type, ComponentInject componentInject, MonoBehaviour monoBehaviour)
    {
        if (type.GetInterfaces().Contains(typeof(IEnumerable)))
        {
            var list = new ArrayList();

            //var list = (IList)Activator.CreateInstance(type);
            Type componentType = null;
            if (type.IsArray)
            {
                componentType = type.GetElementType();
            }
            else
            {
                componentType = type.GetGenericArguments()[0];
            }

            if (componentInject.SearchDirection == SearchDirection.CHILDREN
                || componentInject.SearchDirection == SearchDirection.ALL
                || componentInject.SearchDirection == SearchDirection.DEFAULT)
            {
                var components = monoBehaviour.GetComponentsInChildren(componentType, true);
                foreach (var component in components)
                {
                    list.Add(component);
                }
            }

            if (componentInject.SearchDirection == SearchDirection.PARENT
                || componentInject.SearchDirection == SearchDirection.ALL
                || componentInject.SearchDirection == SearchDirection.DEFAULT)
            {
                var parentComponent = monoBehaviour.GetComponentInParent(componentType);
                if (parentComponent != null)
                {
                    list.Add(parentComponent);
                }
            }

            if (list.Count == 0 && (
                componentInject.Required == Required.DEFAULT
                || componentInject.Required == Required.REQUIRED))
            {
                throw new Exception($"Required component '{type}' on behaviour '{monoBehaviour}' not found");
            }

            if (type.IsArray)
            {
                var array = Array.CreateInstance(componentType, list.Count);
                for (int i = 0; i < list.Count; i++)
                {
                    array.SetValue(list[i], i);
                }
                return array;
            }
            else
            {
                var l = (IList)Activator.CreateInstance(type);
                foreach (var VARIABLE in list)
                {
                    l.Add(VARIABLE);
                }

                return l;
            }
        }
        else
        {
            Component component = null;
            if (componentInject.SearchDirection == SearchDirection.CHILDREN
                || componentInject.SearchDirection == SearchDirection.ALL
                || componentInject.SearchDirection == SearchDirection.DEFAULT)
            {
                component = monoBehaviour.GetComponentInChildren(type, true);
            }

            if (component == null && (componentInject.SearchDirection == SearchDirection.PARENT
                                            || componentInject.SearchDirection == SearchDirection.ALL
                                            || componentInject.SearchDirection == SearchDirection.DEFAULT))
            {
                component = monoBehaviour.GetComponentInParent(type);
            }

            if (component == null && (
                componentInject.Required == Required.DEFAULT
                || componentInject.Required == Required.REQUIRED))
            {
                throw new Exception($"Required component '{type}' on behaviour '{monoBehaviour}' not found");
            }
            return component;
        }
    }

    public static T[] GetComponentsOnlyInChildren<T>(this MonoBehaviour script) where T : class
    {
        List<T> group = new List<T>();

        //collect only if its an interface or a Component
        if (typeof(T).IsInterface
         || typeof(T).IsSubclassOf(typeof(Component))
         || typeof(T) == typeof(Component))
        {
            foreach (Transform child in script.transform)
            {
                group.AddRange(child.GetComponentsInChildren<T>());
            }
        }

        return group.ToArray();
    }

    public static T GetComponentOnlyInDirectChildren<T>(this MonoBehaviour script) where T : class
    {
        return script.GetComponentsOnlyInChildren<T>().FirstOrDefault();
    }

    public static void Destroy(IEnumerable<MonoBehaviour> monos)
    {
        foreach(var mono in monos)
        {
            GameObject.Destroy(mono);
        }
    }

    public static List<T> GetValues<T>() where T : Enum
    {
        var result = new List<T>();
        foreach (T type in Enum.GetValues(typeof(T)))
        {
            result.Add(type);
        }

        return result;
    }

    public static GameObject GetStructureGoFromHex(this Hex hex)
    {
        var props = GetChildGoByName(hex.gameObject, "Props");
        return props.transform.childCount > 0 ? props.transform.GetChild(0).gameObject : null;
    }

    public static GameObject GetChildGoByName(GameObject go, string childname, bool containMatch = false, bool checkAnotherChildLevel = false)
    {
        for (int i = 0; i < go.transform.childCount; i++)
        {
            var child = go.transform.GetChild(i);
            if (child.gameObject.name == childname || (containMatch && child.gameObject.name.Contains(childname)))
            {
                return child.gameObject;
            }

            if(checkAnotherChildLevel)
            {
                var childGoByName = GetChildGoByName(child.gameObject, childname, containMatch, checkAnotherChildLevel: false);
                if (childGoByName != null)
                {
                    return childGoByName;
                }
            }
        }

        return null;
    }

    public static Vector3 DefaultEmptyV3 = new Vector3(-1, -1, -1);
    public static Vector3Int DefaultEmptyV3Int = new Vector3Int(-1, -1, -1);
    public static bool IsDefaultEmptyVector(this Vector3 v3) => v3.x == -1 && v3.y == -1 && v3.z == -1;


    public static GameObject GetStructurePrefabFromRrc(this HexStructureType hexStructure)
    {
        if(hexStructure.HasStructure())
        {
            return Rsc.GoStructureMap.First(x => x.Key == hexStructure.ToString() + "Structure").Value;
        }

        return null;
    }
}