namespace CoHAApi
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICrud<T> 
    {
        Task<T> Create(T item);
        Task<T> Read(string id);
        Task<T> Update(T item);
        Task<T> Delete(string id);
        Task<List<T>> ReadAll(Dictionary<string, string> query);
    }
}