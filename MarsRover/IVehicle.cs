namespace MarsRover
{
    public interface IVehicle
    {
        int X { get; }

        int Y { get; }

        ILandingSurface LandingSurface { get; }

        Direction Direction { get; }

        void Move(Movement movement);
    }
}