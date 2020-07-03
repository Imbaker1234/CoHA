namespace CoHAMVC
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IController<in T>
    {
        Task<IActionResult> Create(T item);
        Task<IActionResult> Read(string id);
        Task<IActionResult> Update(T item);
        Task<IActionResult> Delete(string id);
        Task<IActionResult> ReadAll();
    }
}