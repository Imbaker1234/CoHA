namespace CoHAMVC
{
    using System.Collections.Generic;
    using System.Linq;
    using CoHAApi;
    using CoHAPersistence;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Primitives;

    public static class Extensions
    {
        public static Dictionary<string, string> ToDictionary(this IQueryCollection queryCollection)
        {
            return queryCollection.ToDictionary<KeyValuePair<string, StringValues>, string, string>
                (kvp => kvp.Key, kvp => kvp.Value);
        }

        public static IServiceCollection RegisterIModelPipeline<T>(this IServiceCollection collection)
            where T : class, IModel
        {
            //TODO Take furthest extended class rather than only a direct subclass.
            var controllerImplementation = ModelExtensions.GetAllSubclasses<CoHAController<T>>().SingleOrDefault();
            var serviceImplementation = ModelExtensions.GetAllSubclasses<CoHAService<T>>().SingleOrDefault();
            var repoImplementation = ModelExtensions.GetAllSubclasses<EntityRepository<T>>().SingleOrDefault();

            if (controllerImplementation != null) //If we have a concrete class register it.
            {
                collection.AddTransient(controllerImplementation);
            }
            else //Else register the default class.
            {
                collection.AddTransient<CoHAController<T>>();
            }
            
            if (serviceImplementation != null) //If we have a concrete class register it.
            {
                collection.AddTransient(serviceImplementation);
            }
            else //Else register the default class.
            {
                collection.AddTransient<CoHAService<T>>();
            }
            
            if (repoImplementation != null) //If we have a concrete class register it.
            {
                collection.AddTransient(repoImplementation);
            }
            else //Else register the default class.
            {
                collection.AddTransient<EntityRepository<T>>();
            }

            return collection;
        }
    }
}