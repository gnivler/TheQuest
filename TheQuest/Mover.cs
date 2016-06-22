using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TheQuest
{
    abstract class Mover
    {
        // you change MoveInterval if you want your player and enemies to move in bigger or smaller steps
        //private const int MoveInterval = 10;

        // it should be a read-only property with a default value
        private int MoveInterval { get; } = 10;

        // since protected properties are only available to subclasses, the form object can't set the location, only read it through the public get method
        protected Point location;
        public Point Location { get { return location; } }
        protected Game game;

        // instances of Mover take in the Game object and a current location
        public Mover(Game game, Point location)
        {
            this.game = game;
            this.location = location;
        }

        // the Nearby() method checks a Point against this object's current location.  If they're within distance of each other, then it returns true
        public bool Nearby(Point locationToCheck, int distance)
        {
            return (Nearby(locationToCheck, location, distance));
        }

        public bool Nearby(Point firstPoint, Point secondPoint, int distance)
        {
            if (Math.Abs(firstPoint.X - secondPoint.X) < distance && (Math.Abs(firstPoint.Y - secondPoint.Y) < distance))
            {
                return true;
            }
            return false;
        }

        // the Move() method tries to move one step in a direction.  If it can, it returns a new Point.  If it hits a boundary it returns the original Point

        // overloaded method design maybe defective!
        public Point Move(Direction direction, Rectangle boundaries)
        {
            return Move(direction, location, boundaries);
        }

        public Point Move(Direction direction, Point target, Rectangle boundaries)
        {
            Point newLocation = target;
            switch (direction)
            {
                case Direction.Up:
                    if (newLocation.Y - MoveInterval >= boundaries.Top)
                    {
                        newLocation.Y -= MoveInterval;
                    }
                    break;
                case Direction.Down:
                    if (newLocation.Y + MoveInterval <= boundaries.Bottom)
                    {
                        newLocation.Y += MoveInterval;
                    }
                    break;
                case Direction.Left:
                    if (newLocation.X - MoveInterval >= boundaries.Left)
                    {
                        newLocation.X -= MoveInterval;
                    }
                    break;
                case Direction.Right:
                    if (newLocation.X + MoveInterval <= boundaries.Right)
                    {
                        newLocation.X += MoveInterval;
                    }
                    break;
            }
            // this might still be the same as the starting location if it hit a boundary
            return newLocation;
        }
    }
}