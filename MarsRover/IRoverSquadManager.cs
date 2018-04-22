using System.Collections.Generic;

namespace MarsRover
{
    public interface IRoverSquadManager
    {
        List<Rover> Rovers { get; }

        Rover ActiveRover { get; }

        ILandingSurface LandingSurface { get; }

        void DeployRover(int x, int y, Direction direction);
    }
}