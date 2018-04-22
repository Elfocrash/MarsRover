using System;

namespace MarsRover
{
    public class Rover : IVehicle
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public Direction Direction { get; private set; }
        public ILandingSurface LandingSurface { get; }

        public Rover(int x, int y, Direction direction, ILandingSurface landingSurface)
        {
            X = x;
            Y = y;
            Direction = direction;
            LandingSurface = landingSurface;
        }

        public void Move(Movement movement)
        {
            switch (movement)
            {
                case Movement.L:
                    TurnLeft();
                    break;
                case Movement.R:
                    TurnRight();
                    break;
                case Movement.M:
                    MoveForward();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(movement), movement, null);
            }
        }

        private void MoveForward()
        {
            switch (Direction)
            {
                case Direction.N:
                    if (Y + 1 <= LandingSurface.Size.Height)
                        Y += 1;
                    break;

                case Direction.E:
                    if (X + 1 <= LandingSurface.Size.Width)
                        X += 1;
                    break;

                case Direction.S:
                    if (Y - 1 >= 0)
                        Y -= 1;
                    break;

                case Direction.W:
                    if (X - 1 >= 0)
                        X -= 1;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void TurnLeft()
        {
            switch (Direction)
            {
                case Direction.N:
                    Direction = Direction.W;
                    break;

                case Direction.W:
                    Direction = Direction.S;
                    break;

                case Direction.S:
                    Direction = Direction.E;
                    break;

                case Direction.E:
                    Direction = Direction.N;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void TurnRight()
        {
            switch (Direction)
            {
                case Direction.N:
                    Direction = Direction.E;
                    break;

                case Direction.E:
                    Direction = Direction.S;
                    break;

                case Direction.S:
                    Direction = Direction.W;
                    break;

                case Direction.W:
                    Direction = Direction.N;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString()
        {
            return $"{X} {Y} {Direction:G}";
        }
    }
}