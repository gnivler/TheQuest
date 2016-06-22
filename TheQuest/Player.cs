using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TheQuest
{
    class Player : Mover
    {
        private Weapon equippedWeapon;
        public int HitPoints { get; private set; }

        private List<Weapon> inventory = new List<Weapon>();
        // the Weapons property returns a collection of strings with the weapon names
        public IEnumerable<string> Weapons
        {
            get
            {
                List<string> names = new List<string>();
                foreach (Weapon weapon in inventory)
                {
                    names.Add(weapon.Name);
                }
                return names;
            }
        }

        // Player inherits from Mover so this passes in the Game and location to that base class
        public Player(Game game, Point location) : base (game, location)
        {
            HitPoints = 10;
        }
        
        public void Hit(int maxDamage, Random random)
        {
            HitPoints -= random.Next(1, maxDamage);
        }

        public void IncreaseHealth(int health, Random random)
        {
            HitPoints += random.Next(1, health);
        }

        public void Equip(string weaponName)
        {
            foreach (Weapon weapon in inventory)
            {
                if (weapon.Name == weaponName)
                {
                    equippedWeapon = weapon;
                }
            }
        }

        public bool IsWeaponOrUsablePotion(string weaponName)
        {
            foreach (Weapon item in inventory)
            {
                // only interested in processing items that match the parameter
                if (item.Name == weaponName)
                {
                    if (item is IPotion)
                    {
                        IPotion potion = (IPotion)item;
                        return potion.Used;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            // this should never be reached, try/catch to make sure I haven't screwed up the logic above
            try
            {
                return true;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        public void Move(Direction direction)
        {
            base.location = Move(direction, game.Boundaries);

            // see if the weapon is nearby and possibly pick it up
            // when the player picks up a weapon, it needs to disappear from the dungeon and appear in the inventory
            // the Weapon and form will handle making the weapon's PictureBox invisible when the player picks it up
            // that's not the job of the Player class
            if (!game.WeaponInRoom.PickedUp)
            {
                if (Nearby(game.WeaponInRoom.Location, 30))
                {
                    game.WeaponInRoom.PickUpWeapon();
                    inventory.Add(game.WeaponInRoom);
                }
            }
        }

        // the weapons all have an Attack() method that takes a Direction enum and a Random object.  The player's Attack() will figure out
        // which weapon is equipped and call its Attack()
        // if the weapon is a potion, then Attack() removes it from the inventory after the player drinks it
        public void Attack(Direction direction, Random random)
        {
            // your code goes here
            if (equippedWeapon != null)
            {
                equippedWeapon.Attack(direction, random);
            }
        }
    }
}
