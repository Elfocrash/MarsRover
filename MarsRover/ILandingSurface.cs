namespace MarsRover
{
    public interface ILandingSurface
    {
        SurfaceSize Size { get; }

        void Define(int width, int height);
    }
}