using System;
using System.Collections.Generic;

namespace MarsRover
{
    public class RoverSquadManager : IRoverSquadManager
    {
        public List<Rover> Rovers { get; } = new List<Rover>();

        public ILandingSurface LandingSurface { get; }

        public Rover ActiveRover { get; private set; }

        public RoverSquadManager(ILandingSurface landingSurface)
        {
            LandingSurface = landingSurface;
        }

        public void DeployRover(int x, int y, Direction direction)
        {
            CheckIfLocationToDeployIsValid(x, y);
            var rover = new Rover(x, y, direction, LandingSurface);
            Rovers.Add(rover);
            ActiveRover = rover;
        }

        private void CheckIfLocationToDeployIsValid(int x, int y)
        {
            if (!IsAppropriateLocationToDeployRover(x, y))
                throw new Exception("Rover outside of bounds");
        }

        private bool IsAppropriateLocationToDeployRover(int x, int y)
        {
            return x >= 0 && x < LandingSurface.Size.Width &&
                   y >= 0 && y < LandingSurface.Size.Height;
        }
    }
}