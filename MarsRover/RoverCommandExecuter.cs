using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover
{
    public class RoverCommandExecuter : CommandExecuter
    {
        private readonly IRoverSquadManager _squadManager;

        public RoverCommandExecuter(IServiceProvider serviceProvider)
        {
            _squadManager = serviceProvider.GetService<IRoverSquadManager>();
        }

        public override Regex RegexCommandPattern => new Regex("^[LMR]+$");

        public override void ExecuteCommand(string command)
        {
            if (CheckIfActiveRoverExists(out var activeRover))
                return;

            MoveRoverByCommand(command, activeRover);
            ReportLocation(activeRover);
        }

        private static void MoveRoverByCommand(string command, Rover activeRover)
        {
            foreach (var order in command)
            {
                var movement = Enum.Parse<Movement>(order.ToString());
                activeRover.Move(movement);
            }
        }

        private static void ReportLocation(Rover activeRover)
        {
            Console.WriteLine(activeRover.ToString());
        }

        private bool CheckIfActiveRoverExists(out Rover activeRover)
        {
            activeRover = _squadManager.ActiveRover;
            return activeRover == null;
        }
    }
}