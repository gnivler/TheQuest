using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TheQuest
{
    class Mace : Weapon
    {
        public Mace(Game game, Point location) : base(game, location)
        {
        }

        //public override string Name { get { return "Mace"; } }
        public override string Name => "Mace";

        public override void Attack(Direction direction, Random random)
        {
            // your code goes here
            // try each direction and stop if an attack hits
            /*
            if (!DamageEnemy(Direction.Up, 20, 6, random))
            {
                if (!DamageEnemy(Direction.Right, 20, 6, random))
                {
                    if (!DamageEnemy(Direction.Down, 20, 6, random))
                    {
                        DamageEnemy(Direction.Left, 20, 6, random);
                    }
                }
            }*/

            // this has to be done using the attack direction to provide the player 'primary' focus for the attack
            //  it would be impossible to deliberately damage a specific target if more than one were in range
            /*
            Direction attackDir = direction;
            if (!DamageEnemy(attackDir, 20, 6, random))
            {
                if ((int)direction == 4)
                {
                    attackDir = (Direction)1;
                }
                else
                {
                    attackDir++;
                }
                if (!DamageEnemy(attackDir, 20, 6, random))
                {
                    if ((int)direction == 4)
                    {
                        attackDir = (Direction)1;
                    }
                    else
                    {
                        attackDir++;
                    }
                    if (!DamageEnemy(attackDir, 20, 6, random))
                    {
                        if ((int)direction == 4)
                        {
                            attackDir = (Direction)1;
                        }
                        else
                        {
                            attackDir++;
                        }
                        if (!DamageEnemy(attackDir, 20, 6, random))
                        {

                        }
                    }
                }
            }*/

            // fancy fucker
            Direction attackDir = direction;
            if (!DamageEnemy(attackDir, 20, 6, random))
            {
                attackDir = Clockwise(attackDir);
                if (!DamageEnemy(Clockwise(attackDir), 20, 6, random))
                {
                    attackDir = Clockwise(attackDir);
                    if (!DamageEnemy(Clockwise(attackDir), 20, 6, random))
                    {
                        attackDir = Clockwise(attackDir);
                        DamageEnemy(Clockwise(attackDir), 20, 6, random);
                    }
                }
            }
        }
    }
}