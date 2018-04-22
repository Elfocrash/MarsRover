using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MarsRover.Test
{
    public class CommandCenterTests
    {
        [Theory]
        [InlineData("5 5")]
        [InlineData("1 3")]
        [InlineData("4 2")]
        public void TwoNumbersCommandCreatesPlataue(string command)
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILandingSurface, Plataeu>()
                .AddSingleton<IRoverSquadManager, RoverSquadManager>()
                .BuildServiceProvider();
            var commandSplit = command.Split(' ');
            var expectedWidth = int.Parse(commandSplit[0]) + 1;
            var expectedHeight = int.Parse(commandSplit[1]) + 1;

            ICommandCenter commandCenter = new CommandCenter(serviceProvider);
            var plateue = serviceProvider.GetService<ILandingSurface>();
            
            // Act
            commandCenter.SendCommand(command);

            // Assert
            plateue.Should().NotBeNull();
            plateue.Size.Should().NotBeNull();
            plateue.Size.Width.Should().Be(expectedWidth);
            plateue.Size.Height.Should().Be(expectedHeight);
        }

        [Theory]
        [InlineData("1 1", "5 1 N")]
        public void RoverDeployedOutOfBoundsThrowsException(string plataeuDeployCommand, string command)
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRoverSquadManager, RoverSquadManager>()
                .AddSingleton<ILandingSurface, Plataeu>()
                .BuildServiceProvider();

            var commandCenter = new CommandCenter(serviceProvider);
            commandCenter.SendCommand(plataeuDeployCommand);

            // Act
            var action = new Action(() => commandCenter.SendCommand(command));

            // Assert
            action.Should().Throw<Exception>().WithMessage("Rover outside of bounds");
        }

        [Fact]
        public void WhenNotActiveRoverDontAct()
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRoverSquadManager, RoverSquadManager>()
                .AddSingleton<ILandingSurface, Plataeu>()
                .BuildServiceProvider();

            var commandCenter = new CommandCenter(serviceProvider);
            commandCenter.SendCommand("5 5");
            var roverSquadManager = serviceProvider.GetService<IRoverSquadManager>();

            // Act
            commandCenter.SendCommand("MMM");

            // Assert
            roverSquadManager.ActiveRover.Should().BeNull();
        }

        [Theory]
        [InlineData("MRMRMRMR")]
        [InlineData("MLMLMLML")]
        public void MoveInACircleReturnsToInitialPosition(string command)
        {
            // Arrange
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRoverSquadManager, RoverSquadManager>()
                .AddSingleton<ILandingSurface, Plataeu>()
                .BuildServiceProvider();

            var commandCenter = new CommandCenter(serviceProvider);
            commandCenter.SendCommand("9 9");
            commandCenter.SendCommand("5 5 N");
            var rover = serviceProvider.GetService<IRoverSquadManager>().ActiveRover;

            // Act
            commandCenter.SendCommand(command);

            // Assert
            rover.Should().NotBeNull();
            rover.X.Should().Be(5);
            rover.Y.Should().Be(5);
            rover.Direction.Should().Be(Direction.N);
        }
    }
}