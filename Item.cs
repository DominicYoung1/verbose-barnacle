using System;
using System.Collections.Generic;
using System.Text;

namespace MUD
{
    public class Item
    {
        public string name;

        public Item(string n)
        {
            name = n;
            //Consider adding description to the parent Item class, will require more writing but may make sub class org easier
        }
    }
    public class Weapon : Item
    {
        public string description;
        public double damageValue;

        public Weapon(string n, string f, double d) : base(n)
        {
            description = f;
            damageValue = d;
        }
    }

    public class Armor : Item
    {
        public string description;
        public double armorValue;

        public Armor(string n, string f, double d) : base(n)
        {
            description = f;
            armorValue = d;
        }
    }

}
