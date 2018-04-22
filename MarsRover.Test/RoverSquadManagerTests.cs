using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace MarsRover.Test
{
    public class RoverSquadManagerTests
    {
        [Theory]
        [InlineData("1 1 N")]
        [InlineData("4 2 S")]
        [InlineData("1 6 W")]
        public void RoverIsDeployedInAcceptableLocation(string command)
        {
            // Arrange
            var plataue = new Plataeu();
            plataue.Define(10, 10);

            IRoverSquadManager manager = new RoverSquadManager(plataue);
            var commandSplit = command.Split(' ');
            var expectedX = int.Parse(commandSplit[0]);
            var expectedY = int.Parse(commandSplit[1]);
            var expectedDirection = Enum.Parse<Direction>(commandSplit[2]);

            // Act
            manager.DeployRover(expectedX, expectedY, expectedDirection);

            // Assert
            manager.Rovers.Single().X.Should().Be(expectedX);
            manager.Rovers.Single().Y.Should().Be(expectedY);
            manager.Rovers.Single().Direction.Should().Be(expectedDirection);
        }

        [Theory]
        [InlineData("5 3 N")]
        [InlineData("10 4 S")]
        public void RoverIsDeployedInUnacceptableLocation(string command)
        {
            // Arrange
            var plataue = new Plataeu();
            plataue.Define(1, 1);

            IRoverSquadManager manager = new RoverSquadManager(plataue);
            var commandSplit = command.Split(' ');
            var expectedX = int.Parse(commandSplit[0]);
            var expectedY = int.Parse(commandSplit[1]);
            var expectedDirection = Enum.Parse<Direction>(commandSplit[2]);

            // Act
            var action = new Action(() => manager.DeployRover(expectedX, expectedY, expectedDirection));

            // Assert
            action.Should().Throw<Exception>().WithMessage("Rover outside of bounds");
        }
    }
}