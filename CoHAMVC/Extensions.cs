namespace CoHAMVC
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
    }
}