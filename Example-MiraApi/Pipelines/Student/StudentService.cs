namespace MiraThree
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CoHAApi;
    using CoHAPersistence;

    public class StudentService : IService<Student>
    {
        private IRepository<Student> Repository { get; set; }

        public StudentService(IRepository<Student> repository)
        {
            Repository = repository;
        }

        public async Task<Student> Create(Student item)
        {
            Student product;
            if (Guid.TryParse(item.Id, out Guid guid)) product = await Repository.Create(item);
            else
            {
                item.Id = Guid.NewGuid().ToString();

                product = await Repository.Create(item);
            }

            return product;
        }

        public async Task<Student> Read(string id)
        {
            var product = await Repository.Read(id);
            return product;
        }

        public async Task<Student> Update(Student item)
        {
            var product = await Repository.Update(item);
            return product;
        }

        public async Task<Student> Delete(string id)
        {
            var product = await Repository.Delete(id);

            return product;
        }

        public async Task<List<Student>> ReadAll(Dictionary<string, string> parameters)
        {
            var product = await Repository.ReadAll(parameters);

            return product;
        }
    }
}