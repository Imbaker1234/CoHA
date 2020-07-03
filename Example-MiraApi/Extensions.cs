namespace MiraThree
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Primitives;

    public static class Extensions
    {
        public static Dictionary<string, string> ToDictionary(this IQueryCollection queryCollection)
        {
            return queryCollection.ToDictionary<KeyValuePair<string, StringValues>, string, string>
                (kvp => kvp.Key, kvp => kvp.Value);
        }

        private static IQueryable<T> IndividualFilter<T>(this IQueryable<T> queryable, string parameter, Dictionary<string, string> parameters)
        {
            if (parameters.TryGetValue(parameter, out string outVar))
            {
                queryable = queryable.Where(s => typeof(T).GetProperty(parameter).GetValue(s).ToString() == outVar);
            }

            return queryable;
        }

        // public static List<Student> Filter<Student>(this IQueryable<Student> queryable, Dictionary<string, string> parameters)
        // {
        //     queryable.Where(s => s)
        // }
    }
}