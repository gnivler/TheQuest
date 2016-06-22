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
            //
            // reference the original direction to use in calls
            // try the provided direction first then try clockwise then try counter-clockwise
            // it will only hit the first enemy found then stop
            // testing for false (miss) to continue through
            if (!DamageEnemy(direction, 10, 3, random))
            {
                if (!DamageEnemy(Clockwise(direction), 10, 3, random))
                {
                    DamageEnemy(Counterclockwise(direction), 10, 3, random);
                }
            }
        }
    }
}