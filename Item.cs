using System;

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
        public string Inspect();
        public string Name();
        //public string Describe(); (probably redundant with Insepct())

    }

    public interface IEquipable
    {

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

        public string Inspect()
        {
            return String.Format("{0}\n{1}\nDoes {2} damage", name, description, damageValue.ToString());
            //Console.WriteLine("{0}", name);
            //Console.WriteLine("{0}", description);
            //Console.WriteLine("Does {0} damage", damageValue);
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

        public string Inspect()
        {
            return String.Format("{0}\n{1}\nProvides {2} armor", name, description, armorValue.ToString());
            //Console.WriteLine("{0}", name);
            //Console.WriteLine("{0}", description);
            //Console.WriteLine("Has {0} armor", armorValue);
        }
    }
}

//    public class Aid : Item
//    {
//        public string description;
//        public double healValue;

//        public Aid(string n, string f, double d)
//        {
//            description = f;
//            healValue = d;
//        }
//    }

//}
