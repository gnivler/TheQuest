using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TheQuest
{
    class Sword : Weapon
    {
        public Sword(Game game, Point location) : base(game, location)
        {
        }

        //public override string Name {  get { return "Sword"; } }
        // written as expression bodied:
        public override string Name => "Sword";
        public override void Attack(Direction direction, Random random)
        {
            // your code goes here
            // reference the original direction to use in calls
            // try the provided direction first then try clockwise then try counter-clockwise
            // it will only hit the first enemy found then stop
            // testing for false (miss) to continue through
            Direction attackDir = direction;
            if (!DamageEnemy(attackDir, 10, 3, random))
            {
                // adjust direction to be clockwise (the enum is ordered to facilitate this)
                if ((int)direction == 4)      // 5 would be invalid so loop it to 1
                {
                    attackDir = (Direction)1;
                }
                else
                {
                    attackDir = direction + 1;
                }
                if (!DamageEnemy(attackDir, 10, 3, random))
                {
                    if ((int)direction == 1)  // 0 would be invalid so loop it to 4
                    {
                        attackDir = (Direction)4;
                    }
                    else
                    {
                        attackDir = direction - 1;
                    }
                    // it doesn't really matter if true/false unless we want to act on it
                    // this is only being called if the other directions missed and it will do nothing if it's also a miss
                    DamageEnemy(attackDir, 10, 3, random);
                }
            }

            /* another way to achieve 'rotation' on the attack direction
             * this would have to be duplicated for counter-clockwise
            switch (direction)
            {
                case Direction.Left:
                    attackDir = Direction.Up; break;
                case Direction.Up:
                    attackDir = Direction.Right; break;
                case Direction.Right:
                    attackDir = Direction.Down; break;
                case Direction.Down:
                    attackDir = Direction.Left; break;

            }*/
        }
    }
}
