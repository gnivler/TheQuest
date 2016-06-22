using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TheQuest
{
    class Ghoul : Enemy
    {
        public Ghoul(Game game, Point location) : base(game, location, 10)
        {
        }

        public override void Move(Random random)
        {
            if (HitPoints > 0)
            {
                if (random.Next(2) != 0)
                {
                    // move towards player 66% of the time otherwise don't move
                    Direction direction = FindPlayerDirection(game.PlayerLocation);
                    location = base.Move(direction, game.Boundaries);
                }

                // damage the player when appropriate
                if (NearPlayer())
                {
                    game.HitPlayer(4, random);
                }
            }
        }
    }
}