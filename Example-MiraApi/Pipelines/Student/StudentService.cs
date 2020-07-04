namespace MiraThree
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CoHAApi;
    using CoHAPersistence;

    public class StudentService : CoHAService<Student>
    {
        public StudentService(IRepository<Student> repository) : base(repository)
        {
        }

        public override async Task<Student> Create(Student item)
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
    }
}