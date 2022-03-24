using System;
using System.Collections.Generic;

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

        // TODO** consider a function that combines arraylists into a large readable function/write two more list functions and call them all.
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
                if (name == items[i].Name())
                {
                    return items.Remove(i);
                }
            }
            return null;
        }

        public string Describe()
        {
            string s = String.Format("{0} \n {1}", name, description);
            //Console.WriteLine(name);
            //Console.WriteLine(description);
            foreach (string direction in doors.Keys)
            {
                s = s + String.Format("\n{0} -> {1}", direction, doors[direction].name);
            }
            s = s + String.Format("\nItems in the room:");
            for (int i = 0; i < items.Length(); i++)
            {
                s = s + String.Format("\n{0}", items[i].Name());
            }
            s = s + String.Format("\nThe occupants in the room are:");
            for (int i = 0; i < occupants.Length(); i++)
            {
                s = s + String.Format("\n{0}", occupants[i].name);
            }
            return s;
        }

        public ArrayList<Entity> GetOccupants()
        {
            return occupants;
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

        public string GetRandomDoorName()
        {
            int numberOfDoors = doors.Count;
            Random rnd = new Random();
            int num = rnd.Next(0, numberOfDoors);
            var collectionOfDoors = doors.Keys;
            string[] doorNames = new string[numberOfDoors];
            collectionOfDoors.CopyTo(doorNames, 0);
            string selectedDoorName = doorNames[num];
            return selectedDoorName;
        }

        public bool ContainsEntity(string name)
        {
            if (GetEntity(name) != null)
            {
                return true;
            }
            return false;
        }
    }
}
