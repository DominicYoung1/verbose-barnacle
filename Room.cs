using System;
using System.Collections.Generic;
using System.Text;

namespace MUD
{
    public class Room
    {
        public string name;
        string description;
        public Dictionary<string, Room> doors;
        ArrayList<Item> items;
        //ArrayList<Weapon> weapons;
        //ArrayList<Armor> armor;
        ArrayList<Entity> occupants;


        public Room(string n, string desc)
        {
            name = n;
            description = desc;
            doors = new Dictionary<string, Room>();
            items = new ArrayList<Item>(2);
            occupants = new ArrayList<Entity>(2);
        }

        public ArrayList<Item> ListItems()
        {
            return items;
        }

        public void AddItem(Item i)
        {
            items.Push(i);
        }
        // if this function returns null we couldnt find that item.

        // TODO consider a function that combines arraylists into a large readable function/write two more list functions and call them all.
        //public ArrayList<Weapon> ListWeapons()
        //{
          //  return weapons;
        //}

        //public ArryaList<Armor> ListArmor()
        //{
            //return armor;
        //}

        //public void AddWeapon(Weapon i)
        //{
            //weapons.Push(i);
        //}

        //public Weapon RemoveWeapon(string name)
        //{
            //for (int = 0; int < weapons.Length(); i++)
            //{
                //if (name = weapons[i].name)
                //{
                    //return weapons.Remove(i);
                //}
            //}
            //return null;
        //}

        //public void AddArmor(Armor i)
        //{
            //return Armor;
        //}

        //public Armor RemoveArmor(string name)
        //{
            //for (int i = 0; i < armor.Length(); i++)
            //{
                //if (name == armor[i].name)
                //{
                   //return armor.Remove(i);
                //}
            //}
            //return null;
        //}
        //}
        public Item RemoveItem(string name)
        {
            // For loop through rooms items, find match and write down the index.
            // Call array list method Remove.

            for (int i = 0; i < items.Length(); i++)
            {
                Console.WriteLine("looking for {0}", name);
                if (name == items[i].name)
                {
                   return items.Remove(i);
                }
            }
            return null;
        }

        public void Describe()
        {
            Console.WriteLine(name);
            Console.WriteLine(description);
            foreach(string direction in doors.Keys)
            {
                Console.WriteLine("{0} -> {1}", direction, doors[direction].name);
            }
            Console.WriteLine("Items in the room:");
            for (int i = 0; i < items.Length(); i++)
            {
                Console.WriteLine(items[i].name);
            }
            Console.WriteLine("The occupants in the room are:");
            for (int i = 0; i < occupants.Length(); i++)
            {
                Console.WriteLine(occupants[i].name);
            }
        }
        public void AddOccupant(Entity e)
        {
            //take in a entity 
            // add the entity to the arraylist for that rooms occupants
            occupants.Push(e);
        }

        public Entity RemoveOccupant(Entity e)
        {
            // look through our list of occupants
            // find our input occupant
            // remove and return
            for (int i = 0; i < occupants.Length(); i++)
            {
                if (occupants[i] == e)
                {
                    return occupants.Remove(i);
                }
            }
            return null;
        }

        public Entity GetEntity(string name)
        {
            for (int i = 0; i < occupants.Length(); i++)
            {
                if (occupants[i].name == name)
                {
                    return occupants[i];
                }
            }
            return null;
        }
    }
}
