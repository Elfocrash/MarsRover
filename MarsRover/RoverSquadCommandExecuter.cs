using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover
{
    public class RoverSquadCommandExecuter : CommandExecuter
    {
        private readonly IRoverSquadManager _squadManager;

        public RoverSquadCommandExecuter(IServiceProvider serviceProvider)
        {
            _squadManager = serviceProvider.GetService<IRoverSquadManager>();
        }

        public override Regex RegexCommandPattern => new Regex("^\\d+ \\d+ [NSWE]$");

        public override void ExecuteCommand(string command)
        {
            ParseCommand(command, out var x, out var y, out var direction);
            _squadManager.DeployRover(x, y, direction);
        }

        private static void ParseCommand(string command, out int x, out int y, out Direction direction)
        {
            var splitCommand = command.Split(' ');
            x = int.Parse(splitCommand[0]);
            y = int.Parse(splitCommand[1]);
            direction = Enum.Parse<Direction>(splitCommand[2]);
        }
    }
}