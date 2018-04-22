using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace MarsRover.Test
{
    public class RoverTests
    {
        [Theory]
        [InlineData("MM")]
        [InlineData("MMLR")]
        [InlineData("LLLLMM")]
        public void Move2NorthOn4X4MoveTwoBlocksUp(string command)
        {
            // Arrange
            var plataue = new Plataeu();
            plataue.Define(5, 5);
            IRoverSquadManager manager = new RoverSquadManager(plataue);
            manager.DeployRover(0, 0, Direction.N);
            var movements = command
                .ToCharArray()
                .Select(x => Enum.Parse<Movement>(x.ToString()))
                .ToList();


            // Act
            movements.ForEach(manager.ActiveRover.Move);

            // Assert
            manager.ActiveRover.Should().NotBeNull();
            manager.ActiveRover.X.Should().Be(0);
            manager.ActiveRover.Y.Should().Be(2);
            manager.ActiveRover.Direction.Should().Be(Direction.N);
        }
    }
}