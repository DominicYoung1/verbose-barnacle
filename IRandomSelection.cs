using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MUD.Multithreading;

namespace MUD
{
    interface IRandomSelection
    {
        // pick to return either A or B where the prob of that is returned is the number probA
        // we pass in probs as a decimal, meaning a 50% for a to happen would be 0.5
        IEvent PickAOrB(IEvent a, IEvent b, double probabilityOfA)
        {
            Random rng = new Random();
            double diceRoll = rng.NextDouble(); // this returns a random num between 0 and 1
            if (diceRoll < probabilityOfA)
            {
                return a;
            }
            else
            {
                return b;
            }

        }
    }
}
