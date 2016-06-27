using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TheQuest
{
    class Bat : Enemy
    {
        public Bat(Game game, Point location) : base(game, location, 6)
        {
        }

        public override void Move(Random random)
        {
            // your code will go here
            if (HitPoints < 0)
            {
                Direction direction;
                if (random.Next(1) == 0)
                {
                    // move in random direction
                    direction = (Direction)random.Next(1, 4);
                }
                else
                {
                    // move towards player
                    direction = FindPlayerDirection(game.PlayerLocation);
                }
                location = base.Move(direction, game.Boundaries);

                // damage the player when appropriate
                if (NearPlayer())
                {
                    game.HitPlayer(2, random);
                }
            }
        }
    }
}