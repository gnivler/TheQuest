using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheQuest
{
    // modified the ordering such that +1 is clockwise and -1 is counter-clockwise
    // 0 would mean Left(4), since Up = 1 - 1, that'll have to be coded...
    enum Direction
    {
        Up,
        Right,
        Down,
        Left,
    }
}
