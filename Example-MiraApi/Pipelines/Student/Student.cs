namespace MiraThree
{
    using System.ComponentModel.DataAnnotations;
    using Base;

    public class Student : IModel
    {
        [Key]
        public string Id { get; set; }
        public string First { get; set; }
        public string Last { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}