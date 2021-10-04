using System;
using System.Collections.Generic;
using System.Text;

namespace MUD
{
    // needs to be close to the item class but with more utility, consider using dict or AL
    // **KEEP IN MIND** will interact with entities when it comes to combat and stats
    // **CONSIDER** Pros and Cons for different "builds" with different equipment
    class Weapons
    {
        public string name;
        public string description;
        double damageValue;
    }


}
