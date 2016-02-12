﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Funky;

namespace Reflections
{
    public static class TypeExtensions
    {
        private static readonly ThreadsafeCache<Tuple<Type, Type>, object> GetAttributeCache = new ThreadsafeCache<Tuple<Type, Type>, object>();

        public static IEnumerable<Type> GetAssemblyTypes(this Type type)
        {
            return GetAssemblyTypesMemoized(type);
        }

        public static T GetAttribute<T>(this Type type) where T : Attribute
        {
            var tuple = Tuple.Create(type, typeof(T));
            object result;

            if (GetAttributeCache.TryGetValue(tuple, out result))
            {
                return (T)result;
            }

            result = type.GetCustomAttributes<T>().SingleOrDefault();

            GetAttributeCache.TryAdd(tuple, result);

            return (T)result;
        }

        public static T GetAttribute<T>(this Type type, Func<T, bool> predicate) where T : Attribute
        {
            return type.GetCustomAttributes<T>().SingleOrDefault(predicate);
        }

        public static bool IsGeneric(this Type type)
        {
            return IsGenericMemoized(type);
        }

        public static bool IsNotAbstract(this Type type)
        {
            return !type.IsAbstract;
        }

        public static bool IsNotGeneric(this Type type)
        {
            return !IsGeneric(type);
        }

        public static bool IsNotOfType<T>(this Type type)
        {
            return !IsOfType<T>(type);
        }

        public static bool IsNullable(this Type type)
        {
            if (!type.IsGenericType)
            {
                return false;
            }
            return type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsOfType<T>(this Type type)
        {
            return IsOfTypeMemoized(type, typeof(T));
        }

        private static readonly Func<Type, Type[]> GetAssemblyTypesMemoized = ((Func<Type, Type[]>)(type => type.Assembly.GetTypes())).Memoize(true);

        private static readonly Func<Type, bool> IsGenericMemoized = ((Func<Type, bool>)(type => type.IsGenericType)).Memoize();

        private static readonly Func<Type, Type, bool> IsOfTypeMemoized = ((Func<Type, Type, bool>)((extendedType, typeParameterType) => typeParameterType.IsAssignableFrom(extendedType))).Memoize(true);
    }
}