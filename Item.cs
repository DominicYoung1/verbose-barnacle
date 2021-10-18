using System;
using System.Collections.Generic;
using System.Text;

namespace MUD
{
    //public class Item
    //{
    //    public string name;

    //    public Item(string n)
    //    {
    //        name = n;
    //        //Consider adding description to the parent Item class, will require more writing but may make sub class org easier
    //    }
    //}

    public interface Item
    {
        public void Inspect();
        public string Name();
        //public string Describe(); (probably redundant with Insepct())
        
    }

    public interface IEquipable
    {
        public void Equip();
        public void UnEquip();
    }
    public class Weapon : Item, IEquipable
    {
        public string name;
        public string description;
        public double damageValue;

        public Weapon(string n, string f, double d)
        {
            name = n;
            description = f;
            damageValue = d;
        }

        public string Name()
        {
            return name;
        }

        //public string Describe()
        //{
        //    return description;
        //}

        public void Inspect()
        {
            Console.WriteLine("{0}",name);
            Console.WriteLine("{0}",description);
            Console.WriteLine("Does {0}",damageValue);
        }

        public void Equip()
        {

        }

            }

    public class Armor : Item, IEquipable
    {
        public string name;
        public string description;
        public double armorValue;

        public Armor(string n, string f, double d)
        {
            name = n;
            description = f;
            armorValue = d;
        }

        public string Name()
        {
            return name;
        }

        //public string Describe()
        //{
        //    return description;
        //}

        public void Inspect()
        {
            Console.WriteLine("{0}", name);
            Console.WriteLine("{0}", description);
            Console.WriteLine("Has {0}", armorValue);
        } 
    }

    public class Aid : Item
    {
        public string description;
        public double healValue;

        public Aid(string n, string f, double d)
        {
            description = f;
            healValue = d;
        }
    }

}
