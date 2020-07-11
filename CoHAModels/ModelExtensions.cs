namespace CoHAApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ModelExtensions
    {
        public static IEnumerable<Type> GetAllSubclasses<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic)
                .SelectMany(a => a.GetTypes())
                .Where(t => t.BaseType == typeof(T) && !t.IsAbstract);
        }
        
        public static IEnumerable<Type> GetAllSubclasses(Type parent)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => !a.IsDynamic)
                .SelectMany(a => a.GetTypes())
                .Where(t => t.BaseType == parent && !t.IsAbstract);
        }
    }
}