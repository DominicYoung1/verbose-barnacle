using System;
using System.Collections.Generic;
using System.Text;

namespace MUD
{
    class CombatStats
    {
        double strength;
        double defence;

        public CombatStats()
        {
            strength = 1.0;
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
