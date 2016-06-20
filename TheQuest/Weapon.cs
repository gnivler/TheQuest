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

        public abstract void Attack(Direction direction, Random random);

        // each weapon has a different range and pattern of attack so the weapons implement the Attack() method differently
        protected bool DamageEnemy(Direction direction, int radius, int damage, Random random)
        {
            Point target = game.PlayerLocation;
            for (int distance = 0; distance < radius / 2; distance++)
            {
                foreach (Enemy enemy in game.Enemies)
                {
                    if (Nearby(enemy.Location, target, distance))
                    {
                        enemy.Hit(damage, random);
                        return true;
                    }
                }
                // the DamageEnemy() method is called by Attack().  It attempts to find an enemy in a certain direction and radius.
                // if it does, it calls the enemy's Hit() method and returns true
                // If no enemy is found, it returns false
                target = Move(direction, target, game.Boundaries);
            }
            return false;
        }

        // overloaded Nearby() method that compares two Points to see if they are within distance of each other 
        /*private bool Nearby(Point locationToCheck, Point target, int distance)
        {
            base.Nearby(locationToCheck, distance);
        }*/
    }
}
