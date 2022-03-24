using System;

namespace MUD
{
    public class CombatStats
    {
        double strength;
        double defence;

        public CombatStats()
        {
            //temp change
            strength = 10.0;
            defence = 1.0;
        }

        public CombatStats(double s, double d)
        {
            strength = s;
            defence = d;
        }

        public double ComputeDamageImparted()
        {
            // TODO ASK FRIEND REFF FOR MECHANIC IMPLIMENTATIONS
            // In combat, weapons should have a range (3) of damage that is modified by the strength stat on the entity using it.
            Random rng = new Random();
            double weight = rng.NextDouble();
            double rescaledWeight = (weight / 2.0) + 0.5;
            return rescaledWeight * strength;
        }

        public double ComputeDamageTaken(double dmg)
        {
            // TODO ASK FRIEND REFF FOR MECHANIC IMPLIMENTATIONS
            return dmg / defence;
        }
    }
}
