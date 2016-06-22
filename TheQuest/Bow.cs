using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TheQuest
{
    class Bow : Weapon
    {
        public Bow(Game game, Point location) : base(game, location)
        {
        }

        //public override string Name { get { return "Bow"; } }
        // expression bodied syntax:
        public override string Name => "Bow";
        public override void Attack(Direction direction, Random random)
        {
            // your code goes here
            DamageEnemy(direction, 30, 1, random);
        }
    }
}
