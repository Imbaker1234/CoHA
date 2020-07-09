namespace MiraThree.Rooms
{
    using CoHAApi;
    using CoHAMVC;

    public class RoomController : CoHAController<Room>
    {
        public RoomController(IService<Room> service) : base(service)
        {
        }
    }
}