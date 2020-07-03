using Microsoft.AspNetCore.Mvc;

namespace MiraThree
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http.Extensions;

    [Route("student")]
    public class StudentController : ControllerBase, IController<Student>
    {
        private IService<Student> Service { get; set; }

        public StudentController(IService<Student> service)
        {
            Service = service;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student item)
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
        public async Task<IActionResult> Update([FromBody] Student item)
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