namespace CoHAApi
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CoHAPersistence;

    public class CoHAService<T> : IService<T> where T: IModel
    {
        protected IRepository<T> Repository { get; set; }

        public CoHAService(IRepository<T> repository)
        {
            Repository = repository;
        }

        public virtual async Task<T> Create(T item)
        {
            var product = await Repository.Create(item);

            return product;
        }

        public virtual async Task<T> Read(string id)
        {
            var product = await Repository.Read(id);
            return product;
        }

        public virtual async Task<T> Update(T item)
        {
            var product = await Repository.Update(item);
            return product;
        }

        public virtual async Task<T> Delete(string id)
        {
            var product = await Repository.Delete(id);

            return product;
        }

        public virtual async Task<List<T>> ReadAll(Dictionary<string, string> parameters)
        {
            var product = await Repository.ReadAll(parameters);

            return product;
        }
    }
}