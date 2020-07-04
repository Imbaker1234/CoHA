namespace CoHAMVC
{
    using System.Threading.Tasks;
    using CoHAApi;
    using Microsoft.AspNetCore.Http.Extensions;
    using Microsoft.AspNetCore.Mvc;

    [Route("[controller]")]
    public class CoHAController<T> : ControllerBase, IController<T>
    {
        private IService<T> Service { get; set; }

        public CoHAController(IService<T> service)
        {
            Service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T item)
        {
            var product = await Service.Create(item);
            return Created(Request.GetEncodedUrl(), product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Read(string id)
        {
            var product = await Service.Read(id);
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] T item)
        {
            var product = await Service.Update(item);
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var product = await Service.Delete(id);

            return Ok(product);
        }

        [HttpGet]
        public async Task<IActionResult> ReadAll()
        {
            var product = await Service.ReadAll(Request.Query.ToDictionary());

            return Ok(product);
        }
    }
}