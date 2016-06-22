using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TheQuest
{
    class Game
    {
        // these are OK as public properties if Enemy and Weapon are well encapsulated
        // in other words, just make sure the form can't do anything inappropriate with them
        public IEnumerable<Enemy> Enemies { get; private set; }
        public Weapon WeaponInRoom { get; private set; }

        // the game keeps a private Player object.  The form will only interact with this through methods on Game
        private Player player;
        public  Point PlayerLocation => player.Location;
        public int PlayerHitPoints => player.HitPoints;
        public IEnumerable<string> PlayerWeapons => player.Weapons;
        private int level = 0;
        public int Level => level;

        // the rectangle object has a Top, Bottom, Left and Right field and works perfectly for the game area
        private Rectangle boundaries;
        public Rectangle Boundaries => boundaries;

        // Game starts out with a bounding box for the dungeon, and creates a new Player object in the dungeon
        public Game(Rectangle boundaries)
        {
            this.boundaries = boundaries;
            player = new Player(this, new Point(boundaries.Left + 10, boundaries.Top + 70));
        }

        // movement is simple:  move the player in the direction the form gives us and move each enemy in a random direction
        public void Move(Direction direction, Random random)
        {
            player.Move(direction);
            foreach (Enemy enemy in Enemies)
            {
                enemy.Move(random);
            }
        }

        public void Equip(string weaponName) => player.Equip(weaponName);
        public void HitPlayer(int maxDamage, Random random) => player.Hit(maxDamage, random);
        public void IncreasePlayerHealth(int health, Random random) => player.IncreaseHealth(health, random);

        public bool CheckPlayerInventory(string weaponName)
        {
            if (player.Weapons.Contains(weaponName))
            {
                // this method also returns true if it's not potion
                return player.IsWeaponOrUsablePotion(weaponName);
            }
            return false;
        }

        // Attack() is almost exactly like Move().  The Player attacks, and the enemies all get a turn to move
        public void Attack(Direction direction, Random random)
        {
            player.Attack(direction, random);
            foreach (Enemy enemy in Enemies)
            {
                enemy.Move(random);
            }
        }

        // GetRandomLocation() will come in handy in the NewLevel() method, which will use it to detemrine where to place enemies and weapons
        private Point GetRandomLocation(Random random)
        {
            // this is just a math trick to get a random location within the rectangle that represents the dungeon area
            return new Point(boundaries.Left + random.Next(boundaries.Right / 10 - boundaries.Left / 10) * 10, boundaries.Top + random.Next(boundaries.Bottom / 10 - boundaries.Top / 10) * 10);
        }

        // we only added the case for Level 1, it's your job to add the cases for the other levels
        // we've only got room in the inventory for one blue and one red potion, so the game shouldn't add them if the player already has each
        /// <summary>
        /// spawns enemy and weapon objects
        /// </summary>
        /// <param name="random"></param>
        public void NewLevel(Random random)
        {
            level++;
            switch (level)
            {
                case 1:
                    Enemies = new List<Enemy> { new Bat(this, GetRandomLocation(random)) };
                    WeaponInRoom = new Sword(this, GetRandomLocation(random));
                    break;
                case 2:
                    Enemies = new List<Enemy> { new Ghost(this, GetRandomLocation(random)) };
                    WeaponInRoom = new BluePotion(this, GetRandomLocation(random));
                    break;
                case 3:
                    Enemies = new List<Enemy> { new Ghost(this, GetRandomLocation(random)) };
                    //WeaponInRoom = new Bow(this, GetRandomLocation(random));
                    WeaponInRoom = new BluePotion(this, GetRandomLocation(random));

                    break;
                case 4:
                    Enemies = new List<Enemy> { new Bat(this, GetRandomLocation(random)),
                                                new Ghost(this, GetRandomLocation(random)) };
                    // this should spawn nothing if a Bow and Blue Potion already exist in inventory
                    if (!CheckPlayerInventory("Bow")) {
                        WeaponInRoom = new Bow(this, GetRandomLocation(random));
                    }
                    else if  (!CheckPlayerInventory("Blue Potion"))
                    {
                        WeaponInRoom = new BluePotion(this, GetRandomLocation(random));
                    }
                    break;
                case 5:
                    Enemies = new List<Enemy> { new Bat(this, GetRandomLocation(random)),
                                                new Ghoul(this, GetRandomLocation(random)) };
                    WeaponInRoom = new RedPotion(this, GetRandomLocation(random));
                    break;
                case 6:
                    Enemies = new List<Enemy> { new Ghost(this, GetRandomLocation(random)),
                                                new Ghoul(this, GetRandomLocation(random)) };
                    WeaponInRoom = new Mace(this, GetRandomLocation(random));
                    break;
                case 7:
                    Enemies = new List<Enemy> { new Bat(this, GetRandomLocation(random)),
                                                new Ghost(this, GetRandomLocation(random)),
                                                new Ghoul(this, GetRandomLocation(random)) };
                    // this should spawn nothing if a Mace and Red Potion already exist in inventory
                    if (!CheckPlayerInventory("Mace"))
                    {
                        WeaponInRoom = new Mace(this, GetRandomLocation(random));
                    }
                    else if (!CheckPlayerInventory("Red Potion"))
                    {
                        WeaponInRoom = new RedPotion(this, GetRandomLocation(random));
                    }
                    break;
                case 8:
                    // the game ends
                    // using Systems.Windows.Forms
                    // UI freaks out from this?
                    Application.Exit();
                    break;
            }
        }
    }
}
