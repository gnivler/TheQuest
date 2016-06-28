using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TheQuest
{
    // Weapon inherits from Mover because it uses its Nearby() and Move() methods in DamageEnemy()
    abstract class Weapon : Mover
    {
        // a picked-up weapon shouldn't be displayed anymore, the form can use this get acccessor to figure that out
        public bool PickedUp { get; private set; }

        // the constructor calls the Mover base constructor (which sets the game and location fields) and then sets PickedUp to false
        public Weapon(Game game, Point location) : base(game, location)
        {
            PickedUp = false;
        }

        public void PickUpWeapon()
        {
            PickedUp = true;
        }

        // each weapon class needs to implement a Name property and an Attack() method that determines how that weapon attacks
        // each weapon's Name property returns it's name "Sword", "Mace", "Bow"
        public abstract string Name { get; }

        // each weapon has a different range and pattern of attack so the weapons implement the Attack() method differently
        public abstract void Attack(Direction direction, Random random);

        // the DamageEnemy() method is called by Attack().  It attempts to find an enemy in a certain direction and radius.
        // if it does, it calls the enemy's Hit() method and returns true
        // If no enemy is found, it returns false
        protected bool DamageEnemy(Direction direction, int radius, int damage, Random random)
        {
            // this code doesn't really work imo
            // distance = 0 will only be true if the points are identical
            // distance = 4 means it's checking less than half the distance of a MoveInterval
            Point targetPoint = game.PlayerLocation;
            for (int distance = 0; distance < radius / 2; distance++)
            {
                foreach (Enemy enemy in game.Enemies)
                {
                    // is the enemy Point within distance of targetPoint?
                    if (Nearby(enemy.Location, targetPoint, radius))  
                    {
                        enemy.Hit(damage, random);
                        return true;
                    }
                }
                targetPoint = Move(direction, targetPoint, game.Boundaries);
            }
            return false;
        }

        /*
        // bool methods for each direction?  then we can say if its left of or right of then...
        // X,Y so how much leeway
        protected bool LeftOf(Direction direction)
        {

        }*/

        /// <summary>
        ///  try rewrite to behave intuitively for sword swing
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="radius"></param>
        /// <param name="damage"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        /// 
        public bool IsHit(Direction direction, int radius, int damage, Random random)
        {
            Point playerPoint = game.PlayerLocation;
            int range = radius * 5; // should be radius * moveInterval / 2

            Rectangle upRect = new Rectangle(playerPoint.X - range, playerPoint.Y - 10, range * 2 - 2, range); 
            Rectangle downRect = new Rectangle(playerPoint.X + range, playerPoint.Y - range, range * 2, range);
            //Rectangle leftRect = new Rectangle(playerPoint.X - range, playerPoint.Y + range, range * 2, range);
            //Rectangle rightRect = new Rectangle(playerPoint.X - range, playerPoint.Y + range, range * 2, range);

            if (direction == Direction.Up)
            {
                // manual rectangle?
                // is the player beneath the target and within the weapon's range?  is it also within range laterally?
                // this should eval to true within a rectangle in front of the attack direction
                // so... just create a Rectangle and use .Contains()?  instead
                /*if (playerPoint.Y < location.Y && Math.Abs(playerPoint.Y - location.Y) <= range && Math.Abs(playerPoint.X - location.X) <= range) // radius * 5 evaluates to the same as radius * moveInterval / 2
                {
                    // hit
                    return true;
                
                }*/



                foreach (Enemy enemy in game.Enemies)
                {
                    if (upRect.Contains(enemy.Location))
                    {
                        enemy.Hit(damage, random);
                        bool AoE = false;
                        if (!AoE)
                        {
                            return true; // quit before damaging other targets in the hit area - only 1 damaging attack per attack move (change for AoE attacks?)
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// returns a Direction which is clockwise of the parameter provided
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Direction Clockwise(Direction direction)
        {
            return ((int)direction == 3) ? 0 : direction + 1;
        }

        /// <summary>
        ///  returns a Direction which is counter-clockwise of the parameter provided
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Direction Counterclockwise(Direction direction)
        {
            return (direction == 0) ? (Direction)3 : direction - 1;
        }
    }
}
