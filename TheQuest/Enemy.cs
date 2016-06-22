using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TheQuest
{
    abstract class Enemy : Mover
    {
        private int NearPlayerDistance { get; } = 25;

        public int HitPoints { get; private set; }
        // the form can use this read-only property to see if the enmey should be visible in the game dungeon
        public bool Dead
        {
            get
            {
                return (HitPoints <= 0) ? true : false;
            }
        }

        public Enemy(Game game, Point location, int hitPoints) : base(game, location)
        {
            HitPoints = hitPoints;
        }

        // each subclass of Enemy implements this
        public abstract void Move(Random random);

        // when the player attacks an enemy, it calls the enemy's Hit() method which substracts a random number of hitpoints
        public void Hit(int maxDamage, Random random) => HitPoints -= random.Next(1, maxDamage);

        // the Enemy class inherited the Nearby() method from Mover which is can use to figure out whether it's near the player
        protected bool NearPlayer() => (Nearby(game.PlayerLocation, NearPlayerDistance));

        // if you feed FindPlayerDirection() the player's location it'll use the base class's location field to figure out where
        // the player is in relation to the enemy and return a Direction enyum that tells you in which direction the enemy needs
        // to move in order to move towards the player
        protected Direction FindPlayerDirection(Point playerLocation)
        {
            Direction directionToMove;
            if (playerLocation.X > location.X + 10)
            {
                directionToMove = Direction.Right;
            }
            else if (playerLocation.X < location.X - 10)
            {
                directionToMove = Direction.Left;
            }
            else if (playerLocation.Y < location.Y - 10)
            {
                directionToMove = Direction.Up;
            }
            else
            {
                directionToMove = Direction.Down;
            }
            return directionToMove;
        }
    }
}