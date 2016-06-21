using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TheQuest
{
    class Ghost : Enemy
    {
        public Ghost(Game game, Point location) : base(game, location, 8)
        {
        }

        public override void Move(Random random)
        {
            if (HitPoints > 0)
            {
                if (random.Next(2) == 0)
                {
                    // move towards player 33% of the time otherwise don't move
                    Direction direction = FindPlayerDirection(game.PlayerLocation);
                    location = base.Move(direction, game.Boundaries);
                }

                // damage the player when appropriate
                if (NearPlayer())
                {
                    game.HitPlayer(3, random);
                }
            }
        }
    }
}
