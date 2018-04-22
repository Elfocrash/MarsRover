namespace MarsRover
{
    public class Plataeu : ILandingSurface
    {
        public SurfaceSize Size { get; private set; }
        
        public void Define(int width, int height)
        {
            Size = new SurfaceSize(width, height);
        }
    }
}