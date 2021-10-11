using System;
using System.Collections.Generic;
using System.Text;

namespace MUD
{
    class CombatController
    {
        Entity initiator;
        Entity defender;

        public CombatController(Entity a, Entity d)
        {
            initiator = a;
            defender = d;
        }

        public void ProcessCombat()
        {
            while (initiator.Alive() && defender.Alive())
            {
                double swing = initiator.ImpartDamage();
                defender.TakeDamage(swing);
                double retaliate = defender.ImpartDamage();
                initiator.TakeDamage(retaliate);
                //Console.WriteLine("Attacker Health: {0}", initiator.health);
                //Console.WriteLine("Defender Health: {0}", defender.health);

            }
            if (initiator.Alive())
            {
                Console.WriteLine("You lived!");
            }
            else
            {
                Console.WriteLine("You died!");
            }
        }
    }
}
