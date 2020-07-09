namespace MiraThree.Rooms
{
    using System.ComponentModel.DataAnnotations;
    using CoHAApi;

    public class Room : IModel
    {
        [Key]
        public string Id { get; set; }
        public int Capacity { get; set; }
        public int OpenTime { get; set; }
        public int CloseTime { get; set; }
    }
}