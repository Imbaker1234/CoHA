namespace MiraThree
{
    using CoHAApi;
    using CoHAMVC;

    public class StudentController : CoHAController<Student>
    {
        public StudentController(IService<Student> service) : base(service)
        {
        }
    }
}