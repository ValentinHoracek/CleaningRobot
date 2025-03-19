using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleaningRobot
{
    internal enum Orientation
    {
        [StringValue("N")]
        North,
        [StringValue("S")]
        South,
        [StringValue("E")]
        East,
        [StringValue("W")]
        West,
    }
}
